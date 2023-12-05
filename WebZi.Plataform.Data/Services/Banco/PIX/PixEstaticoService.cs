using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.CrossCutting.Strings;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Domain.Enums;
using WebZi.Plataform.Domain.Models.Banco.PIX;
using WebZi.Plataform.Domain.Models.Banco.PIX.Work;
using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.Models.Sistema;
using WebZi.Plataform.Domain.Services.GRV;
using WebZi.Plataform.Domain.ViewModel.Banco.PIX;
using Z.EntityFramework.Plus;

namespace WebZi.Plataform.Data.Services.Banco.PIX
{
    public class PixEstaticoService
    {
        private readonly AppDbContext _context;

        public PixEstaticoService(AppDbContext context)
        {
            _context = context;
        }

        public PixEstaticoGeradoViewModel Create(int FaturamentoId, int UsuarioId)
        {
            PixEstaticoGeradoViewModel ResultView = new();

            if (FaturamentoId <= 0)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest(MensagemPadraoEnum.IdentificadorFaturamentoInvalido);

                return ResultView;
            }

            FaturamentoModel Faturamento = _context.Faturamento
                .Include(i => i.TipoMeioCobranca)
                .Include(i => i.Atendimento)
                .ThenInclude(t => t.Grv)
                .ThenInclude(t => t.Cliente)
                .ThenInclude(t => t.Endereco)
                .Where(w => w.FaturamentoId == FaturamentoId)
                .AsNoTracking()
                .FirstOrDefault();

            if (Faturamento != null)
            {
                ResultView.Mensagem = new GrvService(_context).ValidateInputGrv(Faturamento.Atendimento.Grv, UsuarioId);

                if (ResultView.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
                {
                    return ResultView;
                }
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound(MensagemPadraoEnum.NaoEncontradoFaturamento);

                return ResultView;
            }
            
            if (Faturamento.TipoMeioCobranca.Alias != TipoMeioCobrancaAliasEnum.PixEstatico)
            {
                ResultView.Mensagem = MensagemViewHelper
                    .SetBadRequest($"Esse Faturamento está cadastrado em outra Forma de Pagamento: {Faturamento.TipoMeioCobranca.Descricao}");

                return ResultView;
            }
            else if (Faturamento.Status == "C")
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Esse Faturamento foi cancelado");

                return ResultView;
            }
            else if (Faturamento.Status == "P")
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Esse Faturamento já foi pago");

                return ResultView;
            }
            else if (Faturamento.ValorFaturado <= 0)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Esse Faturamento não possui valor");

                return ResultView;
            }

            // Exclui o PIX Estático da Fatura caso exista
            _context.PixEstatico
                .Where(w => w.FaturamentoId == FaturamentoId)
                .Delete();

            ConfiguracaoModel Configuracao = _context.Configuracao
                .AsNoTracking()
                .FirstOrDefault();

            PixEstaticoEnvioModel PixEstaticoEnvio = new()
            {
                Chave = Faturamento.Atendimento.Grv.Cliente.PixChave,

                SolicitacaoPagador = Faturamento.Atendimento.Grv.NumeroFormularioGrv,

                Valor = new PixEstaticoEnvioValorModel()
                {
                    Original = Math.Round(Faturamento.ValorFaturado, 2).ToString().Replace(",", ".")
                },

                Merchant = new PixEstaticoEnvioMerchantModel()
                {
                    Name = StringHelper.Normalize(Faturamento.Atendimento.Grv.Cliente.Nome.ToUpper().Trim()),

                    City = Faturamento.Atendimento.Grv.Cliente.Endereco.UF
                }
            };

            PixEstaticoRetornoModel PixEstaticoRetorno = new();

            for (int i = 1; i <= 5; i++)
            {
                try
                {
                    PixEstaticoRetorno = HttpClientHelper.PostBasicAuth<PixEstaticoRetornoModel>(
                        Configuracao.PixUrl, 
                        Configuracao.PixUsername, 
                        Configuracao.PixPassword, 
                        PixEstaticoEnvio);

                    break;
                }
                catch (Exception ex) when (i == 5)
                {
                    ResultView.Mensagem = MensagemViewHelper.SetServiceUnavailable(ex);

                    return ResultView;
                }
            }

            PixEstaticoModel Pix = new()
            {
                FaturamentoId = FaturamentoId,

                Chave = PixEstaticoEnvio.Chave,

                SolicitacaoPagador = PixEstaticoEnvio.SolicitacaoPagador,

                Valor = Convert.ToDecimal(PixEstaticoEnvio.Valor.Original.Replace(",", ".")),

                MerchantName = PixEstaticoEnvio.Merchant.Name,

                MerchantCity = PixEstaticoEnvio.Merchant.City,

                QRString = PixEstaticoRetorno.QrString,

                QRCode = PixEstaticoRetorno.QrCode
            };

            _context.PixEstatico.Add(Pix);

            _context.SaveChanges();

            return new()
            {
                IdentificadorPix = Pix.PixId,

                Chave = Pix.Chave,

                SolicitacaoPagador = Pix.SolicitacaoPagador,

                Valor = Pix.Valor,

                MerchantName = Pix.MerchantName,

                MerchantCity = Pix.MerchantCity,

                QRString = Pix.QRString,

                QRCode = Pix.QRCode,

                Mensagem = MensagemViewHelper.SetCreateSuccess("PIX Estático gerado com sucesso")
            };
        }
    }
}