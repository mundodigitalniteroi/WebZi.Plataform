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
    public class ReboqueService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ReboqueService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ReboqueViewModelList> GetById(int ReboqueId)
        {
            ReboqueViewModelList ResultView = new();

            if (ReboqueId <= 0)
            {
                ResultView.Mensagem = MensagemViewHelper.GetBadRequest("Identificador do Reboque inválido");

                return ResultView;
            }

            ReboqueModel result = await _context.Reboque
                .Where(w => w.ReboqueId == ReboqueId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (result != null)
            {
                ResultView.ListagemReboque.Add(_mapper.Map<ReboqueViewModel>(result));

                ResultView.Mensagem = MensagemViewHelper.GetOkFound();
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound();
            }

            return ResultView;
        }

        public async Task<ReboqueViewModelList> GetByPlaca(string Placa, int ClienteId, int DepositoId)
        {
            List<string> erros = new();

            if (string.IsNullOrWhiteSpace(Placa))
            {
                erros.Add("Informe a Placa");
            }
            else if (!VeiculoHelper.IsPlaca(Placa))
            {
                erros.Add("Placa inválida");
            }

            if (ClienteId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorClienteInvalido);
            }

            if (DepositoId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorDepositoInvalido);
            }

            ReboqueViewModelList ResultView = new();

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
                ResultView.Mensagem = MensagemViewHelper.GetNotFound(MensagemPadraoEnum.NaoEncontradoCliente);

                return ResultView;
            }

            DepositoModel Deposito = await _context.Deposito
                .Where(w => w.DepositoId == DepositoId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Deposito == null)
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound(MensagemPadraoEnum.NaoEncontradoDeposito);

                return ResultView;
            }

            ReboqueModel result = await _context.Reboque
                .Where(w => w.Placa == Placa.ToUpper().Trim() && w.ClienteId == ClienteId && w.DepositoId == DepositoId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (result != null)
            {
                ResultView.ListagemReboque.Add(_mapper.Map<ReboqueViewModel>(result));

                ResultView.Mensagem = MensagemViewHelper.GetOkFound();
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound();
            }

            return ResultView;
        }

        public async Task<ReboqueViewModelList> List(int ClienteId, int DepositoId)
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

            ReboqueViewModelList ResultView = new();

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
                ResultView.Mensagem = MensagemViewHelper.GetNotFound(MensagemPadraoEnum.NaoEncontradoCliente);

                return ResultView;
            }

            DepositoModel Deposito = await _context.Deposito
                .Where(w => w.DepositoId == DepositoId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Deposito == null)
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound(MensagemPadraoEnum.NaoEncontradoDeposito);

                return ResultView;
            }

            List<ReboqueModel> result = await _context.Reboque
                .Where(w => w.ClienteId == ClienteId && w.DepositoId == DepositoId)
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count > 0)
            {
                ResultView.ListagemReboque = _mapper.Map<List<ReboqueViewModel>>(result
                    .OrderBy(o => o.Placa)
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