using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Domain.Models.Banco;
using WebZi.Plataform.Domain.ViewModel.Banco;

namespace WebZi.Plataform.Data.Services.Banco
{
    public class AgenciaBancariaService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AgenciaBancariaService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AgenciaBancariaViewModelList> GetById(int AgenciaBancariaId)
        {
            AgenciaBancariaViewModelList ResultView = new();

            if (AgenciaBancariaId <= 0)
            {
                ResultView.Mensagem = MensagemViewHelper.GetBadRequest("Identificador da Agência Bancária inválido");

                return ResultView;
            }

            AgenciaBancariaModel result = await _context.AgenciaBancaria
                .Where(w => w.BancoId == AgenciaBancariaId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (result != null)
            {
                ResultView.AgenciasBancarias.Add(_mapper.Map<AgenciaBancariaViewModel>(result));

                ResultView.Mensagem = MensagemViewHelper.GetOkFound();
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound();
            }

            return ResultView;
        }

        public async Task<AgenciaBancariaViewModelList> GetByAgencia(int BancoId, string Agencia)
        {
            AgenciaBancariaViewModelList ResultView = new();

            List<string> erros = new();

            if (BancoId <= 0)
            {
                erros.Add("Identificador do Banco inválido");
            }

            if (string.IsNullOrWhiteSpace(Agencia))
            {
                erros.Add("Primeiro é necessário informar o Código da Agência");
            }

            if (erros.Count > 0)
            {
                ResultView.Mensagem = MensagemViewHelper.GetBadRequest(erros);

                return ResultView;
            }

            AgenciaBancariaModel result = await _context.AgenciaBancaria
                .Where(w => w.BancoId == BancoId && w.CodigoAgencia == Agencia)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (result != null)
            {
                ResultView.AgenciasBancarias.Add(_mapper.Map<AgenciaBancariaViewModel>(result));

                ResultView.Mensagem = MensagemViewHelper.GetOkFound();
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound();
            }

            return ResultView;
        }

        public async Task<AgenciaBancariaViewModelList> List(int BancoId)
        {
            AgenciaBancariaViewModelList ResultView = new();

            if (BancoId <= 0)
            {
                ResultView.Mensagem = MensagemViewHelper.GetBadRequest("Identificador do Banco inválido");

                return ResultView;
            }

            List<AgenciaBancariaModel> result = await _context.AgenciaBancaria
                .Where(w => w.BancoId == BancoId)
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count > 0)
            {
                result = result
                    .OrderBy(o => o.CodigoAgencia)
                    .ThenBy(t => t.ContaCorrente)
                    .ToList();

                ResultView.AgenciasBancarias = _mapper.Map<List<AgenciaBancariaViewModel>>(result);

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