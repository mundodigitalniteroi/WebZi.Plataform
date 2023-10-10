using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
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

        public PixDinamicoGeradoViewModel Gerar(int FaturamentoId, int UsuarioId)
        {
            List<string> erros = new();

            if (FaturamentoId <= 0)
            {
                erros.Add(MensagemPadrao.IdentificadorFaturamentoInvalido);
            }

            if (UsuarioId <= 0)
            {
                erros.Add(MensagemPadrao.IdentificadorUsuarioInvalido);
            }

            PixDinamicoGeradoViewModel ResultView = new();

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
                    ResultView.Mensagem = MensagemViewHelper.GetUnauthorized(MensagemPadrao.UsuarioSemPermissaoAcessoGrv);

                    return ResultView;
                }
            }
            else if (Faturamento == null)
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound(MensagemPadrao.FaturamentoNaoEncontrado);

                return ResultView;
            }
            else if (Faturamento.TipoMeioCobranca.Alias != "PIXEST")
            {
                ResultView.Mensagem = MensagemViewHelper
                    .GetBadRequest($"Esse Faturamento está cadastrado em outra Forma de Pagamento: {Faturamento.TipoMeioCobranca.Descricao}");

                return ResultView;
            }
            else if (Faturamento.Status == "C")
            {
                ResultView.Mensagem = MensagemViewHelper.GetBadRequest($"Esse Faturamento foi cancelado");

                return ResultView;
            }
            else if (Faturamento.Status == "P")
            {
                ResultView.Mensagem = MensagemViewHelper.GetBadRequest($"Esse Faturamento já foi pago");

                return ResultView;
            }
            else if (Faturamento.ValorFaturado <= 0)
            {
                ResultView.Mensagem = MensagemViewHelper.GetBadRequest($"Esse Faturamento não possui valor");

                return ResultView;
            }

            PixEstaticoEnvioModel PixEstaticoEnvio = new()
            {
                Chave = Faturamento.Atendimento.Grv.Cliente.PixChave,

                SolicitacaoPagador = Faturamento.Atendimento.Grv.NumeroFormularioGrv,

                InfoAdicionais = string.Empty,

                Valor = new PixEstaticoEnvioValorModel()
                {
                    Original = Math.Round(Faturamento.ValorFaturado, 2).ToString().Replace(",", ".")
                },

                Merchant = new PixEstaticoEnvioMerchantModel()
                {
                    Name = Faturamento.Atendimento.Grv.Cliente.Nome,

                    City = Faturamento.Atendimento.Grv.Cliente.Endereco.UF
                }
            };

            ConfiguracaoModel Configuracao = _context.Configuracao
                .AsNoTracking()
                .FirstOrDefault();

            PixEstaticoRetornoModel PixEstaticoRetorno = HttpClientHelper.PostBasicAuth<PixEstaticoRetornoModel>(Configuracao.PixUrl, Configuracao.PixUsername, Configuracao.PixPassword, PixEstaticoEnvio);

            PixModel Pix = new()
            {
                FaturamentoId = FaturamentoId,

                Chave = PixEstaticoRetorno.Chave,

                SolicitacaoPagador = PixEstaticoRetorno.SolicitacaoPagador,

                InfoAdicionais = PixEstaticoRetorno.InfoAdicionais,

                Valor = Convert.ToDecimal(PixEstaticoRetorno.Valor.Original.Replace(",", ".")),

                MerchantName = PixEstaticoRetorno.Merchant.Name,

                MerchantCity = PixEstaticoRetorno.Merchant.City,

                QRString = PixEstaticoRetorno.QrString,

                QRCode = PixEstaticoRetorno.QrCode
            };

            _context.Pix.Add(Pix);

            _context.SaveChanges();

            PixEstaticoRetorno.PixId = Pix.PixId;

            return null;
        }
    }
}