using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Domain.Enums;
using WebZi.Plataform.Domain.Models.Cliente;
using WebZi.Plataform.Domain.Models.Usuario;
using WebZi.Plataform.Domain.ViewModel.Cliente;
using WebZi.Plataform.Domain.ViewModel.GRV.Pesquisa;

namespace WebZi.Plataform.Data.Services.Cliente
{
    public class ClienteService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ClienteService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ClienteViewModelList> GetByIdAsync(int ClienteId)
        {
            ClienteViewModelList ResultView = new();

            if (ClienteId <= 0)
            {
                ResultView.Mensagem = MensagemViewHelper.GetBadRequest(MensagemPadraoEnum.IdentificadorClienteInvalido);

                return ResultView;
            }

            ClienteModel result = await _context.Cliente
                .Where(w => w.ClienteId == ClienteId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (result != null)
            {
                ResultView.Listagem.Add(_mapper.Map<ClienteViewModel>(result));

                ResultView.Mensagem = MensagemViewHelper.GetOkFound();
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound();
            }

            return ResultView;
        }
        
        public async Task<ClienteViewModelList> GetByNameAsync(string Name)
        {
            ClienteViewModelList ResultView = new();

            if (string.IsNullOrWhiteSpace(Name))
            {
                ResultView.Mensagem = MensagemViewHelper.GetBadRequest("Informe o Nome do Cliente");

                return ResultView;
            }

            List<ClienteModel> result = await _context.Cliente
                .Where(w => w.Nome.ToUpper().Contains(Name.ToUpper().Trim()))
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count > 0)
            {
                ResultView.Listagem = _mapper.Map<List<ClienteViewModel>>(result.OrderBy(o => o.Nome).ToList());

                ResultView.Mensagem = MensagemViewHelper.GetOkFound(result.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound();
            }

            return ResultView;
        }

        public async Task<ClienteViewModelList> ListAsync(int UsuarioId)
        {
            ClienteViewModelList ResultView = new();

            List<UsuarioClienteModel> result = await _context.UsuarioCliente
                .Include(x => x.Cliente)
                .Where(x => x.UsuarioId == UsuarioId)
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count > 0)
            {
                List<ClienteModel> Clientes = new();

                foreach (UsuarioClienteModel UsuarioCliente in result)
                {
                    Clientes.Add(UsuarioCliente.Cliente);
                }

                ResultView.Listagem = _mapper.Map<List<ClienteViewModel>>(Clientes.OrderBy(o => o.Nome).ToList());

                ResultView.Mensagem = MensagemViewHelper.GetOkFound(result.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound();
            }

            return ResultView;
        }

        public async Task<ClienteSimplificadoViewModelList> ListagemSimplificada(int UsuarioId)
        {
            ClienteSimplificadoViewModelList ResultView = new();

            ClienteViewModelList result = await ListAsync(UsuarioId);

            if (result.Listagem?.Count > 0)
            {
                ResultView.Listagem = _mapper.Map<List<ClienteSimplificadoViewModel>>(result.Listagem);

                ResultView.Mensagem = MensagemViewHelper.GetOkFound(result.Listagem.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound();
            }

            return ResultView;
        }
    }
}