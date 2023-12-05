using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Domain.Models.Pessoa.Documento;
using WebZi.Plataform.Domain.ViewModel.Pessoa;

namespace WebZi.Plataform.Data.Services.Pessoa
{
    public class PessoaService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PessoaService(AppDbContext context)
        {
            _context = context;
        }

        public PessoaService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TipoDocumentoIdentificacaoViewModelList> ListTipoDocumentoIdentificacaoAsync()
        {
            TipoDocumentoIdentificacaoViewModelList ResultView = new();

            List<TipoDocumentoIdentificacaoModel> result = await _context.TipoDocumentoIdentificacao
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count > 0)
            {
                ResultView.Listagem = _mapper
                    .Map<List<TipoDocumentoIdentificacaoViewModel>>(result.OrderBy(o => o.Codigo)
                    .ToList());

                ResultView.Mensagem = MensagemViewHelper.SetFound(result.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();
            }

            return ResultView;
        }

        public async Task<TipoDocumentoIdentificacaoSimplificadoViewModelList> ListTipoDocumentoIdentificacaoSimplificadoAsync()
        {
            TipoDocumentoIdentificacaoSimplificadoViewModelList ResultView = new();

            List<TipoDocumentoIdentificacaoModel> result = await _context.TipoDocumentoIdentificacao
                .Where(x => x.FlagAtivo == "S"
                         && x.FlagPrincipal == "S")
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count > 0)
            {
                ResultView.Listagem = _mapper
                    .Map<List<TipoDocumentoIdentificacaoSimplificadoViewModel>>(result.OrderBy(o => o.Codigo)
                    .ToList());

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