using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using WebZi.Plataform.CrossCutting.Strings;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Sistema;
using WebZi.Plataform.Data.Services.WebServices;
using WebZi.Plataform.Domain.DTO.GRV.SolicitacoesReboque;
using WebZi.Plataform.Domain.DTO.Sistema;
using WebZi.Plataform.Domain.Enums;
using WebZi.Plataform.Domain.Models.GRV.SolicitacaoReboque;
using WebZi.Plataform.Domain.ViewModel.GRV.SolicitacaoReboque;

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

    public async Task<SolicitacoesReboqueListDTO> ListSolicitacoesReboqueAsync(
        SolicitacaoReboquePesquisaParameters? parameters,
        CancellationToken ct)
    {
        SolicitacoesReboqueListDTO ResultView = new();
        var query = _context.SolicitacaoReboque
            .AsNoTracking()
            .Where(x => x.UsuarioCadastroId == parameters.IdentificadorUsuario);

        if (!string.IsNullOrWhiteSpace(parameters.Placa))
        {
            var cleanPlaca = parameters.Placa.Trim().ToUpper();
            query = query.Where(x =>
                (x.Placa != null && x.Placa == cleanPlaca) || (x.Grv != null && x.Grv.Placa == cleanPlaca));
        }

        if (!string.IsNullOrWhiteSpace(parameters.NumeroProcesso))
        {
            var cleanProc = parameters.NumeroProcesso.Trim();
            query = query.Where(x =>
                (x.Grv != null && x.Grv.NumeroFormularioGrv == cleanProc) || x.Id.ToString() == cleanProc);
        }

        if (parameters.DataInicial.HasValue)
            query = query.Where(x => x.DataCadastro >= parameters.DataInicial.Value);

        if (parameters.DataFinal.HasValue)
            query = query.Where(x => x.DataCadastro <= parameters.DataFinal.Value);

        var totalCount = await query.CountAsync(cancellationToken: ct);

        var limit = parameters.Take is > 0 ? parameters.Take.Value : 20;
        var offset = parameters.Skip is >= 0 ? parameters.Skip.Value : 0;

        var solicitacoes = await query
            .OrderByDescending(x => x.Id)
            .Skip(offset)
            .Take(limit)
            .Select(x => new SolicitacaoReboqueDTO
            {
                Id = x.Id,
                NumeroProcesso = x.Grv != null ? x.Grv.NumeroFormularioGrv : x.Id.ToString(),
                Cliente = x.ClienteDeposito.Cliente.Nome,
                Deposito = x.ClienteDeposito.Deposito.Nome,
                MotivoApreensao = x.MotivoApreensao != null ? x.MotivoApreensao.Descricao : null,
                Veiculo = new SolicitacaoReboqueVeiculoDTO
                {
                    TipoVeiculo = x.Grv != null
                        ? x.Grv.TipoVeiculo.Descricao
                        : (x.TipoVeiculo != null ? x.TipoVeiculo.Descricao : null),
                    Placa = x.Grv != null ? x.Grv.Placa : x.Placa,
                    Chassi = x.Grv != null ? x.Grv.Chassi : x.Chassi,
                    Renavam = x.Grv != null ? x.Grv.Renavam : x.Renavam,
                    MarcaModelo = x.Grv != null
                        ? x.Grv.MarcaModelo.MarcaModelo
                        : (x.MarcaModelo != null ? x.MarcaModelo.MarcaModelo : null),
                    Cor = x.Grv != null ? x.Grv.Cor.Cor : (x.Cor != null ? x.Cor.Cor : null)
                },
                Status = x.SolicitacaoReboqueStatus.Descricao,
                LocalApreenssao = x.LocalRemocaoCompleto,
                DataCriacaoSolicitacao = x.DataCadastro,
                DataSolicitacaoAceita = x.DataSolicitacaoAceita
            })
            .ToListAsync(cancellationToken: ct)!;

        if (solicitacoes.Count == 0)
        {
            ResultView.Mensagem = MensagemViewHelper.SetNotFound("Nenhuma solicitação encontrada!");
            return ResultView;
        }

        ResultView.Listagem = solicitacoes;
        ResultView.Mensagem = MensagemViewHelper.SetFound(totalCount);
        return ResultView;
    }

    public async Task<SelecionarSolicitacaoReboqueDTO> GetByIdSolicitacaoReboqueAsync(int userId,
        int solicitacaoReboqueId,
        CancellationToken ct)
    {
        SelecionarSolicitacaoReboqueDTO ResultView = new();

        var solicitacao = await _context.SolicitacaoReboque
            .AsNoTracking()
            .Where(x => x.Id == solicitacaoReboqueId && x.UsuarioCadastroId == userId)
            .Select(x => new SelecionarSolicitacaoReboqueDTO
            {
                Id = x.Id,
                NumeroProcesso = x.Grv != null ? x.Grv.NumeroFormularioGrv : x.Id.ToString(),
                Cliente = x.ClienteDeposito.Cliente.Nome,
                Deposito = x.ClienteDeposito.Deposito.Nome,
                MotivoApreensao = x.MotivoApreensao != null ? x.MotivoApreensao.Descricao : null,
                Veiculo = new SolicitacaoReboqueVeiculoDTO
                {
                    TipoVeiculo = x.Grv != null
                        ? x.Grv.TipoVeiculo.Descricao
                        : (x.TipoVeiculo != null ? x.TipoVeiculo.Descricao : null),
                    Placa = x.Grv != null ? x.Grv.Placa : x.Placa,
                    Chassi = x.Grv != null ? x.Grv.Chassi : x.Chassi,
                    Renavam = x.Grv != null ? x.Grv.Renavam : x.Renavam,
                    MarcaModelo = x.Grv != null
                        ? x.Grv.MarcaModelo.MarcaModelo
                        : (x.MarcaModelo != null ? x.MarcaModelo.MarcaModelo : null),
                    Cor = x.Grv != null ? x.Grv.Cor.Cor : (x.Cor != null ? x.Cor.Cor : null)
                },
                Reboque = x.Reboque != null ? x.Reboque.Placa : null,
                Reboquista = x.Reboquista != null ? x.Reboquista.Nome : null,
                SolicitacaoReboqueTipo = x.SolicitacaoReboqueTipo != null ? x.SolicitacaoReboqueTipo.Descricao : null,
                SolicitacaoReboqueStatus =
                    x.SolicitacaoReboqueStatus != null ? x.SolicitacaoReboqueStatus.Descricao : null,
                LocalRemocaoCompleto = x.LocalRemocaoCompleto,
                LocalRemocaoReferencia = x.LocalRemocaoReferencia,
                LocalRemocaoLatitude = x.LocalRemocaoLatitude,
                LocalRemocaoLongitude = x.LocalRemocaoLongitude,
                UsuarioCadastro = x.UsuarioCadastro != null
                    ? (x.UsuarioCadastro.Pessoa != null ? x.UsuarioCadastro.Pessoa.Nome : x.UsuarioCadastro.Login)
                    : null,
                DataCadastro = x.DataCadastro,
                DataSolicitacaoAceito = x.DataSolicitacaoAceita
            })
            .FirstOrDefaultAsync(cancellationToken: ct);

        if (solicitacao is null)
        {
            ResultView.Mensagem = MensagemViewHelper.SetNotFound("Nenhuma solicitação encontrada!");
            return ResultView;
        }

        ResultView = solicitacao;

        ResultView.Mensagem = MensagemViewHelper.SetFound();
        return ResultView;
    }

    public async Task<MensagemDTO> CreateSolicitacaoReboqueAsync(CadastrarSolicitacaoReboqueParameters parameters,
        CancellationToken ct)
    {
        List<string> erros = new();

        var cliente = await _context.UsuarioCliente
            .AsNoTracking()
            .AnyAsync(
                x => x.UsuarioId == parameters.IdentificadorUsuario &&
                     x.ClienteId == parameters.IdentificadorCliente, cancellationToken: ct);

        var deposito = await _context.UsuarioDeposito
            .AsNoTracking()
            .AnyAsync(
                x => x.UsuarioId == parameters.IdentificadorUsuario &&
                     x.DepositoId == parameters.IdentificadorDeposito, cancellationToken: ct)!;

        var clienteDeposito = await _context.ClienteDeposito
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.ClienteId == parameters.IdentificadorCliente &&
                     x.DepositoId == parameters.IdentificadorDeposito, cancellationToken: ct)!;

        var grv = await _context.Grv.FirstOrDefaultAsync(
            x => x.ClienteId == clienteDeposito.ClienteId &&
                 x.DepositoId == clienteDeposito.DepositoId &&
                 x.StatusOperacaoId != "E" &&
                 x.StatusOperacaoId != "7" &&
                 (
                     (!string.IsNullOrWhiteSpace(parameters.Placa) &&
                      x.Placa == parameters.Placa)
                     ||
                     (!string.IsNullOrWhiteSpace(parameters.Chassi) &&
                      x.Chassi == parameters.Chassi)
                 ), cancellationToken: ct)!;

        if (grv is not null)
        {
            var isPlacaDuplicada = !string.IsNullOrWhiteSpace(parameters.Placa) &&
                                   string.Equals(grv.Placa, parameters.Placa,
                                       StringComparison.OrdinalIgnoreCase);
            var isChassiDuplicado = !string.IsNullOrWhiteSpace(parameters.Chassi) &&
                                    string.Equals(grv.Chassi, parameters.Chassi,
                                        StringComparison.OrdinalIgnoreCase);

            if (isPlacaDuplicada && isChassiDuplicado)
            {
                erros.Add("Esse GRV já existe (Placa e Chassi já cadastrados)");
            }
            else if (isPlacaDuplicada)
            {
                erros.Add("Esse GRV já existe (Placa já cadastrada)");
            }
            else if (isChassiDuplicado)
            {
                erros.Add("Esse GRV já existe (Chassi já cadastrado)");
            }
            else
            {
                erros.Add("Esse GRV já existe");
            }

            erros.Add($"{grv.NumeroFormularioGrv}");
        }

        if (!cliente)
            erros.Add(MensagemPadraoEnum.UsuarioSemPermissaoAcessoCliente);
        if (!deposito)
            erros.Add(MensagemPadraoEnum.UsuarioSemPermissaoAcessoDeposito);
        if (clienteDeposito == null)
            erros.Add(MensagemPadraoEnum.NaoEncontradoAssociacaoClienteDeposito);

        if (parameters.IdentificadorMotivoApreensao > 0)
        {
            var tipoExiste = await _context.MotivoApreensao
                .AsNoTracking()
                .AnyAsync(x => x.MotivoApreensaoId == parameters.IdentificadorMotivoApreensao,
                    cancellationToken: ct)!;

            if (!tipoExiste)
            {
                erros.Add("Tipo de motivo de apreensão inválido");
            }
        }
        else
        {
            erros.Add("Tipo de motivo de apreensão é obrigatório");
        }

        byte solicitacaoReboqueTipoId = 0;

        if (parameters.IdentificadorMotivoApreensao > 0)
        {
            var tipo = await _context.SolicitacaoReboqueTipo
                .AsNoTracking()
                .Where(x => x.MotivoApreensaoId == parameters.IdentificadorMotivoApreensao)
                .Select(x => new
                {
                    x.Id
                })
                .FirstOrDefaultAsync(cancellationToken: ct)!;

            if (tipo is null)
            {
                erros.Add("Tipo de Solicitação de Reboque inválido");
            }
            else
            {
                solicitacaoReboqueTipoId = tipo.Id;
            }
        }
        else
            erros.Add("Tipo de motivo de apreensão é obrigatório para buscar o tipo de Solicitação de Reboque");

        if (erros.Count > 0)
            return MensagemViewHelper.SetBadRequest(erros);

        var model = new SolicitacaoReboqueModel
        {
            ClienteDepositoId = clienteDeposito!.ClienteDepositoId,
            MotivoApreensaoId = parameters.IdentificadorMotivoApreensao,
            SolicitacaoReboqueTipoId = solicitacaoReboqueTipoId,
            SolicitacaoReboqueStatusId = 1,
            UsuarioCadastroId = parameters.IdentificadorUsuario,
            TipoVeiculoId = parameters.IdentificadorTipoVeiculo,
            CorId = parameters.IdentificadorCor,
            MarcaModeloId = parameters.IdentificadorMarcaModelo,
            Placa = parameters.Placa.ToUpperTrim().ToNullIfEmpty(),
            Chassi = parameters.Chassi.ToUpperTrim().ToNullIfEmpty(),
            Renavam = parameters.Renavam.ToUpperTrim().ToNullIfEmpty(),
            VeiculoUF = parameters.VeiculoUF.ToUpperTrim().ToNullIfEmpty(),
            LocalRemocaoCompleto = parameters.LocalRemocaoEnderecoCompleto.ToUpperTrim().ToNullIfEmpty(),
            LocalRemocaoReferencia = parameters.LocalRemocaoEnderecoReferencia.ToUpperTrim().ToNullIfEmpty(),
            LocalRemocaoLatitude = parameters.LocalRemocaoEnderecoLatitude.ToUpperTrim().ToNullIfEmpty(),
            LocalRemocaoLongitude = parameters.LocalRemocaoEnderecoLongitude.ToUpperTrim().ToNullIfEmpty(),
            DataCadastro = DateTime.Now
        };


        await using var transaction = await _context.Database.BeginTransactionAsync(ct);
        try
        {
            await _context.SolicitacaoReboque.AddAsync(model, ct);
            await _context.SaveChangesAsync(ct);

            if (solicitacaoReboqueTipoId == 1)
            {
                var solicitacaoReboqueGrv = PayloadSolicitacaoReboqueGrv(model.Id, parameters.SolicitacaoReboqueGrv);
                await _context.SolicitacaoReboqueGrv.AddAsync(solicitacaoReboqueGrv, ct);
                await _context.SaveChangesAsync(ct);
            }

            if (parameters.IdentificadorMotivoApreensao == 1 && parameters.ListagemEnquadramentoInfracao?.Count > 0)
            {
                foreach (var item in parameters.ListagemEnquadramentoInfracao)
                {
                    var infracao = new SolicitacaoReboqueEnquadramentoInfracaoModel
                    {
                        SolicitacaoReboqueGrvId = model.Id,
                        EnquadramentoInfracaoId = item.IdentificadorEnquadramentoInfracao,
                        NumeroInfracao = item.NumeroInfracao.ToUpperTrim().ToNullIfEmpty()
                    };
                    await _context.SolicitacaoReboqueEnquadramentoInfracao.AddAsync(infracao, ct);
                }
            }

            if (parameters.Condutor != null)
            {
                var condutor = new SolicitacaoReboqueCondutorModel
                {
                    SolicitacaoReboqueGrvId = model.Id,
                    Telefone = parameters.Condutor.Telefone.ToUpperTrim().ToNullIfEmpty(),
                    TelefoneDDD = parameters.Condutor.TelefoneDDD.ToUpperTrim().ToNullIfEmpty(),
                    InformacoesAdicionais = parameters.Condutor.InformacoesAdicionais.ToUpperTrim().ToNullIfEmpty(),
                };

                await _context.SolicitacaoReboqueCondutor.AddAsync(condutor, ct);
            }

            await _context.SaveChangesAsync(ct);

            if (parameters.ListagemFoto?.Count > 0)
            {
                new BucketService(_context, _httpClientFactory)
                    .SendFiles(BucketNomeTabelaOrigemEnum.SolicitacaoReboque, model.Id, model.UsuarioCadastroId,
                        parameters.ListagemFoto);
            }

            await transaction.CommitAsync(ct);

            return MensagemViewHelper.SetCreateSuccess("Solicitação de reboque cadastrada com sucesso!");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(ct);
            return MensagemViewHelper.SetInternalServerError(ex);
        }
    }

    private SolicitacaoReboqueGrvModel PayloadSolicitacaoReboqueGrv(int solicitacaoReboqueId,
        SolicitacaoReboqueGrvParameters solicitacaoGrv)
        => new()
        {
            SolicitacaoReboqueId = solicitacaoReboqueId,
            AutoridadeResponsavelId = solicitacaoGrv.IdentificadorAutoridadeResponsavel,
            MatriculaAutoridadeResponsavel = solicitacaoGrv.MatriculaAgenteResponsavel.ToUpperTrim().ToNullIfEmpty(),
            NomeAutoridadeResponsavel = solicitacaoGrv.NomeAgenteResponsavel.ToUpperTrim().ToNullIfEmpty()
        };


    public async Task<MensagemDTO> CancelarSolicitacaoReboqueAsync(int userId, int solicitacaoReboqueId,
        CancellationToken ct)
    {
        if (solicitacaoReboqueId <= 0)
        {
            return MensagemViewHelper.SetBadRequest("Identificador da Solicitação de Reboque inválido!");
        }

        var solicitacao = await _context.SolicitacaoReboque
            .FirstOrDefaultAsync(x => x.Id == solicitacaoReboqueId, cancellationToken: ct);

        if (solicitacao == null)
        {
            return MensagemViewHelper.SetNotFound("Solicitação de reboque não encontrada!");
        }

        var clienteDeposito = await _context.ClienteDeposito
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ClienteDepositoId == solicitacao.ClienteDepositoId, cancellationToken: ct);

        if (clienteDeposito != null)
        {
            var cliente = await _context.UsuarioCliente
                .AsNoTracking()
                .AnyAsync(x => x.UsuarioId == userId && x.ClienteId == clienteDeposito.ClienteId,
                    cancellationToken: ct);

            var deposito = await _context.UsuarioDeposito
                .AsNoTracking()
                .AnyAsync(x => x.UsuarioId == userId && x.DepositoId == clienteDeposito.DepositoId,
                    cancellationToken: ct);

            if (!cliente || !deposito)
            {
                return MensagemViewHelper.SetBadRequest("Usuário sem permissão para cancelar esta solicitação.");
            }
        }

        solicitacao.SolicitacaoReboqueStatusId = 6;
        solicitacao.UsuarioAlteracaoId = userId;
        solicitacao.DataAlteracao = DateTime.UtcNow.AddHours(-3);

        await using var transaction = await _context.Database.BeginTransactionAsync(ct);
        try
        {
            await _context.SaveChangesAsync(ct);
            await transaction.CommitAsync(ct);

            return MensagemViewHelper.SetUpdateSuccess("Solicitação de reboque cancelada com sucesso!");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(ct);
            return MensagemViewHelper.SetInternalServerError(ex);
        }
    }

    public async Task<MensagemDTO> AceitarSolicitacaoReboqueAsync(
        AceitarSolicitacaoReboqueParameters parameters, CancellationToken ct)
    {
        if (parameters.SolicitacaoReboqueId <= 0)
        {
            return MensagemViewHelper.SetBadRequest("Identificador da Solicitação de Reboque inválido!");
        }

        var solicitacao = await _context.SolicitacaoReboque
            .FirstOrDefaultAsync(x => x.Id == parameters.SolicitacaoReboqueId, cancellationToken: ct);

        if (solicitacao == null)
        {
            return MensagemViewHelper.SetNotFound("Solicitação de reboque não encontrada!");
        }

        List<string> erros = new();

        var clienteDeposito = await _context.ClienteDeposito
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ClienteDepositoId == solicitacao.ClienteDepositoId, cancellationToken: ct);

        if (clienteDeposito != null)
        {
            var cliente = await _context.UsuarioCliente
                .AsNoTracking()
                .AnyAsync(
                    x => x.UsuarioId == parameters.IdentificadorUsuario && x.ClienteId == clienteDeposito.ClienteId,
                    cancellationToken: ct);

            var deposito = await _context.UsuarioDeposito
                .AsNoTracking()
                .AnyAsync(
                    x => x.UsuarioId == parameters.IdentificadorUsuario && x.DepositoId == clienteDeposito.DepositoId,
                    cancellationToken: ct);

            if (!cliente)
            {
                erros.Add(MensagemPadraoEnum.UsuarioSemPermissaoAcessoCliente);
            }

            if (!deposito)
            {
                erros.Add(MensagemPadraoEnum.UsuarioSemPermissaoAcessoDeposito);
            }
        }

        if (parameters.ReboqueId <= 0)
        {
            erros.Add(MensagemPadraoEnum.IdentificadorReboqueInvalido);
        }
        else
        {
            var reboqueExiste = await _context.Reboque
                .AsNoTracking()
                .AnyAsync(x => x.ReboqueId == parameters.ReboqueId, cancellationToken: ct);

            if (!reboqueExiste)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorReboqueInvalido);
            }
        }

        if (parameters.ReboquistaId <= 0)
        {
            erros.Add(MensagemPadraoEnum.IdentificadorReboquistaInvalido);
        }
        else
        {
            var reboquistaExiste = await _context.Reboquista
                .AsNoTracking()
                .AnyAsync(x => x.ReboquistaId == parameters.ReboquistaId, cancellationToken: ct);

            if (!reboquistaExiste)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorReboquistaInvalido);
            }
        }

        if (erros.Count > 0)
        {
            return MensagemViewHelper.SetBadRequest(erros);
        }

        solicitacao.ReboqueId = parameters.ReboqueId;
        solicitacao.ReboquistaId = parameters.ReboquistaId;
        solicitacao.SolicitacaoReboqueStatusId = 2;
        solicitacao.UsuarioAlteracaoId = parameters.IdentificadorUsuario;
        solicitacao.DataAlteracao = DateTime.UtcNow.AddHours(-3);

        await using var transaction = await _context.Database.BeginTransactionAsync(ct);
        try
        {
            await _context.SaveChangesAsync(ct);
            await transaction.CommitAsync(ct);

            return MensagemViewHelper.SetUpdateSuccess("Solicitação de reboque aceita com sucesso!");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(ct);
            return MensagemViewHelper.SetInternalServerError(ex);
        }
    }

    public async Task<MensagemDTO> UpdateSolicitacaoReboqueAsync(
        int solicitacaoReboqueId, AtualizarSolicitacaoReboqueParameters parameters, CancellationToken ct)
    {
        if (solicitacaoReboqueId <= 0)
        {
            return MensagemViewHelper.SetBadRequest("Identificador da Solicitação de Reboque inválido!");
        }

        var solicitacao = await _context.SolicitacaoReboque
            .FirstOrDefaultAsync(x => x.Id == solicitacaoReboqueId, cancellationToken: ct);

        if (solicitacao == null)
        {
            return MensagemViewHelper.SetNotFound("Solicitação de reboque não encontrada!");
        }

        List<string> erros = new();

        var statusExiste = await _context.SolicitacaoReboqueStatus
            .AsNoTracking()
            .AnyAsync(x => x.Id == parameters.SolicitacaoReboqueStatusId, cancellationToken: ct);

        if (!statusExiste)
        {
            erros.Add("Status de Solicitação de Reboque inválido");
        }
        else
        {
            solicitacao.SolicitacaoReboqueStatusId = parameters.SolicitacaoReboqueStatusId;
        }

        if (parameters.IdentificadorReboque.HasValue)
        {
            if (parameters.IdentificadorReboque.Value > 0)
            {
                var reboqueExiste = await _context.Reboque
                    .AsNoTracking()
                    .AnyAsync(x => x.ReboqueId == parameters.IdentificadorReboque.Value, cancellationToken: ct);

                if (!reboqueExiste)
                {
                    erros.Add(MensagemPadraoEnum.IdentificadorReboqueInvalido);
                }
                else
                {
                    solicitacao.ReboqueId = parameters.IdentificadorReboque.Value;
                }
            }
            else
            {
                solicitacao.ReboqueId = null;
            }
        }

        if (parameters.IdentificadorReboquista.HasValue)
        {
            if (parameters.IdentificadorReboquista.Value > 0)
            {
                var reboquistaExiste = await _context.Reboquista
                    .AsNoTracking()
                    .AnyAsync(x => x.ReboquistaId == parameters.IdentificadorReboquista.Value, cancellationToken: ct);

                if (!reboquistaExiste)
                {
                    erros.Add(MensagemPadraoEnum.IdentificadorReboquistaInvalido);
                }
                else
                {
                    solicitacao.ReboquistaId = parameters.IdentificadorReboquista.Value;
                }
            }
            else
            {
                solicitacao.ReboquistaId = null;
            }
        }

        if (erros.Count > 0)
        {
            return MensagemViewHelper.SetBadRequest(erros);
        }

        solicitacao.UsuarioAlteracaoId = parameters.IdentificadorUsuario;
        solicitacao.DataAlteracao = DateTime.Now;

        await using var transaction = await _context.Database.BeginTransactionAsync(ct);
        try
        {
            await _context.SaveChangesAsync(ct);
            await transaction.CommitAsync(ct);

            return MensagemViewHelper.SetUpdateSuccess("Solicitação de reboque atualizada com sucesso!");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(ct);
            return MensagemViewHelper.SetInternalServerError("Erro ao atualizar a solicitação de reboque.");
        }
    }

    private async Task SolicitacaoStatusNoLocal()
    {
    }
}