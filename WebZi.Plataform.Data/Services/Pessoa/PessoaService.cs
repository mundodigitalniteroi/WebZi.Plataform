using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Domain.DTO.Pessoa;
using WebZi.Plataform.Domain.Enums;
using WebZi.Plataform.Domain.Models.Pessoa.Documento;
using WebZi.Plataform.Domain.ViewModel.Pessoa;

namespace WebZi.Plataform.Data.Services.Pessoa;

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

    public async Task<string> GetPessoaTelefoneByIdAsync(int pessoaId)
        => await _context.TipoPessoaContatos
            .Include(x => x.Pessoa)
            .Where(x => x.PessoaId == pessoaId && x.TipoContatoId == 1 && x.FlagContatoPrincipal == 'S')
            .Select(x => x.Descricao)
            .FirstOrDefaultAsync();

    public async Task<TipoDocumentoIdentificacaoListDTO> ListTipoDocumentoIdentificacaoAsync(bool FlagAtivo,
        CancellationToken ct)
    {
        TipoDocumentoIdentificacaoListDTO ResultView = new();

        List<TipoDocumentoIdentificacaoModel> result = await _context.TipoDocumentoIdentificacao
            .Where(x => x.FlagAtivo == (FlagAtivo ? "S" : "N"))
            .AsNoTracking()
            .ToListAsync(ct);

        if (result.Count > 0)
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

    public async Task<TipoDocumentoIdentificacaoSimplificadoListDTO>
        ListTipoDocumentoIdentificacaoSimplificadoAsync()
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

    public async Task<PessoaListDTO>
        ConsultarPessoa(int usuarioId, ConsultaPessoaParameters request, CancellationToken ct)
    {
        PessoaListDTO ResultView = new();

        var possuiPermissao = await _context.PerfilAcessoUsuario
            .AsNoTracking()
            .AnyAsync(x => x.UsuarioId == usuarioId
                           && x.PerfilAcessoId == (int)PerfisDeAcessoEnum.GerenciarUsuariosHomolog
                           && _context.SistemaPerfilAcessoSubModulos
                               .Any(s => s.IdPerfilAcesso == (int)PerfisDeAcessoEnum.GerenciarUsuariosHomolog
                                         && s.IdSubModulo == (int)SubModuloEnum.GerenciarUsuariosHomolog),
                cancellationToken: ct);

        if (possuiPermissao)
        {
            ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Não possui permissão");
            return ResultView;
        }

        var nome = request.Nome?.Trim();
        var nomeDoMeio = request.NomeDoMeio?.Trim();
        var sobrenome = request.Sobrenome?.Trim();

        var query = _context.Pessoa
            .AsNoTracking();

        if (!string.IsNullOrWhiteSpace(nome))
        {
            query = query.Where(x => x.Nome != null && x.Nome.Contains(nome));
        }

        if (!string.IsNullOrWhiteSpace(nomeDoMeio))
        {
            query = query.Where(x => x.NomeMeio != null && x.NomeMeio.Contains(nomeDoMeio));
        }

        if (!string.IsNullOrWhiteSpace(sobrenome))
        {
            query = query.Where(x => x.Sobrenome != null && x.Sobrenome.Contains(sobrenome));
        }

        if (request.IdentificadorTipoDocumento.HasValue && request.IdentificadorTipoDocumento.Value > 0)
        {
            query = query.Where(x =>
                x.DocumentosIdentificacao.Any(d =>
                    d.IdTipoDocumentoIdentificacao == request.IdentificadorTipoDocumento.Value));
        }

        var valorDocumento = request.ValorDocumento?.Trim();
        if (!string.IsNullOrWhiteSpace(valorDocumento))
        {
            query = query.Where(x =>
                x.DocumentosIdentificacao.Any(d => d.Descricao != null && d.Descricao.Contains(valorDocumento)));
        }

        var limit = request.Take.HasValue && request.Take.Value > 0 ? request.Take.Value : 20;
        var offset = request.Skip.HasValue && request.Skip.Value >= 0 ? request.Skip.Value : 0;

        var result = await query
            .OrderBy(x => x.Nome)
            .ThenBy(x => x.Sobrenome)
            .Skip(offset)
            .Take(limit)
            .Select(x => new PessoaDTO
            {
                IdentificadorPessoa = x.IdPessoa,
                Nome = x.Nome != null ? x.Nome.Trim() : "",
                NomeDoMeio = x.NomeMeio != null ? x.NomeMeio.Trim() : "",
                Sobrenome = x.Sobrenome != null ? x.Sobrenome.Trim() : "",
                Documento = x.DocumentosIdentificacao
                    .OrderByDescending(d =>
                        request.IdentificadorTipoDocumento.HasValue && d.IdTipoDocumentoIdentificacao ==
                        request.IdentificadorTipoDocumento.Value)
                    .Select(d => new PessoaDocumentoDTO
                    {
                        IdentificadorTipoDocumento = d.IdTipoDocumentoIdentificacao,
                        Formato = d.TipoDocumentoIdentificacao.Formato,
                        Descricao = d.TipoDocumentoIdentificacao.Codigo,
                        Numero = d.Descricao
                    })
                    .FirstOrDefault()
            })
            .ToListAsync(cancellationToken: ct);

        if (result.Count == 0)
        {
            ResultView.Mensagem = MensagemViewHelper.SetNotFound("Nenhuma pessoa encontrada");
            return ResultView;
        }

        ResultView.Listagem = result;
        ResultView.Mensagem = MensagemViewHelper.SetFound(result.Count);
        return ResultView;
    }
}