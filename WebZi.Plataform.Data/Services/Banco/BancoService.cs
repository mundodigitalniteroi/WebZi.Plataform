using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Domain.Models.Banco;
using WebZi.Plataform.Domain.ViewModel.Banco;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebZi.Plataform.Data.Services.Banco
{
    public class BancoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public BancoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BancoViewModelList> GetById(short BancoId)
        {
            BancoViewModelList ResultView = new();

            if (BancoId <= 0)
            {
                ResultView.Mensagem = MensagemViewHelper.GetBadRequest("Identificador do Banco inválido");

                return ResultView;
            }

            BancoModel result = await _context.Banco
                .Where(w => w.BancoId == BancoId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (result != null)
            {
                ResultView.Bancos.Add(_mapper.Map<BancoViewModel>(result));

                ResultView.Mensagem = MensagemViewHelper.GetOkFound();
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound();
            }

            return ResultView;
        }

        public async Task<BancoViewModelList> GetByName(string Name)
        {
            BancoViewModelList ResultView = new();

            if (string.IsNullOrWhiteSpace(Name))
            {
                ResultView.Mensagem = MensagemViewHelper.GetBadRequest("Informe o Nome do Banco");

                return ResultView;
            }

            List<BancoModel> result = await _context.Banco
                .Where(w => w.Nome.ToUpper().Contains(Name.ToUpper().Trim()))
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count > 0)
            {
                result = result.OrderBy(o => o.Nome).ToList();

                ResultView.Bancos = _mapper.Map<List<BancoViewModel>>(result);

                ResultView.Mensagem = MensagemViewHelper.GetOkFound(result.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound();
            }

            return ResultView;
        }

        public async Task<BancoViewModelList> List()
        {
            BancoViewModelList ResultView = new();

            List<BancoModel> result = await _context.Banco
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count > 0)
            {
                result = result
                    .OrderBy(o => o.Nome)
                    .ToList();

                ResultView.Bancos = _mapper.Map<List<BancoViewModel>>(result);

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