using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using WebZi.Plataform.CrossCutting.Contacts;
using WebZi.Plataform.CrossCutting.Documents;
using WebZi.Plataform.CrossCutting.Localizacao;
using WebZi.Plataform.CrossCutting.Number;
using WebZi.Plataform.CrossCutting.Strings;
using WebZi.Plataform.CrossCutting.Veiculo;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.WebServices;
using WebZi.Plataform.Data.Services.Cliente;
using WebZi.Plataform.Data.Services.Deposito;
using WebZi.Plataform.Data.Services.Faturamento;
using WebZi.Plataform.Data.Services.Localizacao;
using WebZi.Plataform.Data.Services.Sistema;
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
using WebZi.Plataform.Domain.Services.Usuario;
using WebZi.Plataform.Domain.ViewModel;
using WebZi.Plataform.Domain.ViewModel.Generic;
using WebZi.Plataform.Domain.ViewModel.GRV;
using WebZi.Plataform.Domain.ViewModel.GRV.Cadastro;
using WebZi.Plataform.Domain.ViewModel.GRV.Pesquisa;
using WebZi.Plataform.Domain.ViewModel.Localizacao;
using WebZi.Plataform.Domain.ViewModel.Usuario;
using WebZi.Plataform.Domain.Views.Usuario;
using Z.EntityFramework.Plus;
using WebZi.Plataform.Domain.Models.WebServices.Boleto;

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

        public GrvService(AppDbContext context, IMapper mapper, IServiceProvider provider, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _mapper = mapper;
            _provider = provider;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<MensagemViewModel> CreateAssinaturaAgenteAsync(int GrvId, int UsuarioId, byte[] Imagem)
        {
            MensagemViewModel ResultView = ValidateInputGrv(GrvId, UsuarioId);

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
                return MensagemViewHelper.SetBadRequest($"O Status atual deste GRV não permite o envio da Imagem da Assinatura do Agente. Status atual: {Grv.StatusOperacao.Descricao}");
            }

            BucketArquivoModel BucketArquivo = await _context.BucketArquivo
                .Include(x => x.BucketNomeTabelaOrigem)
                .Where(x => x.TabelaOrigemId == GrvId && x.BucketNomeTabelaOrigem.Codigo == "GRVASSINAAGENTE")
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (BucketArquivo != null)
            {
                return MensagemViewHelper.SetBadRequest("Já existe uma Imagem da Assinatura do Agente cadastrada");
            }

            new BucketService(_context, _httpClientFactory)
                .SendFile("GRVASSINAAGENTE", GrvId, UsuarioId, Imagem);

            return MensagemViewHelper.SetCreateSuccess();
        }

        public async Task<MensagemViewModel> CreateAssinaturaCondutorAsync(int GrvId, int UsuarioId, byte[] Imagem)
        {
            MensagemViewModel ResultView = ValidateInputGrv(GrvId, UsuarioId);

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
                return MensagemViewHelper.SetBadRequest($"O Status atual deste GRV não permite o envio da Imagem da Assinatura do Condutor. Status atual: {Grv.StatusOperacao.Descricao}");
            }

            BucketArquivoModel BucketArquivo = await _context.BucketArquivo
                .Include(x => x.BucketNomeTabelaOrigem)
                .Where(x => x.TabelaOrigemId == GrvId && x.BucketNomeTabelaOrigem.Codigo == "GRVASSINACONDUT")
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (BucketArquivo != null)
            {
                return MensagemViewHelper.SetBadRequest("Já existe uma Imagem da Assinatura do Condutor cadastrada");
            }

            new BucketService(_context, _httpClientFactory)
                .SendFile("GRVASSINACONDUT", GrvId, UsuarioId, Imagem);

            return MensagemViewHelper.SetCreateSuccess();
        }

        public ResultadoCadastroGrvViewModel CreateGrv(CadastroGrvViewModel GrvPersistencia)
        {
            GrvModel Grv = new()
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

                NumeroFormularioGrv = GrvPersistencia.NumeroProcesso.Trim(),

                FaturamentoProdutoId = GrvPersistencia.CodigoProduto,

                MatriculaAutoridadeResponsavel = GrvPersistencia.MatriculaAutoridadeResponsavel.ToUpperTrim().ToNullIfEmpty(),

                NomeAutoridadeResponsavel = GrvPersistencia.NomeAutoridadeResponsavel.ToUpperTrim().ToNullIfEmpty(),

                Placa = GrvPersistencia.Placa.ToUpperTrim().ToNullIfEmpty(),

                Chassi = GrvPersistencia.Chassi.ToUpperTrim().ToNullIfEmpty(),

                Renavam = GrvPersistencia.Renavam.ToUpperTrim().ToNullIfEmpty(),

                Rfid = GrvPersistencia.Rfid.ToUpperTrim().ToNullIfEmpty(),

                EnderecoLocalizacaoVeiculoLogradouro = GrvPersistencia.EnderecoLocalizacaoVeiculoLogradouro.ToUpperTrim().ToNullIfEmpty(),

                EnderecoLocalizacaoVeiculoNumero = GrvPersistencia.EnderecoLocalizacaoVeiculoNumero.ToUpperTrim().ToNullIfEmpty(),

                EnderecoLocalizacaoVeiculoComplemento = GrvPersistencia.EnderecoLocalizacaoVeiculoComplemento.ToUpperTrim().ToNullIfEmpty(),

                EnderecoLocalizacaoVeiculoBairro = GrvPersistencia.EnderecoLocalizacaoVeiculoBairro.ToUpperTrim().ToNullIfEmpty(),

                EnderecoLocalizacaoVeiculoMunicipio = GrvPersistencia.EnderecoLocalizacaoVeiculoMunicipio.ToUpperTrim().ToNullIfEmpty(),

                EnderecoLocalizacaoVeiculoUF = GrvPersistencia.EnderecoLocalizacaoVeiculoUF.ToUpperTrim().ToNullIfEmpty(),

                EnderecoLocalizacaoVeiculoReferencia = GrvPersistencia.EnderecoLocalizacaoVeiculoReferencia.ToUpperTrim().ToNullIfEmpty(),

                EnderecoLocalizacaoVeiculoPontoReferencia = GrvPersistencia.EnderecoLocalizacaoVeiculoPontoReferencia.ToUpperTrim().ToNullIfEmpty(),

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

            Grv.Condutor.Email = Grv.Condutor.Email.ToLowerTrim().ToNullIfEmpty();

            TabelaGenericaModel AssinaturaCondutor = new TabelaGenericaService(_context)
                .GetById(GrvPersistencia.Condutor.IdentificadorAssinaturaCondutor);

            Grv.Condutor.StatusAssinaturaCondutor = AssinaturaCondutor.ValorCadastro;

            if (!string.IsNullOrWhiteSpace(GrvPersistencia.EnderecoLocalizacaoVeiculoCEP))
            {
                EnderecoViewModel Endereco = new EnderecoService(_context, _mapper)
                    .GetByCEP(GrvPersistencia.EnderecoLocalizacaoVeiculoCEP
                    .Replace("-", ""));

                if (Endereco != null)
                {
                    Grv.EnderecoLocalizacaoVeiculoCEPId = Endereco.IdentificadorCEP;

                    Grv.EnderecoLocalizacaoVeiculoLogradouro = Endereco.Logradouro;

                    Grv.EnderecoLocalizacaoVeiculoBairro = Endereco.Bairro;

                    Grv.EnderecoLocalizacaoVeiculoMunicipio = Endereco.MunicipioPtbr;

                    Grv.EnderecoLocalizacaoVeiculoUF = Endereco.UF;
                }
            }

            if (GrvPersistencia.ListagemEnquadramentoInfracao?.Count > 0)
            {
                GrvPersistencia.ListagemEnquadramentoInfracao = GrvPersistencia.ListagemEnquadramentoInfracao
                    .OrderBy(x => x.NumeroInfracao)
                    .ToList();

                GrvPersistencia.ListagemEnquadramentoInfracao
                    .ForEach(x => x.NumeroInfracao = x.NumeroInfracao.ToUpperTrim().ToNullIfEmpty());

                Grv.ListagemEnquadramentoInfracao = _mapper
                    .Map<List<EnquadramentoInfracaoGrvModel>>(GrvPersistencia.ListagemEnquadramentoInfracao);
            }

            if (GrvPersistencia.ListagemLacre?.Count > 0)
            {
                GrvPersistencia.ListagemLacre = GrvPersistencia.ListagemLacre
                    .ConvertAll(x => x.ToUpperTrim().ToNullIfEmpty())
                    .OrderBy(x => x)
                    .ToList();

                Grv.ListagemLacre = new HashSet<LacreModel>();

                foreach (string item in GrvPersistencia.ListagemLacre)
                {
                    Grv.ListagemLacre.Add(new LacreModel { UsuarioCadastroId = GrvPersistencia.IdentificadorUsuario, Lacre = item });
                }
            }

            if (GrvPersistencia.ListagemEquipamentoOpcional?.Count > 0)
            {
                Grv.ListagemCondutorEquipamentoOpcional = new HashSet<CondutorEquipamentoOpcionalModel>();

                CondutorEquipamentoOpcionalModel CondutorEquipamentoOpcional = new();

                foreach (var item in GrvPersistencia.ListagemEquipamentoOpcional)
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

                    Grv.ListagemCondutorEquipamentoOpcional.Add(CondutorEquipamentoOpcional);
                }
            }

            ClienteDepositoModel ClienteDeposito = _context.ClienteDeposito
                .Include(x => x.Cliente)
                .Where(x => x.ClienteId == GrvPersistencia.IdentificadorCliente
                    && x.DepositoId == GrvPersistencia.IdentificadorDeposito)
                .AsNoTracking()
                .FirstOrDefault();

            if (ClienteDeposito.FlagCadastrarGrvComStatusOperacaoBloqueado == "S")
            {
                Grv.StatusOperacaoId = "B";
            }

            ResultadoCadastroGrvViewModel ResultView = new();

            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Grv.Add(Grv);

                    _context.SaveChanges();

                    if (ClienteDeposito.Cliente.FlagClientePossuiCodigoIdentificacao == "S")
                    {
                        ClienteCodigoIdentificacaoModel ClienteCodigoIdentificacao = new()
                        {
                            GrvId = Grv.GrvId,

                            UsuarioCadastroId = GrvPersistencia.IdentificadorUsuario,

                            CodigoIdentificacao = GrvPersistencia.CodigoIdentificacaoCliente
                        };

                        _context.ClienteCodigoIdentificacao.Add(ClienteCodigoIdentificacao);

                        _context.SaveChanges();
                    }

                    ResultView.IdentificadorGrv = Grv.GrvId;

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                    return ResultView;
                }
            }

            if (GrvPersistencia.ListagemDocumentoCondutor?.Count > 0)
            {
                CreateDocumentosCondutor(Grv.GrvId, Grv.UsuarioCadastroId, GrvPersistencia.ListagemDocumentoCondutor);
            }

            if (GrvPersistencia.ListagemFoto?.Count > 0)
            {
                new BucketService(_context, _httpClientFactory)
                    .SendFiles("GRVFOTOSVEICCAD", Grv.GrvId, Grv.UsuarioCadastroId, GrvPersistencia.ListagemFoto);
            }

            if (GrvPersistencia.ImagemAssinaturaAgente != null)
            {
                new BucketService(_context, _httpClientFactory)
                    .SendFile("GRVASSINAAGENTE", Grv.GrvId, Grv.UsuarioCadastroId, GrvPersistencia.ImagemAssinaturaAgente);
            }

            if (GrvPersistencia.ImagemAssinaturaCondutor != null)
            {
                new BucketService(_context, _httpClientFactory)
                    .SendFile("GRVASSINACONDUT", Grv.GrvId, Grv.UsuarioCadastroId, GrvPersistencia.ImagemAssinaturaAgente);
            }

            ResultView.Mensagem = MensagemViewHelper.SetCreateSuccess();

            return ResultView;
        }

        private void CreateDocumentosCondutor(int GrvId, int UsuarioId, List<CadastroCondutorDocumentoViewModel> ListagemDocumentoCondutor)
        {
            List<BucketListaCadastroModel> Files = new();

            CondutorDocumentoModel CondutorDocumento;

            foreach (CadastroCondutorDocumentoViewModel item in ListagemDocumentoCondutor)
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

        public MensagemViewModel CreateDocumentosCondutor(CadastroCondutorDocumentoViewModelList ListagemDocumentoCondutor)
        {
            MensagemViewModel ResultView = ValidateInputGrv(ListagemDocumentoCondutor.IdentificadorGrv, ListagemDocumentoCondutor.IdentificadorUsuario);

            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            if (ListagemDocumentoCondutor.ListagemDocumentoCondutor?.Count == 0)
            {
                return MensagemViewHelper.SetBadRequest("Nenhuma imagem enviada para a API");
            }

            GrvModel Grv = GetById(ListagemDocumentoCondutor.IdentificadorGrv);

            if (!new[] { "G", "V", "L", "U", "T", "R", "E", "B", "D", "1", "2", "3", "4" }.Contains(Grv.StatusOperacao.StatusOperacaoId))
            {
                return MensagemViewHelper.SetBadRequest($"O Status atual deste GRV não permite o envio de Fotos. Status atual: {Grv.StatusOperacao.Descricao}");
            }

            CreateDocumentosCondutor(ListagemDocumentoCondutor.IdentificadorGrv, ListagemDocumentoCondutor.IdentificadorUsuario, ListagemDocumentoCondutor.ListagemDocumentoCondutor);

            return MensagemViewHelper.SetCreateSuccess(ListagemDocumentoCondutor.ListagemDocumentoCondutor.Count);
        }

        public MensagemViewModel CreateFotos(CadastroFotoGrvViewModel Fotos)
        {
            MensagemViewModel ResultView = ValidateInputGrv(Fotos.IdentificadorGrv, Fotos.IdentificadorUsuario);

            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            if (Fotos.Fotos.Count == 0)
            {
                return MensagemViewHelper.SetBadRequest("Nenhuma imagem enviada para a API");
            }

            GrvModel Grv = GetById(Fotos.IdentificadorGrv);

            if (!new[] { "G", "V", "L", "U", "T", "R", "E", "B", "D", "1", "2", "3", "4" }.Contains(Grv.StatusOperacao.StatusOperacaoId))
            {
                return MensagemViewHelper.SetBadRequest($"O Status atual deste GRV não permite o envio de Fotos. Status atual: {Grv.StatusOperacao.Descricao}");
            }

            new BucketService(_context, _httpClientFactory)
                .SendFiles("GRVFOTOSVEICCAD", Fotos.IdentificadorGrv, Fotos.IdentificadorUsuario, Fotos.Fotos);

            return MensagemViewHelper.SetCreateSuccess(Fotos.Fotos.Count);
        }

        public async Task<MensagemViewModel> CreateLacresAsync(int GrvId, int UsuarioId, List<string> ListagemLacre)
        {
            MensagemViewModel ResultView = ValidateInputGrv(GrvId, UsuarioId);

            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            if (ListagemLacre.Count == 0)
            {
                return MensagemViewHelper.SetBadRequest("Informe os Lacres");
            }

            ListagemLacre = ListagemLacre
                .ConvertAll(x => x.ToUpperTrim().ToNullIfEmpty())
                .Distinct()
                .OrderBy(x => x)
                .ToList();

            GrvModel Grv = await GetByIdAsync(GrvId);

            if (!new[] { "E", "G", "L", "R", "T", "U", "V" }.Contains(Grv.StatusOperacaoId))
            {
                return MensagemViewHelper.SetBadRequest($"O Status atual deste GRV não permite o cadastro de Lacres. Status atual: {Grv.StatusOperacao.Descricao}");
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

        public async Task<MensagemViewModel> DeleteAssinaturaAgenteAsync(int GrvId, int UsuarioId)
        {
            MensagemViewModel ResultView = ValidateInputGrv(GrvId, UsuarioId);

            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            GrvModel Grv = await GetByIdAsync(GrvId);

            if (Grv.StatusOperacao.StatusOperacaoId != "G")
            {
                return MensagemViewHelper.SetBadRequest($"O Status atual deste GRV não permite a exclusão da Imagem da Assinatura do Agente. Status atual: {Grv.StatusOperacao.Descricao}");
            }

            BucketArquivoModel BucketArquivo = await _context.BucketArquivo
                .Include(x => x.BucketNomeTabelaOrigem)
                .Where(x => x.TabelaOrigemId == GrvId && x.BucketNomeTabelaOrigem.Codigo == "GRVASSINAAGENTE")
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (BucketArquivo == null)
            {
                return MensagemViewHelper.SetBadRequest("Registro da Imagem da Assinatura do Agente inexistente");
            }

            new BucketService(_context, _httpClientFactory)
                .DeleteFile(BucketArquivo.RepositorioArquivoId);

            return MensagemViewHelper.SetDeleteSuccess();
        }

        public async Task<MensagemViewModel> DeleteAssinaturaCondutorAsync(int GrvId, int UsuarioId)
        {
            MensagemViewModel ResultView = ValidateInputGrv(GrvId, UsuarioId);

            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            GrvModel Grv = await GetByIdAsync(GrvId);

            if (Grv.StatusOperacao.StatusOperacaoId != "G")
            {
                return MensagemViewHelper.SetBadRequest($"O Status atual deste GRV não permite a exclusão da Imagem da Assinatura do Condutor. Status atual: {Grv.StatusOperacao.Descricao}");
            }

            BucketArquivoModel BucketArquivo = await _context.BucketArquivo
                .Include(x => x.BucketNomeTabelaOrigem)
                .Where(x => x.TabelaOrigemId == GrvId && x.BucketNomeTabelaOrigem.Codigo == "GRVASSINACondutor")
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (BucketArquivo == null)
            {
                return MensagemViewHelper.SetBadRequest("Registro da Imagem da Assinatura do Condutor inexistente");
            }

            new BucketService(_context, _httpClientFactory)
                .DeleteFile(BucketArquivo.RepositorioArquivoId);

            return MensagemViewHelper.SetDeleteSuccess();
        }

        public async Task<MensagemViewModel> DeleteFotosAsync(int GrvId, int UsuarioId, List<int> ListagemTabelaOrigemId)
        {
            MensagemViewModel ResultView = ValidateInputGrv(GrvId, UsuarioId);

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
                return MensagemViewHelper.SetBadRequest($"O Status atual deste GRV não permite a exclusão de Fotos. Status atual: {Grv.StatusOperacao.Descricao}");
            }

            List<BucketArquivoModel> BucketArquivos = await _context.BucketArquivo
                .Include(x => x.BucketNomeTabelaOrigem)
                .Where(x => x.TabelaOrigemId != GrvId
                         && ListagemTabelaOrigemId.Contains(x.RepositorioArquivoId)
                         && x.BucketNomeTabelaOrigem.Codigo == "GRVFOTOSVEICCAD")
                .AsNoTracking()
                .ToListAsync();

            if (BucketArquivos?.Count > 0)
            {
                List<string> erros = new()
                {
                    $"A(s) seguinte(s) Fotos não pertencem ao GRV {GrvId}:"
                };

                foreach (BucketArquivoModel BucketArquivo in BucketArquivos)
                {
                    erros.Add(BucketArquivo.RepositorioArquivoId.ToString());
                }

                return MensagemViewHelper.SetBadRequest(erros);
            }

            new BucketService(_context, _httpClientFactory)
                .DeleteFiles("GRVFOTOSVEICCAD", ListagemTabelaOrigemId);

            return MensagemViewHelper.SetFound(BucketArquivos.Count, "Foto(s) excluída(s) com sucesso");
        }

        public async Task<MensagemViewModel> DeleteGrvAsync(string NumeroFormularioGrv, string FaturamentoProdutoId, int ClienteId, int DepositoId, string Login, string Senha)
        {
            UsuarioViewModel Usuario = await new UsuarioService(_context, _mapper)
                .GetByLoginAsync(Login, Senha);

            if (Usuario.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return Usuario.Mensagem;
            }

            MensagemViewModel ResultView = ValidateInputGrv(NumeroFormularioGrv, FaturamentoProdutoId, ClienteId, DepositoId, Usuario.IdentificadorUsuario);

            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            GrvModel Grv = await _context.Grv
                .Where(x => x.FaturamentoProdutoId == FaturamentoProdutoId
                         && x.ClienteId == ClienteId
                         && x.DepositoId == DepositoId
                         && x.NumeroFormularioGrv == NumeroFormularioGrv)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return await DeleteGrvAsync(Grv.GrvId, Usuario.IdentificadorUsuario);
        }

        public async Task<MensagemViewModel> DeleteGrvAsync(int GrvId, string Login, string Senha)
        {
            UsuarioViewModel Usuario = await new UsuarioService(_context, _mapper)
                .GetByLoginAsync(Login, Senha);

            if (Usuario.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return Usuario.Mensagem;
            }
            else if (GrvId <= 0)
            {
                return MensagemViewHelper.SetBadRequest(MensagemPadraoEnum.IdentificadorGrvInvalido);
            }

            MensagemViewModel ResultView = ValidateInputGrv(GrvId, Usuario.IdentificadorUsuario);

            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            return await DeleteGrvAsync(GrvId, Usuario.IdentificadorUsuario);
        }

        private async Task<MensagemViewModel> DeleteGrvAsync(int GrvId, int UsuarioId)
        {
            MensagemViewModel ResultView = new();

            UsuarioPermissaoModel UsuarioPermissao = await _context.UsuarioPermissao
                .Include(x => x.TipoPermissao)
                .Where(x => x.UsuarioId == UsuarioId
                         && x.TipoPermissao.Codigo == "EXCLUSAOGRV")
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (UsuarioPermissao == null)
            {
                return MensagemViewHelper.SetUnauthorized("Usuário não possui permissão para excluir GRV");
            }

            GrvModel Grv = await _context.Grv
                .Include(x => x.StatusOperacao)
                .Include(x => x.ListagemCondutorDocumento)
                .Include(x => x.Atendimento)
                .Include(x => x.Liberacao)
                .Where(g => g.GrvId == GrvId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Grv == null)
            {
                return MensagemViewHelper.SetNotFound(MensagemPadraoEnum.NaoEncontradoGrv);
            }
            else if (!new[] { "M", "P", "G", "V" }.Contains(Grv.StatusOperacaoId))
            {
                return MensagemViewHelper.SetBadRequest($"O Status atual deste GRV não permite a exclusão. Status atual: {Grv.StatusOperacao.Descricao}");
            }

            List<FaturamentoModel> Faturamentos = null;

            if (Grv.Atendimento != null)
            {
                Faturamentos = _context.Faturamento
                    .Include(x => x.FaturamentoBoletos)
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
                        .Where(w => w.LiberacaoId == Grv.LiberacaoId)
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
                .DeleteFiles("GRVFOTOSVEICCAD", GrvId);

            new BucketService(_context, _httpClientFactory)
                .DeleteFiles("GGVFOTOSVEICCAD", GrvId);

            if (Grv.ListagemCondutorDocumento?.Count > 0)
            {
                new BucketService(_context, _httpClientFactory)
                    .DeleteFiles("GRV_DOCCONDUTOR", Grv.ListagemCondutorDocumento
                        .Select(x => x.CondutorDocumentoId)
                        .ToList());
            }

            if (Grv.Atendimento != null)
            {
                new BucketService(_context, _httpClientFactory)
                    .DeleteFiles("ATENDIMFOTORESP", Grv.Atendimento.AtendimentoId);

                if (Faturamentos?.Count > 0)
                {
                    foreach (FaturamentoModel Faturamento in Faturamentos)
                    {
                        if (Faturamento.FaturamentoBoletos?.Count > 0)
                        {
                            foreach (BoletoModel FaturamentoBoleto in Faturamento.FaturamentoBoletos)
                            {
                                new BucketService(_context, _httpClientFactory)
                                    .DeleteFiles("FATURAMENBOLETO", FaturamentoBoleto.FaturamentoBoletoId);
                            }
                        }
                    }
                }
            }

            return MensagemViewHelper.SetOk("GRV excluído com sucesso");
        }

        public async Task<MensagemViewModel> DeleteLacresAsync(int GrvId, int UsuarioId, List<int> ListagemIdentificadorLacre)
        {
            MensagemViewModel ResultView = ValidateInputGrv(GrvId, UsuarioId);

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
                return MensagemViewHelper.SetBadRequest($"O Status atual deste GRV não permite a exclusão de Lacres. Status atual: {Grv.StatusOperacao.Descricao}");
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
                    $"O(s) seguinte(s) Lacre(s) pertencem à outro GRV:"
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

            return MensagemViewHelper.SetDeleteSuccess(ListagemIdentificadorLacre.Count, "Lacre(s) excluído(s) com sucesso");
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
                .Where(x => x.GrvId == GrvId)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<GrvViewModelList> GetByIdAsync(int GrvId, int UsuarioId)
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

            ResultView.Listagem.Add(_mapper.Map<GrvViewModel>(Grv));

            ResultView.Mensagem = MensagemViewHelper.SetFound();

            return ResultView;
        }

        public async Task<GrvViewModelList> GetByNumeroFormularioGrvAsync(string NumeroFormularioGrv, string FaturamentoProdutoId, int ClienteId, int DepositoId, int UsuarioId)
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
                .Where(x => x.NumeroFormularioGrv == NumeroFormularioGrv
                         && x.ClienteId == ClienteId
                         && x.DepositoId == DepositoId
                         && x.FaturamentoProdutoId == FaturamentoProdutoId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

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

            ResultView.Listagem.Add(_mapper.Map<GrvViewModel>(Grv));

            ResultView.Mensagem = MensagemViewHelper.SetFound();

            return ResultView;
        }

        public async Task<ImageViewModelList> GetAssinaturaAgenteAsync(int GrvId, int UsuarioId)
        {
            ImageViewModelList ResultView = new()
            {
                Mensagem = ValidateInputGrv(GrvId, UsuarioId)
            };

            if (ResultView.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            BucketArquivoModel BucketArquivo = await _context.BucketArquivo
                .Include(x => x.BucketNomeTabelaOrigem)
                .Where(x => x.TabelaOrigemId == GrvId && x.BucketNomeTabelaOrigem.Codigo == "GRVASSINAAGENTE")
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (BucketArquivo == null)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Registro da Imagem da Assinatura do Agente inexistente");

                return ResultView;
            }

            return await new BucketService(_context, _httpClientFactory)
                .DownloadFileAsync("GRVASSINAAGENTE", GrvId);
        }

        public async Task<ImageViewModelList> GetAssinaturaCondutorAsync(int GrvId, int UsuarioId)
        {
            ImageViewModelList ResultView = new()
            {
                Mensagem = ValidateInputGrv(GrvId, UsuarioId)
            };

            if (ResultView.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            BucketArquivoModel BucketArquivo = await _context.BucketArquivo
                .Include(x => x.BucketNomeTabelaOrigem)
                .Where(x => x.TabelaOrigemId == GrvId && x.BucketNomeTabelaOrigem.Codigo == "GRVASSINACONDUT")
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (BucketArquivo == null)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Registro da Imagem da Assinatura do Condutor inexistente");

                return ResultView;
            }

            return await new BucketService(_context, _httpClientFactory)
                .DownloadFileAsync("GRVASSINACONDUT", GrvId);
        }

        public async Task<StatusOperacaoViewModelList> GetStatusOperacaoByIdAsync(string StatusOperacaoId)
        {
            StatusOperacaoViewModelList ResultView = new();

            if (string.IsNullOrWhiteSpace(StatusOperacaoId))
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Identificador do Status da Operação inválido");

                return ResultView;
            }

            StatusOperacaoModel result = await _context.StatusOperacao
                .Where(w => w.StatusOperacaoId == StatusOperacaoId.ToUpperTrim().ToNullIfEmpty())
                .AsNoTracking()
                .FirstOrDefaultAsync();

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

        public async Task<AutoridadeResponsavelViewModelList> ListAutoridadeResponsavelAsync(string UF)
        {
            AutoridadeResponsavelViewModelList ResultView = new();

            if (string.IsNullOrWhiteSpace(UF))
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Informe a Unidade Federativa");

                return ResultView;
            }
            else if (!LocalizacaoHelper.IsUF(UF))
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Unidade Federativa inválida");

                return ResultView;
            }

            OrgaoEmissorModel result = await _context.OrgaoEmissor
                .Include(i => i.AutoridadesResponsaveis)
                .Where(w => w.UF == UF.ToUpperTrim().ToNullIfEmpty())
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (result == null)
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound("Unidade Federativa sem Órgão Emissor cadastrado");

                return ResultView;
            }

            if (result.AutoridadesResponsaveis?.Count > 0)
            {
                ResultView.Listagem = _mapper.Map<List<AutoridadeResponsavelViewModel>>(result.AutoridadesResponsaveis
                    .OrderBy(o => o.Divisao)
                    .ToList());

                ResultView.Mensagem = MensagemViewHelper.SetFound(result.AutoridadesResponsaveis.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();
            }

            return ResultView;
        }

        public async Task<ImageViewModelList> ListDocumentosCondutorAsync(int GrvId, int UsuarioId)
        {
            ImageViewModelList ResultView = new()
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

        public async Task<EnquadramentoInfracaoViewModelList> ListEnquadramentoInfracaoAsync()
        {
            EnquadramentoInfracaoViewModelList ResultView = new();

            List<EnquadramentoInfracaoModel> result = await _context.EnquadramentoInfracao
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count > 0)
            {
                ResultView.Listagem = _mapper.Map<List<EnquadramentoInfracaoViewModel>>(result
                    .OrderBy(o => o.Descricao.Trim())
                    .ToList());

                ResultView.Mensagem = MensagemViewHelper.SetFound(result.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();
            }

            return ResultView;
        }

        public async Task<ImageViewModelList> ListFotoAsync(int GrvId, int UsuarioId)
        {
            ImageViewModelList ResultView = new()
            {
                Mensagem = ValidateInputGrv(GrvId, UsuarioId)
            };

            if (ResultView.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            return await new BucketService(_context, _httpClientFactory)
                .DownloadFileAsync("GRVFOTOSVEICCAD", GrvId);
        }

        public async Task<GrvPesquisaDadosMestresViewModel> ListItemPesquisaAsync(int UsuarioId)
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
                ResultView.Listagem = _mapper.Map<List<LacreViewModel>>(result
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

        public async Task<MotivoApreensaoViewModelList> ListMotivoApreensaoAsync()
        {
            MotivoApreensaoViewModelList ResultView = new();

            List<MotivoApreensaoModel> result = await _context.MotivoApreensao
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count > 0)
            {
                ResultView.Listagem = _mapper.Map<List<MotivoApreensaoViewModel>>(result
                    .OrderBy(o => o.Descricao)
                    .ToList());

                ResultView.Mensagem = MensagemViewHelper.SetFound(result.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();
            }

            return ResultView;
        }

        public async Task<StatusOperacaoViewModelList> ListStatusOperacaoAsync()
        {
            StatusOperacaoViewModelList ResultView = new();

            List<StatusOperacaoModel> result = await _context.StatusOperacao
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count > 0)
            {
                result = result
                    .OrderBy(o => o.Descricao)
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

        public async Task<GrvPesquisaResultViewModelList> SearchAsync(GrvPesquisaInputViewModel GrvPesquisa)
        {
            List<string> erros = new();

            if (GrvPesquisa.ListagemCodigoProduto?.Count == 0)
            {
                erros.Add("Informe ao menos um Código do Produto");
            }
            else
            {
                if (GrvPesquisa.ListagemCodigoProduto.Where(string.IsNullOrWhiteSpace).ToList().Count > 0)
                {
                    erros.Add("Na listagem do Código do Produto, existem itens vazios");
                }
                else
                {
                    List<string> Produtos = await _context.FaturamentoProduto
                        .Select(x => x.FaturamentoProdutoId)
                        .AsNoTracking()
                        .ToListAsync();

                    foreach (string Codigo in GrvPesquisa.ListagemCodigoProduto)
                    {
                        if (Produtos.FirstOrDefault(f => f == Codigo.ToUpperTrim().ToNullIfEmpty()) == null)
                        {
                            erros.Add($"{MensagemPadraoEnum.NaoEncontradoFaturamentoProduto}: {Codigo}");
                        }
                    }
                }
            }

            if (GrvPesquisa.ListagemStatusOperacao?.Count > 0)
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

                    if (StatusOperacoes.FirstOrDefault(f => f == StatusOperacao.ToUpperTrim().ToNullIfEmpty()) == null)
                    {
                        erros.Add($"Status Operação inexistente: {StatusOperacao}");
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(GrvPesquisa.NumeroProcesso) &&
                (!NumberHelper.IsNumber(GrvPesquisa.NumeroProcesso) || Convert.ToInt64(GrvPesquisa.NumeroProcesso) <= 0))
            {
                erros.Add(MensagemPadraoEnum.NumeroProcessoInvalido);
            }

            if (!string.IsNullOrWhiteSpace(GrvPesquisa.FlagVeiculoNaoIdentificado))
            {
                if (!string.IsNullOrWhiteSpace(GrvPesquisa.PlacaVeiculo))
                {
                    erros.Add("Ao informar que o Veículo não possui identificação, não informe a Placa");
                }

                if (!string.IsNullOrWhiteSpace(GrvPesquisa.Chassi))
                {
                    erros.Add("Ao informar que o Veículo não possui identificação, não informe o Chassi");
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(GrvPesquisa.PlacaVeiculo) && !VeiculoHelper.IsPlaca(GrvPesquisa.PlacaVeiculo))
                {
                    erros.Add("Placa inválida");
                }

                if (!string.IsNullOrWhiteSpace(GrvPesquisa.Chassi) && !VeiculoHelper.IsChassi(GrvPesquisa.Chassi))
                {
                    erros.Add("Chassi inválido");
                }
            }

            if (GrvPesquisa.IdentificadorUsuario <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorUsuarioInvalido);
            }

            if (!GrvPesquisa.DataInicialRemocao.HasValue)
            {
                GrvPesquisa.DataInicialRemocao = DateTime.Now.AddDays(-180);
            }

            if (!GrvPesquisa.DataFinalRemocao.HasValue)
            {
                GrvPesquisa.DataFinalRemocao = DateTime.Now;
            }

            if (GrvPesquisa.DataInicialRemocao.Value.Date > GrvPesquisa.DataFinalRemocao.Value.Date)
            {
                erros.Add("A Data Inicial não pode ser maior do que a Data Final");
            }
            else if ((GrvPesquisa.DataFinalRemocao.Value.Date - GrvPesquisa.DataInicialRemocao.Value.Date).Days > 180)
            {
                erros.Add("O período de pesquisa não pode superar 180 dias");
            }

            GrvPesquisaResultViewModelList ResultView = new();

            if (erros.Count > 0)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest(erros);

                return ResultView;
            }

            List<GrvModel> result = await _context.Grv
                .Include(x => x.Cliente)
                .Include(x => x.Deposito)
                .Include(x => x.StatusOperacao)
                .Include(x => x.UsuarioClienteDepositoGrv)
                .Where(x => x.UsuarioClienteDepositoGrv.UsuarioId == GrvPesquisa.IdentificadorUsuario &&
                             GrvPesquisa.ListagemCodigoProduto.Contains(x.FaturamentoProdutoId) &&
                             x.UsuarioClienteDepositoGrv.FaturamentoProdutoCodigo == x.FaturamentoProdutoId &&
                            (x.DataHoraRemocao.Date >= GrvPesquisa.DataInicialRemocao.Value.Date && x.DataHoraRemocao.Date <= GrvPesquisa.DataFinalRemocao.Value.Date) &&
                            (GrvPesquisa.ListagemCodigoProduto.Count > 0 ? GrvPesquisa.ListagemCodigoProduto.Contains(x.FaturamentoProdutoId) : true) &&
                            (GrvPesquisa.ListagemStatusOperacao.Count > 0 ? GrvPesquisa.ListagemStatusOperacao.Contains(x.StatusOperacaoId) : true) &&
                            (!string.IsNullOrWhiteSpace(GrvPesquisa.NumeroProcesso) ? x.NumeroFormularioGrv == GrvPesquisa.NumeroProcesso : true) &&
                            (!string.IsNullOrWhiteSpace(GrvPesquisa.PlacaVeiculo) ? x.Placa == GrvPesquisa.PlacaVeiculo : true) &&
                            (!string.IsNullOrWhiteSpace(GrvPesquisa.Chassi) ? x.Chassi == GrvPesquisa.Chassi : true) &&
                            (!string.IsNullOrWhiteSpace(GrvPesquisa.FlagVeiculoNaoIdentificado) ? x.FlagVeiculoNaoIdentificado == GrvPesquisa.FlagVeiculoNaoIdentificado : true) &&
                            (GrvPesquisa.IdentificadorCliente > 0 ? x.ClienteId == GrvPesquisa.IdentificadorCliente : true) &&
                            (GrvPesquisa.IdentificadorDeposito > 0 ? x.DepositoId == GrvPesquisa.IdentificadorDeposito : true))
                .OrderBy(x => Convert.ToInt64(x.NumeroFormularioGrv))
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
                    IdentificadorGrv = Grv.GrvId,

                    StatusOperacaoId = Grv.StatusOperacaoId,

                    NumeroProcesso = Grv.NumeroFormularioGrv,

                    Placa = Grv.Placa,

                    Chassi = Grv.Chassi,

                    StatusOperacao = Grv.StatusOperacao.Descricao,

                    DataHoraRemocao = Grv.DataHoraRemocao,

                    DataHoraGuarda = Grv.DataHoraGuarda.Value,

                    Cliente = Grv.Cliente.Nome,

                    Deposito = Grv.Deposito.Nome
                });
            }

            ResultView.Mensagem = MensagemViewHelper.SetFound(result.Count);

            return ResultView;
        }

        public MensagemViewModel ValidateInputGrv(int GrvId, int UsuarioId)
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

        public MensagemViewModel ValidateInputGrv(GrvModel Grv, int UsuarioId)
        {
            return ValidateInputGrv(Grv.GrvId, UsuarioId, Grv.NumeroFormularioGrv, Grv.FaturamentoProdutoId, Grv.ClienteId, Grv.DepositoId);
        }

        public MensagemViewModel ValidateInputGrv(string NumeroFormularioGrv, string FaturamentoProdutoId, int ClienteId, int DepositoId, int UsuarioId)
        {
            return ValidateInputGrv(0, UsuarioId, NumeroFormularioGrv, FaturamentoProdutoId, ClienteId, DepositoId);
        }

        private MensagemViewModel ValidateInputGrv(int GrvId, int UsuarioId, string NumeroFormularioGrv, string FaturamentoProdutoId, int ClienteId, int DepositoId)
        {
            if (!new UsuarioService(_context).IsUserActive(UsuarioId))
            {
                return MensagemViewHelper.SetUnauthorized();
            }

            List<string> erros = new();

            if (GrvId <= 0 && string.IsNullOrWhiteSpace(NumeroFormularioGrv))
            {
                erros.Add("Informe o Identificador do GRV ou o Número do Processo");
            }
            else if (GrvId <= 0)
            {
                if (!NumberHelper.IsNumber(NumeroFormularioGrv) || NumeroFormularioGrv.Length > 14 || Convert.ToInt64(NumeroFormularioGrv) <= 0)
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
                erros.Add("Primeiro é necessário informar o Identificador do Usuário");
            }

            if (erros.Count > 0)
            {
                return MensagemViewHelper.SetBadRequest(erros);
            }

            if (GrvId <= 0)
            {
                FaturamentoProdutoModel FaturamentoProduto = _context.FaturamentoProduto
                    .Where(x => x.FaturamentoProdutoId == FaturamentoProdutoId)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (FaturamentoProduto == null)
                {
                    return MensagemViewHelper.SetNotFound(MensagemPadraoEnum.NaoEncontradoFaturamentoProduto);
                }

                ClienteModel Cliente = _context.Cliente
                    .Where(x => x.ClienteId == ClienteId)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (Cliente == null)
                {
                    return MensagemViewHelper.SetNotFound(MensagemPadraoEnum.NaoEncontradoCliente);
                }

                DepositoModel Deposito = _context.Deposito
                    .Where(x => x.DepositoId == DepositoId)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (Deposito == null)
                {
                    return MensagemViewHelper.SetNotFound(MensagemPadraoEnum.NaoEncontradoDeposito);
                }

                ClienteDepositoModel ClienteDeposito = _context.ClienteDeposito
                    .Where(x => x.ClienteId == ClienteId
                        && x.DepositoId == DepositoId)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (ClienteDeposito == null)
                {
                    return MensagemViewHelper.SetNotFound("Este Cliente e Depósito não são associados");
                }
            }

            ViewUsuarioClienteDepositoGrvModel Grv = new();

            if (GrvId > 0)
            {
                Grv = _context.ViewUsuarioClienteDepositoGrv
                    .Where(x => x.GrvId == GrvId)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (Grv == null)
                {
                    return MensagemViewHelper.SetUnauthorized("Usuário não possui acesso ao GRV ou o GRV não existe");
                }
            }
            else
            {
                Grv = _context.ViewUsuarioClienteDepositoGrv
                    .Where(x => x.FaturamentoProdutoCodigo == FaturamentoProdutoId
                             && x.ClienteId == ClienteId
                             && x.DepositoId == DepositoId
                             && x.NumeroFormularioGrv == NumeroFormularioGrv)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (Grv == null)
                {
                    return MensagemViewHelper.SetUnauthorized("Usuário não possui acesso ao GRV ou o GRV não existe");
                }
            }

            return MensagemViewHelper.SetOk();
        }

        public async Task<MensagemViewModel> CheckAlteracaoStatusGrvAsync(int GrvId, string StatusOperacaoId, int UsuarioId)
        {
            MensagemViewModel ResultView = ValidateInputGrv(GrvId, UsuarioId);

            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            GrvModel Grv = await _context.Grv
                .Include(x => x.StatusOperacao)
                .Where(x => x.GrvId == GrvId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Grv == null)
            {
                return MensagemViewHelper.SetNotFound(MensagemPadraoEnum.NaoEncontradoGrv);
            }

            StatusOperacaoModel StatusOperacao = await _context.StatusOperacao
                .Where(x => x.StatusOperacaoId == StatusOperacaoId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (StatusOperacao == null)
            {
                return MensagemViewHelper.SetNotFound(MensagemPadraoEnum.NaoEncontradoStatusOperacao);
            }
            else if (Grv.StatusOperacao.StatusOperacaoId != StatusOperacaoId)
            {
                return MensagemViewHelper.SetBadRequest($"O Status da Operação foi alterado de \"{Grv.StatusOperacao.Descricao.ToUpper()}\" para \"{StatusOperacao.Descricao.ToUpper()}\"");
            }

            return MensagemViewHelper.SetOk("O Status da Operação não foi alterado");
        }

        public async Task<MensagemViewModel> CheckInformacoesPersistenciaAsync(CadastroGrvViewModel GrvPersistencia)
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

            if (string.IsNullOrWhiteSpace(GrvPersistencia.NumeroProcesso))
            {
                erros.Add(MensagemPadraoEnum.InformeNumeroProcesso);
            }
            else if (!NumberHelper.IsNumber(GrvPersistencia.NumeroProcesso) || Convert.ToInt64(GrvPersistencia.NumeroProcesso) <= 0)
            {
                erros.Add(MensagemPadraoEnum.NumeroProcessoInvalido);
            }

            if (GrvPersistencia.FlagVeiculoNaoIdentificado == "S")
            {
                if (!string.IsNullOrWhiteSpace(GrvPersistencia.Placa) || !string.IsNullOrWhiteSpace(GrvPersistencia.Chassi))
                {
                    erros.Add("Ao informar que o Veículo não foi identificado, não se deve informar a Placa nem o Chassi");
                }
            }
            else if (GrvPersistencia.FlagVeiculoSemRegistro == "S")
            {
                if (!string.IsNullOrWhiteSpace(GrvPersistencia.Placa))
                {
                    erros.Add("Ao informar que o Veículo não possui registro, não se deve informar a Placa");
                }
                else if (string.IsNullOrWhiteSpace(GrvPersistencia.Chassi))
                {
                    erros.Add("Informe o Chassi");
                }
                else if (!VeiculoHelper.IsChassi(GrvPersistencia.Chassi))
                {
                    erros.Add("Chassi inválido");
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(GrvPersistencia.Placa))
                {
                    erros.Add("Informe a Placa");
                }
                else if (!VeiculoHelper.IsPlaca(GrvPersistencia.Placa))
                {
                    erros.Add("Placa inválida");
                }

                if (string.IsNullOrWhiteSpace(GrvPersistencia.Chassi))
                {
                    erros.Add("Informe o Chassi");
                }
                else if (!VeiculoHelper.IsChassi(GrvPersistencia.Chassi))
                {
                    erros.Add("Chassi inválido");
                }
            }

            if (GrvPersistencia.FlagVeiculoSemRegistro == "S")
            {
                if (string.IsNullOrWhiteSpace(GrvPersistencia.Chassi))
                {
                    erros.Add("Informe o Chassi");
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(GrvPersistencia.Chassi) && !VeiculoHelper.IsChassi(GrvPersistencia.Chassi))
                    {
                        erros.Add("Chassi inválido");
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(GrvPersistencia.EnderecoLocalizacaoVeiculoCEP))
            {
                if (!LocalizacaoHelper.IsCEP(GrvPersistencia.EnderecoLocalizacaoVeiculoCEP))
                {
                    erros.Add("CEP inválido");
                }
            }

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
                    erros.Add("Ao informar que a Chave ficou no Veículo, é necessário informar o Número/Código da Chave");
                }
            }

            if (GrvPersistencia.ListagemLacre?.Count == 0)
            {
                erros.Add("Informe os Lacres");
            }
            else
            {
                List<IGrouping<string, string>> Lacres = GrvPersistencia.ListagemLacre
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .GroupBy(x => x)
                    .Where(x => x.Count() > 1)
                    .ToList();

                if (Lacres.Count >= 1)
                {
                    erros.Add("Existem Lacres duplicados");
                }

                Lacres = GrvPersistencia.ListagemLacre
                    .Where(x => string.IsNullOrWhiteSpace(x))
                    .GroupBy(x => x)
                    .ToList();

                if (Lacres.Count >= 1)
                {
                    erros.Add("Existem Lacres não informados");
                }
            }

            if (GrvPersistencia.ListagemEquipamentoOpcional?.Count > 0)
            {
                if (GrvPersistencia.ListagemEquipamentoOpcional.Where(x => x.IdentificadorEquipamentoOpcional <= 0).ToList().Count > 0)
                {
                    erros.Add("Existe um ou mais Identificador do Equipamento Opcional inválido");
                }

                if (GrvPersistencia.ListagemEquipamentoOpcional.Where(x => x.FlagEquipamentoAvariado == "S" && (x.IdentificadorTipoAvaria <= 0 || x.IdentificadorTipoAvaria == null)).ToList().Count > 0)
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

            MensagemViewModel ResultView = new();

            ClienteModel Cliente = await _context.Cliente
                .Include(x => x.Endereco)
                .Where(x => x.ClienteId == GrvPersistencia.IdentificadorCliente)
                .AsNoTracking()
                .FirstOrDefaultAsync();

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
                .Where(x => x.DepositoId == GrvPersistencia.IdentificadorDeposito)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Deposito == null)
            {
                ResultView.AvisosImpeditivos.Add(MensagemPadraoEnum.NaoEncontradoDeposito);
            }

            ClienteDepositoModel ClienteDeposito = await _context.ClienteDeposito
                .Where(x => x.ClienteId == GrvPersistencia.IdentificadorCliente && x.DepositoId == GrvPersistencia.IdentificadorDeposito)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (ClienteDeposito == null)
            {
                ResultView.AvisosImpeditivos.Add("O Cliente e Depósito informados não são associados");
            }
            else if (ClienteDeposito.FlagCadastrarGrvComStatusOperacaoBloqueado == "S")
            {
                StatusOperacaoModel StatusOperacao = await _context.StatusOperacao
                    .Where(x => x.StatusOperacaoId == "B")
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                ResultView.AvisosInformativos.Add($"Esse GRV receberá o Status de Operação {StatusOperacao.Descricao} devido à configuração do Cliente");
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
                .Where(x => x.TipoVeiculoId == GrvPersistencia.IdentificadorTipoVeiculo)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (TipoVeiculo == null)
            {
                ResultView.AvisosImpeditivos.Add("Tipo do Veículo inexistente");
            }

            ReboquistaModel Reboquista = await _context.Reboquista
                .Where(x => x.ReboquistaId == GrvPersistencia.IdentificadorReboquista)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Reboquista == null)
            {
                ResultView.AvisosImpeditivos.Add("Reboquista inexistente");
            }

            ReboqueModel Reboque = await _context.Reboque
                .Where(x => x.ReboqueId == GrvPersistencia.IdentificadorReboque)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Reboque == null)
            {
                ResultView.AvisosImpeditivos.Add("Reboque inexistente");
            }

            AutoridadeResponsavelModel AutoridadeResponsavel = await _context.AutoridadeResponsavel
                .Include(x => x.OrgaoEmissor)
                .Where(x => x.AutoridadeResponsavelId == GrvPersistencia.IdentificadorAutoridadeResponsavel)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (AutoridadeResponsavel == null)
            {
                ResultView.AvisosImpeditivos.Add("Autoridade Responsável não encontrada");
            }
            else if (AutoridadeResponsavel.OrgaoEmissor.UF != Cliente.Endereco.UF)
            {
                ResultView.AvisosImpeditivos.Add($"A Autoridade Responsável ({AutoridadeResponsavel.OrgaoEmissor.UF}) informada não pertence a mesma Unidade Federativa do cadastro do Cliente {Cliente.Nome} ({Cliente.Endereco.UF})");
            }

            CorModel Cor = await _context.Cor
                .Where(x => x.CorId == GrvPersistencia.IdentificadorCor)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Cor == null)
            {
                ResultView.AvisosImpeditivos.Add("Cor não encontrada");
            }

            MarcaModeloModel MarcaModelo = await _context.MarcaModelo
                .Where(x => x.MarcaModeloId == GrvPersistencia.IdentificadorMarcaModelo)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (MarcaModelo == null)
            {
                ResultView.AvisosImpeditivos.Add("Marca/Modelo inexistente");
            }

            MotivoApreensaoModel MotivoApreensao = await _context.MotivoApreensao
                .Where(x => x.MotivoApreensaoId == GrvPersistencia.IdentificadorMotivoApreensao)
                .AsNoTracking()
                .FirstOrDefaultAsync();

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
                    List<IGrouping<decimal, decimal>> EnquadramentosInfracoes = GrvPersistencia.ListagemEnquadramentoInfracao
                        .Where(x => x.IdentificadorEnquadramentoInfracao <= 0)
                        .Select(x => x.IdentificadorEnquadramentoInfracao)
                        .GroupBy(x => x)
                        .ToList();

                    if (EnquadramentosInfracoes.Count >= 1)
                    {
                        ResultView.AvisosImpeditivos.Add("Existem Enquadramento da Infração com Identificador inválido");
                    }

                    EnquadramentosInfracoes = GrvPersistencia.ListagemEnquadramentoInfracao
                        .Where(x => x.IdentificadorEnquadramentoInfracao > 0)
                        .Select(x => x.IdentificadorEnquadramentoInfracao)
                        .GroupBy(x => x)
                        .Where(x => x.Count() > 1)
                        .ToList();

                    if (EnquadramentosInfracoes.Count >= 1)
                    {
                        ResultView.AvisosImpeditivos.Add("Existem Enquadramento da Infração duplicados");
                    }
                    else
                    {
                        if (GrvPersistencia.ListagemEnquadramentoInfracao.Exists(x => x.IdentificadorEnquadramentoInfracao > 0))
                        {
                            List<decimal> ids = GrvPersistencia.ListagemEnquadramentoInfracao
                                .Where(x => x.IdentificadorEnquadramentoInfracao > 0 /*&& x.IdentificadorEnquadramentoInfracao < 9999*/)
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
                .Where(x => x.FaturamentoProdutoId == GrvPersistencia.CodigoProduto)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Produtos == null)
            {
                ResultView.AvisosImpeditivos.Add(MensagemPadraoEnum.NaoEncontradoFaturamentoProduto);
            }

            if (!string.IsNullOrWhiteSpace(GrvPersistencia.EnderecoLocalizacaoVeiculoCEP))
            {
                if (await _context.CEP
                    .Where(x => x.CEP == GrvPersistencia.EnderecoLocalizacaoVeiculoCEP.Replace("-", ""))
                    .FirstOrDefaultAsync() == null)
                {
                    ResultView.AvisosImpeditivos.Add("CEP inexistente");
                }
            }

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

                if (IdentificadorTipoDocumentoIdentificacao.Where(x => TiposDocumentosIdentificacoes.All(y => y != x)).ToList().Count > 0)
                {
                    ResultView.AvisosImpeditivos.Add("Existem Identificador do Tipo de Documento de Identificação inválido");
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
                    ResultView.AvisosImpeditivos.Add("A listagem de Equipamento Opcional possui um ou mais Identificador inexistente");
                }
                else if (GrvPersistencia.IdentificadorTipoVeiculo > 0)
                {
                    int CountTipoVeiculoEquipamentoAssociacao = _context.TipoVeiculoEquipamentoAssociacao
                        .Where(x => EquipamentoOpcionalIds.Contains(x.EquipamentoOpcionalId) && x.TipoVeiculoId == GrvPersistencia.IdentificadorTipoVeiculo)
                        .AsNoTracking()
                        .Count();

                    if (CountTipoVeiculoEquipamentoAssociacao != ListagemEquipamentoOpcionalEncontrada)
                    {
                        ResultView.AvisosImpeditivos.Add("A listagem de Equipamento Opcional possui um ou mais Identificador não associado ao Tipo de Veículo");
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

                if (GrvPersistencia.ListagemEquipamentoOpcional.Where(x => x.IdentificadorTipoAvaria > 0).ToList().Count > 0)
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
                            ResultView.AvisosImpeditivos.Add("A listagem de Equipamento Opcional possui um ou mais Identificador de Tipo de Avaria inexistente");
                        }
                    }
                }
            }

            GrvModel Grv = await _context.Grv
                    .Where(x => x.FaturamentoProdutoId == GrvPersistencia.CodigoProduto
                             && x.NumeroFormularioGrv == GrvPersistencia.NumeroProcesso
                             && x.ClienteId == GrvPersistencia.IdentificadorCliente
                             && x.DepositoId == GrvPersistencia.IdentificadorDeposito)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

            if (Grv != null)
            {
                ResultView.AvisosImpeditivos.Add("GRV já cadastrado");
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