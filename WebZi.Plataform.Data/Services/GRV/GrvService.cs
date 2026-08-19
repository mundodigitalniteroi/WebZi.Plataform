using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using WebZi.Plataform.CrossCutting.Contacts;
using WebZi.Plataform.CrossCutting.Documents;
using WebZi.Plataform.CrossCutting.Linq;
using WebZi.Plataform.CrossCutting.Localizacao;
using WebZi.Plataform.CrossCutting.Number;
using WebZi.Plataform.CrossCutting.Strings;
using WebZi.Plataform.CrossCutting.Veiculo;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Cliente;
using WebZi.Plataform.Data.Services.Deposito;
using WebZi.Plataform.Data.Services.DRFA;
using WebZi.Plataform.Data.Services.Faturamento;
using WebZi.Plataform.Data.Services.Localizacao;
using WebZi.Plataform.Data.Services.Sistema;
using WebZi.Plataform.Data.Services.Usuario;
using WebZi.Plataform.Data.Services.WebServices;
using WebZi.Plataform.Domain.DTO.Deposito;
using WebZi.Plataform.Domain.DTO.Generic;
using WebZi.Plataform.Domain.DTO.GRV;
using WebZi.Plataform.Domain.DTO.GRV.Cadastro;
using WebZi.Plataform.Domain.DTO.GRV.Pesquisa;
using WebZi.Plataform.Domain.DTO.Localizacao;
using WebZi.Plataform.Domain.DTO.Sistema;
using WebZi.Plataform.Domain.DTO.Usuario;
using WebZi.Plataform.Domain.Enums;
using WebZi.Plataform.Domain.Models.Bucket;
using WebZi.Plataform.Domain.Models.Bucket.Work;
using WebZi.Plataform.Domain.Models.Cliente;
using WebZi.Plataform.Domain.Models.ClienteDeposito;
using WebZi.Plataform.Domain.Models.Condutor;
using WebZi.Plataform.Domain.Models.Deposito;
using WebZi.Plataform.Domain.Models.Documento;
using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Models.Servico;
using WebZi.Plataform.Domain.Models.Sistema;
using WebZi.Plataform.Domain.Models.Usuario;
using WebZi.Plataform.Domain.Models.Veiculo;
using WebZi.Plataform.Domain.Models.WebServices.Boleto;
using WebZi.Plataform.Domain.ViewModel.GGV;
using WebZi.Plataform.Domain.ViewModel.GRV.Cadastro;
using WebZi.Plataform.Domain.ViewModel.GRV.Pesquisa;
using WebZi.Plataform.Domain.Views.Usuario;
using Z.EntityFramework.Plus;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebZi.Plataform.Domain.Services.GRV
{
    public class GrvService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IServiceProvider _provider;
        private readonly IHttpClientFactory _httpClientFactory;

        public GrvService(AppDbContext context)
        {
            _context = context;
        }

        public GrvService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public GrvService(AppDbContext context, IMapper mapper, IServiceProvider provider,
            IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _mapper = mapper;
            _provider = provider;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<MensagemDTO> CreateAssinaturaAgenteAsync(int GrvId, int UsuarioId, byte[] Imagem)
        {
            MensagemDTO ResultView = ValidateInputGrv(GrvId, UsuarioId);

            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            if (Imagem == null)
            {
                return MensagemViewHelper.SetBadRequest("Nenhuma imagem enviada para a API");
            }

            GrvModel Grv = await GetByIdAsync(GrvId);

            if (Grv.StatusOperacao.StatusOperacaoId != "G")
            {
                return MensagemViewHelper.SetBadRequest(
                    $"O Status atual deste Processo não permite o envio da Imagem da Assinatura do Agente. Status atual: {Grv.StatusOperacao.Descricao}");
            }

            BucketArquivoModel BucketArquivo = await _context.BucketArquivo
                .Include(x => x.BucketNomeTabelaOrigem)
                .AsNoTracking()
                .FirstOrDefaultAsync(x =>
                    x.BucketNomeTabelaOrigem.Codigo == BucketNomeTabelaOrigemEnum.AssinaturaAgente &&
                    x.TabelaOrigemId == GrvId);

            if (BucketArquivo != null)
            {
                return MensagemViewHelper.SetBadRequest("Já existe uma Imagem da Assinatura do Agente cadastrada");
            }

            new BucketService(_context, _httpClientFactory)
                .SendFile(BucketNomeTabelaOrigemEnum.AssinaturaAgente, GrvId, UsuarioId, Imagem);

            return MensagemViewHelper.SetCreateSuccess();
        }

        public async Task<MensagemDTO> CreateAssinaturaCondutorAsync(int GrvId, int UsuarioId, byte[] Imagem)
        {
            MensagemDTO ResultView = ValidateInputGrv(GrvId, UsuarioId);

            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            if (Imagem == null)
            {
                return MensagemViewHelper.SetBadRequest("Nenhuma imagem enviada para a API");
            }

            GrvModel Grv = await GetByIdAsync(GrvId);

            if (Grv.StatusOperacao.StatusOperacaoId != "G")
            {
                return MensagemViewHelper.SetBadRequest(
                    $"O Status atual deste Processo não permite o envio da Imagem da Assinatura do Condutor. Status atual: {Grv.StatusOperacao.Descricao}");
            }

            BucketArquivoModel BucketArquivo = await _context.BucketArquivo
                .Include(x => x.BucketNomeTabelaOrigem)
                .AsNoTracking()
                .FirstOrDefaultAsync(x =>
                    x.BucketNomeTabelaOrigem.Codigo == BucketNomeTabelaOrigemEnum.AssinaturaCondutor &&
                    x.TabelaOrigemId == GrvId);

            if (BucketArquivo != null)
            {
                return MensagemViewHelper.SetBadRequest("Já existe uma Imagem da Assinatura do Condutor cadastrada");
            }

            new BucketService(_context, _httpClientFactory)
                .SendFile(BucketNomeTabelaOrigemEnum.AssinaturaCondutor, GrvId, UsuarioId, Imagem);

            return MensagemViewHelper.SetCreateSuccess();
        }

        public async Task<MensagemDTO> UpdateGrv(GrvAtualizarParameters GrvPersistencia, CancellationToken ct)
        {
            MensagemDTO ResultView = new();

            #region Consulta

            var grv = await _context.Grv
                .Include(x => x.UsuarioClienteDepositoGrv)
                .Include(x => x.Condutor)
                .Include(x => x.ListagemEnquadramentoInfracao)
                .Include(x => x.ListagemLacre)
                .Include(x => x.ListagemCondutorEquipamentoOpcional)
                .Include(x => x.ListagemFaturamentoServicoGrv)
                .AsTracking()
                .FirstOrDefaultAsync(x => x.GrvId == GrvPersistencia.IdentificadorGrv, cancellationToken: ct);

            var possuiPermissaoEdicao = await _context.PerfilAcessoUsuario
                .AsNoTracking()
                .AnyAsync(x => x.UsuarioId == GrvPersistencia.IdentificadorUsuario
                               && (x.PerfilAcessoId == (int)PerfisDeAcessoEnum.GrvEditProd || x.PerfilAcessoId == (int)PerfisDeAcessoEnum.GrvEditHomolog)
                               && _context.SistemaPerfilAcessoSubModulos
                                   .Any(s => (s.IdPerfilAcesso == (int)PerfisDeAcessoEnum.GrvEditProd || s.IdPerfilAcesso == (int)PerfisDeAcessoEnum.GrvEditHomolog)
                                             && (s.IdSubModulo == (int)SubModuloEnum.EditarGrvProd || s.IdSubModulo == (int)SubModuloEnum.EditarGrvHomolog)), cancellationToken: ct);
            
            if (!possuiPermissaoEdicao)
                return MensagemViewHelper.SetBadRequest(
                    "O usuário não possui permissão para edição do GRV.");

            #endregion Consulta

            if (grv == null)
            {
                ResultView = MensagemViewHelper.SetBadRequest("Grv não existe");
                return ResultView;
            }

            grv.ClienteId = GrvPersistencia.IdentificadorCliente;
            grv.DepositoId = GrvPersistencia.IdentificadorDeposito;
            grv.TipoVeiculoId = GrvPersistencia.IdentificadorTipoVeiculo;
            grv.ReboquistaId = GrvPersistencia.IdentificadorReboquista;
            grv.ReboqueId = GrvPersistencia.IdentificadorReboque;
            grv.AutoridadeResponsavelId = GrvPersistencia.IdentificadorAutoridadeResponsavel;
            grv.CorId = GrvPersistencia.IdentificadorCor;
            grv.MarcaModeloId = GrvPersistencia.IdentificadorMarcaModelo;
            grv.MotivoApreensaoId = GrvPersistencia.IdentificadorMotivoApreensao;
            grv.NumeroFormularioGrv = GrvPersistencia.NumeroProcesso;
            grv.FaturamentoProdutoId = GrvPersistencia.IdentificadorMotivoApreensao == 4
                ? "DRF"
                : GrvPersistencia.CodigoProduto;
            grv.MatriculaAutoridadeResponsavel =
                GrvPersistencia.MatriculaAutoridadeResponsavel.ToUpperTrim().ToNullIfEmpty();
            grv.NomeAutoridadeResponsavel = GrvPersistencia.NomeAutoridadeResponsavel.ToUpperTrim().ToNullIfEmpty();
            grv.Placa = GrvPersistencia.Placa.ToUpperTrim().ToNullIfEmpty();
            grv.Chassi = GrvPersistencia.Chassi.ToUpperTrim().ToNullIfEmpty();
            grv.Renavam = GrvPersistencia.Renavam.ToUpperTrim().ToNullIfEmpty();
            grv.Rfid = GrvPersistencia.Rfid.ToUpperTrim().ToNullIfEmpty();
            grv.EnderecoLocalizacaoVeiculoLogradouro =
                GrvPersistencia.EnderecoLocalizacaoVeiculoLogradouro.ToUpperTrim().ToNullIfEmpty();
            grv.EnderecoLocalizacaoVeiculoNumero =
                GrvPersistencia.EnderecoLocalizacaoVeiculoNumero.ToUpperTrim().ToNullIfEmpty();
            grv.EnderecoLocalizacaoVeiculoComplemento =
                GrvPersistencia.EnderecoLocalizacaoVeiculoComplemento.ToUpperTrim().ToNullIfEmpty();
            grv.EnderecoLocalizacaoVeiculoBairro =
                GrvPersistencia.EnderecoLocalizacaoVeiculoBairro.ToUpperTrim().ToNullIfEmpty();
            grv.EnderecoLocalizacaoVeiculoMunicipio =
                GrvPersistencia.EnderecoLocalizacaoVeiculoMunicipio.ToUpperTrim().ToNullIfEmpty();
            grv.EnderecoLocalizacaoVeiculoUF =
                GrvPersistencia.EnderecoLocalizacaoVeiculoUF.ToUpperTrim().ToNullIfEmpty();
            grv.EnderecoLocalizacaoVeiculoReferencia =
                GrvPersistencia.EnderecoLocalizacaoVeiculoReferencia.ToUpperTrim().ToNullIfEmpty();
            grv.EnderecoLocalizacaoVeiculoPontoReferencia = GrvPersistencia.EnderecoLocalizacaoVeiculoPontoReferencia
                .ToUpperTrim().ToNullIfEmpty();
            grv.NumeroChave = GrvPersistencia.NumeroChave.ToUpperTrim().ToNullIfEmpty();
            grv.EstacionamentoSetor = GrvPersistencia.EstacionamentoSetor.ToUpperTrim().ToNullIfEmpty();
            grv.EstacionamentoNumeroVaga = GrvPersistencia.EstacionamentoNumeroVaga.ToUpperTrim().ToNullIfEmpty();
            grv.Latitude = GrvPersistencia.Latitude.ToUpperTrim().ToNullIfEmpty();
            grv.Longitude = GrvPersistencia.Longitude.ToUpperTrim().ToNullIfEmpty();
            grv.VeiculoUF = GrvPersistencia.VeiculoUF.ToUpperTrim().ToNullIfEmpty();
            grv.DataHoraRemocao = GrvPersistencia.DataHoraRemocao;
            grv.LatitudeAcautelamento = GrvPersistencia.LatitudeAcautelamento.ToUpperTrim().ToNullIfEmpty();
            grv.LongitudeAcautelamento = GrvPersistencia.LongitudeAcautelamento.ToUpperTrim().ToNullIfEmpty();
            grv.FlagComboio = GrvPersistencia.FlagVeiculoNaoUsouReboque;
            grv.FlagVeiculoNaoIdentificado = GrvPersistencia.FlagVeiculoNaoIdentificado;
            grv.FlagVeiculoSemRegistro = GrvPersistencia.FlagVeiculoSemRegistro;
            grv.FlagVeiculoRoubadoFurtado = GrvPersistencia.FlagVeiculoRoubadoFurtado;
            grv.FlagEstadoLacre = GrvPersistencia.FlagEstadoLacre;
            grv.FlagVeiculoNaoOstentaPlaca = GrvPersistencia.FlagVeiculoNaoOstentaPlaca;

            grv.UsuarioAlteracaoId = GrvPersistencia.IdentificadorUsuario;
            grv.DataAlteracao = DateTime.UtcNow;

            grv.Condutor = _mapper.Map<CondutorModel>(GrvPersistencia.Condutor);
            grv.Condutor.Email = grv.Condutor.Email
                .ToLowerTrim()
                .ToNullIfEmpty();

            TabelaGenericaModel AssinaturaCondutor = new TabelaGenericaService(_context)
                .GetById(GrvPersistencia.Condutor.IdentificadorAssinaturaCondutor);

            grv.Condutor.StatusAssinaturaCondutor = AssinaturaCondutor.ValorCadastro;

            if (!string.IsNullOrWhiteSpace(GrvPersistencia.EnderecoLocalizacaoVeiculoCEP))
            {
                EnderecoDTO Endereco = new EnderecoService(_context, _mapper)
                    .GetByCEP(GrvPersistencia.EnderecoLocalizacaoVeiculoCEP
                        .Replace("-", ""));

                if (Endereco.Mensagem.Erros.Count < 0 && Endereco != null)
                {
                    grv.EnderecoLocalizacaoVeiculoCEPId = Endereco.IdentificadorCEP;

                    grv.EnderecoLocalizacaoVeiculoLogradouro = Endereco.Logradouro;

                    grv.EnderecoLocalizacaoVeiculoBairro = Endereco.Bairro;

                    grv.EnderecoLocalizacaoVeiculoMunicipio = Endereco.MunicipioPtbr;

                    grv.EnderecoLocalizacaoVeiculoUF = Endereco.UF;
                }
            }

            if (GrvPersistencia.ListagemEnquadramentoInfracao?.Count > 0)
            {
                var identificadoresEntrada = GrvPersistencia.ListagemEnquadramentoInfracao
                    .Where(x => x.IdentificadorEnquadramentoGrv.HasValue)
                    .Select(x => x.IdentificadorEnquadramentoGrv.Value)
                    .ToList();

                var infracoesParaRemover = grv.ListagemEnquadramentoInfracao
                    .Where(e => !identificadoresEntrada.Contains(e.GrvEnquadramentoInfracaoId) &&
                                e.GrvEnquadramentoInfracaoId > 0)
                    .ToList();

                foreach (var infracao in infracoesParaRemover)
                {
                    grv.ListagemEnquadramentoInfracao.Remove(infracao);
                }

                foreach (var input in GrvPersistencia.ListagemEnquadramentoInfracao)
                {
                    string numeroInfracao = input.NumeroInfracao.ToUpperTrim().ToNullIfEmpty();

                    if (input.IdentificadorEnquadramentoGrv.HasValue)
                    {
                        EnquadramentoInfracaoGrvModel infracaoExistente = grv.ListagemEnquadramentoInfracao
                            .FirstOrDefault(e =>
                                e.GrvEnquadramentoInfracaoId == input.IdentificadorEnquadramentoGrv.Value);

                        if (infracaoExistente != null)
                        {
                            infracaoExistente.EnquadramentoInfracaoId = input.IdentificadorEnquadramentoInfracao;

                            infracaoExistente.NumeroInfracao = numeroInfracao;
                        }
                    }
                    else
                    {
                        if (!grv.ListagemEnquadramentoInfracao.Any(e =>
                                e.EnquadramentoInfracaoId == input.IdentificadorEnquadramentoInfracao &&
                                e.NumeroInfracao == numeroInfracao))
                        {
                            grv.ListagemEnquadramentoInfracao.Add(new EnquadramentoInfracaoGrvModel
                            {
                                GrvId = grv.GrvId,

                                EnquadramentoInfracaoId = input.IdentificadorEnquadramentoInfracao,

                                NumeroInfracao = numeroInfracao
                            });
                        }
                    }
                }
            }

            if (GrvPersistencia.ListagemLacre?.Count > 0)
            {
                // var identificadoresEntrada = GrvPersistencia.ListagemLacre
                //     .Where(x => x.IdentificadorLacre.HasValue)
                //     .Select(x => x.IdentificadorLacre.Value)
                //     .ToList();

                // var lacresParaRemover = grv.ListagemLacre
                //     .Where(l => !identificadoresEntrada.Contains(l.LacreId) && l.LacreId > 0)
                //     .ToList();
                //
                // foreach (var lacre in lacresParaRemover)
                // {
                //     grv.ListagemLacre.Remove(lacre);
                // }

                foreach (var item in GrvPersistencia.ListagemLacre)
                {
                    string lacreValue = item.Lacre.ToUpperTrim().ToNullIfEmpty();

                    if (string.IsNullOrWhiteSpace(lacreValue))
                    {
                        continue;
                    }

                    if (item.IdentificadorLacre.HasValue)
                    {
                        LacreModel lacreExistente = grv.ListagemLacre
                            .FirstOrDefault(l => l.LacreId == item.IdentificadorLacre.Value);

                        if (lacreExistente != null)
                        {
                            if (lacreExistente.Lacre != lacreValue)
                            {
                                lacreExistente.LacreAnterior = lacreExistente.Lacre;

                                lacreExistente.Lacre = lacreValue;

                                lacreExistente.UsuarioAlteracaoId = GrvPersistencia.IdentificadorUsuario;

                                lacreExistente.DataAlteracao = DateTime.Now;
                            }
                        }
                    }
                    else
                    {
                        if (grv.ListagemLacre.All(l => l.Lacre != lacreValue))
                        {
                            grv.ListagemLacre.Add(new LacreModel
                            {
                                GrvId = grv.GrvId,

                                UsuarioCadastroId = GrvPersistencia.IdentificadorUsuario,

                                Lacre = lacreValue,

                                DataCadastro = DateTime.Now
                            });
                        }
                    }
                }
            }

            if (GrvPersistencia.ListagemEquipamentoOpcional?.Count > 0)
            {
                // Remover equipamentos que não estão na entrada
                var equipamentosRemover = grv.ListagemCondutorEquipamentoOpcional
                    .Where(e => GrvPersistencia.ListagemEquipamentoOpcional.All(i =>
                        i.IdentificadorEquipamentoOpcional != e.EquipamentoOpcionalId))
                    .ToList();

                foreach (var remover in equipamentosRemover)
                {
                    grv.ListagemCondutorEquipamentoOpcional.Remove(remover);
                }

                foreach (var item in GrvPersistencia.ListagemEquipamentoOpcional)
                {
                    var equipamentoExistente = grv.ListagemCondutorEquipamentoOpcional
                        .FirstOrDefault(e => e.EquipamentoOpcionalId == item.IdentificadorEquipamentoOpcional);

                    if (equipamentoExistente != null)
                    {
                        equipamentoExistente.FlagPossuiEquipamento = item.FlagPossuiEquipamento;
                        equipamentoExistente.UsuarioAlteracaoId = GrvPersistencia.IdentificadorUsuario;
                        equipamentoExistente.DataAtualizacao = DateTime.Now;

                        if (item.FlagPossuiEquipamento == "S")
                        {
                            equipamentoExistente.FlagEquipamentoAvariado = item.FlagEquipamentoAvariado;
                            equipamentoExistente.CodigoAvaria = item.IdentificadorTipoAvaria;
                        }
                        else
                        {
                            equipamentoExistente.FlagEquipamentoAvariado = "N";
                            equipamentoExistente.CodigoAvaria = null;
                        }
                    }
                    else
                    {
                        var novoEquipamento = new CondutorEquipamentoOpcionalModel
                        {
                            GrvId = grv.GrvId,
                            EquipamentoOpcionalId = item.IdentificadorEquipamentoOpcional,
                            FlagPossuiEquipamento = item.FlagPossuiEquipamento,
                            UsuarioCadastroId = GrvPersistencia.IdentificadorUsuario,
                            DataCadastro = DateTime.Now
                        };

                        if (item.FlagPossuiEquipamento == "S")
                        {
                            novoEquipamento.FlagEquipamentoAvariado = item.FlagEquipamentoAvariado;
                            novoEquipamento.CodigoAvaria = item.IdentificadorTipoAvaria;
                        }

                        grv.ListagemCondutorEquipamentoOpcional.Add(novoEquipamento);
                    }
                }
            }

            ClienteDepositoModel ClienteDeposito = _context.ClienteDeposito
                .Include(x => x.Cliente)
                .AsNoTracking()
                .FirstOrDefault(x => x.ClienteId == GrvPersistencia.IdentificadorCliente
                                     && x.DepositoId == GrvPersistencia.IdentificadorDeposito);

            if (ClienteDeposito.FlagCadastrarGrvComStatusOperacaoBloqueado == "S")
            {
                grv.StatusOperacaoId = "B";
            }


            using (IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync(ct))
            {
                try
                {
                    _context.SetUserContextInfo(GrvPersistencia.IdentificadorUsuario);

                    if (GrvPersistencia.IdentificadorMotivoApreensao == 4)
                    {
                        var result = await _provider
                            .GetService<DRFAService>()
                            .UpdateDRFAGrv(GrvPersistencia);
                        if (result.Erros?.Count > 0)
                        {
                            await transaction.RollbackAsync(ct);
                            ResultView = result;
                            return ResultView;
                        }
                    }

                    if (ClienteDeposito.Cliente.FlagClientePossuiCodigoIdentificacao == "S")
                    {
                        var clienteCodigoIdentificacao = await _context.ClienteCodigoIdentificacao
                            .FirstOrDefaultAsync(x => x.GrvId == grv.GrvId, cancellationToken: ct);

                        if (clienteCodigoIdentificacao == null)
                        {
                            clienteCodigoIdentificacao = new ClienteCodigoIdentificacaoModel
                            {
                                GrvId = grv.GrvId,
                                UsuarioCadastroId = GrvPersistencia.IdentificadorUsuario,
                                CodigoIdentificacao = GrvPersistencia.CodigoIdentificacaoCliente,
                                DataCadastro = DateTime.Now
                            };
                            _context.ClienteCodigoIdentificacao.Add(clienteCodigoIdentificacao);
                        }
                        else
                        {
                            clienteCodigoIdentificacao.CodigoIdentificacao = GrvPersistencia.CodigoIdentificacaoCliente;
                            clienteCodigoIdentificacao.UsuarioAlteracaoId = GrvPersistencia.IdentificadorUsuario;
                            clienteCodigoIdentificacao.DataAlteracao = DateTime.Now;
                            _context.ClienteCodigoIdentificacao.Update(clienteCodigoIdentificacao);
                        }
                    }

                    _context.Grv.Update(grv);

                    await _context.SaveChangesAsync(ct);
                    await transaction.CommitAsync(ct);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(ct);

                    ResultView = MensagemViewHelper.SetInternalServerError(ex);

                    return ResultView;
                }
            }

            if (GrvPersistencia.ListagemDocumentoCondutor?.Count > 0)
            {
                CreateDocumentosCondutor(grv.GrvId, grv.UsuarioCadastroId, GrvPersistencia.ListagemDocumentoCondutor);
            }

            if (GrvPersistencia.ListagemFoto?.Count > 0)
            {
                new BucketService(_context, _httpClientFactory)
                    .SendFiles(BucketNomeTabelaOrigemEnum.FotoVeiculoGRV, grv.GrvId, grv.UsuarioCadastroId,
                        GrvPersistencia.ListagemFoto);
            }

            if (GrvPersistencia.DRFA?.ArquivoDoRegistroDoRouboFurto is not null)
            {
                new BucketService(_context, _httpClientFactory)
                    .SendFile(BucketNomeTabelaOrigemEnum.DRFAArquivoDeRouboFurto, grv.GrvId, grv.UsuarioCadastroId,
                        GrvPersistencia.DRFA?.ArquivoDoRegistroDoRouboFurto);
            }

            if (GrvPersistencia.DRFA?.RegistroRecuperacao?.ArquivoDeRecuperacao is not null)
            {
                new BucketService(_context, _httpClientFactory)
                    .SendFile(BucketNomeTabelaOrigemEnum.DRFAArquivoRegistroRecuperacao, grv.GrvId,
                        grv.UsuarioCadastroId, GrvPersistencia.DRFA.RegistroRecuperacao.ArquivoDeRecuperacao);
            }

            if (GrvPersistencia.ImagemAssinaturaAgente != null)
            {
                new BucketService(_context, _httpClientFactory)
                    .SendFile(BucketNomeTabelaOrigemEnum.AssinaturaAgente, grv.GrvId, grv.UsuarioCadastroId,
                        GrvPersistencia.ImagemAssinaturaAgente);
            }

            if (GrvPersistencia.ImagemAssinaturaCondutor != null)
            {
                new BucketService(_context, _httpClientFactory)
                    .SendFile(BucketNomeTabelaOrigemEnum.AssinaturaCondutor, grv.GrvId, grv.UsuarioCadastroId,
                        GrvPersistencia.ImagemAssinaturaCondutor);
            }

            ResultView = MensagemViewHelper.SetUpdateSuccess();
            return ResultView;
        }

        public async Task<MensagemDTO> UpdateStatusToUAsync(int grvId, int usuarioId, CancellationToken ct)
        {
            MensagemDTO ResultView = ValidateInputGrv(grvId, usuarioId);

            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            GrvModel grv = await _context.Grv
                .Include(x => x.StatusOperacao)
                .AsTracking()
                .FirstOrDefaultAsync(x => x.GrvId == grvId, cancellationToken: ct);

            if (grv == null)
            {
                return MensagemViewHelper.SetNotFound(MensagemPadraoEnum.NaoEncontradoGrv);
            }

            if (grv.StatusOperacaoId == "U")
            {
                return MensagemViewHelper.SetBadRequest("O GRV já está com o status U.");
            }

            using (IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync(ct))
            {
                try
                {
                    _context.SetUserContextInfo(usuarioId);

                    grv.StatusOperacaoId = "U";
                    grv.UsuarioAlteracaoId = usuarioId;
                    grv.DataAlteracao = DateTime.UtcNow;

                    _context.Grv.Update(grv);

                    await _context.SaveChangesAsync(ct);
                    await transaction.CommitAsync(ct);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(ct);

                    return MensagemViewHelper.SetInternalServerError(ex);
                }
            }

            return MensagemViewHelper.SetUpdateSuccess();
        }

        public async Task<ResultadoCadastroGrvDTO> CreateGrv(GrvParameters GrvPersistencia, CancellationToken ct)
        {
            GrvModel grv = new()
            {
                ClienteId = GrvPersistencia.IdentificadorCliente,

                DepositoId = GrvPersistencia.IdentificadorDeposito,

                TipoVeiculoId = GrvPersistencia.IdentificadorTipoVeiculo,

                ReboquistaId = GrvPersistencia.IdentificadorReboquista,

                ReboqueId = GrvPersistencia.IdentificadorReboque,

                AutoridadeResponsavelId = GrvPersistencia.IdentificadorAutoridadeResponsavel,

                CorId = GrvPersistencia.IdentificadorCor,

                MarcaModeloId = GrvPersistencia.IdentificadorMarcaModelo,

                MotivoApreensaoId = GrvPersistencia.IdentificadorMotivoApreensao,

                UsuarioCadastroId = GrvPersistencia.IdentificadorUsuario,

                NumeroFormularioGrv = GrvPersistencia.NumeroProcesso,

                FaturamentoProdutoId = GrvPersistencia.CodigoProduto,

                MatriculaAutoridadeResponsavel =
                    GrvPersistencia.MatriculaAutoridadeResponsavel.ToUpperTrim().ToNullIfEmpty(),

                NomeAutoridadeResponsavel = GrvPersistencia.NomeAutoridadeResponsavel.ToUpperTrim().ToNullIfEmpty(),

                Placa = GrvPersistencia.Placa.ToUpperTrim().ToNullIfEmpty(),

                Chassi = GrvPersistencia.Chassi.ToUpperTrim().ToNullIfEmpty(),

                Renavam = GrvPersistencia.Renavam.ToUpperTrim().ToNullIfEmpty(),

                Rfid = GrvPersistencia.Rfid.ToUpperTrim().ToNullIfEmpty(),

                EnderecoLocalizacaoVeiculoLogradouro =
                    GrvPersistencia.EnderecoLocalizacaoVeiculoLogradouro.ToUpperTrim().ToNullIfEmpty(),

                EnderecoLocalizacaoVeiculoNumero =
                    GrvPersistencia.EnderecoLocalizacaoVeiculoNumero.ToUpperTrim().ToNullIfEmpty(),

                EnderecoLocalizacaoVeiculoComplemento = GrvPersistencia.EnderecoLocalizacaoVeiculoComplemento
                    .ToUpperTrim().ToNullIfEmpty(),

                EnderecoLocalizacaoVeiculoBairro =
                    GrvPersistencia.EnderecoLocalizacaoVeiculoBairro.ToUpperTrim().ToNullIfEmpty(),

                EnderecoLocalizacaoVeiculoMunicipio =
                    GrvPersistencia.EnderecoLocalizacaoVeiculoMunicipio.ToUpperTrim().ToNullIfEmpty(),

                EnderecoLocalizacaoVeiculoUF =
                    GrvPersistencia.EnderecoLocalizacaoVeiculoUF.ToUpperTrim().ToNullIfEmpty(),

                EnderecoLocalizacaoVeiculoReferencia =
                    GrvPersistencia.EnderecoLocalizacaoVeiculoReferencia.ToUpperTrim().ToNullIfEmpty(),

                EnderecoLocalizacaoVeiculoPontoReferencia = GrvPersistencia.EnderecoLocalizacaoVeiculoPontoReferencia
                    .ToUpperTrim().ToNullIfEmpty(),

                NumeroChave = GrvPersistencia.NumeroChave.ToUpperTrim().ToNullIfEmpty(),

                EstacionamentoSetor = GrvPersistencia.EstacionamentoSetor.ToUpperTrim().ToNullIfEmpty(),

                EstacionamentoNumeroVaga = GrvPersistencia.EstacionamentoNumeroVaga.ToUpperTrim().ToNullIfEmpty(),

                Latitude = GrvPersistencia.Latitude.ToUpperTrim().ToNullIfEmpty(),

                Longitude = GrvPersistencia.Longitude.ToUpperTrim().ToNullIfEmpty(),

                VeiculoUF = GrvPersistencia.VeiculoUF.ToUpperTrim().ToNullIfEmpty(),

                DataHoraRemocao = GrvPersistencia.DataHoraRemocao,

                LatitudeAcautelamento = GrvPersistencia.LatitudeAcautelamento.ToUpperTrim().ToNullIfEmpty(),

                LongitudeAcautelamento = GrvPersistencia.LongitudeAcautelamento.ToUpperTrim().ToNullIfEmpty(),

                FlagComboio = GrvPersistencia.FlagVeiculoNaoUsouReboque,

                FlagVeiculoNaoIdentificado = GrvPersistencia.FlagVeiculoNaoIdentificado,

                FlagVeiculoSemRegistro = GrvPersistencia.FlagVeiculoSemRegistro,

                FlagVeiculoRoubadoFurtado = GrvPersistencia.FlagVeiculoRoubadoFurtado,

                FlagEstadoLacre = GrvPersistencia.FlagEstadoLacre,

                FlagVeiculoNaoOstentaPlaca = GrvPersistencia.FlagVeiculoNaoOstentaPlaca,

                Condutor = _mapper.Map<CondutorModel>(GrvPersistencia.Condutor)
            };

            grv.Condutor.Email = grv.Condutor.Email
                .ToLowerTrim()
                .ToNullIfEmpty();

            TabelaGenericaModel AssinaturaCondutor = await new TabelaGenericaService(_context)
                .GetByIdAsync(GrvPersistencia.Condutor.IdentificadorAssinaturaCondutor);

            grv.Condutor.StatusAssinaturaCondutor = AssinaturaCondutor.ValorCadastro;

            var (numeroFormulario, mensagem) =
                CreateNumeroProcesso(GrvPersistencia.IdentificadorCliente, GrvPersistencia.NumeroProcesso);

            if (!string.IsNullOrWhiteSpace(GrvPersistencia.EnderecoLocalizacaoVeiculoCEP))
            {
                EnderecoDTO Endereco = new EnderecoService(_context, _mapper)
                    .GetByCEP(GrvPersistencia.EnderecoLocalizacaoVeiculoCEP
                        .Replace("-", ""));

                if (Endereco.Mensagem.Erros.Count < 0 && Endereco != null)
                {
                    grv.EnderecoLocalizacaoVeiculoCEPId = Endereco.IdentificadorCEP;

                    grv.EnderecoLocalizacaoVeiculoLogradouro = Endereco.Logradouro;

                    grv.EnderecoLocalizacaoVeiculoBairro = Endereco.Bairro;

                    grv.EnderecoLocalizacaoVeiculoMunicipio = Endereco.MunicipioPtbr;

                    grv.EnderecoLocalizacaoVeiculoUF = Endereco.UF;
                }
            }

            if (GrvPersistencia.ListagemEnquadramentoInfracao?.Count > 0)
            {
                GrvPersistencia.ListagemEnquadramentoInfracao = GrvPersistencia.ListagemEnquadramentoInfracao
                    .OrderBy(x => x.NumeroInfracao)
                    .ToList();

                GrvPersistencia.ListagemEnquadramentoInfracao
                    .ForEach(x => x.NumeroInfracao = x.NumeroInfracao.ToUpperTrim().ToNullIfEmpty());

                grv.ListagemEnquadramentoInfracao = _mapper
                    .Map<List<EnquadramentoInfracaoGrvModel>>(GrvPersistencia.ListagemEnquadramentoInfracao);
            }

            if (GrvPersistencia.ListagemLacre?.Count > 0)
            {
                GrvPersistencia.ListagemLacre = GrvPersistencia.ListagemLacre
                    .ConvertAll(x => x.ToUpperTrim().ToNullIfEmpty())
                    .OrderBy(x => x)
                    .ToList();

                grv.ListagemLacre = new List<LacreModel>();

                foreach (string item in GrvPersistencia.ListagemLacre)
                {
                    grv.ListagemLacre.Add(new LacreModel
                        { UsuarioCadastroId = GrvPersistencia.IdentificadorUsuario, Lacre = item });
                }
            }

            if (GrvPersistencia.ListagemEquipamentoOpcional?.Count > 0 &&
                GrvPersistencia.ListagemEquipamentoOpcional is not null)
            {
                grv.ListagemCondutorEquipamentoOpcional = new List<CondutorEquipamentoOpcionalModel>();

                CondutorEquipamentoOpcionalModel CondutorEquipamentoOpcional = new();

                foreach (EquipamentoOpcionalParameters item in GrvPersistencia.ListagemEquipamentoOpcional)
                {
                    CondutorEquipamentoOpcional = new()
                    {
                        EquipamentoOpcionalId = item.IdentificadorEquipamentoOpcional,

                        FlagPossuiEquipamento = item.FlagPossuiEquipamento,

                        UsuarioCadastroId = GrvPersistencia.IdentificadorUsuario
                    };

                    if (item.FlagPossuiEquipamento == "S")
                    {
                        CondutorEquipamentoOpcional.FlagEquipamentoAvariado = item.FlagEquipamentoAvariado;

                        CondutorEquipamentoOpcional.CodigoAvaria = item.IdentificadorTipoAvaria;
                    }

                    grv.ListagemCondutorEquipamentoOpcional.Add(CondutorEquipamentoOpcional);
                }
            }

            ClienteDepositoModel ClienteDeposito = _context.ClienteDeposito
                .Include(x => x.Cliente)
                .AsNoTracking()
                .FirstOrDefault(x => x.ClienteId == GrvPersistencia.IdentificadorCliente
                                     && x.DepositoId == GrvPersistencia.IdentificadorDeposito);

            if (ClienteDeposito.FlagCadastrarGrvComStatusOperacaoBloqueado == "S")
            {
                grv.StatusOperacaoId = "B";
            }

            ResultadoCadastroGrvDTO ResultView = new();

            using (IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync(ct))
            {
                _context.SetUserContextInfo(GrvPersistencia.IdentificadorUsuario);
                try
                {
                    if (mensagem != null)
                    {
                        await transaction.RollbackAsync(ct);
                        ResultView.Mensagem = mensagem;
                        return ResultView;
                    }

                    grv.NumeroFormularioGrv = numeroFormulario;

                    _context.Grv.Add(grv);

                    await _context.SaveChangesAsync(ct);

                    if (GrvPersistencia.IdentificadorMotivoApreensao == 4)
                    {
                        var result = await _provider
                            .GetService<DRFAService>()
                            .CreateDRFAGrv(grv.GrvId, GrvPersistencia);
                        if (result.Erros?.Count > 0)
                        {
                            await transaction.RollbackAsync(ct);
                            ResultView.Mensagem = result;
                            return ResultView;
                        }
                    }

                    if (ClienteDeposito.Cliente.FlagClientePossuiCodigoIdentificacao == "S")
                    {
                        ClienteCodigoIdentificacaoModel ClienteCodigoIdentificacao = new()
                        {
                            GrvId = grv.GrvId,

                            UsuarioCadastroId = GrvPersistencia.IdentificadorUsuario,

                            CodigoIdentificacao = GrvPersistencia.CodigoIdentificacaoCliente
                        };

                        _context.ClienteCodigoIdentificacao.Add(ClienteCodigoIdentificacao);

                        await _context.SaveChangesAsync(ct);
                    }

                    ResultView.IdentificadorProcesso = grv.GrvId;
                    ResultView.NumeroFormularioProcesso = grv.NumeroFormularioGrv;

                    await transaction.CommitAsync(ct);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();

                    ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                    return ResultView;
                }
            }

            if (GrvPersistencia.ListagemDocumentoCondutor?.Count > 0)
            {
                CreateDocumentosCondutor(grv.GrvId, grv.UsuarioCadastroId, GrvPersistencia.ListagemDocumentoCondutor);
            }

            if (GrvPersistencia.ListagemFoto?.Count > 0)
            {
                new BucketService(_context, _httpClientFactory)
                    .SendFiles(BucketNomeTabelaOrigemEnum.FotoVeiculoGRV, grv.GrvId, grv.UsuarioCadastroId,
                        GrvPersistencia.ListagemFoto);
            }

            if (GrvPersistencia.DRFA?.ArquivoDoRegistroDoRouboFurto is not null)
            {
                new BucketService(_context, _httpClientFactory)
                    .SendFile(BucketNomeTabelaOrigemEnum.DRFAArquivoDeRouboFurto, grv.GrvId, grv.UsuarioCadastroId,
                        GrvPersistencia.DRFA?.ArquivoDoRegistroDoRouboFurto);
            }

            if (GrvPersistencia.DRFA?.RegistroRecuperacao?.ArquivoDeRecuperacao is not null)
            {
                new BucketService(_context, _httpClientFactory)
                    .SendFile(BucketNomeTabelaOrigemEnum.DRFAArquivoRegistroRecuperacao, grv.GrvId,
                        grv.UsuarioCadastroId, GrvPersistencia.DRFA.RegistroRecuperacao.ArquivoDeRecuperacao);
            }

            if (GrvPersistencia.ImagemAssinaturaAgente != null)
            {
                new BucketService(_context, _httpClientFactory)
                    .SendFile(BucketNomeTabelaOrigemEnum.AssinaturaAgente, grv.GrvId, grv.UsuarioCadastroId,
                        GrvPersistencia.ImagemAssinaturaAgente);
            }

            if (GrvPersistencia.ImagemAssinaturaCondutor != null)
            {
                new BucketService(_context, _httpClientFactory)
                    .SendFile(BucketNomeTabelaOrigemEnum.AssinaturaCondutor, grv.GrvId, grv.UsuarioCadastroId,
                        GrvPersistencia.ImagemAssinaturaCondutor);
            }

            ResultView.Mensagem = MensagemViewHelper.SetCreateSuccess();

            return ResultView;
        }

        private (string numeroProcesso, MensagemDTO Mensagem) CreateNumeroProcesso(int ClienteId, string NumeroProcesso)
        {
            const int maxLength = 14;

            if (!string.IsNullOrWhiteSpace(NumeroProcesso))
            {
                string numero = NumeroProcesso.Trim().Replace("-", "");

                if (numero.Length > maxLength)
                    return (null, MensagemViewHelper.SetBadRequest(MensagemPadraoEnum.NumeroProcessoInvalido));
                if (!numero.IsNumber() || Convert.ToInt64(numero) <= 0)
                    return (null, MensagemViewHelper.SetBadRequest(MensagemPadraoEnum.NumeroProcessoInvalido));

                bool exists = _context.Grv.Any(x => x.NumeroFormularioGrv == numero);
                if (exists)
                    return (null, MensagemViewHelper.SetBadRequest("Número do processo já utilizado anteriormente."));
                var partes = NumeroProcesso.Trim().Split('-');

                if (partes.Length == 3 && int.TryParse(partes[1], out int idUsuario))
                {
                    var user = _context.Usuario.AsTracking()
                        .FirstOrDefault(x => x.UsuarioId == idUsuario);

                    if (user is null)
                        return (null, MensagemViewHelper.SetNotFound("Este usuario não existe."));

                    user.NumeroFormularioGrvSequencia += 1;
                }

                return (numero, null);
            }

            ClienteModel client = _context.Cliente.AsTracking().FirstOrDefault(x => x.ClienteId == ClienteId);
            if (client == null)
                return (null, MensagemViewHelper.SetBadRequest(MensagemPadraoEnum.NaoEncontradoCliente));

            client.NumeroFormularioGrvSequencia += 1;
            string numeroFormulario = ClienteId.ToString().ToString()
                                      + DateTime.Now.Year.ToString().Substring(2)
                                      + StringHelper.AddCharToLeft(client.NumeroFormularioGrvSequencia.ToString(), '0',
                                          5);

            if (numeroFormulario.Length > maxLength)
                return (null,
                    MensagemViewHelper.SetInternalServerError(
                        new InvalidOperationException("Número do formulário excedeu o tamanho máximo.")));

            return (numeroFormulario, null);
        }

        private void CreateDocumentosCondutor(int GrvId, int UsuarioId,
            List<CondutorDocumentoParameters> ListagemDocumentoCondutor)
        {
            List<BucketListaCadastroModel> Files = new();

            CondutorDocumentoModel CondutorDocumento;

            foreach (CondutorDocumentoParameters item in ListagemDocumentoCondutor)
            {
                CondutorDocumento = new()
                {
                    GrvId = GrvId,

                    UsuarioCadastroId = UsuarioId,

                    TipoDocumentoIdentificacaoId = item.IdentificadorTipoDocumentoIdentificacao,
                };

                _context.CondutorDocumento.Add(CondutorDocumento);

                _context.SaveChanges();

                Files.Add(new()
                {
                    Id = CondutorDocumento.CondutorDocumentoId,

                    File = item.Imagem
                });
            }

            new BucketService(_context, _httpClientFactory)
                .SendFiles("GRV_DOCCONDUTOR", UsuarioId, Files);
        }

        public MensagemDTO CreateDocumentosCondutor(CondutorDocumentoParametersList ListagemDocumentoCondutor)
        {
            MensagemDTO ResultView = ValidateInputGrv(ListagemDocumentoCondutor.IdentificadorProcesso,
                ListagemDocumentoCondutor.IdentificadorUsuario);

            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            if (ListagemDocumentoCondutor.ListagemDocumentoCondutor?.Count == 0)
            {
                return MensagemViewHelper.SetBadRequest("Nenhuma imagem enviada para a API");
            }

            GrvModel Grv = GetById(ListagemDocumentoCondutor.IdentificadorProcesso);

            if (!new[] { "G", "V", "L", "U", "T", "R", "E", "B", "D", "1", "2", "3", "4" }.Contains(Grv.StatusOperacao
                    .StatusOperacaoId))
            {
                return MensagemViewHelper.SetBadRequest(
                    $"O Status atual deste Processo não permite o envio de Fotos. Status atual: {Grv.StatusOperacao.Descricao}");
            }

            CreateDocumentosCondutor(ListagemDocumentoCondutor.IdentificadorProcesso,
                ListagemDocumentoCondutor.IdentificadorUsuario, ListagemDocumentoCondutor.ListagemDocumentoCondutor);

            return MensagemViewHelper.SetCreateSuccess(ListagemDocumentoCondutor.ListagemDocumentoCondutor.Count);
        }

        public MensagemDTO CreateFotos(FotoGrvParameters Fotos)
        {
            MensagemDTO ResultView = ValidateInputGrv(Fotos.IdentificadorProcesso, Fotos.IdentificadorUsuario);

            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            if (Fotos.Fotos.Count == 0)
            {
                return MensagemViewHelper.SetBadRequest("Nenhuma imagem enviada para a API");
            }

            GrvModel Grv = GetById(Fotos.IdentificadorProcesso);

            if (new[] { "C" }.Contains(Grv.StatusOperacao.StatusOperacaoId))
            {
                return MensagemViewHelper.SetBadRequest(
                    $"O Status atual deste Processo não permite o envio de Fotos. Status atual: {Grv.StatusOperacao.Descricao}");
            }

            new BucketService(_context, _httpClientFactory)
                .SendFiles("GRVFOTOSVEICCAD", Fotos.IdentificadorProcesso, Fotos.IdentificadorUsuario, Fotos.Fotos);

            return MensagemViewHelper.SetCreateSuccess(Fotos.Fotos.Count);
        }

        public async Task<MensagemDTO> CreateLacresAsync(int GrvId, int UsuarioId, List<string> ListagemLacre)
        {
            MensagemDTO ResultView = ValidateInputGrv(GrvId, UsuarioId);

            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            if (ListagemLacre.Count == 0)
            {
                return MensagemViewHelper.SetBadRequest("Informe os Lacres");
            }

            ListagemLacre = LinqHelper
                .GetList(ListagemLacre,
                    LinqHelper.LinqListFlags.Distinct | LinqHelper.LinqListFlags.ToUpper |
                    LinqHelper.LinqListFlags.OrderBy);

            GrvModel Grv = await GetByIdAsync(GrvId);

            if (!new[] { "E", "G", "L", "R", "T", "U", "V" }.Contains(Grv.StatusOperacaoId))
            {
                return MensagemViewHelper.SetBadRequest(
                    $"O Status atual deste Processo não permite o cadastro de Lacres. Status atual: {Grv.StatusOperacao.Descricao}");
            }

            List<LacreModel> Lacres = await _context.Lacre
                .Where(x => x.GrvId == GrvId
                            && ListagemLacre.Contains(x.Lacre))
                .AsNoTracking()
                .ToListAsync();

            if (Lacres?.Count > 0)
            {
                List<string> erros = new()
                {
                    $"O(s) seguinte(s) Lacre(s) já estão cadastrados:"
                };

                foreach (LacreModel item in Lacres)
                {
                    erros.Add(item.Lacre);
                }

                return MensagemViewHelper.SetBadRequest(erros);
            }

            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    foreach (string Lacre in ListagemLacre)
                    {
                        _context.Lacre.Add(new()
                        {
                            GrvId = GrvId,

                            UsuarioCadastroId = UsuarioId,

                            Lacre = Lacre
                        });
                    }

                    _context.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    return MensagemViewHelper.SetInternalServerError(ex);
                }
            }

            return MensagemViewHelper.SetCreateSuccess(ListagemLacre.Count, "Lacre(s) cadastrado(s) com sucesso");
        }

        public async Task<MensagemDTO> DeleteAssinaturaAgenteAsync(int GrvId, int UsuarioId)
        {
            MensagemDTO ResultView = ValidateInputGrv(GrvId, UsuarioId);

            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            GrvModel Grv = await GetByIdAsync(GrvId);

            if (Grv.StatusOperacao.StatusOperacaoId != "G")
            {
                return MensagemViewHelper.SetBadRequest(
                    $"O Status atual deste Processo não permite a exclusão da Imagem da Assinatura do Agente. Status atual: {Grv.StatusOperacao.Descricao}");
            }

            BucketArquivoModel BucketArquivo = await _context.BucketArquivo
                .Include(x => x.BucketNomeTabelaOrigem)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.TabelaOrigemId == GrvId
                                          && x.BucketNomeTabelaOrigem.Codigo ==
                                          BucketNomeTabelaOrigemEnum.AssinaturaAgente);

            if (BucketArquivo == null)
            {
                return MensagemViewHelper.SetBadRequest("Registro da Imagem da Assinatura do Agente inexistente");
            }

            new BucketService(_context, _httpClientFactory)
                .DeleteFile(BucketArquivo.RepositorioArquivoId);

            return MensagemViewHelper.SetDeleteSuccess();
        }

        public async Task<MensagemDTO> DeleteAssinaturaCondutorAsync(int GrvId, int UsuarioId)
        {
            MensagemDTO ResultView = ValidateInputGrv(GrvId, UsuarioId);

            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            GrvModel Grv = await GetByIdAsync(GrvId);

            if (Grv.StatusOperacao.StatusOperacaoId != "G")
            {
                return MensagemViewHelper.SetBadRequest(
                    $"O Status atual deste Processo não permite a exclusão da Imagem da Assinatura do Condutor. Status atual: {Grv.StatusOperacao.Descricao}");
            }

            BucketArquivoModel BucketArquivo = await _context.BucketArquivo
                .Include(x => x.BucketNomeTabelaOrigem)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.TabelaOrigemId == GrvId
                                          && x.BucketNomeTabelaOrigem.Codigo ==
                                          BucketNomeTabelaOrigemEnum.AssinaturaCondutor);

            if (BucketArquivo == null)
            {
                return MensagemViewHelper.SetBadRequest("Registro da Imagem da Assinatura do Condutor inexistente");
            }

            new BucketService(_context, _httpClientFactory)
                .DeleteFile(BucketArquivo.RepositorioArquivoId);

            return MensagemViewHelper.SetDeleteSuccess();
        }

        public async Task<MensagemDTO> DeleteFotosAsync(int GrvId, int UsuarioId, List<int> ListagemTabelaOrigemId)
        {
            MensagemDTO ResultView = ValidateInputGrv(GrvId, UsuarioId);

            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            if (ListagemTabelaOrigemId.Count == 0)
            {
                return MensagemViewHelper.SetBadRequest("Informe os Identificadores das Fotos");
            }

            GrvModel Grv = await GetByIdAsync(GrvId);

            if (!new[] { "E", "G", "L", "R", "T", "U", "V" }.Contains(Grv.StatusOperacaoId))
            {
                return MensagemViewHelper.SetBadRequest(
                    $"O Status atual deste Processo não permite a exclusão de Fotos. Status atual: {Grv.StatusOperacao.Descricao}");
            }

            List<BucketArquivoModel> BucketArquivos = await _context.BucketArquivo
                .Include(x => x.BucketNomeTabelaOrigem)
                .Where(x => x.TabelaOrigemId != GrvId
                            && ListagemTabelaOrigemId.Contains(x.RepositorioArquivoId)
                            && x.BucketNomeTabelaOrigem.Codigo == BucketNomeTabelaOrigemEnum.FotoVeiculoGRV)
                .AsNoTracking()
                .ToListAsync();

            if (BucketArquivos?.Count > 0)
            {
                List<string> erros = new()
                {
                    $"A(s) seguinte(s) Fotos não pertencem ao Processo {GrvId}:"
                };

                foreach (BucketArquivoModel BucketArquivo in BucketArquivos)
                {
                    erros.Add(BucketArquivo.RepositorioArquivoId.ToString());
                }

                return MensagemViewHelper.SetBadRequest(erros);
            }

            int totalExcluido = new BucketService(_context, _httpClientFactory)
                .DeleteFiles(BucketNomeTabelaOrigemEnum.FotoVeiculoGRV, ListagemTabelaOrigemId, true);

            return MensagemViewHelper.SetFound(totalExcluido, "Foto(s) excluída(s) com sucesso");
        }

        public async Task<MensagemDTO> DeleteGrvAsync(string NumeroFormularioGrv, string FaturamentoProdutoId,
            int ClienteId, int DepositoId, string Login, string Senha)
        {
            UsuarioDTO Usuario = await new UsuarioService(_context, _mapper)
                .GetByCredentialsAsync(Login, Senha);

            if (Usuario.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return Usuario.Mensagem;
            }

            MensagemDTO ResultView = ValidateInputGrv(NumeroFormularioGrv, FaturamentoProdutoId, ClienteId, DepositoId,
                Usuario.IdentificadorUsuario);

            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            GrvModel Grv = await _context.Grv
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.FaturamentoProdutoId == FaturamentoProdutoId
                                          && x.ClienteId == ClienteId
                                          && x.DepositoId == DepositoId
                                          && x.NumeroFormularioGrv == NumeroFormularioGrv);

            return await DeleteGrvAsync(Grv.GrvId, Usuario.IdentificadorUsuario);
        }

        public async Task<MensagemDTO> DeleteInfracaoAsync(int grvId, int infracaoId)
        {
            if (grvId <= 0)
                return MensagemViewHelper.SetBadRequest(MensagemPadraoEnum.IdentificadorGrvInvalido);
            if (infracaoId <= 0)
                return MensagemViewHelper.SetBadRequest("Identificador da infração inválido.");
            EnquadramentoInfracaoGrvModel infracao = await _context.EnquadramentoInfracaoGrv
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.EnquadramentoInfracaoId == infracaoId && x.GrvId == grvId);
            if (infracao == null)
                return MensagemViewHelper.SetBadRequest("Infração não encontrada ou não pertence a este processo.");
            GrvModel grv = await GetByIdAsync(grvId);
            if (grv == null)
                return MensagemViewHelper.SetNotFound(MensagemPadraoEnum.NaoEncontradoGrv);

            if (!new[] { "E", "G", "L", "R", "T", "U", "V" }.Contains(grv.StatusOperacaoId))
                return MensagemViewHelper.SetBadRequest(
                    $"O status atual deste processo não permite a exclusão de infração. Status atual: {grv.StatusOperacao?.Descricao}");

            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.EnquadramentoInfracaoGrv.Remove(infracao);
                    await _context.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return MensagemViewHelper.SetInternalServerError(ex);
                }
            }

            return MensagemViewHelper.SetDeleteSuccess(1, "Infração excluída com sucesso.");
        }

        public async Task<MensagemDTO> DeleteGrvAsync(int GrvId, string Login, string Senha)
        {
            UsuarioDTO Usuario = await _provider.GetService<UsuarioService>()
                .GetByCredentialsAsync(Login, Senha);
            // UsuarioDTO Usuario = await new UsuarioService(_context, _mapper)
            // .GetByCredentialsAsync(Login, Senha);

            if (Usuario.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return Usuario.Mensagem;
            }
            else if (GrvId <= 0)
            {
                return MensagemViewHelper.SetBadRequest(MensagemPadraoEnum.IdentificadorGrvInvalido);
            }

            MensagemDTO ResultView = ValidateInputGrv(GrvId, Usuario.IdentificadorUsuario);

            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            return await DeleteGrvAsync(GrvId, Usuario.IdentificadorUsuario);
        }

        private async Task<MensagemDTO> DeleteGrvAsync(int GrvId, int UsuarioId)
        {
            MensagemDTO ResultView = new();

            UsuarioPermissaoModel UsuarioPermissao = await _context.UsuarioPermissao
                .Include(x => x.TipoPermissao)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UsuarioId == UsuarioId
                                          && x.TipoPermissao.Codigo == "EXCLUSAOGRV");

            if (UsuarioPermissao == null)
            {
                return MensagemViewHelper.SetUnauthorized("Usuário não possui permissão para excluir Processos");
            }

            GrvModel Grv = await _context.Grv
                .Include(x => x.StatusOperacao)
                .Include(x => x.ListagemCondutorDocumento)
                .Include(x => x.Atendimento)
                .Include(x => x.Liberacao)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.GrvId == GrvId);

            if (Grv == null)
            {
                return MensagemViewHelper.SetNotFound(MensagemPadraoEnum.NaoEncontradoGrv);
            }
            else if (!new[] { "M", "P", "G", "V" }.Contains(Grv.StatusOperacaoId))
            {
                return MensagemViewHelper.SetBadRequest(
                    $"O Status atual deste Processo não permite a exclusão. Status atual: {Grv.StatusOperacao.Descricao}");
            }

            List<FaturamentoModel> Faturamentos = null;

            if (Grv.Atendimento != null)
            {
                Faturamentos = _context.Faturamento
                    .Include(x => x.ListagemBoleto)
                    .Where(x => x.AtendimentoId == Grv.Atendimento.AtendimentoId)
                    .AsNoTracking()
                    .ToList();
            }

            using IDbContextTransaction transaction = _context.Database.BeginTransaction();

            try
            {
                _context.SetUserContextInfo(UsuarioId);

                if (Grv.Liberacao != null)
                {
                    _context.Liberacao
                        .Where(x => x.LiberacaoId == Grv.LiberacaoId)
                        .Delete();
                }

                new ExclusaoHierarquicaService(_context).Iniciar("tb_dep_grv", "id_grv", GrvId);

                _context.SaveChanges();

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();

                return MensagemViewHelper.SetInternalServerError(ex);
            }

            new BucketService(_context, _httpClientFactory)
                .DeleteFiles(BucketNomeTabelaOrigemEnum.FotoVeiculoGRV, GrvId);

            new BucketService(_context, _httpClientFactory)
                .DeleteFiles(BucketNomeTabelaOrigemEnum.FotoVeiculoGGV, GrvId);

            if (Grv.ListagemCondutorDocumento?.Count > 0)
            {
                new BucketService(_context, _httpClientFactory)
                    .DeleteFiles(BucketNomeTabelaOrigemEnum.DocumentoCondutor, Grv.ListagemCondutorDocumento
                        .Select(x => x.CondutorDocumentoId)
                        .ToList());
            }

            if (Grv.Atendimento != null)
            {
                new BucketService(_context, _httpClientFactory)
                    .DeleteFiles(BucketNomeTabelaOrigemEnum.AtendimentoFotoResponsavel, Grv.Atendimento.AtendimentoId);

                if (Faturamentos?.Count > 0)
                {
                    foreach (FaturamentoModel Faturamento in Faturamentos)
                    {
                        if (Faturamento.ListagemBoleto?.Count > 0)
                        {
                            foreach (BoletoModel FaturamentoBoleto in Faturamento.ListagemBoleto)
                            {
                                new BucketService(_context, _httpClientFactory)
                                    .DeleteFiles(BucketNomeTabelaOrigemEnum.Boleto,
                                        FaturamentoBoleto.FaturamentoBoletoId);
                            }
                        }
                    }
                }
            }

            return MensagemViewHelper.SetOk("Processo excluído com sucesso");
        }

        public async Task<MensagemDTO> DeleteLacresAsync(int GrvId, int UsuarioId, List<int> ListagemIdentificadorLacre)
        {
            MensagemDTO ResultView = ValidateInputGrv(GrvId, UsuarioId);

            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            if (ListagemIdentificadorLacre.Count == 0)
            {
                return MensagemViewHelper.SetBadRequest("Informe os Lacres");
            }

            GrvModel Grv = await GetByIdAsync(GrvId);

            if (!new[] { "E", "G", "L", "R", "T", "U", "V" }.Contains(Grv.StatusOperacaoId))
            {
                return MensagemViewHelper.SetBadRequest(
                    $"O Status atual deste Processo não permite a exclusão de Lacres. Status atual: {Grv.StatusOperacao.Descricao}");
            }

            List<LacreModel> Lacres = await _context.Lacre
                .Where(x => x.GrvId != GrvId
                            && ListagemIdentificadorLacre.Contains(x.LacreId))
                .AsNoTracking()
                .ToListAsync();

            if (Lacres?.Count > 0)
            {
                List<string> erros = new()
                {
                    $"O(s) seguinte(s) Lacre(s) pertencem à outro Processo:"
                };

                foreach (LacreModel item in Lacres)
                {
                    erros.Add($"Identificador {item.LacreId}");
                }

                return MensagemViewHelper.SetBadRequest(erros);
            }

            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    foreach (int Lacre in ListagemIdentificadorLacre)
                    {
                        _context.Lacre.DeleteByKey(Lacre);
                    }

                    _context.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    return MensagemViewHelper.SetInternalServerError(ex);
                }
            }

            return MensagemViewHelper.SetDeleteSuccess(ListagemIdentificadorLacre.Count,
                "Lacre(s) excluído(s) com sucesso");
        }

        public GrvModel GetById(int GrvId)
        {
            return _context.Grv
                .Include(x => x.StatusOperacao)
                .Where(x => x.GrvId == GrvId)
                .AsNoTracking()
                .FirstOrDefault();
        }

        public async Task<GrvModel> GetByIdAsync(int GrvId)
        {
            return await _context.Grv
                .Include(x => x.StatusOperacao)
                .Include(x => x.Deposito)
                .ThenInclude(x => x.Endereco)
                .Include(x => x.Cliente)
                .Include(x => x.Reboque)
                .Include(x => x.Reboquista)
                .Include(x => x.MarcaModelo)
                .Include(x => x.Cor)
                .Include(x => x.MotivoApreensao)
                .Include(x => x.TipoVeiculo)
                .Include(x => x.Condutor)
                .Include(x => x.AutoridadeResponsavel)
                .Include(x => x.ListagemEnquadramentoInfracao)
                .ThenInclude(x => x.EnquadramentoInfracao)
                .Include(x => x.ListagemCondutorEquipamentoOpcional)
                .ThenInclude(x => x.EquipamentoOpcional)
                .Include(x => x.ListagemLacre)
                .Include(x => x.ListagemFaturamentoServicoGrv)
                .ThenInclude(x => x.FaturamentoServicoTipoVeiculo)
                .ThenInclude(x => x.FaturamentoServicoAssociado)
                .Include(x => x.Vistoria)
                .ThenInclude(x => x.VistoriaSituacaoChassi)
                .Include(x => x.Vistoria)
                .ThenInclude(x => x.VistoriaStatus)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.GrvId == GrvId);
        }

        public async Task<GrvViewModelList> GetByIdAsync(int GrvId, int UsuarioId, CancellationToken ct)
        {
            GrvViewModelList ResultView = new()
            {
                Mensagem = ValidateInputGrv(GrvId, UsuarioId)
            };

            if (ResultView.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            GrvModel Grv = await GetByIdAsync(GrvId);

            if (Grv == null)
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound(MensagemPadraoEnum.NaoEncontradoGrv);

                return ResultView;
            }

            ResultView.Mensagem = ValidateInputGrv(Grv, UsuarioId);

            if (ResultView.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            var grvDTO = _mapper.Map<GrvDTO>(Grv);
            if (Grv.EnderecoLocalizacaoVeiculoCEPId.HasValue)
            {
                var cep = await _context.CEP.AsNoTracking()
                    .FirstOrDefaultAsync(c => c.CEPId == Grv.EnderecoLocalizacaoVeiculoCEPId, cancellationToken: ct);
                grvDTO.EnderecoLocalizacaoVeiculoCEP = cep?.CEP;
            }

            grvDTO.NumeroChave = Grv.NumeroChave;
            grvDTO.EstacionamentoSetor = Grv.EstacionamentoSetor;
            grvDTO.EstacionamentoNumeroVaga = Grv.EstacionamentoNumeroVaga;
            grvDTO.FlagChaveDeposito = Grv.FlagChaveDeposito;
            grvDTO.FlagTransbordo = Grv.FlagTransbordo;
            grvDTO.DataTransbordo = Grv.DataTransbordo;

            // grvDTO.ListagemServicoAssociadoVeiculo = (await _provider
            //     .GetService<FaturamentoService>()
            //     .ListServicoAssociadoTipoVeiculoAsync(Grv.GrvId, UsuarioId)).Listagem;

            grvDTO.ListagemServicoGgv = (await _provider
                .GetService<FaturamentoService>()
                .ListServicoAssociadoGrvAsync(Grv.GrvId, UsuarioId, ct)).Listagem;

            ResultView.Listagem.Add(grvDTO);
            ResultView.Mensagem = MensagemViewHelper.SetFound();

            return ResultView;
        }

        public async Task<GrvViewModelList> GetByNumeroFormularioGrvAsync(string NumeroFormularioGrv,
            string FaturamentoProdutoId, int ClienteId, int DepositoId, int UsuarioId, CancellationToken ct)
        {
            GrvViewModelList ResultView = new()
            {
                Mensagem = ValidateInputGrv(NumeroFormularioGrv, FaturamentoProdutoId, ClienteId, DepositoId, UsuarioId)
            };

            if (ResultView.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            GrvModel Grv = await _context.Grv
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.NumeroFormularioGrv == NumeroFormularioGrv
                                          && x.ClienteId == ClienteId
                                          && x.DepositoId == DepositoId
                                          && x.FaturamentoProdutoId == FaturamentoProdutoId, cancellationToken: ct);

            if (Grv == null)
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound(MensagemPadraoEnum.NaoEncontradoGrv);

                return ResultView;
            }

            Grv = await GetByIdAsync(Grv.GrvId);

            ResultView.Mensagem = ValidateInputGrv(Grv, UsuarioId);

            if (ResultView.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            var grvDTO = _mapper.Map<GrvDTO>(Grv);

            grvDTO.NumeroChave = Grv.NumeroChave;
            grvDTO.EstacionamentoSetor = Grv.EstacionamentoSetor;
            grvDTO.EstacionamentoNumeroVaga = Grv.EstacionamentoNumeroVaga;
            grvDTO.FlagChaveDeposito = Grv.FlagChaveDeposito;
            grvDTO.FlagTransbordo = Grv.FlagTransbordo;
            grvDTO.DataTransbordo = Grv.DataTransbordo;

            // grvDTO.ListagemServicoAssociadoVeiculo = (await _provider
            //     .GetService<FaturamentoService>()
            //     .ListServicoAssociadoTipoVeiculoAsync(Grv.GrvId, UsuarioId)).Listagem;

            grvDTO.ListagemServicoGgv = (await _provider
                .GetService<FaturamentoService>()
                .ListServicoAssociadoGrvAsync(Grv.GrvId, UsuarioId, ct)).Listagem;

            ResultView.Listagem.Add(grvDTO);

            ResultView.Mensagem = MensagemViewHelper.SetFound();

            return ResultView;
        }

        public async Task<ImageListDTO> GetAssinaturaAgenteAsync(int GrvId, int UsuarioId)
        {
            ImageListDTO ResultView = new()
            {
                Mensagem = ValidateInputGrv(GrvId, UsuarioId)
            };

            if (ResultView.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            BucketArquivoModel BucketArquivo = await _context.BucketArquivo
                .Include(x => x.BucketNomeTabelaOrigem)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.TabelaOrigemId == GrvId
                                          && x.BucketNomeTabelaOrigem.Codigo ==
                                          BucketNomeTabelaOrigemEnum.AssinaturaAgente);

            if (BucketArquivo == null)
            {
                ResultView.Mensagem =
                    MensagemViewHelper.SetBadRequest("Registro da Imagem da Assinatura do Agente inexistente");

                return ResultView;
            }

            return await new BucketService(_context, _httpClientFactory)
                .DownloadFileAsync(BucketNomeTabelaOrigemEnum.AssinaturaAgente, GrvId);
        }

        public async Task<ImageListDTO> GetAssinaturaCondutorAsync(int GrvId, int UsuarioId)
        {
            ImageListDTO ResultView = new()
            {
                Mensagem = ValidateInputGrv(GrvId, UsuarioId)
            };

            if (ResultView.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            BucketArquivoModel BucketArquivo = await _context.BucketArquivo
                .Include(x => x.BucketNomeTabelaOrigem)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.TabelaOrigemId == GrvId
                                          && x.BucketNomeTabelaOrigem.Codigo ==
                                          BucketNomeTabelaOrigemEnum.AssinaturaCondutor);

            if (BucketArquivo == null)
            {
                ResultView.Mensagem =
                    MensagemViewHelper.SetBadRequest("Registro da Imagem da Assinatura do Condutor inexistente");

                return ResultView;
            }

            return await new BucketService(_context, _httpClientFactory)
                .DownloadFileAsync(BucketNomeTabelaOrigemEnum.AssinaturaCondutor, GrvId);
        }

        public async Task<StatusOperacaoListDTO> GetStatusOperacaoByIdAsync(string StatusOperacaoId)
        {
            StatusOperacaoListDTO ResultView = new();

            if (string.IsNullOrWhiteSpace(StatusOperacaoId))
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Identificador do Status da Operação inválido");

                return ResultView;
            }

            StatusOperacaoModel result = await _context.StatusOperacao
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.StatusOperacaoId == StatusOperacaoId.ToUpperTrim().ToNullIfEmpty());

            if (result != null)
            {
                ResultView.Listagem.Add(result);

                ResultView.Mensagem = MensagemViewHelper.SetFound();
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();
            }

            return ResultView;
        }

        public async Task<AutoridadeResponsavelListDTO> ListAutoridadesResponsaveisUfOuDepositoAsync(int? depositoId,
            string? UF, string? nomeAutoridade, int skip = 0, int take = 300)
        {
            AutoridadeResponsavelListDTO ResultView = new();

            if ((depositoId.HasValue && !string.IsNullOrWhiteSpace(UF)) ||
                (!depositoId.HasValue && string.IsNullOrWhiteSpace(UF)))
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest(
                    "Informe apenas um dos parâmetros: depositoId OU UF");
                return ResultView;
            }

            string uf;

            if (!string.IsNullOrWhiteSpace(UF))
            {
                if (!LocalizacaoHelper.IsUF(UF))
                {
                    ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Unidade Federativa inválida");
                    return ResultView;
                }

                uf = UF.Trim().ToUpper();
            }
            else
            {
                var deposito = await _context.Deposito
                    .Include(x => x.Endereco)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.DepositoId == depositoId);

                if (deposito?.Endereco?.UF == null)
                {
                    ResultView.Mensagem = MensagemViewHelper.SetNotFound("Depósito sem UF válida");
                    return ResultView;
                }

                uf = deposito.Endereco.UF.Trim().ToUpper();
            }

            return await ListAutoridadesResponsaveisAsync(uf, nomeAutoridade, skip, take);
        }

        private async Task<AutoridadeResponsavelListDTO> ListAutoridadesResponsaveisAsync(string uf,
            string? nomeAutoridade, int skip, int take)
        {
            AutoridadeResponsavelListDTO ResultView = new();

            var query = _context.OrgaoEmissor
                .AsNoTracking()
                .Where(x =>
                    x.UF == uf &&
                    x.FlagAutoridadeResponsavel == "S" &&
                    x.FlagAtivo == "S"
                )
                .SelectMany(x => x.AutoridadesResponsaveis)
                .Distinct();

            if (!string.IsNullOrWhiteSpace(nomeAutoridade))
            {
                nomeAutoridade = nomeAutoridade.Trim().ToUpper();
                query = query.Where(x =>
                    EF.Functions.Like(x.Divisao, $"%{nomeAutoridade}%"));
            }

            if (skip > 0)
                query = query.Skip(skip);
            if (take > 0)
                query = query.Take(take);

            var result = await query
                .ToListAsync();

            if (result == null || result.Count == 0)
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();
                return ResultView;
            }

            ResultView.Listagem = _mapper.Map<List<AutoridadeResponsavelDTO>>(result);
            ResultView.Mensagem = MensagemViewHelper.SetFound(result.Count);

            return ResultView;
        }

        public async Task<ImageListDTO> ListDocumentosCondutorAsync(int GrvId, int UsuarioId)
        {
            ImageListDTO ResultView = new()
            {
                Mensagem = ValidateInputGrv(GrvId, UsuarioId)
            };

            if (ResultView.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            List<int> DocumentosCondutor = await _context.CondutorDocumento
                .Where(x => x.GrvId == GrvId)
                .AsNoTracking()
                .Select(x => x.CondutorDocumentoId)
                .ToListAsync();

            return await new BucketService(_context, _httpClientFactory)
                .DownloadFilesAsync("GRV_DOCCONDUTOR", DocumentosCondutor);
        }

        public async Task<EnquadramentoInfracaoListDTO> ListEnquadramentoInfracaoAsync()
        {
            EnquadramentoInfracaoListDTO ResultView = new();

            List<EnquadramentoInfracaoModel> result = await _context.EnquadramentoInfracao
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count > 0)
            {
                ResultView.Listagem = _mapper.Map<List<EnquadramentoInfracaoDTO>>(result
                    .OrderBy(x => x.Descricao.Trim())
                    .ToList());

                ResultView.Mensagem = MensagemViewHelper.SetFound(result.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();
            }

            return ResultView;
        }

        public async Task<TipoRegistroListDTO> ListTipoRegistroAsync()
        {
            TipoRegistroListDTO ResultView = new();

            List<TipoRegistroModel> result = await _context.TipoRegistro
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count > 0)
            {
                ResultView.Listagem = _mapper.Map<List<TipoRegistroDTO>>(result
                    .OrderBy(x => x.Descricao.Trim())
                    .ToList());

                ResultView.Mensagem = MensagemViewHelper.SetFound(result.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();
            }

            return ResultView;
        }

        public async Task<ImageListDTO> ListFotoAsync(int GrvId, int UsuarioId)
        {
            ImageListDTO ResultView = new()
            {
                Mensagem = ValidateInputGrv(GrvId, UsuarioId)
            };

            if (ResultView.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            return await new BucketService(_context, _httpClientFactory)
                .DownloadFileAsync(BucketNomeTabelaOrigemEnum.FotoVeiculoGRV, GrvId);
        }

        public async Task<GrvPesquisaDadosMestresDTO> ListItemPesquisaAsync(int UsuarioId)
        {
            return new()
            {
                ListagemProduto = await _provider
                    .GetService<FaturamentoService>()
                    .ListProdutosAsync(),

                ListagemCliente = await _provider
                    .GetService<ClienteService>()
                    .ListResumeAsync(UsuarioId),

                ListagemDeposito = await _provider
                    .GetService<DepositoService>()
                    .ListResumeAsync(UsuarioId),

                ListagemStatusOperacao = await _provider
                    .GetService<GrvService>()
                    .ListStatusOperacaoAsync(),

                //ListagemReboque = await _provider
                //    .GetService<UsuarioService>()
                //    .ListarUsuarioClienteDepositoReboqueSimplificada(UsuarioId),

                //ListagemReboquista = await _provider
                //    .GetService<UsuarioService>()
                //    .ListarUsuarioClienteDepositoReboquistaSimplificada(UsuarioId)
            };
        }

        public async Task<LacreViewModelList> ListLacreAsync(int GrvId, int UsuarioId)
        {
            LacreViewModelList ResultView = new()
            {
                Mensagem = ValidateInputGrv(GrvId, UsuarioId)
            };

            if (ResultView.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            List<LacreModel> result = await _context.Lacre
                .Where(x => x.GrvId == GrvId)
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count > 0)
            {
                ResultView.Listagem = _mapper.Map<List<LacreDTO>>(result
                    .OrderBy(x => x.Lacre)
                    .ToList());

                ResultView.Mensagem = MensagemViewHelper.SetFound(result.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();
            }

            return ResultView;
        }

        public async Task<MotivoApreensaoListDTO> ListMotivoApreensaoAsync()
        {
            MotivoApreensaoListDTO ResultView = new();

            List<MotivoApreensaoModel> result = await _context.MotivoApreensao
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count > 0)
            {
                ResultView.Listagem = _mapper.Map<List<MotivoApreensaoDTO>>(result
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

        public async Task<StatusOperacaoListDTO> ListStatusOperacaoAsync()
        {
            StatusOperacaoListDTO ResultView = new();

            List<StatusOperacaoModel> result = await _context.StatusOperacao
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count > 0)
            {
                result = result
                    .OrderBy(x => x.Descricao)
                    .ToList();

                ResultView.Listagem = result;

                ResultView.Mensagem = MensagemViewHelper.SetFound(result.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();
            }

            return ResultView;
        }

        public async Task<GrvPesquisaResultListDTO> SearchAsync(GrvPesquisaParameters GrvPesquisa)
        {
            List<string> erros = new();

            // if (GrvPesquisa.ListagemCodigoProduto?.Count == 0)
            // {
            //     erros.Add("Informe ao menos um Código do Produto");
            // }
            // else
            // {
            // if (GrvPesquisa.ListagemCodigoProduto.Where(string.IsNullOrWhiteSpace).ToList().Count > 0)
            // {
            // erros.Add("Na listagem do Código do Produto, existem itens vazios");
            // }
            // else
            // {
            if (GrvPesquisa.ListagemCodigoProduto != null &&
                GrvPesquisa.ListagemCodigoProduto.Where(string.IsNullOrWhiteSpace).ToList().Count > 0)
            {
                List<string> Produtos = await _context.FaturamentoProduto
                    .Select(x => x.FaturamentoProdutoId)
                    .AsNoTracking()
                    .ToListAsync();

                foreach (string Codigo in GrvPesquisa.ListagemCodigoProduto)
                {
                    if (Produtos.FirstOrDefault(x => x == Codigo.ToUpperTrim().ToNullIfEmpty()) == null)
                    {
                        erros.Add($"{MensagemPadraoEnum.NaoEncontradoFaturamentoProduto}: {Codigo}");
                    }
                }
            }

            // }
            // }
            if (GrvPesquisa.ListagemStatusOperacao?.Any() == true)
            {
                List<string> StatusOperacoes = await _context.StatusOperacao
                    .Select(x => x.StatusOperacaoId)
                    .AsNoTracking()
                    .ToListAsync();

                foreach (string StatusOperacao in GrvPesquisa.ListagemStatusOperacao)
                {
                    if (string.IsNullOrWhiteSpace(StatusOperacao))
                    {
                        continue;
                    }

                    if (StatusOperacoes.FirstOrDefault(x => x == StatusOperacao.ToUpperTrim().ToNullIfEmpty()) == null)
                    {
                        erros.Add($"Status Operação inexistente: {StatusOperacao}");
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(GrvPesquisa.NumeroProcesso) &&
                (!NumberHelper.IsNumber(GrvPesquisa.NumeroProcesso) ||
                 Convert.ToInt64(GrvPesquisa.NumeroProcesso) <= 0))
            {
                erros.Add(MensagemPadraoEnum.NumeroProcessoInvalido);
            }

            if (!string.IsNullOrWhiteSpace(GrvPesquisa.FlagVeiculoNaoIdentificado))
            {
                // if (!string.IsNullOrWhiteSpace(GrvPesquisa.PlacaVeiculo))
                // {
                //     erros.Add("Ao informar que o Veículo não possui identificação, não informe a Placa");
                // }

                // if (!string.IsNullOrWhiteSpace(GrvPesquisa.Chassi))
                // {
                //     erros.Add("Ao informar que o Veículo não possui identificação, não informe o Chassi");
                // }
            }
            else
            {
                // if (!string.IsNullOrWhiteSpace(GrvPesquisa.PlacaVeiculo) && !GrvPesquisa.PlacaVeiculo.IsPlaca())
                // {
                //     erros.Add("Placa inválida");
                // }
            }

            if (GrvPesquisa.IdentificadorUsuario <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorUsuarioInvalido);
            }

            //if (!GrvPesquisa.DataInicialRemocao.HasValue)
            //{
            //    var baseDate = GrvPesquisa.DataFinalRemocao ?? DateTime.Now;
            //    GrvPesquisa.DataInicialRemocao = baseDate.AddDays(-180);
            //}

            //if (!GrvPesquisa.DataFinalRemocao.HasValue)
            //{
            //    GrvPesquisa.DataFinalRemocao = DateTime.Now;
            //}

            if (GrvPesquisa.DataInicialRemocao.HasValue && GrvPesquisa.DataFinalRemocao.HasValue)
            {
                if (GrvPesquisa.DataInicialRemocao.Value.Date > GrvPesquisa.DataFinalRemocao.Value.Date)
                {
                    erros.Add("A Data Inicial não pode ser maior do que a Data Final");
                }
                else if ((GrvPesquisa.DataFinalRemocao.Value.Date - GrvPesquisa.DataInicialRemocao.Value.Date).Days >
                         180)
                {
                    erros.Add("O período de pesquisa não pode superar 180 dias");
                }
            }

            GrvPesquisaResultListDTO ResultView = new();

            if (erros.Count > 0)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest(erros);

                return ResultView;
            }

            var query = _context.Grv
                .Include(x => x.Cliente)
                .Include(x => x.Atendimento)
                .ThenInclude(x => x.ListagemFaturamento)
                .ThenInclude(x => x.ListagemFaturamentoComposicao)
                .Include(x => x.Deposito)
                .Include(x => x.StatusOperacao)
                .Include(x => x.MarcaModelo)
                .Include(x => x.UsuarioClienteDepositoGrv)
                .Where(x =>
                    x.UsuarioClienteDepositoGrv.UsuarioId == GrvPesquisa.IdentificadorUsuario &&
                    x.UsuarioClienteDepositoGrv.FaturamentoProdutoCodigo == x.FaturamentoProdutoId)
                .AsQueryable();

            #region Filtros

            if (GrvPesquisa.ListagemCodigoProduto?.Any() == true)
                query = query.Where(x => GrvPesquisa.ListagemCodigoProduto.Contains(x.FaturamentoProdutoId));
            if (GrvPesquisa.ListagemStatusOperacao?.Any() == true)
                query = query.Where(x => GrvPesquisa.ListagemStatusOperacao.Contains(x.StatusOperacaoId));
            if (!string.IsNullOrWhiteSpace(GrvPesquisa.NumeroProcesso))
                query = query.Where(x => x.NumeroFormularioGrv == GrvPesquisa.NumeroProcesso);
            if (!string.IsNullOrWhiteSpace(GrvPesquisa.PlacaVeiculo))
                query = query.Where(x => x.Placa == GrvPesquisa.PlacaVeiculo);
            if (!string.IsNullOrWhiteSpace(GrvPesquisa.Chassi))
                query = query.Where(x => x.Chassi == GrvPesquisa.Chassi);
            if (!string.IsNullOrWhiteSpace(GrvPesquisa.FlagVeiculoNaoIdentificado))
                query = query.Where(x => x.FlagVeiculoNaoIdentificado == GrvPesquisa.FlagVeiculoNaoIdentificado);
            if (GrvPesquisa.IdentificadorCliente > 0)
                query = query.Where(x => x.ClienteId == GrvPesquisa.IdentificadorCliente);
            if (GrvPesquisa.IdentificadorDeposito > 0)
                query = query.Where(x => x.DepositoId == GrvPesquisa.IdentificadorDeposito);
            if (GrvPesquisa.DataInicialRemocao.HasValue)
                query = query.Where(x => x.DataHoraRemocao >= GrvPesquisa.DataInicialRemocao.Value);
            if (GrvPesquisa.DataFinalRemocao.HasValue)
                query = query.Where(x => x.DataHoraRemocao <= GrvPesquisa.DataFinalRemocao.Value);

            #endregion

            var result = await query
                .OrderByDescending(x => x.DataCadastro)
                .Take(100)
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count == 0)
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound("A pesquisa não retornou registro");

                return ResultView;
            }

            foreach (GrvModel Grv in result)
            {
                ResultView.Listagem.Add(new()
                {
                    IdentificadorProcesso = Grv.GrvId,
                    IdentificadorCliente = Grv.ClienteId,
                    IdentificadorDeposito = Grv.DepositoId,
                    IdentificadorAtendimento = Grv.Atendimento?.AtendimentoId ?? 0,
                    IdentificadorFaturamento =
                        Grv.Atendimento?.ListagemFaturamento?.FirstOrDefault()?.FaturamentoId ?? 0,
                    StatusOperacaoId = Grv.StatusOperacaoId,

                    NumeroProcesso = Grv.NumeroFormularioGrv,

                    Placa = Grv?.Placa ?? "",

                    Chassi = Grv?.Chassi ?? "",

                    Renavam = Grv?.Renavam ?? "",

                    MarcaModelo = Grv.MarcaModelo?.MarcaModelo ?? "",

                    StatusOperacao = Grv.StatusOperacao.Descricao,

                    DataHoraRemocao = Grv.DataHoraRemocao,

                    DataHoraGuarda = Grv.DataHoraGuarda.GetValueOrDefault(),

                    Cliente = Grv.Cliente.Nome,

                    Deposito = Grv.Deposito.Nome
                });
            }

            ResultView.Mensagem = MensagemViewHelper.SetFound(result.Count);

            return ResultView;
        }

        public MensagemDTO ValidateInputGrv(int GrvId, int UsuarioId)
        {
            List<string> erros = new();

            if (GrvId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorGrvInvalido);
            }

            if (UsuarioId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorUsuarioInvalido);
            }

            if (erros.Count > 0)
            {
                return MensagemViewHelper.SetBadRequest(erros);
            }

            return ValidateInputGrv(GrvId, UsuarioId, "", "", 0, 0);
        }

        public MensagemDTO ValidateInputGrv(GrvModel Grv, int UsuarioId)
        {
            return ValidateInputGrv(Grv.GrvId, UsuarioId, Grv.NumeroFormularioGrv, Grv.FaturamentoProdutoId,
                Grv.ClienteId, Grv.DepositoId);
        }

        public MensagemDTO ValidateInputGrv(string NumeroFormularioGrv, string FaturamentoProdutoId, int ClienteId,
            int DepositoId, int UsuarioId)
        {
            return ValidateInputGrv(0, UsuarioId, NumeroFormularioGrv, FaturamentoProdutoId, ClienteId, DepositoId);
        }

        private MensagemDTO ValidateInputGrv(int GrvId, int UsuarioId, string NumeroFormularioGrv,
            string FaturamentoProdutoId, int ClienteId, int DepositoId)
        {
            if (!new UsuarioService(_context).IsUserActive(UsuarioId))
            {
                return MensagemViewHelper.SetUnauthorized();
            }

            List<string> erros = new();

            if (GrvId <= 0 && string.IsNullOrWhiteSpace(NumeroFormularioGrv))
            {
                erros.Add("Informe o Identificador ou o Número do Processo");
            }
            else if (GrvId <= 0)
            {
                if (!NumberHelper.IsNumber(NumeroFormularioGrv) || NumeroFormularioGrv.Length > 14 ||
                    Convert.ToInt64(NumeroFormularioGrv) <= 0)
                {
                    erros.Add(MensagemPadraoEnum.NumeroProcessoInvalido);
                }

                if (string.IsNullOrWhiteSpace(FaturamentoProdutoId))
                {
                    erros.Add(MensagemPadraoEnum.InformeCodigoProduto);
                }

                if (ClienteId <= 0)
                {
                    erros.Add(MensagemPadraoEnum.IdentificadorClienteInvalido);
                }

                if (DepositoId <= 0)
                {
                    erros.Add(MensagemPadraoEnum.IdentificadorDepositoInvalido);
                }
            }

            if (UsuarioId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorUsuarioInvalido);
            }

            if (erros.Count > 0)
            {
                return MensagemViewHelper.SetBadRequest(erros);
            }

            if (GrvId <= 0)
            {
                FaturamentoProdutoModel FaturamentoProduto = _context.FaturamentoProduto
                    .AsNoTracking()
                    .FirstOrDefault(x => x.FaturamentoProdutoId == FaturamentoProdutoId);

                if (FaturamentoProduto == null)
                {
                    return MensagemViewHelper.SetNotFound(MensagemPadraoEnum.NaoEncontradoFaturamentoProduto);
                }

                ClienteModel Cliente = _context.Cliente
                    .AsNoTracking()
                    .FirstOrDefault(x => x.ClienteId == ClienteId);

                if (Cliente == null)
                {
                    return MensagemViewHelper.SetNotFound(MensagemPadraoEnum.NaoEncontradoCliente);
                }

                DepositoModel Deposito = _context.Deposito
                    .AsNoTracking()
                    .FirstOrDefault(x => x.DepositoId == DepositoId);

                if (Deposito == null)
                {
                    return MensagemViewHelper.SetNotFound(MensagemPadraoEnum.NaoEncontradoDeposito);
                }

                ClienteDepositoModel ClienteDeposito = _context.ClienteDeposito
                    .AsNoTracking()
                    .FirstOrDefault(x => x.ClienteId == ClienteId
                                         && x.DepositoId == DepositoId);

                if (ClienteDeposito == null)
                {
                    return MensagemViewHelper.SetNotFound(MensagemPadraoEnum.NaoEncontradoAssociacaoClienteDeposito);
                }
            }

            ViewUsuarioClienteDepositoGrvModel Grv = new();

            if (GrvId > 0)
            {
                Grv = _context.ViewUsuarioClienteDepositoGrv
                    .AsNoTracking()
                    .FirstOrDefault(x => x.GrvId == GrvId);

                if (Grv == null)
                {
                    return MensagemViewHelper.SetUnauthorized(
                        "Usuário não possui acesso ao Processo ou o Processo não existe");
                }
            }
            else
            {
                Grv = _context.ViewUsuarioClienteDepositoGrv
                    .AsNoTracking()
                    .FirstOrDefault(x => x.FaturamentoProdutoCodigo == FaturamentoProdutoId
                                         && x.ClienteId == ClienteId
                                         && x.DepositoId == DepositoId
                                         && x.NumeroFormularioGrv == NumeroFormularioGrv);

                if (Grv == null)
                {
                    return MensagemViewHelper.SetUnauthorized(
                        "Usuário não possui acesso ao Processo ou o Processo não existe");
                }
            }

            return MensagemViewHelper.SetOk();
        }

        public async Task<MensagemDTO> CheckAlteracaoStatusGrvAsync(int GrvId, string StatusOperacaoId, int UsuarioId)
        {
            MensagemDTO ResultView = ValidateInputGrv(GrvId, UsuarioId);

            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            GrvModel Grv = await _context.Grv
                .Include(x => x.StatusOperacao)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.GrvId == GrvId);

            if (Grv == null)
            {
                return MensagemViewHelper.SetNotFound(MensagemPadraoEnum.NaoEncontradoGrv);
            }

            StatusOperacaoModel StatusOperacao = await _context.StatusOperacao
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.StatusOperacaoId == StatusOperacaoId);

            if (StatusOperacao == null)
            {
                return MensagemViewHelper.SetNotFound(MensagemPadraoEnum.NaoEncontradoStatusOperacao);
            }
            else if (Grv.StatusOperacao.StatusOperacaoId != StatusOperacaoId)
            {
                return MensagemViewHelper.SetBadRequest(
                    $"O Status da Operação foi alterado de \"{Grv.StatusOperacao.Descricao.ToUpper()}\" para \"{StatusOperacao.Descricao.ToUpper()}\"");
            }

            return MensagemViewHelper.SetOk("O Status da Operação não foi alterado");
        }

        public async Task<MensagemDTO> CheckInformacoesPersistenciaAsync(GrvParameters GrvPersistencia, CancellationToken ct)
        {
            if (GrvPersistencia == null)
            {
                return MensagemViewHelper.SetBadRequest("O Modelo está nulo");
            }

            #region Validações de IDs

            List<string> erros = new();

            if (GrvPersistencia.IdentificadorUsuario <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorUsuarioInvalido);
            }

            if (GrvPersistencia.IdentificadorCliente <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorClienteInvalido);
            }

            if (GrvPersistencia.IdentificadorDeposito <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorDepositoInvalido);
            }

            if (GrvPersistencia.IdentificadorTipoVeiculo <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorTipoVeiculoInvalido);
            }

            if (GrvPersistencia.FlagVeiculoNaoUsouReboque == "N")
            {
                if (GrvPersistencia.IdentificadorReboquista <= 0)
                {
                    erros.Add(MensagemPadraoEnum.IdentificadorReboquistaInvalido);
                }

                if (GrvPersistencia.IdentificadorReboque <= 0)
                {
                    erros.Add(MensagemPadraoEnum.IdentificadorReboqueInvalido);
                }
            }
            else
            {
                if (GrvPersistencia.IdentificadorReboquista > 0)
                {
                    erros.Add("Ao informar que o Veículo não usou Reboque, não informe o Identificador do Reboquista");
                }

                if (GrvPersistencia.IdentificadorReboque > 0)
                {
                    erros.Add("Ao informar que o Veículo não usou Reboque, não informe o Identificador do Reboque");
                }
            }

            if (GrvPersistencia.IdentificadorAutoridadeResponsavel <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorAutoridadeResponsavelInvalido);
            }

            if (string.IsNullOrWhiteSpace(GrvPersistencia.MatriculaAutoridadeResponsavel))
            {
                erros.Add("Informe a Matrícula da Autoridade Responsável");
            }

            if (GrvPersistencia.IdentificadorCor <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorCorInvalido);
            }

            if (GrvPersistencia.IdentificadorMarcaModelo <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorMarcaModeloInvalido);
            }

            if (GrvPersistencia.IdentificadorMotivoApreensao <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorMotivoApreensaoInvalido);
            }

            if (GrvPersistencia.IdentificadorUsuario <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorUsuarioInvalido);
            }

            if (string.IsNullOrWhiteSpace(GrvPersistencia.CodigoProduto))
            {
                erros.Add("Informe o Código de Produto");
            }

            //if (string.IsNullOrWhiteSpace(GrvPersistencia.NumeroProcesso))
            //{
            //    erros.Add(MensagemPadraoEnum.InformeNumeroProcesso);
            //}
            //else if (!NumberHelper.IsNumber(GrvPersistencia.NumeroProcesso) || Convert.ToInt64(GrvPersistencia.NumeroProcesso) <= 0)
            //{
            //    erros.Add(MensagemPadraoEnum.NumeroProcessoInvalido);
            //}
            if (GrvPersistencia.FlagVeiculoNaoIdentificado == "S")
            {
                // if (!string.IsNullOrWhiteSpace(GrvPersistencia.Placa) ||
                //     !string.IsNullOrWhiteSpace(GrvPersistencia.Chassi))
                // {
                //     erros.Add(
                //         "Ao informar que o Veículo não foi identificado, não se deve informar a Placa nem o Chassi");
                // }
            }
            else if (GrvPersistencia.FlagVeiculoSemRegistro == "S")
            {
                // if (!string.IsNullOrWhiteSpace(GrvPersistencia.Placa))
                // {
                //     erros.Add("Ao informar que o Veículo não possui registro, não se deve informar a Placa");
                // }

                // if (string.IsNullOrWhiteSpace(GrvPersistencia.Chassi))
                // {
                //     erros.Add("Informe o Chassi");
                // } //(GrvPersistencia.Chassi.Length == 17 && !GrvPersistencia.Chassi.IsChassi()
                // else if (GrvPersistencia.Chassi.Length < 6 || GrvPersistencia.Chassi.Length > 24)
                // {
                //     erros.Add("Chassi inválido");
                // }
            }
            else
            {
                // if (!string.IsNullOrWhiteSpace(GrvPersistencia.Placa))
                // {
                //     // if (!GrvPersistencia.Placa.IsPlaca())
                //     // {
                //     //     erros.Add("Placa inválida");
                //     // }
                // }

                // if (!string.IsNullOrWhiteSpace(GrvPersistencia.Chassi))
                // {
                //     if (GrvPersistencia.Chassi.Length < 6 || GrvPersistencia.Chassi.Length > 24)
                //     {
                //         erros.Add("Chassi inválido");
                //     }
                // }
            }

            // if (!string.IsNullOrWhiteSpace(GrvPersistencia.EnderecoLocalizacaoVeiculoCEP))
            // {
            //     if (!LocalizacaoHelper.IsCEP(GrvPersistencia.EnderecoLocalizacaoVeiculoCEP))
            //     {
            //         erros.Add("CEP inválido");
            //     }
            // }

            if (GrvPersistencia.Condutor == null)
            {
                erros.Add("Informe os dados do Condutor");
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(GrvPersistencia.Condutor.Email)
                    && !EmailHelper.IsEmail(GrvPersistencia.Condutor.Email))
                {
                    erros.Add($"E-mail do Condutor é inválido: {GrvPersistencia.Condutor.Email}");
                }

                if (!string.IsNullOrWhiteSpace(GrvPersistencia.Condutor.Documento))
                {
                    if (!DocumentHelper.IsCPF(GrvPersistencia.Condutor.Documento))
                    {
                        erros.Add("CPF do Condutor inválido");
                    }
                }

                if (GrvPersistencia.Condutor.IdentificadorAssinaturaCondutor < 0)
                {
                    erros.Add("Identificador da Assinatura do Condutor inválido");
                }
                else
                {
                    TabelaGenericaModel AssinaturaCondutor = await new TabelaGenericaService(_context)
                        .GetByIdAsync(GrvPersistencia.Condutor.IdentificadorAssinaturaCondutor);

                    if (AssinaturaCondutor == null)
                    {
                        erros.Add("Identificador da Assinatura do Condutor inválido");
                    }
                }

                if (!string.IsNullOrWhiteSpace(GrvPersistencia.Condutor.TelefoneDDD)
                    && !ContactHelper.IsDDD(GrvPersistencia.Condutor.TelefoneDDD))
                {
                    erros.Add("DDD do Telefone do Condutor inválido");
                }

                if (!string.IsNullOrWhiteSpace(GrvPersistencia.Condutor.Telefone)
                    && !ContactHelper.IsTelephoneOrCellphone(GrvPersistencia.Condutor.Telefone))
                {
                    erros.Add("Telefone do Condutor inválido");
                }

                if (!string.IsNullOrWhiteSpace(GrvPersistencia.Condutor.TelefoneDDD)
                    && string.IsNullOrWhiteSpace(GrvPersistencia.Condutor.Telefone))
                {
                    erros.Add("Ao informar o DDD do Telefone do Condutor é necessário informar o Telefone do Condutor");
                }
                else if (string.IsNullOrWhiteSpace(GrvPersistencia.Condutor.TelefoneDDD)
                         && !string.IsNullOrWhiteSpace(GrvPersistencia.Condutor.Telefone))
                {
                    erros.Add("Ao informar o Telefone do Condutor é necessário informar o DDD do Telefone do Condutor");
                }

                if (GrvPersistencia.Condutor.FlagChaveVeiculo == "S"
                    && string.IsNullOrWhiteSpace(GrvPersistencia.Condutor.NumeroChaveVeiculo))
                {
                    erros.Add(
                        "Ao informar que a Chave ficou no Veículo, é necessário informar o Número/Código da Chave");
                }
            }

            if (GrvPersistencia.ListagemLacre?.Count == 0)
            {
                erros.Add("Informe os Lacres");
            }
            else
            {
                if (GrvPersistencia.ListagemLacre.ContainsDuplicates())
                {
                    erros.Add("Existem Lacres duplicados");
                }

                if (GrvPersistencia.ListagemLacre.ContainsNullOrWhiteSpaceValues())
                {
                    erros.Add("Existem Lacres não informados");
                }
            }

            if (GrvPersistencia.ListagemEquipamentoOpcional?.Count > 0)
            {
                if (GrvPersistencia.ListagemEquipamentoOpcional.Where(x => x.IdentificadorEquipamentoOpcional <= 0)
                        .ToList().Count > 0)
                {
                    erros.Add("Existe um ou mais Identificador do Equipamento Opcional inválido");
                }

                if (GrvPersistencia.ListagemEquipamentoOpcional.Where(x =>
                        x.FlagEquipamentoAvariado == "S" &&
                        (x.IdentificadorTipoAvaria <= 0 || x.IdentificadorTipoAvaria == null)).ToList().Count > 0)
                {
                    erros.Add("Existe um ou mais Identificador do Tipo de Avaria inválido");
                }
            }

            if (erros.Count > 0)
            {
                return MensagemViewHelper.SetBadRequest(erros);
            }

            #endregion Validações de IDs

            #region Consultas

            if (!await new UsuarioService(_context).IsUserActiveAsync(GrvPersistencia.IdentificadorUsuario))
            {
                return MensagemViewHelper.SetUnauthorized();
            }

            MensagemDTO ResultView = new();


            ClienteModel Cliente = await _context.Cliente
                .Include(x => x.Endereco)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ClienteId == GrvPersistencia.IdentificadorCliente, cancellationToken: ct);

            if (Cliente == null)
            {
                ResultView.AvisosImpeditivos.Add(MensagemPadraoEnum.NaoEncontradoCliente);
            }
            else if (Cliente.FlagClientePossuiCodigoIdentificacao == "S"
                     && string.IsNullOrWhiteSpace(GrvPersistencia.CodigoIdentificacaoCliente))
            {
                ResultView.AvisosImpeditivos.Add($"Informe o {Cliente.LabelClienteCodigoIdentificacao}");
            }

            DepositoModel Deposito = await _context.Deposito
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.DepositoId == GrvPersistencia.IdentificadorDeposito, cancellationToken: ct);

            if (Deposito == null)
            {
                ResultView.AvisosImpeditivos.Add(MensagemPadraoEnum.NaoEncontradoDeposito);
            }

            ClienteDepositoModel ClienteDeposito = await _context.ClienteDeposito
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ClienteId == GrvPersistencia.IdentificadorCliente
                                          && x.DepositoId == GrvPersistencia.IdentificadorDeposito, cancellationToken: ct);

            if (ClienteDeposito == null)
            {
                ResultView.AvisosImpeditivos.Add("O Cliente e Depósito informados não são associados");
            }
            else if (ClienteDeposito.FlagCadastrarGrvComStatusOperacaoBloqueado == "S")
            {
                StatusOperacaoModel StatusOperacao = await _context.StatusOperacao
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.StatusOperacaoId == "B", cancellationToken: ct);

                ResultView.AvisosInformativos.Add(
                    $"Esse Processo receberá o Status de Operação {StatusOperacao.Descricao} devido à configuração do Cliente");
            }

            DateTime DataHoraPorDeposito = new DepositoService(_context)
                .GetDataHoraPorDeposito(GrvPersistencia.IdentificadorDeposito);

            if (GrvPersistencia.DataHoraRemocao.Date > DataHoraPorDeposito.Date)
            {
                ResultView.AvisosImpeditivos.Add("A Data da Remoção não pode ser maior do que a Data atual");
            }
            else if (GrvPersistencia.DataHoraRemocao.Hour == 0 && GrvPersistencia.DataHoraRemocao.Minute == 0)
            {
                ResultView.AvisosImpeditivos.Add("A Hora da Remoção não pode ser igual a 00:00");
            }

            TipoVeiculoModel TipoVeiculo = await _context.TipoVeiculo
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.TipoVeiculoId == GrvPersistencia.IdentificadorTipoVeiculo, cancellationToken: ct);

            if (TipoVeiculo == null)
            {
                ResultView.AvisosImpeditivos.Add("Tipo do Veículo inexistente");
            }

            if (GrvPersistencia.FlagVeiculoNaoUsouReboque.Equals("N", StringComparison.CurrentCultureIgnoreCase))
            {
                ReboquistaModel Reboquista = await _context.Reboquista
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.ReboquistaId == GrvPersistencia.IdentificadorReboquista, cancellationToken: ct);

                if (Reboquista == null)
                {
                    ResultView.AvisosImpeditivos.Add("Reboquista inexistente");
                }

                ReboqueModel Reboque = await _context.Reboque
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.ReboqueId == GrvPersistencia.IdentificadorReboque, cancellationToken: ct);

                if (Reboque == null)
                {
                    ResultView.AvisosImpeditivos.Add("Reboque inexistente");
                }
            }

            AutoridadeResponsavelModel AutoridadeResponsavel = await _context.AutoridadeResponsavel
                .Include(x => x.OrgaoEmissor)
                .AsNoTracking()
                .FirstOrDefaultAsync(x =>
                    x.AutoridadeResponsavelId == GrvPersistencia.IdentificadorAutoridadeResponsavel, cancellationToken: ct);

            if (AutoridadeResponsavel == null)
            {
                ResultView.AvisosImpeditivos.Add("Autoridade Responsável não encontrada");
            }
            else if (!string.Equals(
                         AutoridadeResponsavel.OrgaoEmissor.UF?.Trim(),
                         Cliente.Endereco.UF?.Trim(),
                         StringComparison.OrdinalIgnoreCase))
            {
                ResultView.AvisosImpeditivos.Add(
                    $"A Autoridade Responsável ({AutoridadeResponsavel.OrgaoEmissor.UF}) informada não pertence a mesma Unidade Federativa do cadastro do Cliente {Cliente.Nome} ({Cliente.Endereco.UF})");
            }

            CorModel Cor = await _context.Cor
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.CorId == GrvPersistencia.IdentificadorCor, cancellationToken: ct);

            if (Cor == null)
            {
                ResultView.AvisosImpeditivos.Add("Cor não encontrada");
            }

            MarcaModeloModel MarcaModelo = await _context.MarcaModelo
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.MarcaModeloId == GrvPersistencia.IdentificadorMarcaModelo, cancellationToken: ct);

            if (MarcaModelo == null)
            {
                ResultView.AvisosImpeditivos.Add("Marca/Modelo inexistente");
            }

            MotivoApreensaoModel MotivoApreensao = await _context.MotivoApreensao
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.MotivoApreensaoId == GrvPersistencia.IdentificadorMotivoApreensao, cancellationToken: ct);

            if (MotivoApreensao == null)
            {
                ResultView.AvisosImpeditivos.Add("Motivo de Apreensão inexistente");
            }
            else if (MotivoApreensao.FlagDefault == "S")
            {
                if (GrvPersistencia?.ListagemEnquadramentoInfracao.Count == 0)
                {
                    ResultView.AvisosImpeditivos.Add("Informe o Enquadramento da Infração");
                }
                else
                {
                    if (GrvPersistencia.ListagemEnquadramentoInfracao.ContainsNegativeOrZeroNumbers())
                    {
                        ResultView.AvisosImpeditivos.Add(
                            "Existem Enquadramento da Infração com Identificador inválido");
                    }

                    if (GrvPersistencia.ListagemEnquadramentoInfracao.ContainsDuplicates())
                    {
                        ResultView.AvisosImpeditivos.Add("Existem Enquadramento da Infração duplicados");
                    }
                    else
                    {
                        if (GrvPersistencia.ListagemEnquadramentoInfracao.Exists(x =>
                                x.IdentificadorEnquadramentoInfracao > 0))
                        {
                            List<decimal> ids = GrvPersistencia.ListagemEnquadramentoInfracao
                                .Where(x => x.IdentificadorEnquadramentoInfracao > 0)
                                .Select(x => x.IdentificadorEnquadramentoInfracao)
                                .ToList();

                            int count = _context.EnquadramentoInfracao
                                .Where(x => ids.Contains(x.EnquadramentoInfracaoId))
                                .AsNoTracking()
                                .Count();

                            if (ids.Count != count)
                            {
                                ResultView.AvisosImpeditivos.Add("Existem Enquadramento da Infração inexistentes");
                            }
                        }
                    }

                    List<IGrouping<string, string>> NumeroInfracao = GrvPersistencia.ListagemEnquadramentoInfracao
                        .Where(x => string.IsNullOrWhiteSpace(x.NumeroInfracao.Trim()))
                        .Select(x => x.NumeroInfracao.Trim())
                        .GroupBy(x => x)
                        .ToList();

                    if (NumeroInfracao.Count >= 1)
                    {
                        ResultView.AvisosImpeditivos.Add("Existem Número da Infração não informados");
                    }

                    NumeroInfracao = GrvPersistencia.ListagemEnquadramentoInfracao
                        .Where(x => !string.IsNullOrWhiteSpace(x.NumeroInfracao.Trim()))
                        .Select(x => x.NumeroInfracao.Trim())
                        .GroupBy(x => x)
                        .Where(x => x.Count() > 1)
                        .ToList();

                    if (NumeroInfracao.Count >= 1)
                    {
                        ResultView.AvisosImpeditivos.Add("Existem Número da Infração duplicados");
                    }
                }
            }

            FaturamentoProdutoModel Produtos = await _context.FaturamentoProduto
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.FaturamentoProdutoId == GrvPersistencia.CodigoProduto, cancellationToken: ct);

            if (Produtos == null)
            {
                ResultView.AvisosImpeditivos.Add(MensagemPadraoEnum.NaoEncontradoFaturamentoProduto);
            }

            // if (!string.IsNullOrWhiteSpace(GrvPersistencia.EnderecoLocalizacaoVeiculoCEP))
            // {
            //     if (GrvPersistencia.EnderecoLocalizacaoVeiculoCEP.IsCEP())
            //     {
            //         if (await _context.CEP
            //             .FirstOrDefaultAsync(x => x.CEP == GrvPersistencia.EnderecoLocalizacaoVeiculoCEP.GetNumbers()) == null)
            //         {
            //             ResultView.AvisosImpeditivos.Add("CEP inexistente");
            //         }
            //     }
            // }

            if (GrvPersistencia.ListagemDocumentoCondutor?.Count > 0)
            {
                List<byte> TiposDocumentosIdentificacoes = _context.TipoDocumentoIdentificacao
                    .Where(x => x.FlagPrincipal == "S")
                    .AsNoTracking()
                    .Select(x => x.TipoDocumentoIdentificacaoId)
                    .ToList();

                List<byte> IdentificadorTipoDocumentoIdentificacao = GrvPersistencia.ListagemDocumentoCondutor
                    .Select(x => x.IdentificadorTipoDocumentoIdentificacao)
                    .ToList();

                if (IdentificadorTipoDocumentoIdentificacao.Where(x => TiposDocumentosIdentificacoes.All(y => y != x))
                        .ToList().Count > 0)
                {
                    ResultView.AvisosImpeditivos.Add(
                        "Existem Identificador do Tipo de Documento de Identificação inválido");
                }
            }

            if (GrvPersistencia.ListagemEquipamentoOpcional?.Count > 0)
            {
                List<decimal> EquipamentoOpcionalIds = GrvPersistencia.ListagemEquipamentoOpcional
                    .Select(x => x.IdentificadorEquipamentoOpcional)
                    .Distinct()
                    .ToList();

                int ListagemEquipamentoOpcionalEncontrada = _context.EquipamentoOpcional
                    .Where(x => EquipamentoOpcionalIds.Contains(x.EquipamentoOpcionalId))
                    .AsNoTracking()
                    .Count();

                if (EquipamentoOpcionalIds.Count != ListagemEquipamentoOpcionalEncontrada)
                {
                    ResultView.AvisosImpeditivos.Add(
                        "A listagem de Equipamento Opcional possui um ou mais Identificador inexistente");
                }
                else if (GrvPersistencia.IdentificadorTipoVeiculo > 0)
                {
                    int CountTipoVeiculoEquipamentoAssociacao = _context.TipoVeiculoEquipamentoAssociacao
                        .Where(x => EquipamentoOpcionalIds.Contains(x.EquipamentoOpcionalId) &&
                                    x.TipoVeiculoId == GrvPersistencia.IdentificadorTipoVeiculo)
                        .AsNoTracking()
                        .Count();

                    if (CountTipoVeiculoEquipamentoAssociacao != ListagemEquipamentoOpcionalEncontrada)
                    {
                        ResultView.AvisosImpeditivos.Add(
                            "A listagem de Equipamento Opcional possui um ou mais Identificador não associado ao Tipo de Veículo");
                    }
                }

                // Verificar duplicidade
                int duplicado = GrvPersistencia.ListagemEquipamentoOpcional
                    .Where(x => x.IdentificadorEquipamentoOpcional > 0)
                    .Select(x => x.IdentificadorEquipamentoOpcional)
                    .GroupBy(x => x)
                    .Where(x => x.Count() > 1)
                    .Count();

                if (duplicado >= 1)
                {
                    ResultView.AvisosImpeditivos.Add("Existe Identificador do Equipamento Opcional duplicado");
                }

                if (GrvPersistencia.ListagemEquipamentoOpcional.Where(x => x.IdentificadorTipoAvaria > 0).ToList()
                        .Count > 0)
                {
                    List<int?> ListagemIdentificadorTipoAvariaIdIds = GrvPersistencia.ListagemEquipamentoOpcional
                        .Where(x => x.IdentificadorTipoAvaria > 0)
                        .Select(x => x.IdentificadorTipoAvaria)
                        .Distinct()
                        .ToList();

                    if (ListagemIdentificadorTipoAvariaIdIds?.Count > 0)
                    {
                        int TipoAvariaIds = _context.TipoAvaria
                            .Where(x => ListagemIdentificadorTipoAvariaIdIds.Contains(x.TipoAvariaId))
                            .AsNoTracking()
                            .Count();

                        if (TipoAvariaIds != ListagemIdentificadorTipoAvariaIdIds?.Count)
                        {
                            ResultView.AvisosImpeditivos.Add(
                                "A listagem de Equipamento Opcional possui um ou mais Identificador de Tipo de Avaria inexistente");
                        }
                    }
                }
            }

            var grv = await _context.Grv.AsNoTracking()
                .FirstOrDefaultAsync(x =>
                    x.ClienteId == GrvPersistencia.IdentificadorCliente &&
                    x.DepositoId == GrvPersistencia.IdentificadorDeposito &&
                    x.FaturamentoProdutoId == GrvPersistencia.CodigoProduto &&
                    (
                        (!string.IsNullOrWhiteSpace(GrvPersistencia.Placa) &&
                         x.Placa == GrvPersistencia.Placa)
                        ||
                        (!string.IsNullOrWhiteSpace(GrvPersistencia.Chassi) &&
                         x.Chassi == GrvPersistencia.Chassi)
                    ), cancellationToken: ct);

            if (grv is not null && grv.StatusOperacaoId != "E" && grv.StatusOperacaoId != "7")
            {
                bool isPlacaDuplicada = !string.IsNullOrWhiteSpace(GrvPersistencia.Placa) &&
                                        string.Equals(grv.Placa, GrvPersistencia.Placa,
                                            StringComparison.OrdinalIgnoreCase);
                bool isChassiDuplicado = !string.IsNullOrWhiteSpace(GrvPersistencia.Chassi) &&
                                         string.Equals(grv.Chassi, GrvPersistencia.Chassi,
                                             StringComparison.OrdinalIgnoreCase);

                if (isPlacaDuplicada && isChassiDuplicado)
                {
                    ResultView.AvisosImpeditivos.Add("Esse GRV já existe (Placa e Chassi já cadastrados)");
                }
                else if (isPlacaDuplicada)
                {
                    ResultView.AvisosImpeditivos.Add("Esse GRV já existe (Placa já cadastrada)");
                }
                else if (isChassiDuplicado)
                {
                    ResultView.AvisosImpeditivos.Add("Esse GRV já existe (Chassi já cadastrado)");
                }
                else
                {
                    ResultView.AvisosImpeditivos.Add("Esse GRV já existe");
                }

                ResultView.AvisosInformativos.Add($"{grv.NumeroFormularioGrv}");
            }

            #endregion Consultas

            if (ResultView.AvisosImpeditivos.Count > 0)
            {
                ResultView.HtmlStatusCode = HtmlStatusCodeEnum.BadRequest;
            }
            else
            {
                ResultView.HtmlStatusCode = HtmlStatusCodeEnum.Ok;
            }

            return ResultView;
        }
    }
}