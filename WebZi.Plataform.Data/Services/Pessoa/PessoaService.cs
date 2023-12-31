using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Domain.DTO.Pessoa;
using WebZi.Plataform.Domain.Models.Pessoa.Documento;

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

        public async Task<TipoDocumentoIdentificacaoListDTO> ListTipoDocumentoIdentificacaoAsync()
        {
            TipoDocumentoIdentificacaoListDTO ResultView = new();

            List<TipoDocumentoIdentificacaoModel> result = await _context.TipoDocumentoIdentificacao
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count > 0)
            {
                ResultView.Listagem = _mapper
                    .Map<List<TipoDocumentoIdentificacaoDTO>>(result
                    .OrderBy(x => x.Codigo)
                    .ToList());

                ResultView.Mensagem = MensagemViewHelper.SetFound(result.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();
            }

            return ResultView;
        }

        public async Task<TipoDocumentoIdentificacaoSimplificadoListDTO> ListTipoDocumentoIdentificacaoSimplificadoAsync()
        {
            TipoDocumentoIdentificacaoSimplificadoListDTO ResultView = new();

            List<TipoDocumentoIdentificacaoModel> result = await _context.TipoDocumentoIdentificacao
                .Where(x => x.FlagAtivo == "S"
                         && x.FlagPrincipal == "S")
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count > 0)
            {
                ResultView.Listagem = _mapper
                    .Map<List<TipoDocumentoIdentificacaoSimplificadoDTO>>(result
                    .OrderBy(x => x.Codigo)
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