using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Domain.Models.Cliente;
using WebZi.Plataform.Domain.ViewModel.Cliente;

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
                ResultView.Mensagem = MensagemViewHelper.GetBadRequest("Identificador do Cliente inválido");

                return ResultView;
            }

            ClienteModel result = await _context.Cliente
                .Where(w => w.ClienteId == ClienteId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (result != null)
            {
                ResultView.Clientes.Add(_mapper.Map<ClienteViewModel>(result));

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
                ResultView.Mensagem = MensagemViewHelper.GetBadRequest("Primeiro é necessário informar o Nome do Cliente");

                return ResultView;
            }

            List<ClienteModel> result = await _context.Cliente
                .Where(w => w.Nome.ToUpper().Contains(Name.ToUpper().Trim()))
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count > 0)
            {
                result = result.OrderBy(o => o.Nome).ToList();

                ResultView.Clientes = _mapper.Map<List<ClienteViewModel>>(result);

                ResultView.Mensagem = MensagemViewHelper.GetOkFound(result.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound();
            }

            return ResultView;
        }

        public async Task<ClienteViewModelList> List()
        {
            ClienteViewModelList ResultView = new();

            List<ClienteModel> result = await _context.Cliente
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count > 0)
            {
                result = result
                    .OrderBy(o => o.Nome)
                    .ToList();

                ResultView.Clientes = _mapper.Map<List<ClienteViewModel>>(result);

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