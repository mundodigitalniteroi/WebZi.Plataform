using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.CrossCutting.Veiculo;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Domain.Enums;
using WebZi.Plataform.Domain.Models.Cliente;
using WebZi.Plataform.Domain.Models.Deposito;
using WebZi.Plataform.Domain.Models.Servico;
using WebZi.Plataform.Domain.ViewModel.GRV.Pesquisa;
using WebZi.Plataform.Domain.ViewModel.Servico;
using WebZi.Plataform.Domain.ViewModel.Usuario;
using WebZi.Plataform.Domain.Views.Usuario;

namespace WebZi.Plataform.Data.Services.Servico
{
    public class ServicoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ServicoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ReboqueViewModelList> GetReboqueByIdAsync(int ReboqueId)
        {
            ReboqueViewModelList ResultView = new();

            if (ReboqueId <= 0)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Identificador do Reboque inválido");

                return ResultView;
            }

            ReboqueModel result = await _context.Reboque
                .Where(w => w.ReboqueId == ReboqueId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (result != null)
            {
                ResultView.Listagem.Add(_mapper.Map<ReboqueViewModel>(result));

                ResultView.Mensagem = MensagemViewHelper.SetFound();
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();
            }

            return ResultView;
        }

        public async Task<ReboqueViewModelList> GetReboqueByPlacaAsync(string Placa, int ClienteId, int DepositoId)
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
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest(erros);

                return ResultView;
            }

            ClienteModel Cliente = await _context.Cliente
                .Where(w => w.ClienteId == ClienteId)
                .AsNoTracking().
                FirstOrDefaultAsync();

            if (Cliente == null)
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound(MensagemPadraoEnum.NaoEncontradoCliente);

                return ResultView;
            }

            DepositoModel Deposito = await _context.Deposito
                .Where(w => w.DepositoId == DepositoId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Deposito == null)
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound(MensagemPadraoEnum.NaoEncontradoDeposito);

                return ResultView;
            }

            ReboqueModel result = await _context.Reboque
                .Where(w => w.Placa == Placa.ToUpper().Trim() && w.ClienteId == ClienteId && w.DepositoId == DepositoId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (result != null)
            {
                ResultView.Listagem.Add(_mapper.Map<ReboqueViewModel>(result));

                ResultView.Mensagem = MensagemViewHelper.SetFound();
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();
            }

            return ResultView;
        }

        public async Task<ReboquistaViewModelList> GetReboquistaByIdAsync(int ReboquistaId)
        {
            ReboquistaViewModelList ResultView = new();

            if (ReboquistaId <= 0)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Identificador do Reboquista inválido");

                return ResultView;
            }

            ReboquistaModel result = await _context.Reboquista
                .Where(w => w.ReboquistaId == ReboquistaId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (result != null)
            {
                ResultView.Listagem.Add(_mapper.Map<ReboquistaViewModel>(result));

                ResultView.Mensagem = MensagemViewHelper.SetFound();
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();
            }

            return ResultView;
        }

        public async Task<ReboqueViewModelList> ListReboqueAsync(int ClienteId, int DepositoId)
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
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest(erros);

                return ResultView;
            }

            ClienteModel Cliente = await _context.Cliente
                .Where(w => w.ClienteId == ClienteId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Cliente == null)
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound(MensagemPadraoEnum.NaoEncontradoCliente);

                return ResultView;
            }

            DepositoModel Deposito = await _context.Deposito
                .Where(w => w.DepositoId == DepositoId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Deposito == null)
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound(MensagemPadraoEnum.NaoEncontradoDeposito);

                return ResultView;
            }

            List<ReboqueModel> result = await _context.Reboque
                .Where(w => w.ClienteId == ClienteId && w.DepositoId == DepositoId)
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count > 0)
            {
                ResultView.Listagem = _mapper.Map<List<ReboqueViewModel>>(result
                    .OrderBy(o => o.Placa)
                    .ToList());

                ResultView.Mensagem = MensagemViewHelper.SetFound(result.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();
            }

            return ResultView;
        }

        public async Task<UsuarioClienteDepositoReboqueViewModelList> ListReboqueByUsuarioIdAsync(int UsuarioId)
        {
            UsuarioClienteDepositoReboqueViewModelList ResultView = new();

            List<ViewUsuarioClienteDepositoReboqueModel> result = await _context.ViewUsuarioClienteDepositoReboque
                .Where(x => x.UsuarioId == UsuarioId)
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count > 0)
            {
                ResultView.Listagem = _mapper.Map<List<UsuarioClienteDepositoReboqueViewModel>>(result.OrderBy(o => o.ReboquePlaca).ToList());

                ResultView.Mensagem = MensagemViewHelper.SetFound(result.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();
            }

            return ResultView;
        }

        public async Task<ReboqueSimplificadoViewModelList> ListResumeReboqueByUsuarioIdAsync(int UsuarioId)
        {
            ReboqueSimplificadoViewModelList ResultView = new();

            UsuarioClienteDepositoReboqueViewModelList result = await ListReboqueByUsuarioIdAsync(UsuarioId);

            if (result.Listagem?.Count > 0)
            {
                ResultView.Listagem = _mapper.Map<List<ReboqueSimplificadoViewModel>>(result.Listagem);

                ResultView.Mensagem = MensagemViewHelper.SetFound(result.Listagem.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();
            }

            return ResultView;
        }

        public async Task<ReboquistaViewModelList> ListReboquistaAsync(int ClienteId, int DepositoId)
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
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest(erros);

                return ResultView;
            }

            ClienteModel Cliente = await _context.Cliente
                .Where(w => w.ClienteId == ClienteId)
                .AsNoTracking().
                FirstOrDefaultAsync();

            if (Cliente == null)
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound(MensagemPadraoEnum.NaoEncontradoCliente);

                return ResultView;
            }

            DepositoModel Deposito = await _context.Deposito
                .Where(w => w.DepositoId == DepositoId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Deposito == null)
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound(MensagemPadraoEnum.NaoEncontradoDeposito);

                return ResultView;
            }

            List<ReboquistaModel> result = await _context.Reboquista
                .Where(w => w.ClienteId == ClienteId && w.DepositoId == DepositoId)
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count > 0)
            {
                ResultView.Listagem = _mapper.Map<List<ReboquistaViewModel>>(result
                    .OrderBy(o => o.Nome)
                    .ToList());

                ResultView.Mensagem = MensagemViewHelper.SetFound(result.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();
            }

            return ResultView;
        }

        public async Task<UsuarioClienteDepositoReboquistaViewModelList> ListReboquistaByUsuarioIdAsync(int UsuarioId)
        {
            UsuarioClienteDepositoReboquistaViewModelList ResultView = new();

            List<ViewUsuarioClienteDepositoReboquistaModel> result = await _context.ViewUsuarioClienteDepositoReboquista
                .Where(x => x.UsuarioId == UsuarioId)
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count > 0)
            {
                ResultView.Listagem = _mapper.Map<List<UsuarioClienteDepositoReboquistaViewModel>>(result.OrderBy(o => o.ReboquistaNome).ToList());

                ResultView.Mensagem = MensagemViewHelper.SetFound(result.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();
            }

            return ResultView;
        }

        public async Task<ReboquistaSimplificadoViewModelList> ListResumeReboquistaByUsuarioIdAsync(int UsuarioId)
        {
            ReboquistaSimplificadoViewModelList ResultView = new();

            UsuarioClienteDepositoReboquistaViewModelList result = await ListReboquistaByUsuarioIdAsync(UsuarioId);

            if (result.Listagem?.Count > 0)
            {
                ResultView.Listagem = _mapper.Map<List<ReboquistaSimplificadoViewModel>>(result.Listagem);

                ResultView.Mensagem = MensagemViewHelper.SetFound(result.Listagem.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();
            }

            return ResultView;
        }
    }
}