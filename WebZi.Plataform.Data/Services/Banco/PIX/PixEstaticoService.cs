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
using WebZi.Plataform.Domain.Services.Usuario;
using WebZi.Plataform.Domain.ViewModel.Banco.PIX;
using Z.EntityFramework.Plus;

namespace WebZi.Plataform.Data.Services.Banco.PIX
{
    public class PixEstaticoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PixEstaticoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public PixEstaticoGeradoViewModel Create(int FaturamentoId, int UsuarioId)
        {
            #region Validações
            List<string> erros = new();

            if (FaturamentoId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorFaturamentoInvalido);
            }

            if (UsuarioId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorUsuarioInvalido);
            }

            PixEstaticoGeradoViewModel ResultView = new();

            if (erros.Count > 0)
            {
                ResultView.Mensagem = MensagemViewHelper.GetBadRequest(erros);

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
                .ThenInclude(t => t.Cliente)
                .ThenInclude(t => t.Endereco)
                .Where(w => w.FaturamentoId == FaturamentoId)
                .AsNoTracking()
                .FirstOrDefault();

            if (Faturamento != null)
            {
                if (!new GrvService(_context, _mapper).UserCanAccessGrv(Faturamento.Atendimento.Grv, UsuarioId))
                {
                    ResultView.Mensagem = MensagemViewHelper.GetUnauthorized(MensagemPadraoEnum.UsuarioSemPermissaoAcessoGrv);

                    return ResultView;
                }
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound(MensagemPadraoEnum.NaoEncontradoFaturamento);

                return ResultView;
            }
            
            if (Faturamento.TipoMeioCobranca.Alias != TipoMeioCobrancaAliasEnum.PixEstatico)
            {
                ResultView.Mensagem = MensagemViewHelper
                    .GetBadRequest($"Esse Faturamento está cadastrado em outra Forma de Pagamento: {Faturamento.TipoMeioCobranca.Descricao}");

                return ResultView;
            }
            else if (Faturamento.Status == "C")
            {
                ResultView.Mensagem = MensagemViewHelper.GetBadRequest("Esse Faturamento foi cancelado");

                return ResultView;
            }
            else if (Faturamento.Status == "P")
            {
                ResultView.Mensagem = MensagemViewHelper.GetBadRequest("Esse Faturamento já foi pago");

                return ResultView;
            }
            else if (Faturamento.ValorFaturado <= 0)
            {
                ResultView.Mensagem = MensagemViewHelper.GetBadRequest("Esse Faturamento não possui valor");

                return ResultView;
            }
            #endregion Validações

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
                    ResultView.Mensagem = MensagemViewHelper.GetServiceUnavailable(ex);

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
                PixId = Pix.PixId,

                Chave = Pix.Chave,

                SolicitacaoPagador = Pix.SolicitacaoPagador,

                Valor = Pix.Valor,

                MerchantName = Pix.MerchantName,

                MerchantCity = Pix.MerchantCity,

                QRString = Pix.QRString,

                QRCode = Pix.QRCode,

                Mensagem = MensagemViewHelper.GetOkCreate("PIX Estático gerado com sucesso")
            };
        }
    }
}