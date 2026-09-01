using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Domain.DTO.GRV.SolicitacoesReboque;
using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Data.Services.SolicitacaoReboque;

public class SolicitacaoReboqueService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly IServiceProvider _provider;
    private readonly IHttpClientFactory _httpClientFactory;

    public SolicitacaoReboqueService(AppDbContext context)
    {
        _context = context;
    }

    public SolicitacaoReboqueService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public SolicitacaoReboqueService(AppDbContext context, IMapper mapper, IServiceProvider provider)
    {
        _context = context;
        _mapper = mapper;
        _provider = provider;
    }

    public SolicitacaoReboqueService(AppDbContext context, IMapper mapper, IServiceProvider provider,
        IHttpClientFactory httpClientFactory)
    {
        _context = context;
        _mapper = mapper;
        _provider = provider;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<SolicitacoesReboqueListDTO> ListSolicitacoesReboqueAsync(int userId, short? skip, short? take,
        CancellationToken ct)
    {
        SolicitacoesReboqueListDTO ResultView = new();
        var query = _context.SolicitacaoReboque
            .AsNoTracking();

        var limit = take.HasValue && take.Value > 0 ? take.Value : 20;
        var offset = skip.HasValue && skip.Value >= 0 ? skip.Value : 0;

        var solicitacoes = await query
            .Where(x => x.UsuarioCadastroId == userId)
            .Skip(offset)
            .Take(limit)
            .Select(x => new SolicitacaoReboqueDTO
            {
                Mensagem = null,
                Id = x.Id,
                ClienteId = x.ClienteDeposito.ClienteId,
                DepositoId = x.ClienteDeposito.DepositoId,
                GrvId = x.GrvId,
                NumeroFormularioGrv = x.Grv != null ? x.Grv.NumeroFormularioGrv : null,
                Placa = x.Grv != null ? x.Grv.Placa : null,
                Chassi = x.Grv != null ? x.Grv.Chassi : null,
                ReboqueId = x.ReboqueId,
                ReboquePlaca = x.Reboque != null ? x.Reboque.Placa : null,
                ReboquistaId = x.ReboquistaId,
                ReboquistaNome = x.Reboquista != null ? x.Reboquista.Nome : null,
                SolicitacaoReboqueTipoId = x.SolicitacaoReboqueTipoId,
                SolicitacaoReboqueTipoDescricao =
                    x.SolicitacaoReboqueTipo != null ? x.SolicitacaoReboqueTipo.Descricao : null,
                SolicitacaoReboqueStatusId = x.SolicitacaoReboqueStatusId,
                SolicitacaoReboqueStatusDescricao =
                    x.SolicitacaoReboqueStatus != null ? x.SolicitacaoReboqueStatus.Descricao : null,
                LocalRemocaoCompleto = x.LocalRemocaoCompleto,
                LocalRemocaoReferencia = x.LocalRemocaoReferencia,
                LocalRemocaoLatitude = x.LocalRemocaoLatitude,
                LocalRemocaoLongitude = x.LocalRemocaoLongitude,
                UsuarioCadastroId = x.UsuarioCadastroId,
                UsuarioCadastroNome = x.UsuarioCadastro != null ? x.UsuarioCadastro.Login : null,
                DataCadastro = x.DataCadastro,
                UsuarioAlteracaoId = x.UsuarioAlteracaoId,
                UsuarioAlteracaoNome = x.UsuarioAlteracao != null ? x.UsuarioAlteracao.Login : null,
                DataAlteracao = x.DataAlteracao
            })
            .ToListAsync(cancellationToken: ct);


        if (solicitacoes.Count == 0)
        {
            ResultView.Mensagem = MensagemViewHelper.SetNotFound("Nenhuma solicitação encontrada!");
            return ResultView;
        }

        ResultView.Listagem = solicitacoes;
        ResultView.Mensagem = MensagemViewHelper.SetFound(solicitacoes.Count);
        return ResultView;
    }

    public async Task<SolicitacaoReboqueDTO> GetByIdSolicitacaoReboqueAsync(int userId, int solicitacaoReboqueId,
        CancellationToken ct)
    {
        SolicitacaoReboqueDTO ResultView = new();
        ResultView = await _context.SolicitacaoReboque
            .AsNoTracking()
            .Select(x => new SolicitacaoReboqueDTO
            {
                Id = x.Id,
                ClienteId = x.ClienteDeposito.ClienteId,
                DepositoId = x.ClienteDeposito.DepositoId,
                GrvId = x.GrvId,
                NumeroFormularioGrv = x.Grv != null ? x.Grv.NumeroFormularioGrv : null,
                Placa = x.Grv != null ? x.Grv.Placa : null,
                Chassi = x.Grv != null ? x.Grv.Chassi : null,
                ReboqueId = x.ReboqueId,
                ReboquePlaca = x.Reboque != null ? x.Reboque.Placa : null,
                ReboquistaId = x.ReboquistaId,
                ReboquistaNome = x.Reboquista != null ? x.Reboquista.Nome : null,
                SolicitacaoReboqueTipoId = x.SolicitacaoReboqueTipoId,
                SolicitacaoReboqueTipoDescricao =
                    x.SolicitacaoReboqueTipo != null ? x.SolicitacaoReboqueTipo.Descricao : null,
                SolicitacaoReboqueStatusId = x.SolicitacaoReboqueStatusId,
                SolicitacaoReboqueStatusDescricao =
                    x.SolicitacaoReboqueStatus != null ? x.SolicitacaoReboqueStatus.Descricao : null,
                LocalRemocaoCompleto = x.LocalRemocaoCompleto,
                LocalRemocaoReferencia = x.LocalRemocaoReferencia,
                LocalRemocaoLatitude = x.LocalRemocaoLatitude,
                LocalRemocaoLongitude = x.LocalRemocaoLongitude,
                UsuarioCadastroId = x.UsuarioCadastroId,
                UsuarioCadastroNome = x.UsuarioCadastro != null ? x.UsuarioCadastro.Login : null,
                DataCadastro = x.DataCadastro,
                UsuarioAlteracaoId = x.UsuarioAlteracaoId,
                UsuarioAlteracaoNome = x.UsuarioAlteracao != null ? x.UsuarioAlteracao.Login : null,
                DataAlteracao = x.DataAlteracao
            })
            .FirstOrDefaultAsync(x => x.UsuarioCadastroId == userId, cancellationToken: ct)


        if (ResultView is null)
        {
            ResultView.Mensagem = MensagemViewHelper.SetNotFound("Nenhuma solicitação encontrada!");
            return ResultView;
        }

        ResultView.Mensagem = MensagemViewHelper.SetFound();
        return ResultView;
    }

    public async Task<MensagemDTO> CreateSolicitacaoReboqueAsync(SolicitacaoReboqueParameters solicitacaoReboque)
    {
        
    }
}