using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Cliente;
using WebZi.Plataform.Data.Services.Deposito;
using WebZi.Plataform.Domain.DTO.GRV.Pesquisa;
using WebZi.Plataform.Domain.DTO.Sistema;
using WebZi.Plataform.Domain.Enums;

namespace WebZi.Plataform.Data.Services.ClienteDeposito
{
    public class ClienteDepositoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ClienteDepositoService(AppDbContext context)
        {
            _context = context;
        }

        public ClienteDepositoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MensagemDTO> ValidateClienteDepositoAsync(int ClienteId, int DepositoId)
        {
            MensagemDTO ResultView = new();

            ResultView = MensagemViewHelper.SetNewMessages(ResultView, await new ClienteService(_context)
                .ValidateClienteAsync(ClienteId));

            ResultView = MensagemViewHelper.SetNewMessages(ResultView, await new DepositoService(_context)
                .ValidateDepositoAsync(DepositoId));

            if (ClienteId > 0 && DepositoId > 0)
            {
                if (!await _context.ClienteDeposito.AsNoTracking().AnyAsync(x => x.ClienteId == ClienteId && x.DepositoId == DepositoId))
                {
                    ResultView = MensagemViewHelper.SetNewMessage(ResultView, MensagemPadraoEnum.NaoEncontradoAssociacaoClienteDeposito, MensagemTipoAvisoEnum.Impeditivo);
                }
            }

            if (ResultView.AvisosImpeditivos.Count + ResultView.Erros.Count > 0)
            {
                ResultView.HtmlStatusCode = HtmlStatusCodeEnum.BadRequest;

                return ResultView;
            }

            return MensagemViewHelper.SetOk();
        }

        public async Task<ClienteDepositoFlagParcelamentoDTO> GetClienteDepositoFlagParcelamento(int ClienteId, int DepositoId)
        {

            ClienteDepositoFlagParcelamentoDTO ResultView = new();

            #region Validações
                ResultView.Mensagem = MensagemViewHelper.SetNewMessages(ResultView.Mensagem, await new ClienteService(_context)
                    .ValidateClienteAsync(ClienteId));

                ResultView.Mensagem = MensagemViewHelper.SetNewMessages(ResultView.Mensagem, await new DepositoService(_context)
                    .ValidateDepositoAsync(DepositoId));
            #endregion Validações
            #region Consultas
            var query = await _context
                    .ClienteDeposito
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.ClienteId == ClienteId && x.DepositoId == DepositoId);
            #endregion Consultas

            if (query is null)
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();

            if (ResultView.Mensagem.AvisosImpeditivos.Count > 0 || ResultView.Mensagem.Erros.Count > 0)
                return ResultView;

            ResultView = _mapper.Map<ClienteDepositoFlagParcelamentoDTO>(query);
            ResultView.Mensagem = MensagemViewHelper.SetOk();
            return ResultView;
        }
    }
}