using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using WebZi.Plataform.CrossCutting.Date;
using WebZi.Plataform.CrossCutting.Documents;
using WebZi.Plataform.CrossCutting.Strings;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Bucket;
using WebZi.Plataform.Data.WsBoleto;
using WebZi.Plataform.Domain.Models.Bucket;
using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.Models.Faturamento.Boleto;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Models.Sistema;
using WebZi.Plataform.Domain.Services.GRV;
using WebZi.Plataform.Domain.Services.Usuario;
using WebZi.Plataform.Domain.ViewModel.Generic;
using WebZi.Plataform.Domain.Views.Faturamento;
using Z.EntityFramework.Plus;
using static WebZi.Plataform.Data.WsBoleto.WsBoletoSoapClient;

namespace WebZi.Plataform.Data.Services.Faturamento
{
    public class FaturamentoBoletoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public FaturamentoBoletoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FaturamentoBoletoModel> GetBoletoNaoPago(int FaturamentoId)
        {
            return await _context.FaturamentoBoleto
                .Where(w => w.FaturamentoId == FaturamentoId && w.Status == "N")
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public ImageViewModel GetBoletoNaoCancelado(int FaturamentoId, int UsuarioId)
        {
            ImageViewModel ResultView = new();

            List<string> erros = new();

            if (FaturamentoId <= 0)
            {
                erros.Add("Identificador do Faturamento inválido");
            }

            if (UsuarioId <= 0)
            {
                erros.Add("Identificador do Usuário inválido");
            }

            if (erros.Count > 0)
            {
                ResultView.Mensagem.HtmlStatusCode = HtmlStatusCodeEnum.BadRequest;

                foreach (var erro in erros)
                {
                    ResultView.Mensagem.AvisosImpeditivos.Add(erro);
                }

                return ResultView;
            }

            if (!new UsuarioService(_context).IsUserActive(UsuarioId))
            {
                ResultView.Mensagem = MensagemViewHelper.GetUnauthorized();

                return ResultView;
            }

            FaturamentoModel Faturamento = _context.Faturamento
                .Include(i => i.FaturamentoBoletos.Where(w => w.Status != "C"))
                .Include(i => i.TipoMeioCobranca)
                .Include(i => i.Atendimento)
                .ThenInclude(t => t.Grv)
                .Where(w => w.FaturamentoId == FaturamentoId)
                .AsNoTracking()
                .FirstOrDefault();

            if (Faturamento != null)
            {
                if (!new GrvService(_context, _mapper).UserCanAccessGrv(Faturamento.Atendimento.Grv, UsuarioId))
                {
                    ResultView.Mensagem = MensagemViewHelper.GetUnauthorized("Usuário sem permissão de acesso ao GRV ou GRV inexistente");

                    return ResultView;
                }
            }
            else if (Faturamento == null)
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound("Faturamento não encontrado");

                return ResultView;
            }
            else if (Faturamento.TipoMeioCobranca.Alias != "BOL" && Faturamento.TipoMeioCobranca.Alias != "BOLESP")
            {
                ResultView.Mensagem = MensagemViewHelper.GetBadRequest($"Esse Faturamento está cadastrado em outra Forma de Pagamento: {Faturamento.TipoMeioCobranca.Descricao}");

                return ResultView;
            }
            else if (Faturamento.FaturamentoBoletos?.Count == null)
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound("Boleto foi cancelado ou inexistente");

                return ResultView;
            }

            BucketArquivoModel BucketArquivo = _context.BucketArquivo
                .Include(i => i.BucketNomeTabelaOrigem)
                .Where(w => w.BucketNomeTabelaOrigem.Codigo == "FATURAMENBOLETO")
                .AsNoTracking()
                .FirstOrDefault();

            if (BucketArquivo != null)
            {
                ResultView.Mensagem.HtmlStatusCode = HtmlStatusCodeEnum.Ok;

                ResultView.Imagem = HttpClientHelper.DownloadFile(BucketArquivo.Url);

                return ResultView;
            }
            else
            {
                FaturamentoBoletoImagemModel FaturamentoBoletoImagem = _context.FaturamentoBoletoImagem
                    .Include(i => i.FaturamentoBoleto)
                    .Where(w => w.FaturamentoBoleto.FaturamentoId == Faturamento.FaturamentoBoletos.FirstOrDefault().FaturamentoBoletoId && w.FaturamentoBoleto.Status != "C")
                    .OrderByDescending(o => o.FaturamentoBoletoId)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (FaturamentoBoletoImagem != null)
                {
                    ResultView.Mensagem.HtmlStatusCode = HtmlStatusCodeEnum.Ok;

                    ResultView.Imagem = FaturamentoBoletoImagem.Imagem;

                    return ResultView;
                }
                else
                {
                    ResultView.Mensagem = MensagemViewHelper.GetNotFound("A imagem do Boleto não foi gerado ou foi excluído");

                    return ResultView;
                }
            }
        }

        public ImageViewModel Create(int FaturamentoId, int UsuarioId)
        {
            ImageViewModel ResultView = new();

            List<string> erros = new();

            if (FaturamentoId <= 0)
            {
                erros.Add("Identificador do Faturamento inválido");
            }

            if (UsuarioId <= 0)
            {
                erros.Add("Identificador do Usuário inválido");
            }

            if (erros.Count > 0)
            {
                ResultView.Mensagem.HtmlStatusCode = HtmlStatusCodeEnum.BadRequest;

                foreach (var erro in erros)
                {
                    ResultView.Mensagem.AvisosImpeditivos.Add(erro);
                }

                return ResultView;
            }

            if (!new UsuarioService(_context).IsUserActive(UsuarioId))
            {
                ResultView.Mensagem = MensagemViewHelper.GetUnauthorized();

                return ResultView;
            }

            FaturamentoModel Faturamento = _context.Faturamento
                .Include(i => i.TipoMeioCobranca)
                .Include(i => i.Atendimento)
                .ThenInclude(t => t.Grv)
                .Where(w => w.FaturamentoId == FaturamentoId)
                .AsNoTracking()
                .FirstOrDefault();

            if (Faturamento != null)
            {
                if (!new GrvService(_context, _mapper).UserCanAccessGrv(Faturamento.Atendimento.Grv, UsuarioId))
                {
                    ResultView.Mensagem = MensagemViewHelper.GetUnauthorized("Usuário sem permissão de acesso ao GRV ou GRV inexistente");

                    return ResultView;
                }
            }
            else if (Faturamento == null)
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound("Faturamento não encontrado");

                return ResultView;
            }
            else if (Faturamento.TipoMeioCobranca.Alias != "BOL" && Faturamento.TipoMeioCobranca.Alias != "BOLESP")
            {
                ResultView.Mensagem = MensagemViewHelper.GetBadRequest($"Esse Faturamento está cadastrado em outra Forma de Pagamento: {Faturamento.TipoMeioCobranca.Descricao}");

                return ResultView;
            }
            else if (Faturamento.ValorFaturado <= 0)
            {
                ResultView.Mensagem = MensagemViewHelper.GetBadRequest($"Esse Faturamento está com o valor zerado");

                return ResultView;
            }

            TipoMeioCobrancaModel TipoMeioCobranca = _context.TipoMeioCobranca
                .Where(w => w.TipoMeioCobrancaId == Faturamento.TipoMeioCobrancaId)
                .AsNoTracking()
                .FirstOrDefault();

            if (TipoMeioCobranca == null)
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound("Forma de Pagamento inexistente");

                return ResultView;
            }
            else if (TipoMeioCobranca.Alias != "BOL" && TipoMeioCobranca.Alias != "BOLESP")
            {
                ResultView.Mensagem = MensagemViewHelper.GetBadRequest($"Esse Faturamento está cadastrado com outra Forma de Pagamento: {TipoMeioCobranca.Descricao}");

                return ResultView;
            }

            // TODO: Ver o que é isso
            if (TipoMeioCobranca.CodigoERP != "D")
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError("TipoMeioCobranca.CodigoERP != \"D\"");

                return ResultView;
            }

            ViewFaturamentoBoletoModel ViewBoleto = _context.ViewFaturamentoBoleto
                .FirstOrDefault(w => w.FaturamentoId == Faturamento.FaturamentoId);

            if (ViewBoleto == null)
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound("Dados para a geração do Boleto não encontrados");

                return ResultView;
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
                List<FaturamentoRegraModel> FaturamentoRegras = _context.FaturamentoRegra
                    .Include(i => i.FaturamentoRegraTipo)
                    .Where(w => w.ClienteId == Faturamento.Atendimento.Grv.ClienteId && w.DepositoId == Faturamento.Atendimento.Grv.DepositoId)
                    .AsNoTracking()
                    .ToList();

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

                        UsuarioCadastroId = UsuarioId,

                        SequenciaEmissao = 1,

                        Linha = !string.IsNullOrWhiteSpace(BoletoGerado.Linha) ? BoletoGerado.Linha : "LINHA NÃO RETORNADA",

                        Valor = Faturamento.ValorFaturado,

                        DataEmissao = DateTime.Now
                    };

                    _context.FaturamentoBoleto.Add(FaturamentoBoleto);

                    _context.SaveChanges();

                    new BucketArquivoService(_context).SendFile("FATURAMENBOLETO", FaturamentoBoleto.FaturamentoBoletoId, UsuarioId, BoletoGerado.Boleto);

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();

                    throw;
                }
            }
            #endregion Cadastro do Boleto e da Imagem

            ResultView.Imagem = BoletoGerado.Boleto;

            ResultView.Mensagem = MensagemViewHelper.GetOk("Boleto gerado com sucesso");

            return ResultView;
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