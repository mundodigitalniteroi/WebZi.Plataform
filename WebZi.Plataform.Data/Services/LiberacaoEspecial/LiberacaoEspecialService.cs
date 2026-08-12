using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Usuario;
using WebZi.Plataform.Domain.DTO.Liberacao;
using WebZi.Plataform.Domain.DTO.Sistema;
using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Models.Liberacao;
using WebZi.Plataform.Domain.ViewModel.Atendimento;
using Z.EntityFramework.Plus;

namespace WebZi.Plataform.Data.Services.LiberacaoEspecial;

public class LiberacaoEspecialService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public LiberacaoEspecialService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<TipoLiberacaoEspecialListDTO> ListTipoLiberacaoAsync(int identificadorUsuario)
    {
        TipoLiberacaoEspecialListDTO ResultView = new();

        if (identificadorUsuario <= 0)
        {
            ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Identificador do Usuário inválido");

            return ResultView;
        }

        bool isUserActive = await new UsuarioService(_context).IsUserActiveAsync(identificadorUsuario);
        if (!isUserActive)
        {
            ResultView.Mensagem = MensagemViewHelper.SetUnauthorized();

            return ResultView;
        }

        List<TipoLiberacaoEspecialModel> result = await _context.TipoLiberacaoEspecial
            .AsNoTracking()
            .ToListAsync();

        if (result?.Count > 0)
        {
            ResultView.Listagem = _mapper.Map<List<TipoLiberacaoEspecialDTO>>(result
                .OrderBy(x => x.Descricao)
                .ToList());

            ResultView.Mensagem = MensagemViewHelper.SetFound(result.Count);
        }
        else
        {
            ResultView.Mensagem = MensagemViewHelper.SetNotFound();
        }

        return ResultView;
    }
    
    public async Task<MensagemDTO> CreateLiberacaoEspecialAsync(LiberacaoEspecialParameters parameters, DateTime dataLiberacao, bool saidaParaReparo, CancellationToken ct)
    {
        #region Validação

        if (parameters.DataEmissaoDocumento < DateTime.Today)
            return MensagemViewHelper.SetBadRequest("Data não pode ser menor que hoje");

        #endregion Validação

        LiberacaoEspecialModel liberacaoEspecial = new()
        {
            IdGrv = parameters.IdentificadorProcesso,
            IdFaturamento = parameters.IdentificadorFaturamento,
            IdLiberacaoEspecialTipo = parameters.IdLiberacaoEspecialTipo,
            IdUsuarioCadastro = parameters.IdentificadorUsuario,
            NumeroDocumento = parameters.NumeroDocumento.ToUpper(),
            TipoDocumento = parameters.TipoDocumento.ToUpper(),
            NumeroProcesso = parameters.NumeroProcesso.ToUpper(),
            OrgaoEmissor = parameters.OrgaoEmissor.ToUpper(),
            PortadorNome = parameters.PortadorNome.ToUpper(),
            PortadorCargo = parameters.PortadorCargo.ToUpper(),
            PortadorMatricula = parameters.PortadorMatricula.ToUpper(),
            SignatarioNomeDocumento = parameters.SignatarioNomeDocumento.ToUpper(),
            SignatarioMatricula = parameters.SignatarioMatricula.ToUpper(),
            SignatarioTitulo = parameters.SignatarioTitulo.ToUpper(),
            DataEmissaoDocumento = parameters.DataEmissaoDocumento.Date,
            DataLiberacao = dataLiberacao
        };
        
        try
        {
            await _context.LiberacaoEspecial.AddAsync(liberacaoEspecial, ct);
            await _context.Faturamento
                .Where(x => x.FaturamentoId == parameters.IdentificadorFaturamento)
                .UpdateAsync(x => new FaturamentoModel()
                {
                    Status = "P",
                    UsuarioAlteracaoId = parameters.IdentificadorUsuario,
                    DataPrazoRetiradaVeiculo = DateTime.Now.AddDays(1),
                    DataPagamento = DateTime.Now
                }, cancellationToken: ct);
            await _context.Grv
                .Where(x => x.GrvId == parameters.IdentificadorProcesso)
                .UpdateAsync(x => new GrvModel()
                {
                    StatusOperacaoId = saidaParaReparo ? "R" : "E",
                    DataAlteracao = DateTime.Now,
                    UsuarioAlteracaoId = parameters.IdentificadorUsuario
                }, cancellationToken: ct);
            await _context.SaveChangesAsync(ct);
            return MensagemViewHelper.SetCreateSuccess();
        }
        catch (Exception e)
        {
            return MensagemViewHelper.SetBadRequest(e.Message);
        }
    }
           public async Task UpdateLiberacaoEspecialAsync(AtualizarLiberacaoEspecialParameters parameters)
        {
            var libEspecial = await _context.LiberacaoEspecial
                .AsTracking()
                .FirstOrDefaultAsync(x => x.IdGrv == parameters.IdGrv);
            try
            {
                if (libEspecial != null)
                {
                    libEspecial.IdLiberacaoEspecialTipo = parameters.IdLiberacaoEspecialTipo;
                    libEspecial.NumeroDocumento = parameters.NumeroDocumento.ToUpper();
                    libEspecial.TipoDocumento = parameters.TipoDocumento.ToUpper();
                    libEspecial.NumeroProcesso = parameters.NumeroProcesso.ToUpper();
                    libEspecial.OrgaoEmissor = parameters.OrgaoEmissor.ToUpper();
                    libEspecial.PortadorNome = parameters.PortadorNome.ToUpper();
                    libEspecial.PortadorCargo = parameters.PortadorCargo.ToUpper();
                    libEspecial.PortadorMatricula = parameters.PortadorMatricula.ToUpper();
                    libEspecial.SignatarioNomeDocumento = parameters.SignatarioNomeDocumento.ToUpper();
                    libEspecial.SignatarioMatricula = parameters.SignatarioMatricula.ToUpper();
                    libEspecial.SignatarioTitulo = parameters.SignatarioTitulo.ToUpper();
                    libEspecial.DataEmissaoDocumento = parameters.DataEmissaoDocumento.Date;
                    return;
                }

                LiberacaoEspecialModel liberacaoEspecial = new()
                {
                    IdGrv = parameters.IdGrv,
                    IdFaturamento = parameters.IdFaturamento.Value,
                    IdLiberacaoEspecialTipo = parameters.IdLiberacaoEspecialTipo,
                    IdUsuarioCadastro = parameters.IdUsuarioCadastro.Value,
                    NumeroDocumento = parameters.NumeroDocumento.ToUpper(),
                    TipoDocumento = parameters.TipoDocumento.ToUpper(),
                    NumeroProcesso = parameters.NumeroProcesso.ToUpper(),
                    OrgaoEmissor = parameters.OrgaoEmissor.ToUpper(),
                    PortadorNome = parameters.PortadorNome.ToUpper(),
                    PortadorCargo = parameters.PortadorCargo.ToUpper(),
                    PortadorMatricula = parameters.PortadorMatricula.ToUpper(),
                    SignatarioNomeDocumento = parameters.SignatarioNomeDocumento.ToUpper(),
                    SignatarioMatricula = parameters.SignatarioMatricula.ToUpper(),
                    SignatarioTitulo = parameters.SignatarioTitulo.ToUpper(),
                    DataEmissaoDocumento = parameters.DataEmissaoDocumento.Date,
                    DataLiberacao = DateTime.Now
                };
                await _context.LiberacaoEspecial.AddAsync(liberacaoEspecial);
            }
            catch (Exception e)
            {
                throw new DbUpdateException(e.Message);
            }
        }
}