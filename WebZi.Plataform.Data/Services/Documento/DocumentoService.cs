using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.CrossCutting.Localizacao;
using WebZi.Plataform.CrossCutting.Strings;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Domain.Models.Documento;
using WebZi.Plataform.Domain.ViewModel.Documento;

namespace WebZi.Plataform.Data.Services.Documento
{
    public class DocumentoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public DocumentoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OrgaoEmissorViewModelList> ListarOrgaoEmissorAsync(string UF)
        {
            OrgaoEmissorViewModelList ResultView = new();

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

            List<OrgaoEmissorModel> result = await _context.OrgaoEmissor
                .Where(w => w.UF == UF.ToUpperTrim())
                .AsNoTracking()
                .ToListAsync();

            if (result == null)
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound("Unidade Federativa sem Órgão Emissor cadastrado");

                return ResultView;
            }

            if (result?.Count > 0)
            {
                ResultView.Listagem = _mapper.Map<List<OrgaoEmissorViewModel>>(result
                    .OrderBy(o => o.Descricao)
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