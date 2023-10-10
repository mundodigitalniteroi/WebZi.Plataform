using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.CrossCutting.Localizacao;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Domain.Models.Documento;
using WebZi.Plataform.Domain.ViewModel.GRV;

namespace WebZi.Plataform.Data.Services.GRV
{
    public class AutoridadeResponsavelService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AutoridadeResponsavelService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AutoridadeResponsavelViewModelList> List(string UF)
        {
            AutoridadeResponsavelViewModelList ResultView = new();

            if (string.IsNullOrWhiteSpace(UF))
            {
                ResultView.Mensagem = MensagemViewHelper.GetBadRequest("Informe a Unidade Federativa");

                return ResultView;
            }
            else if (!LocalizacaoHelper.IsUF(UF))
            {
                ResultView.Mensagem = MensagemViewHelper.GetBadRequest("Unidade Federativa inválida");

                return ResultView;
            }

            OrgaoEmissorModel result = await _context.OrgaoEmissor
                .Include(i => i.AutoridadesResponsaveis)
                .Where(w => w.UF == UF.ToUpper().Trim())
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (result == null)
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound("Unidade Federativa sem Órgão Emissor cadastrado");

                return ResultView;
            }

            if (result.AutoridadesResponsaveis?.Count > 0)
            {
                ResultView.AutoridadesResponsaveis = _mapper.Map<List<AutoridadeResponsavelViewModel>>(result.AutoridadesResponsaveis
                    .OrderBy(o => o.Divisao)
                    .ToList());

                ResultView.Mensagem = MensagemViewHelper.GetOkFound(result.AutoridadesResponsaveis.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound();
            }

            return ResultView;
        }
    }
}