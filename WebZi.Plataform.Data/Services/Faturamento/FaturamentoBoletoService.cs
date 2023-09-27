using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebZi.Plataform.CrossCutting.Date;
using WebZi.Plataform.CrossCutting.Documents;
using WebZi.Plataform.CrossCutting.Strings;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Services.Sistema;
using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.Models.Faturamento.Boleto;
using WebZi.Plataform.Domain.Models.Faturamento.View;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Models.Sistema;

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
            return await _context.FaturamentoBoletos
                .Where(w => w.FaturamentoId == FaturamentoId && w.Status == "N")
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<List<FaturamentoBoletoModel>> GetBoletoByFaturamentoId(int FaturamentoId)
        {
            List<FaturamentoBoletoModel> result = await _context.FaturamentoBoletos
                .Where(w => w.FaturamentoId == FaturamentoId)
                .AsNoTracking()
                .ToListAsync();

            return result
                .OrderBy(o => o.DataEmissao)
                .ToList();
        }

        public async void Gerar(GrvModel Grv, FaturamentoModel Faturamento, TipoMeioCobrancaModel TipoMeioCobranca, List<FaturamentoRegraModel> FaturamentoRegras = null)
        {
            if (TipoMeioCobranca.CodigoERP != "D" || Faturamento.ValorFaturado <= 0)
            {
                return;
            }

            ViewFaturamentoBoletoModel Boleto = await _context.ViewFaturamentoBoleto
                    .FirstOrDefaultAsync(w => w.FaturamentoId == Faturamento.FaturamentoId);

            if (Boleto == null)
            {
                throw new Exception("Dados para impressão do Boleto não encontrado");
            }

            Cancelar(Faturamento.FaturamentoId);

            if (FaturamentoRegras == null)
            {
                FaturamentoRegras = await _context.FaturamentoRegras
                    .Include(i => i.FaturamentoRegraTipo)
                    .Where(w => w.ClienteId == Grv.Cliente.ClienteId && w.DepositoId == Grv.Deposito.DepositoId)
                    .AsNoTracking()
                    .ToListAsync();
            }

            FaturamentoRegraModel FaturamentoRegra = null;

            if (TipoMeioCobranca.Alias.Equals("BOLESP"))
            {
                FaturamentoRegra = FaturamentoRegras
                    .Where(w => w.FaturamentoRegraTipo.Codigo == "VENCIMENTOBOLETOD+")
                    .FirstOrDefault();
            }

            #region Preenchimento do Modelo
            BoletoWSModel BoletoWS = new()
            {
                cedente_nome = StringHelper.Normalize(Boleto.CedenteNome),

                cedente_cpfCnpj = DocumentHelper.FormatCNPJ(Boleto.CedenteDocumento),

                cedente_codigo_febraban = Boleto.CedenteCodigoFebraban,

                cedente_codigo = Boleto.CedenteCodigo,

                cedente_agencia = Boleto.CedenteAgencia,

                cedente_conta_corrente = Boleto.CedenteContaCorrente,

                cedente_dv = Boleto.CedenteDv,

                numeroDocumento = Boleto.NumeroDocumento,

                cedente_nossoNumeroBoleto = Boleto.CedenteNossoNumero,

                sacado_bairro = Boleto.SacadoBairro,

                sacado_cep = Boleto.SacadoCep,

                sacado_cidade = Boleto.SacadoCidade,

                sacado_cpfCnpj = Boleto.SacadoDocumento.Trim(),

                sacado_endereco = Boleto.SacadoEndereco,

                sacado_nome = StringHelper.Normalize(Boleto.SacadoNome),

                sacado_uf = Boleto.SacadoUf,

                valor_boleto = Boleto.ValorBoleto,

                sacado_carteira = Boleto.SacadoCarteira,

                sacado_instrucoes = Boleto.SacadoInstrucoes,

                vencimento = Boleto.Vencimento
            };

            FaturamentoBoletoGeradoModel BoletoGerado = new()
            {
                DataVencimento = DateTimeHelper.GetDateTime(Boleto.Vencimento[..10], "dd/MM/yyyy")
            };

            if (FaturamentoRegra != null)
            {
                BoletoGerado.DiasConfiguracaoDataVencimento = int.Parse(FaturamentoRegra.Valor);

                BoletoGerado.DataVencimento = DateTimeHelper.AddDays(BoletoGerado.DataVencimento, BoletoGerado.DiasConfiguracaoDataVencimento);

                BoletoWS.vencimento = BoletoGerado.DataVencimento.ToString("dd/MM/yyyy");
            }
            #endregion Preenchimento do Modelo

            #region Execução do WebService de geração do Boleto
            WebServiceUrlModel WebServiceUrl = await _context.WebServiceUrl
                .Where(w => w.WsName.Equals("WsBoletoSoap", StringComparison.CurrentCultureIgnoreCase))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            int linhaId;

            string linha;

            WsBoletoSoapClient

            try
            {
                BoletoGerado.Boleto = WebServices.WsBoletoController.RetornarBoletoCaixa(BoletoWS,
                    ConfiguracoesController.IsDEV,
                    out linhaId,
                    out linha);

                BoletoGerado.BoletoId = linhaId;

                BoletoGerado.Linha = linha;
            }
            catch (Exception)
            {
                throw;
            }
            #endregion Execução do WebService de geração do Boleto

            FaturamentoBoletoModel FaturamentoBoleto = new()
            {
                
            };

            await _context.FaturamentoBoletos.AddAsync(FaturamentoBoleto);
        }

        public async void Cancelar(int FaturamentoId)
        {
            // Apesar de ser uma lista, por regra, só pode haver 1 Boleto cadastrado não pago
            List<FaturamentoBoletoModel> result = await _context.FaturamentoBoletos
                    .Where(w => w.FaturamentoId == FaturamentoId && w.Status == "N")
                    .ToListAsync();

            if (result != null)
            {
                foreach (var item in result)
                {
                    item.Status = "C";

                    _context.FaturamentoBoletos.Update(item);
                }
            }
        }
    }
}