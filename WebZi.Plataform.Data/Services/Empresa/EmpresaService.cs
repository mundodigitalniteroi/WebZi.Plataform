using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.CrossCutting.Documents;
using WebZi.Plataform.CrossCutting.Strings;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Domain.Models.Empresa;
using WebZi.Plataform.Domain.ViewModel.Empresa;

namespace WebZi.Plataform.Data.Services.Empresa
{
    public class EmpresaService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public EmpresaService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<EmpresaViewModelList> GetByIdAsync(int EmpresaId)
        {
            EmpresaViewModelList ResultView = new();

            if (EmpresaId <= 0)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Identificador da Empresa inválido");

                return ResultView;
            }

            List<EmpresaModel> result = await _context.Empresa
                .Where(x => x.EmpresaId == EmpresaId)
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count > 0)
            {
                ResultView.Listagem = _mapper.Map<List<EmpresaViewModel>>(result.OrderBy(x => x.Nome).ToList());

                ResultView.Mensagem = MensagemViewHelper.SetFound(result.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound("Empresa não encontrada");
            }

            return ResultView;
        }

        public async Task<EmpresaViewModelList> ListAsync(string CNPJ = "", string Nome = "")
        {
            List<string> erros = new();

            if (!string.IsNullOrWhiteSpace(CNPJ) && !DocumentHelper.IsCNPJ(CNPJ))
            {
                erros.Add("CNPJ inválido");
            }

            EmpresaViewModelList ResultView = new();

            if (erros.Count > 0)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest(erros);

                return ResultView;
            }

            List<EmpresaModel> result = await _context.Empresa
                .Where(x => (!CNPJ.IsNullOrWhiteSpace() ? x.CNPJ == CNPJ : true) &&
                            (!Nome.IsNullOrWhiteSpace() ? x.Nome.Contains(Nome.ToUpper().Trim()) : true))
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count > 0)
            {
                ResultView.Listagem = _mapper.Map<List<EmpresaViewModel>>(result.OrderBy(x => x.Nome).ToList());

                ResultView.Mensagem = MensagemViewHelper.SetFound(result.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound("Empresa não encontrada");
            }

            return ResultView;
        }
    }
}