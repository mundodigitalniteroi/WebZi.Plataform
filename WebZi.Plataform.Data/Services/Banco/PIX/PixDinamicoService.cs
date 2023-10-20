using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Domain.Enums;
using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.Models.Sistema;
using WebZi.Plataform.Domain.Services.GRV;
using WebZi.Plataform.Domain.Services.Usuario;
using WebZi.Plataform.Domain.ViewModel.Banco.PIX;

namespace WebZi.Plataform.Data.Services.Banco.PIX
{
    public class PixDinamicoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PixDinamicoService(AppDbContext context, IMapper mapper)
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
                .Include(i => i.PixEstaticos)
                .Include(i => i.Atendimento)
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

            ConfiguracaoModel Configuracao = _context.Configuracao
                .AsNoTracking()
                .FirstOrDefault();

            // TODO:
            return ResultView;
        }
    }
}