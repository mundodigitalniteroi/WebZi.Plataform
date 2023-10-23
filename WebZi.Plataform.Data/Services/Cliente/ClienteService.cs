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

        public async Task<ClienteViewModelList> GetById(int ClienteId)
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
                ResultView.ListagemCliente.Add(_mapper.Map<ClienteViewModel>(result));

                ResultView.Mensagem = MensagemViewHelper.GetOkFound();
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound();
            }

            return ResultView;
        }
        
        public async Task<ClienteViewModelList> GetByName(string Name)
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
                result = result.OrderBy(o => o.Nome).ToList();

                ResultView.ListagemCliente = _mapper.Map<List<ClienteViewModel>>(result);

                ResultView.Mensagem = MensagemViewHelper.GetOkFound(result.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound();
            }

            return ResultView;
        }

        public async Task<ClienteViewModelList> List(int UsuarioId)
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

                ResultView.ListagemCliente = _mapper.Map<List<ClienteViewModel>>(Clientes.OrderBy(o => o.Nome).ToList());

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

            ClienteViewModelList result = await List(UsuarioId);

            if (result.ListagemCliente?.Count > 0)
            {
                ResultView.ListagemCliente = _mapper.Map<List<ClienteSimplificadoViewModel>>(result.ListagemCliente);

                ResultView.Mensagem = MensagemViewHelper.GetOkFound(result.ListagemCliente.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound();
            }

            return ResultView;
        }
    }
}