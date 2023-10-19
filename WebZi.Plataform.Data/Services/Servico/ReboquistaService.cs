using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.CrossCutting.Veiculo;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Domain.Enums;
using WebZi.Plataform.Domain.Models.Cliente;
using WebZi.Plataform.Domain.Models.Deposito;
using WebZi.Plataform.Domain.Models.Servico;
using WebZi.Plataform.Domain.ViewModel.Servico;

namespace WebZi.Plataform.Data.Services.Servico
{
    public class ReboquistaService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ReboquistaService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ReboquistaViewModelList> GetById(int ReboquistaId)
        {
            ReboquistaViewModelList ResultView = new();

            if (ReboquistaId <= 0)
            {
                ResultView.Mensagem = MensagemViewHelper.GetBadRequest("Identificador do Reboquista inválido");

                return ResultView;
            }

            ReboquistaModel result = await _context.Reboquista
                .Where(w => w.ReboquistaId == ReboquistaId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (result != null)
            {
                ResultView.ListagemReboquista.Add(_mapper.Map<ReboquistaViewModel>(result));

                ResultView.Mensagem = MensagemViewHelper.GetOkFound();
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound();
            }

            return ResultView;
        }

        public async Task<ReboquistaViewModelList> List(int ClienteId, int DepositoId)
        {
            List<string> erros = new();

            if (ClienteId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorClienteInvalido);
            }

            if (DepositoId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorDepositoInvalido);
            }

            ReboquistaViewModelList ResultView = new();

            if (erros.Count > 0)
            {
                ResultView.Mensagem = MensagemViewHelper.GetBadRequest(erros);

                return ResultView;
            }

            ClienteModel Cliente = await _context.Cliente
                .Where(w => w.ClienteId == ClienteId)
                .AsNoTracking().
                FirstOrDefaultAsync();

            if (Cliente == null)
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound(MensagemPadraoEnum.ClienteNaoEncontrado);

                return ResultView;
            }

            DepositoModel Deposito = await _context.Deposito
                .Where(w => w.DepositoId == DepositoId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Deposito == null)
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound(MensagemPadraoEnum.DepositoNaoEncontrado);

                return ResultView;
            }

            List<ReboquistaModel> result = await _context.Reboquista
                .Where(w => w.ClienteId == ClienteId && w.DepositoId == DepositoId)
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count > 0)
            {
                ResultView.ListagemReboquista = _mapper.Map<List<ReboquistaViewModel>>(result
                    .OrderBy(o => o.Nome)
                    .ToList());

                ResultView.Mensagem = MensagemViewHelper.GetOkFound(result.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound();
            }

            return ResultView;
        }
    }
}