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

        public ClienteDepositoService(AppDbContext context)
        {
            _context = context;
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

            #region Validações
                ClienteDepositoFlagParcelamentoDTO ResultView = new();
                ResultView.Mensagem = MensagemViewHelper.SetNewMessages(ResultView.Mensagem, await new ClienteService(_context)
                    .ValidateClienteAsync(ClienteId));

                ResultView.Mensagem = MensagemViewHelper.SetNewMessages(ResultView.Mensagem, await new DepositoService(_context)
                    .ValidateDepositoAsync(DepositoId));
            #endregion Validações
            #region Consultas
            var ClienteDeposito = await _context.
                ClienteDeposito.
                AsNoTracking()
                .FirstOrDefaultAsync(x => x.ClienteId == ClienteId && x.DepositoId == DepositoId);
            #endregion Consultas

            ClienteDepositoFlagParcelamentoDTO clienteDepositoDTO = new()
            {
                IdentificadorCliente = ClienteDeposito.ClienteId,
                IdentificadorDeposito = ClienteDeposito.DepositoId,
                FlagAtivo = ClienteDeposito.FlagAtivo,
                FlagPossuiParcelamento = ClienteDeposito.FlagPossuiParcelamento
            };

            if(ResultView.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest();
            }
            ResultView.Mensagem = MensagemViewHelper.SetOk();
            return clienteDepositoDTO;
        }
    }
}