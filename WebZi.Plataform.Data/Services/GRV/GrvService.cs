using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using WebZi.Plataform.CrossCutting.Contacts;
using WebZi.Plataform.CrossCutting.Documents;
using WebZi.Plataform.CrossCutting.Localizacao;
using WebZi.Plataform.CrossCutting.Number;
using WebZi.Plataform.CrossCutting.Strings;
using WebZi.Plataform.CrossCutting.Veiculo;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Bucket;
using WebZi.Plataform.Data.Services.Cliente;
using WebZi.Plataform.Data.Services.Deposito;
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
using WebZi.Plataform.Domain.Models.Faturamento.Boleto;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Models.Servico;
using WebZi.Plataform.Domain.Models.Sistema;
using WebZi.Plataform.Domain.Models.Usuario;
using WebZi.Plataform.Domain.Models.Veiculo;
using WebZi.Plataform.Domain.Services.Usuario;
using WebZi.Plataform.Domain.ViewModel;
using WebZi.Plataform.Domain.ViewModel.Documento;
using WebZi.Plataform.Domain.ViewModel.Faturamento;
using WebZi.Plataform.Domain.ViewModel.Generic;
using WebZi.Plataform.Domain.ViewModel.GRV;
using WebZi.Plataform.Domain.ViewModel.GRV.Cadastro;
using WebZi.Plataform.Domain.ViewModel.GRV.Pesquisa;
using WebZi.Plataform.Domain.ViewModel.Localizacao;
using WebZi.Plataform.Domain.ViewModel.Usuario;
using WebZi.Plataform.Domain.Views.Usuario;
using Z.EntityFramework.Plus;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebZi.Plataform.Domain.Services.GRV
{
    public class GrvService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IServiceProvider _provider;
        private readonly IHttpClientFactory _httpClientFactory;

        public GrvService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public GrvService(AppDbContext context)
        {
            _context = context;
        }

        public GrvService(AppDbContext context, IMapper mapper, IServiceProvider provider, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _mapper = mapper;
            _provider = provider;
            _httpClientFactory = httpClientFactory;
        }

        public GrvCadastradoViewModel Cadastrar(GrvPersistenciaViewModel GrvPersistencia)
        {
            GrvCadastradoViewModel ResultView = new();

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

                MatriculaAutoridadeResponsavel = GrvPersistencia.MatriculaAutoridadeResponsavel.ToUpperTrim(),

                NomeAutoridadeResponsavel = GrvPersistencia.NomeAutoridadeResponsavel.ToUpperTrim(),

                Placa = GrvPersistencia.Placa.ToUpperTrim(),

                PlacaOstentada = GrvPersistencia.PlacaOstentada.ToUpperTrim(),

                Chassi = GrvPersistencia.Chassi.ToUpperTrim(),

                Renavam = GrvPersistencia.Renavam.ToUpperTrim(),

                Rfid = GrvPersistencia.Rfid.ToUpperTrim(),

                EnderecoLocalizacaoVeiculoLogradouro = GrvPersistencia.EnderecoLocalizacaoVeiculoLogradouro.ToUpperTrim(),

                EnderecoLocalizacaoVeiculoNumero = GrvPersistencia.EnderecoLocalizacaoVeiculoNumero.ToUpperTrim(),

                EnderecoLocalizacaoVeiculoComplemento = GrvPersistencia.EnderecoLocalizacaoVeiculoComplemento.ToUpperTrim(),

                EnderecoLocalizacaoVeiculoBairro = GrvPersistencia.EnderecoLocalizacaoVeiculoBairro.ToUpperTrim(),

                EnderecoLocalizacaoVeiculoMunicipio = GrvPersistencia.EnderecoLocalizacaoVeiculoMunicipio.ToUpperTrim(),

                EnderecoLocalizacaoVeiculoUF = GrvPersistencia.EnderecoLocalizacaoVeiculoUF.ToUpperTrim(),

                EnderecoLocalizacaoVeiculoReferencia = GrvPersistencia.EnderecoLocalizacaoVeiculoReferencia.ToUpperTrim(),

                EnderecoLocalizacaoVeiculoPontoReferencia = GrvPersistencia.EnderecoLocalizacaoVeiculoPontoReferencia.ToUpperTrim(),

                NumeroChave = GrvPersistencia.NumeroChave.ToUpperTrim(),

                EstacionamentoSetor = GrvPersistencia.EstacionamentoSetor.ToUpperTrim(),

                EstacionamentoNumeroVaga = GrvPersistencia.EstacionamentoNumeroVaga.ToUpperTrim(),

                Latitude = GrvPersistencia.Latitude.ToUpperTrim(),

                Longitude = GrvPersistencia.Longitude.ToUpperTrim(),

                VeiculoUF = GrvPersistencia.VeiculoUF.ToUpperTrim(),

                DataHoraRemocao = GrvPersistencia.DataHoraRemocao,

                LatitudeAcautelamento = GrvPersistencia.LatitudeAcautelamento.ToUpperTrim(),

                LongitudeAcautelamento = GrvPersistencia.LongitudeAcautelamento.ToUpperTrim(),

                FlagComboio = GrvPersistencia.FlagVeiculoNaoUsouReboque,

                FlagVeiculoNaoIdentificado = GrvPersistencia.FlagVeiculoNaoIdentificado,

                FlagVeiculoSemRegistro = GrvPersistencia.FlagVeiculoSemRegistro,

                FlagVeiculoRoubadoFurtado = GrvPersistencia.FlagVeiculoRoubadoFurtado,

                FlagEstadoLacre = GrvPersistencia.FlagEstadoLacre,

                FlagVeiculoNaoOstentaPlaca = GrvPersistencia.FlagVeiculoNaoOstentaPlaca,

                Condutor = _mapper.Map<CondutorModel>(GrvPersistencia.Condutor)
            };

            TabelaGenericaModel AssinaturaCondutor = new SistemaService(_context)
                .ListarTabelaGenericaById("GRV_ASSINATURA_CONDUTOR", GrvPersistencia.Condutor.IdentificadorAssinaturaCondutor)
                .FirstOrDefault();

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
                    .ForEach(x => x.NumeroInfracao = x.NumeroInfracao.ToUpperTrim());

                Grv.ListagemEnquadramentoInfracao = _mapper
                    .Map<List<EnquadramentoInfracaoGrvModel>>(GrvPersistencia.ListagemEnquadramentoInfracao);
            }

            if (GrvPersistencia.ListagemLacre?.Count > 0)
            {
                GrvPersistencia.ListagemLacre = GrvPersistencia.ListagemLacre
                    .ConvertAll(x => x.ToUpperTrim())
                    .OrderBy(x => x)
                    .ToList();

                Grv.ListagemLacre = new HashSet<LacreModel>();

                foreach (string item in GrvPersistencia.ListagemLacre)
                {
                    Grv.ListagemLacre.Add(new LacreModel { UsuarioCadastroId = GrvPersistencia.IdentificadorUsuario, Lacre = item });
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

                    ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                    return ResultView;
                }
            }

            if (GrvPersistencia.ListagemDocumentoCondutor?.Count > 0)
            {
                CadastrarDocumentosCondutor(Grv.GrvId, Grv.UsuarioCadastroId, GrvPersistencia.ListagemDocumentoCondutor);
            }

            if (GrvPersistencia.ListagemFoto?.Count > 0)
            {
                new BucketArquivoService(_context, _httpClientFactory)
                    .SendFiles("GRVFOTOSVEICCAD", Grv.GrvId, Grv.UsuarioCadastroId, GrvPersistencia.ListagemFoto);
            }

            ResultView.Mensagem = MensagemViewHelper.GetOkCreate();

            return ResultView;
        }

        private void CadastrarDocumentosCondutor(int GrvId, int UsuarioId, List<CadastroCondutorDocumentoViewModel> ListagemDocumentoCondutor)
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

            new BucketArquivoService(_context, _httpClientFactory)
                .SendFiles("GRV_DOCCONDUTOR", UsuarioId, Files);
        }

        public MensagemViewModel CadastrarDocumentosCondutor(CadastroCondutorDocumentoViewModelList ListagemDocumentoCondutor)
        {
            MensagemViewModel ResultView = ValidarInputGrv(ListagemDocumentoCondutor.IdentificadorGrv, ListagemDocumentoCondutor.IdentificadorUsuario);

            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            if (ListagemDocumentoCondutor.ListagemDocumentoCondutor?.Count == 0)
            {
                return MensagemViewHelper.GetBadRequest("Nenhuma imagem enviada para a API");
            }

            GrvModel Grv = GetGrv(ListagemDocumentoCondutor.IdentificadorGrv);

            if (!new[] { "G", "V", "L", "U", "T", "R", "E", "B", "D", "1", "2", "3", "4" }.Contains(Grv.StatusOperacao.StatusOperacaoId))
            {
                return MensagemViewHelper.GetBadRequest($"O Status da Operação deste GRV não permite o envio de Fotos. Status atual: {Grv.StatusOperacao.Descricao}");
            }

            CadastrarDocumentosCondutor(ListagemDocumentoCondutor.IdentificadorGrv, ListagemDocumentoCondutor.IdentificadorUsuario, ListagemDocumentoCondutor.ListagemDocumentoCondutor);

            return MensagemViewHelper.GetOkCreate(ListagemDocumentoCondutor.ListagemDocumentoCondutor.Count);
        }

        public MensagemViewModel CadastrarFotos(CadastroFotoViewModel Fotos)
        {
            MensagemViewModel ResultView = ValidarInputGrv(Fotos.IdentificadorGrv, Fotos.IdentificadorUsuario);

            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            if (Fotos.Fotos.Count == 0)
            {
                return MensagemViewHelper.GetBadRequest("Nenhuma imagem enviada para a API");
            }

            GrvModel Grv = GetGrv(Fotos.IdentificadorGrv);

            if (!new[] { "G", "V", "L", "U", "T", "R", "E", "B", "D", "1", "2", "3", "4" }.Contains(Grv.StatusOperacao.StatusOperacaoId))
            {
                return MensagemViewHelper.GetBadRequest($"O Status da Operação deste GRV não permite o envio de Fotos. Status atual: {Grv.StatusOperacao.Descricao}");
            }

            new BucketArquivoService(_context, _httpClientFactory)
                .SendFiles("GRVFOTOSVEICCAD", Fotos.IdentificadorGrv, Fotos.IdentificadorUsuario, Fotos.Fotos);

            return MensagemViewHelper.GetOkCreate(Fotos.Fotos.Count);
        }

        public async Task<MensagemViewModel> CadastrarLacresAsync(int GrvId, int UsuarioId, List<string> ListagemLacre)
        {
            MensagemViewModel ResultView = ValidarInputGrv(GrvId, UsuarioId);

            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            if (ListagemLacre.Count == 0)
            {
                return MensagemViewHelper.GetBadRequest("Informe os Lacres");
            }

            ListagemLacre = ListagemLacre
                .ConvertAll(x => x.ToUpperTrim())
                .Distinct()
                .OrderBy(x => x)
                .ToList();

            GrvModel Grv = await GetGrvAsync(GrvId);

            if (!new[] { "E", "G", "L", "R", "T", "U", "V" }.Contains(Grv.StatusOperacaoId))
            {
                return MensagemViewHelper.GetBadRequest($"O GRV está em um Status de Operação que impede o cadastro de Lacres. Status atual: {Grv.StatusOperacao.Descricao}");
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

                return MensagemViewHelper.GetBadRequest(erros);
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

                    return MensagemViewHelper.GetInternalServerError(ex);
                }
            }

            return MensagemViewHelper.GetOkCreate(ListagemLacre.Count, "Lacre(s) cadastrado(s) com sucesso");
        }

        public async Task<MensagemViewModel> ExcluirFotosAsync(int GrvId, int UsuarioId, List<int> ListagemTabelaOrigemId)
        {
            MensagemViewModel ResultView = ValidarInputGrv(GrvId, UsuarioId);

            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            if (ListagemTabelaOrigemId.Count == 0)
            {
                return MensagemViewHelper.GetBadRequest("Informe os Identificadores das Fotos");
            }

            GrvModel Grv = await GetGrvAsync(GrvId);

            if (!new[] { "E", "G", "L", "R", "T", "U", "V" }.Contains(Grv.StatusOperacaoId))
            {
                return MensagemViewHelper.GetBadRequest($"O GRV está em um Status de Operação que impede a exclusão de Fotos. Status atual: {Grv.StatusOperacao.Descricao}");
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

                return MensagemViewHelper.GetBadRequest(erros);
            }

            new BucketArquivoService(_context, _httpClientFactory)
                .DeleteFiles("GRVFOTOSVEICCAD", ListagemTabelaOrigemId);

            return MensagemViewHelper.GetOkFound(BucketArquivos.Count, "Foto(s) excluída(s) com sucesso");
        }

        public async Task<MensagemViewModel> ExcluirGrvAsync(string NumeroFormularioGrv, string CodigoProduto, int ClienteId, int DepositoId, string Login, string Senha)
        {
            UsuarioViewModel Usuario = await new UsuarioService(_context, _mapper)
                .GetByLoginAsync(Login, Senha);

            if (Usuario.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return Usuario.Mensagem;
            }

            MensagemViewModel ResultView = ValidarInputGrv(NumeroFormularioGrv, CodigoProduto, ClienteId, DepositoId, Usuario.IdentificadorUsuario);

            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            GrvModel Grv = await _context.Grv
                .Where(x => x.FaturamentoProdutoId == CodigoProduto
                         && x.ClienteId == ClienteId
                         && x.DepositoId == DepositoId
                         && x.NumeroFormularioGrv == NumeroFormularioGrv)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return await ExcluirGrvAsync(Grv.GrvId, Usuario.IdentificadorUsuario);
        }

        public async Task<MensagemViewModel> ExcluirGrvAsync(int GrvId, string Login, string Senha)
        {
            UsuarioViewModel Usuario = await new UsuarioService(_context, _mapper)
                .GetByLoginAsync(Login, Senha);

            if (Usuario.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return Usuario.Mensagem;
            }
            else if (GrvId <= 0)
            {
                return MensagemViewHelper.GetBadRequest(MensagemPadraoEnum.IdentificadorGrvInvalido);
            }

            MensagemViewModel ResultView = ValidarInputGrv(GrvId, Usuario.IdentificadorUsuario);

            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            return await ExcluirGrvAsync(GrvId, Usuario.IdentificadorUsuario);
        }

        private async Task<MensagemViewModel> ExcluirGrvAsync(int GrvId, int UsuarioId)
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
                return MensagemViewHelper.GetUnauthorized("Usuário não possui permissão para excluir GRV");
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
                return MensagemViewHelper.GetNotFound(MensagemPadraoEnum.NaoEncontradoGrv);
            }
            else if (!new[] { "M", "P", "G", "V" }.Contains(Grv.StatusOperacaoId))
            {
                return MensagemViewHelper.GetBadRequest($"O Status da Operação deste GRV não permite a exclusão. Status atual: {Grv.StatusOperacao.Descricao}");
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

                return MensagemViewHelper.GetInternalServerError(ex);
            }

            new BucketArquivoService(_context, _httpClientFactory)
                .DeleteFiles("GRVFOTOSVEICCAD", GrvId);

            new BucketArquivoService(_context, _httpClientFactory)
                .DeleteFiles("GGVFOTOSVEICCAD", GrvId);

            if (Grv.ListagemCondutorDocumento?.Count > 0)
            {
                new BucketArquivoService(_context, _httpClientFactory)
                    .DeleteFiles("GRV_DOCCONDUTOR", Grv.ListagemCondutorDocumento
                        .Select(x => x.CondutorDocumentoId)
                        .ToList());
            }

            if (Grv.Atendimento != null)
            {
                new BucketArquivoService(_context, _httpClientFactory)
                    .DeleteFiles("ATENDIMFOTORESP", Grv.Atendimento.AtendimentoId);

                if (Faturamentos?.Count > 0)
                {
                    foreach (FaturamentoModel Faturamento in Faturamentos)
                    {
                        if (Faturamento.FaturamentoBoletos?.Count > 0)
                        {
                            foreach (FaturamentoBoletoModel FaturamentoBoleto in Faturamento.FaturamentoBoletos)
                            {
                                new BucketArquivoService(_context, _httpClientFactory)
                                    .DeleteFiles("FATURAMENBOLETO", FaturamentoBoleto.FaturamentoBoletoId);
                            }
                        }
                    }
                }
            }

            return MensagemViewHelper.GetOk("GRV excluído com sucesso");
        }

        public async Task<MensagemViewModel> ExcluirLacresAsync(int GrvId, int UsuarioId, List<int> ListagemIdentificadorLacre)
        {
            MensagemViewModel ResultView = ValidarInputGrv(GrvId, UsuarioId);

            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            if (ListagemIdentificadorLacre.Count == 0)
            {
                return MensagemViewHelper.GetBadRequest("Informe os Lacres");
            }

            GrvModel Grv = await GetGrvAsync(GrvId);

            if (!new[] { "E", "G", "L", "R", "T", "U", "V" }.Contains(Grv.StatusOperacaoId))
            {
                return MensagemViewHelper.GetBadRequest($"O GRV está em um Status de Operação que impede o exclusão de Lacres. Status atual: {Grv.StatusOperacao.Descricao}");
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

                return MensagemViewHelper.GetBadRequest(erros);
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

                    return MensagemViewHelper.GetInternalServerError(ex);
                }
            }

            return MensagemViewHelper.GetOkDelete(ListagemIdentificadorLacre.Count, "Lacre(s) excluído(s) com sucesso");
        }

        public async Task<GrvViewModelList> GetByIdAsync(int GrvId, int UsuarioId)
        {
            GrvViewModelList ResultView = new()
            {
                Mensagem = ValidarInputGrv(GrvId, UsuarioId)
            };

            if (ResultView.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            GrvModel Grv = await GetGrvAsync(GrvId);

            if (Grv == null)
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound(MensagemPadraoEnum.NaoEncontradoGrv);

                return ResultView;
            }

            ResultView.Mensagem = ValidarInputGrv(Grv, UsuarioId);

            if (ResultView.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            ResultView.Listagem.Add(_mapper.Map<GrvViewModel>(Grv));

            ResultView.Mensagem = MensagemViewHelper.GetOkFound();

            return ResultView;
        }

        public async Task<GrvViewModelList> GetByNumeroFormularioGrvAsync(string NumeroFormularioGrv, string CodigoProduto, int ClienteId, int DepositoId, int UsuarioId)
        {
            GrvViewModelList ResultView = new()
            {
                Mensagem = ValidarInputGrv(NumeroFormularioGrv, CodigoProduto, ClienteId, DepositoId, UsuarioId)
            };

            if (ResultView.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            GrvModel Grv = await _context.Grv
                .Where(x => x.NumeroFormularioGrv == NumeroFormularioGrv
                         && x.ClienteId == ClienteId
                         && x.DepositoId == DepositoId
                         && x.FaturamentoProdutoId == CodigoProduto)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Grv == null)
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound(MensagemPadraoEnum.NaoEncontradoGrv);

                return ResultView;
            }

            ResultView.Mensagem = ValidarInputGrv(Grv, UsuarioId);

            if (ResultView.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            ResultView.Listagem.Add(_mapper.Map<GrvViewModel>(Grv));

            ResultView.Mensagem = MensagemViewHelper.GetOkFound();

            return ResultView;
        }

        public async Task<StatusOperacaoViewModelList> GetStatusOperacaoByIdAsync(string StatusOperacaoId)
        {
            StatusOperacaoViewModelList ResultView = new();

            if (string.IsNullOrWhiteSpace(StatusOperacaoId))
            {
                ResultView.Mensagem = MensagemViewHelper.GetBadRequest("Identificador do Status da Operação inválido");

                return ResultView;
            }

            StatusOperacaoModel result = await _context.StatusOperacao
                .Where(w => w.StatusOperacaoId == StatusOperacaoId.ToUpperTrim())
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (result != null)
            {
                ResultView.Listagem.Add(result);

                ResultView.Mensagem = MensagemViewHelper.GetOkFound();
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound();
            }

            return ResultView;
        }

        public async Task<AutoridadeResponsavelViewModelList> ListarAutoridadeResponsavelAsync(string UF)
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
                .Where(w => w.UF == UF.ToUpperTrim())
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (result == null)
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound("Unidade Federativa sem Órgão Emissor cadastrado");

                return ResultView;
            }

            if (result.AutoridadesResponsaveis?.Count > 0)
            {
                ResultView.Listagem = _mapper.Map<List<AutoridadeResponsavelViewModel>>(result.AutoridadesResponsaveis
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

        public async Task<ImageViewModelList> ListarDocumentosCondutorAsync(int GrvId, int UsuarioId)
        {
            ImageViewModelList ResultView = new()
            {
                Mensagem = ValidarInputGrv(GrvId, UsuarioId)
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

            return await new BucketArquivoService(_context, _httpClientFactory)
                .DownloadFiles("GRV_DOCCONDUTOR", DocumentosCondutor);
        }

        public async Task<ImageViewModelList> ListarFotoAsync(int GrvId, int UsuarioId)
        {
            ImageViewModelList ResultView = new()
            {
                Mensagem = ValidarInputGrv(GrvId, UsuarioId)
            };

            if (ResultView.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            return await new BucketArquivoService(_context, _httpClientFactory)
                .DownloadFiles("GRVFOTOSVEICCAD", GrvId);
        }

        public async Task<GrvPesquisaDadosMestresViewModel> ListarItemPesquisaAsync(int UsuarioId)
        {
            return new()
            {
                ListagemProduto = await ListarProdutosAsync(),

                ListagemCliente = await _provider
                    .GetService<ClienteService>()
                    .ListagemSimplificada(UsuarioId),

                ListagemDeposito = await _provider
                    .GetService<DepositoService>()
                    .ListagemSimplificada(UsuarioId),

                ListagemStatusOperacao = await _provider
                    .GetService<GrvService>()
                    .ListarStatusOperacaoAsync(),

                //ListagemReboque = await _provider
                //    .GetService<UsuarioService>()
                //    .ListarUsuarioClienteDepositoReboqueSimplificada(UsuarioId),

                //ListagemReboquista = await _provider
                //    .GetService<UsuarioService>()
                //    .ListarUsuarioClienteDepositoReboquistaSimplificada(UsuarioId)
            };
        }

        public async Task<LacreViewModelList> ListarLacreAsync(int GrvId, int UsuarioId)
        {
            LacreViewModelList ResultView = new()
            {
                Mensagem = ValidarInputGrv(GrvId, UsuarioId)
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

                ResultView.Mensagem = MensagemViewHelper.GetOkFound(result.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound();
            }

            return ResultView;
        }

        public async Task<MotivoApreensaoViewModelList> ListarMotivoApreensaoAsync()
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

                ResultView.Mensagem = MensagemViewHelper.GetOkFound(result.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound();
            }

            return ResultView;
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

        public async Task<FaturamentoProdutoViewModelList> ListarProdutosAsync()
        {
            FaturamentoProdutoViewModelList ResultView = new();

            List<FaturamentoProdutoModel> result = await _context.FaturamentoProduto
                .AsNoTracking()
                .ToListAsync();

            ResultView.Listagem = _mapper.Map<List<FaturamentoProdutoViewModel>>(result.OrderBy(x => x.Descricao).ToList());

            ResultView.Mensagem = MensagemViewHelper.GetOkFound(result.Count);

            return ResultView;
        }

        public async Task<StatusOperacaoViewModelList> ListarStatusOperacaoAsync()
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

                ResultView.Mensagem = MensagemViewHelper.GetOkFound(result.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound();
            }

            return ResultView;
        }

        public async Task<GrvPesquisaResultViewModelList> SearchAsync(GrvPesquisaInputViewModel GrvPesquisa)
        {
            List<string> erros = new();

            if (GrvPesquisa.ListagemCodigoProduto.Count == 0)
            {
                erros.Add("Informe ao menos um Código de Produto");
            }
            else
            {
                List<string> Produtos = await _context.FaturamentoProduto
                    .Select(x => x.FaturamentoProdutoId)
                    .AsNoTracking()
                    .ToListAsync();

                foreach (string Codigo in GrvPesquisa.ListagemCodigoProduto)
                {
                    if (Produtos.FirstOrDefault(f => f == Codigo.ToUpperTrim()) == null)
                    {
                        erros.Add($"{MensagemPadraoEnum.NaoEncontradoFaturamentoProduto}: {Codigo}");
                    }
                }
            }

            if (GrvPesquisa.ListagemStatusOperacao.Count > 0)
            {
                List<string> StatusOperacoes = await _context.StatusOperacao
                    .Select(x => x.StatusOperacaoId)
                    .AsNoTracking()
                    .ToListAsync();

                foreach (string StatusOperacao in GrvPesquisa.ListagemStatusOperacao)
                {
                    if (StatusOperacoes.FirstOrDefault(f => f == StatusOperacao.ToUpperTrim()) == null)
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

            if (!string.IsNullOrWhiteSpace(GrvPesquisa.FlagVeiculoNaoIdentificado) && GrvPesquisa.FlagVeiculoNaoIdentificado != "S" && GrvPesquisa.FlagVeiculoNaoIdentificado != "N")
            {
                erros.Add("Flag do Veículo não identificado inválida, informe \"S\" ou \"N\" (sem aspas)");
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
                ResultView.Mensagem = MensagemViewHelper.GetBadRequest(erros);

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
                .OrderBy(o => Convert.ToInt64(o.NumeroFormularioGrv))
                .Take(100)
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count == 0)
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound("A pesquisa não retornou registro");

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

            ResultView.Mensagem = MensagemViewHelper.GetOkFound(result.Count);

            return ResultView;
        }

        public MensagemViewModel ValidarInputGrv(int GrvId, int UsuarioId)
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
                return MensagemViewHelper.GetBadRequest(erros);
            }

            return ValidarInputGrv(GrvId, UsuarioId, "", "", 0, 0);
        }

        public MensagemViewModel ValidarInputGrv(GrvModel Grv, int UsuarioId)
        {
            return ValidarInputGrv(Grv.GrvId, UsuarioId, Grv.NumeroFormularioGrv, Grv.FaturamentoProdutoId, Grv.ClienteId, Grv.DepositoId);
        }

        public MensagemViewModel ValidarInputGrv(string NumeroFormularioGrv, string CodigoProduto, int ClienteId, int DepositoId, int UsuarioId)
        {
            return ValidarInputGrv(0, UsuarioId, NumeroFormularioGrv, CodigoProduto, ClienteId, DepositoId);
        }

        private MensagemViewModel ValidarInputGrv(int GrvId, int UsuarioId, string NumeroFormularioGrv, string CodigoProduto, int ClienteId, int DepositoId)
        {
            if (!new UsuarioService(_context).IsUserActive(UsuarioId))
            {
                return MensagemViewHelper.GetUnauthorized();
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

                if (string.IsNullOrWhiteSpace(CodigoProduto))
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
                return MensagemViewHelper.GetBadRequest(erros);
            }

            if (GrvId <= 0)
            {
                FaturamentoProdutoModel FaturamentoProduto = _context.FaturamentoProduto
                    .Where(x => x.FaturamentoProdutoId == CodigoProduto)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (FaturamentoProduto == null)
                {
                    return MensagemViewHelper.GetNotFound(MensagemPadraoEnum.NaoEncontradoFaturamentoProduto);
                }

                ClienteModel Cliente = _context.Cliente
                    .Where(x => x.ClienteId == ClienteId)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (Cliente == null)
                {
                    return MensagemViewHelper.GetNotFound(MensagemPadraoEnum.NaoEncontradoCliente);
                }

                DepositoModel Deposito = _context.Deposito
                    .Where(x => x.DepositoId == DepositoId)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (Deposito == null)
                {
                    return MensagemViewHelper.GetNotFound(MensagemPadraoEnum.NaoEncontradoDeposito);
                }

                ClienteDepositoModel ClienteDeposito = _context.ClienteDeposito
                    .Where(x => x.ClienteId == ClienteId
                        && x.DepositoId == DepositoId)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (ClienteDeposito == null)
                {
                    return MensagemViewHelper.GetNotFound("Este Cliente e Depósito não são associados");
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
                    return MensagemViewHelper.GetUnauthorized("Usuário não possui acesso ao GRV ou o GRV não existe");
                }
            }
            else
            {
                Grv = _context.ViewUsuarioClienteDepositoGrv
                    .Where(x => x.FaturamentoProdutoCodigo == CodigoProduto
                             && x.ClienteId == ClienteId
                             && x.DepositoId == DepositoId
                             && x.NumeroFormularioGrv == NumeroFormularioGrv)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (Grv == null)
                {
                    return MensagemViewHelper.GetUnauthorized("Usuário não possui acesso ao GRV ou o GRV não existe");
                }
            }

            return MensagemViewHelper.GetOk();
        }

        public async Task<MensagemViewModel> VerificarAlteracaoStatusGRVAsync(int GrvId, string StatusOperacaoId, int UsuarioId)
        {
            MensagemViewModel ResultView = ValidarInputGrv(GrvId, UsuarioId);

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
                return MensagemViewHelper.GetNotFound(MensagemPadraoEnum.NaoEncontradoGrv);
            }

            StatusOperacaoModel StatusOperacao = await _context.StatusOperacao
                .Where(x => x.StatusOperacaoId == StatusOperacaoId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (StatusOperacao == null)
            {
                return MensagemViewHelper.GetNotFound(MensagemPadraoEnum.NaoEncontradoStatusOperacao);
            }
            else if (Grv.StatusOperacao.StatusOperacaoId != StatusOperacaoId)
            {
                return MensagemViewHelper.GetBadRequest($"O Status da Operação foi alterado de \"{Grv.StatusOperacao.Descricao.ToUpper()}\" para \"{StatusOperacao.Descricao.ToUpper()}\"");
            }

            return MensagemViewHelper.GetOk("O Status da Operação não foi alterado");
        }

        public async Task<MensagemViewModel> ValidarInformacoesPersistenciaAsync(GrvPersistenciaViewModel GrvPersistencia)
        {
            if (GrvPersistencia == null)
            {
                return MensagemViewHelper.GetBadRequest("O Modelo está nulo");
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

            if (GrvPersistencia.FlagVeiculoNaoUsouReboque != "S"
                && GrvPersistencia.FlagVeiculoNaoUsouReboque != "N")
            {
                erros.Add("Flag do Veículo não usou Reboque inválida, informe \"S\" ou \"N\" (sem aspas)");
            }
            else if (GrvPersistencia.FlagVeiculoNaoUsouReboque == "N")
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

            if (GrvPersistencia.FlagVeiculoNaoOstentaPlaca != "S"
             && GrvPersistencia.FlagVeiculoNaoOstentaPlaca != "N")
            {
                erros.Add("Flag do Veículo não ostenta Placa inválida, informe \"S\" ou \"N\" (sem aspas)");
            }

            if (GrvPersistencia.IdentificadorAutoridadeResponsavel <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorAutoridadeResponsavelInvalido);
            }

            if (string.IsNullOrWhiteSpace(GrvPersistencia.MatriculaAutoridadeResponsavel))
            {
                erros.Add("Informe a Matrícula da Autoridade Responsável.");
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

            if (GrvPersistencia.FlagVeiculoNaoIdentificado != "S"
             && GrvPersistencia.FlagVeiculoNaoIdentificado != "N")
            {
                erros.Add("Flag do Veículo não identificado inválida, informe \"S\" ou \"N\" (sem aspas)");
            }
            else if (GrvPersistencia.FlagVeiculoNaoIdentificado == "S")
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

            if (GrvPersistencia.FlagVeiculoSemRegistro != "S"
             && GrvPersistencia.FlagVeiculoSemRegistro != "N")
            {
                erros.Add("Flag do Veículo sem registro inválida, informe \"S\" ou \"N\" (sem aspas)");
            }
            else if (GrvPersistencia.FlagVeiculoSemRegistro == "S")
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

            if (GrvPersistencia.FlagVeiculoRoubadoFurtado != "S"
             && GrvPersistencia.FlagVeiculoRoubadoFurtado != "N")
            {
                erros.Add("Flag do Veículo Roubado/Furtado inválida, informe \"S\" ou \"N\" (sem aspas)");
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
                    List<TabelaGenericaModel> AssinaturaCondutor = await new SistemaService(_context)
                        .ListarTabelaGenericaByIdAsync("GRV_ASSINATURA_CONDUTOR", GrvPersistencia.Condutor.IdentificadorAssinaturaCondutor);

                    if (AssinaturaCondutor?.Count == 0)
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

                if (GrvPersistencia.Condutor.FlagDocumentacaoVeiculo != "S"
                 && GrvPersistencia.Condutor.FlagDocumentacaoVeiculo != "N")
                {
                    erros.Add("Flag da Documentação foi deixada no Veículo inválida, informe \"S\" ou \"N\" (sem aspas)");
                }

                if (GrvPersistencia.Condutor.FlagChaveVeiculo != "S"
                 && GrvPersistencia.Condutor.FlagChaveVeiculo != "N")
                {
                    erros.Add("Flag da Chave ficou no Veículo inválida, informe \"S\" ou \"N\" (sem aspas)");
                }
                else if (GrvPersistencia.Condutor.FlagChaveVeiculo == "S"
                      && string.IsNullOrWhiteSpace(GrvPersistencia.Condutor.NumeroChaveVeiculo))
                {
                    erros.Add("Ao informar que a Chave ficou no Veículo, é necessário informar o Número/Código da Chave");
                }
            }

            if (GrvPersistencia.FlagEstadoLacre != "S"
             && GrvPersistencia.FlagEstadoLacre != "N")
            {
                erros.Add("Flag do Status dos Lacres inválida, informe \"S\" ou \"N\" (sem aspas)");
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

            if (erros.Count > 0)
            {
                return MensagemViewHelper.GetBadRequest(erros);
            }
            #endregion Validações de IDs

            #region Consultas
            if (!new UsuarioService(_context).IsUserActive(GrvPersistencia.IdentificadorUsuario))
            {
                return MensagemViewHelper.GetUnauthorized();
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
                ResultView.AvisosImpeditivos.Add("A Hora da Remoção não pode ser igual a 00:00.");
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

        public GrvModel GetGrv(int GrvId)
        {
            return _context.Grv
                .Include(x => x.StatusOperacao)
                .Where(x => x.GrvId == GrvId)
                .AsNoTracking()
                .FirstOrDefault();
        }

        public async Task<GrvModel> GetGrvAsync(int GrvId)
        {
            return await _context.Grv
                .Include(x => x.StatusOperacao)
                .Where(x => x.GrvId == GrvId)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
    }
}