using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using WebZi.Plataform.CrossCutting.Date;
using WebZi.Plataform.CrossCutting.Documents;
using WebZi.Plataform.CrossCutting.Strings;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Services.Bucket;
using WebZi.Plataform.Data.WsBoleto;
using WebZi.Plataform.Domain.Models.Bucket;
using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.Models.Faturamento.Boleto;
using WebZi.Plataform.Domain.Models.Faturamento.View;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Models.Sistema;
using Z.EntityFramework.Plus;
using static WebZi.Plataform.Data.WsBoleto.WsBoletoSoapClient;

namespace WebZi.Plataform.Data.Services.Faturamento
{
    public class FaturamentoBoletoService
    {
        private readonly AppDbContext _context;

        public FaturamentoBoletoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<FaturamentoBoletoModel> GetBoletoNaoPagoByFaturamentoId(int FaturamentoId)
        {
            return await _context.FaturamentoBoleto
                .Where(w => w.FaturamentoId == FaturamentoId && w.Status == "N")
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<byte[]> GetUltimoBoleto(int FaturamentoBoletoId)
        {
            BucketArquivoModel BucketArquivo = _context.BucketArquivo
                .Include(i => i.BucketNomeTabelaOrigem)
                .Where(w => w.BucketNomeTabelaOrigem.Codigo == "FATURAMENBOLETO")
                .AsNoTracking()
                .FirstOrDefault();

            if (BucketArquivo != null)
            {
                return await HttpClientHelper.DownloadFileAsync(BucketArquivo.Url);
            }
            else
            {
                var result = await _context.FaturamentoBoletoImagem
                    .Include(i => i.FaturamentoBoleto)
                    .Where(w => w.FaturamentoBoleto.FaturamentoId == FaturamentoBoletoId && w.FaturamentoBoleto.Status != "C")
                    .OrderByDescending(o => o.FaturamentoBoletoId)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                return result.Imagem;
            }
        }

        public async Task<List<FaturamentoBoletoModel>> GetBoletoByFaturamentoId(int FaturamentoId)
        {
            List<FaturamentoBoletoModel> result = await _context.FaturamentoBoleto
                .Where(w => w.FaturamentoId == FaturamentoId)
                .AsNoTracking()
                .ToListAsync();

            return result
                .OrderBy(o => o.DataEmissao)
                .ToList();
        }

        public byte[] Gerar(GrvModel Grv, FaturamentoModel Faturamento, int UsuarioCadastroId, TipoMeioCobrancaModel TipoMeioCobranca = null, List<FaturamentoRegraModel> FaturamentoRegras = null)
        {
            if (TipoMeioCobranca == null)
            {
                TipoMeioCobranca = _context.TipoMeioCobranca
                    .Where(w => w.TipoMeioCobrancaId == Faturamento.TipoMeioCobrancaId)
                    .AsNoTracking()
                    .FirstOrDefault();
            }

            if (TipoMeioCobranca.CodigoERP != "D" || Faturamento.ValorFaturado <= 0)
            {
                return null;
            }

            ViewFaturamentoBoletoModel ViewBoleto = _context.ViewFaturamentoBoleto
                .FirstOrDefault(w => w.FaturamentoId == Faturamento.FaturamentoId);

            if (ViewBoleto == null)
            {
                throw new Exception("Dados para impressão do Boleto não encontrado");
            }

            Cancelar(Faturamento.FaturamentoId);

            #region Preenchimento do Modelo
            BoletoTodos boletoTodos = new()
            {
                cedente_agencia = ViewBoleto.CedenteAgencia,

                banco = ViewBoleto.CedenteCodigoFebraban,

                cedente_codigo = ViewBoleto.CedenteCodigo,

                cedente_conta = ViewBoleto.CedenteContaCorrente,

                cedente_cpfCnpj = DocumentHelper.FormatCNPJ(ViewBoleto.CedenteDocumento.Trim()),

                cedente_digitoConta = ViewBoleto.CedenteDv,

                cedente_nome = StringHelper.Normalize(ViewBoleto.CedenteNome),

                cedente_nossoNumeroBoleto = ViewBoleto.CedenteNossoNumero,

                numeroDocumento = ViewBoleto.NumeroDocumento,

                sacado_bairro = ViewBoleto.SacadoBairro,

                sacado_cep = ViewBoleto.SacadoCep,

                sacado_cidade = ViewBoleto.SacadoCidade,

                sacado_cpfCnpj = ViewBoleto.SacadoDocumento.Trim(),

                sacado_endereco = ViewBoleto.SacadoEndereco,

                sacado_nome = StringHelper.Normalize(ViewBoleto.SacadoNome ?? string.Empty),

                sacado_uf = ViewBoleto.SacadoUf,

                valor_boleto = ViewBoleto.ValorBoleto,

                vencimento = ViewBoleto.Vencimento,

                carteira = ViewBoleto.SacadoCarteira,

                instrucoes = ViewBoleto.SacadoInstrucoes
            };

            FaturamentoBoletoGeradoModel BoletoGerado = new()
            {
                DataVencimento = DateTimeHelper.GetDateTime(ViewBoleto.Vencimento[..10], "dd/MM/yyyy")
            };

            if (TipoMeioCobranca.Alias.Equals("BOLESP"))
            {
                if (FaturamentoRegras == null)
                {
                    FaturamentoRegras = _context.FaturamentoRegra
                        .Include(i => i.FaturamentoRegraTipo)
                        .Where(w => w.ClienteId == Grv.ClienteId && w.DepositoId == Grv.DepositoId)
                        .AsNoTracking()
                        .ToList();
                }

                if (FaturamentoRegras?.Count > 0)
                {
                    FaturamentoRegraModel FaturamentoRegra = FaturamentoRegras
                        .Where(w => w.FaturamentoRegraTipo.Codigo == "VENCIMENTOBOLETOD+")
                        .FirstOrDefault();

                    if (FaturamentoRegra != null)
                    {
                        BoletoGerado.DiasConfiguracaoDataVencimento = int.Parse(FaturamentoRegra.Valor);

                        BoletoGerado.DataVencimento = DateTimeHelper.AddDays(BoletoGerado.DataVencimento, BoletoGerado.DiasConfiguracaoDataVencimento);

                        boletoTodos.vencimento = BoletoGerado.DataVencimento.ToString("dd/MM/yyyy");
                    }
                }
            }
            #endregion Preenchimento do Modelo

            #region Execução do WebService de geração do Boleto
            WebServiceUrlModel WebServiceUrl = _context.WebServiceUrl
                .Where(w => w.WsName == "WsBoletoSoap")
                .AsNoTracking()
                .FirstOrDefault();

            int linhaId;

            string linha;

            string url;

            BoletoGerado.Boleto = new WsBoletoSoapClient(EndpointConfiguration.WsBoletoSoap, WebServiceUrl.WsUrl).BoletoBancosRetornoLinha(
                boleto: boletoTodos,
                login: WebServiceUrl.WsUsername,
                senha: WebServiceUrl.WsPassword,
                Tipo: "img",
                isdev: true,
                linha: out linha,
                linha_id: out linhaId,
                url: out url);

            BoletoGerado.BoletoId = linhaId;

            BoletoGerado.Linha = linha;
            #endregion Execução do WebService de geração do Boleto

            #region Cadastro do Boleto e da Imagem
            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    FaturamentoBoletoModel FaturamentoBoleto = new()
                    {
                        FaturamentoId = Faturamento.FaturamentoId,

                        BoletoId = BoletoGerado.BoletoId,

                        UsuarioCadastroId = UsuarioCadastroId,

                        SequenciaEmissao = 1,

                        Linha = !string.IsNullOrWhiteSpace(BoletoGerado.Linha) ? BoletoGerado.Linha : "LINHA NÃO RETORNADA",

                        Valor = Faturamento.ValorFaturado,

                        DataEmissao = DateTime.Now
                    };

                    _context.FaturamentoBoleto.Add(FaturamentoBoleto);

                    _context.SaveChanges();

                    new BucketArquivoService(_context).SendFile("FATURAMENBOLETO", FaturamentoBoleto.FaturamentoBoletoId, UsuarioCadastroId, BoletoGerado.Boleto);

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();

                    throw;
                }
            }
            #endregion Cadastro do Boleto e da Imagem

            return BoletoGerado.Boleto;
        }

        public void Cancelar(int FaturamentoId)
        {
            // Apesar de ser uma lista, por regra, só pode haver 1 Boleto cadastrado não pago
            List<FaturamentoBoletoModel> result = _context.FaturamentoBoleto
                    .Where(w => w.FaturamentoId == FaturamentoId && w.Status == "N")
                    .ToList();

            if (result?.Count > 0)
            {
                foreach (FaturamentoBoletoModel FaturamentoBoleto in result)
                {
                    FaturamentoBoleto.Status = "C";

                    _context.FaturamentoBoleto.Update(FaturamentoBoleto);

                    _context.FaturamentoBoletoImagem
                        .Where(w => w.FaturamentoBoletoId == FaturamentoBoleto.FaturamentoBoletoId)
                        .Delete();

                    new BucketArquivoService(_context)
                        .DeleteFile("FATURAMENBOLETO", FaturamentoBoleto.FaturamentoBoletoId);
                }
            }
        }
    }
}