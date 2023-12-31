using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Domain.DTO.Banco;
using WebZi.Plataform.Domain.Models.Banco;

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

        public async Task<BancoListDTO> GetByIdAsync(short BancoId)
        {
            BancoListDTO ResultView = new();

            if (BancoId <= 0)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Identificador do Banco inválido");

                return ResultView;
            }

            BancoModel result = await _context.Banco
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.BancoId == BancoId);

            if (result != null)
            {
                ResultView.Listagem.Add(_mapper.Map<BancoDTO>(result));

                ResultView.Mensagem = MensagemViewHelper.SetFound();
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();
            }

            return ResultView;
        }

        public async Task<BancoListDTO> GetByNameAsync(string Name)
        {
            BancoListDTO ResultView = new();

            if (string.IsNullOrWhiteSpace(Name))
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Informe o Nome do Banco");

                return ResultView;
            }

            List<BancoModel> result = await _context.Banco
                .Where(x => x.Nome.ToUpper().Contains(Name.ToUpper().Trim()))
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count > 0)
            {
                result = result
                    .OrderBy(x => x.Nome)
                    .ToList();

                ResultView.Listagem = _mapper.Map<List<BancoDTO>>(result);

                ResultView.Mensagem = MensagemViewHelper.SetFound(result.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();
            }

            return ResultView;
        }

        public async Task<BancoListDTO> ListAsync()
        {
            BancoListDTO ResultView = new();

            List<BancoModel> result = await _context.Banco
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count > 0)
            {
                result = result
                    .OrderBy(x => x.Nome)
                    .ToList();

                ResultView.Listagem = _mapper.Map<List<BancoDTO>>(result);

                ResultView.Mensagem = MensagemViewHelper.SetFound(result.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();
            }

            return ResultView;
        }
    }
}