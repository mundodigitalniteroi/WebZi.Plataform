using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Globalization;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using WebZi.Plataform.CrossCutting.Configuration;
using WebZi.Plataform.CrossCutting.Date;
using WebZi.Plataform.CrossCutting.Documents;
using WebZi.Plataform.CrossCutting.Number;
using WebZi.Plataform.CrossCutting.Secutity;
using WebZi.Plataform.CrossCutting.Strings;
using WebZi.Plataform.CrossCutting.Veiculo;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Atendimento;
using WebZi.Plataform.Data.Services.Cliente;
using WebZi.Plataform.Data.Services.Deposito;
using WebZi.Plataform.Data.Services.Faturamento;
using WebZi.Plataform.Data.Services.Localizacao;
using WebZi.Plataform.Data.Services.Report;
using WebZi.Plataform.Data.Services.WebServices;
using WebZi.Plataform.Domain.DTO.Generic;
using WebZi.Plataform.Domain.DTO.Report;
using WebZi.Plataform.Domain.DTO.Sistema;
using WebZi.Plataform.Domain.DTO.Usuario;
using WebZi.Plataform.Domain.Enums;
using WebZi.Plataform.Domain.Models.Atendimento;
using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Models.Liberacao;
using WebZi.Plataform.Domain.Models.Localizacao;
using WebZi.Plataform.Domain.Models.Usuario;
using WebZi.Plataform.Domain.Models.WebServices.DetranAlagoas.ConsultaVeiculoApreensao.Response;
using WebZi.Plataform.Domain.Options;
using WebZi.Plataform.Domain.Services.GRV;
using WebZi.Plataform.Domain.Services.Usuario;
using WebZi.Plataform.Domain.ViewModel.Atendimento;
using WebZi.Plataform.Domain.ViewModel.Liberacao;
using WebZi.Plataform.Domain.Views.Usuario;
using Z.EntityFramework.Plus;

namespace WebZi.Plataform.Data.Services.Liberacao
{
    public class LiberacaoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IServiceProvider _provider;
        private readonly IOptions<WSNfseOptions> _options;

        public LiberacaoService(AppDbContext context)
        {
            _context = context;
        }

        public LiberacaoService(AppDbContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
        }

        public LiberacaoService(AppDbContext context, IMapper mapper, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
        }

        public LiberacaoService(AppDbContext context, IMapper mapper, IHttpClientFactory httpClientFactory,
            IServiceProvider provider)
        {
            _context = context;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
            _provider = provider;
        }

        public LiberacaoService(AppDbContext context, IMapper mapper, IHttpClientFactory httpClientFactory,
            IServiceProvider provider, IOptions<WSNfseOptions> options)
        {
            _context = context;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
            _provider = provider;
            _options = options;
        }

        public async Task<GuiaAutorizacaoRetiradaVeiculoDTO> CreateGuiaAutorizacaoRetiradaVeiculoAsync(int GrvId,
            int UsuarioId)
        {
            GuiaAutorizacaoRetiradaVeiculoDTO ResultView = new()
            {
                Mensagem = new GrvService(_context)
                    .ValidateInputGrv(GrvId, UsuarioId)
            };

            if (ResultView.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            GrvModel Grv = await _context.Grv
                .Include(x => x.TipoVeiculo)
                .Include(x => x.StatusOperacao)
                .Include(x => x.Cliente)
                .Include(x => x.Atendimento)
                .Include(x => x.Liberacao)
                .Include(x => x.ListagemLacre)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.GrvId == GrvId);

            if (Grv.StatusOperacaoId is not "R" and not "T" and not "E" and not "U")
            {
                ResultView.Mensagem = MensagemViewHelper
                    .SetBadRequest(
                        $"O Status atual deste Processo não permite a geração do Documento. Status atual: {Grv.StatusOperacao.Descricao}");

                return ResultView;
            }
            else if (Grv.StatusOperacaoId == "E")
            {
                if (DateTime.Now.Date > Grv.Liberacao.DataCadastro.Date)
                {
                    ResultView.Mensagem.Alertas
                        .Add(
                            $"Este Processo foi entregue em {Grv.Liberacao.DataCadastro:dd/MM/yyyy}, as informações impressas no Documento estão desatualizadas");
                }
            }

            int FaturamentoId = await new FaturamentoService(_context).GetUltimoFaturamentoIdAsync(GrvId);

            if (FaturamentoId == 0)
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound("Faturamento não encontrado");

                return ResultView;
            }

            GuiaPagamentoReboqueEstadiaDTO GuiaPagamentoReboqueEstadia =
                await new GuiaPagamentoReboqueEstadiaService(_context, _mapper, _httpClientFactory)
                    .GetGuiaPagamentoReboqueEstadiaAsync(FaturamentoId, UsuarioId, true);

            if (GuiaPagamentoReboqueEstadia == null)
            {
                ResultView.Mensagem = MensagemViewHelper
                    .SetNotFound(
                        "Informações para a geração da Guia de Autorização para a Retirada do Veículo não encontradas");

                return ResultView;
            }

            var prazo = GuiaPagamentoReboqueEstadia.PrazoRetiradaVeiculo;

            var prazoFormatado = !string.IsNullOrEmpty(prazo) && prazo.Length >= 10
                ? prazo.Substring(0, 10)
                : prazo ?? "";
            ResultView.IdentificadorProcesso = Grv.GrvId;

            ResultView.NumeroProcesso = Grv.NumeroFormularioGrv;

            ResultView.ClienteNome = GuiaPagamentoReboqueEstadia.ClienteNome;

            ResultView.ClienteEndereco = GuiaPagamentoReboqueEstadia.ClienteEndereco;

            ResultView.DadosCodigoAutorizacao = "Link para validação";

            ResultView.DadosProcessoGrv = "Dados do Processo Processo: " +
                                          GuiaPagamentoReboqueEstadia.NumeroFormularioGrv + " - " + "Depósito: " +
                                          GuiaPagamentoReboqueEstadia.DepositoNome;

            ResultView.DadosTipoProcesso = "REGISTRO DE APREENSÃO";

            ResultView.DadosReboque = !string.IsNullOrWhiteSpace(GuiaPagamentoReboqueEstadia.ReboquePlaca)
                ? VeiculoHelper.FormatPlaca(GuiaPagamentoReboqueEstadia.ReboquePlaca)
                : string.Empty;

            ResultView.DadosDataEntrada = GuiaPagamentoReboqueEstadia.DataHoraGuarda.Left(10);

            ResultView.DadosHoraEntrada = GuiaPagamentoReboqueEstadia.DataHoraGuarda.Right(5);

            ResultView.DadosPermanencia = GuiaPagamentoReboqueEstadia.QuantidadeEstadias == 1
                ? GuiaPagamentoReboqueEstadia.QuantidadeEstadias.ToString() + " dia"
                : GuiaPagamentoReboqueEstadia.QuantidadeEstadias.ToString() + " dias";

            ResultView.DadosAutorizadaRetiradaVeiculoEm =
                DateTime.Now.ToString("dd/MM/yyyy"); //GuiaPagamentoReboqueEstadia.FaturamentoDataVencimento.Left(10);

            ResultView.VeiculoTipo = Grv.TipoVeiculo.Descricao;

            ResultView.VeiculoMarcaModelo = GuiaPagamentoReboqueEstadia.MarcaModelo;

            ResultView.VeiculoPlaca = VeiculoHelper.FormatPlaca(GuiaPagamentoReboqueEstadia.Placa);

            ResultView.VeiculoRenavam = GuiaPagamentoReboqueEstadia.Renavam;

            ResultView.VeiculoChassi = GuiaPagamentoReboqueEstadia.Chassi;

            ResultView.VeiculoCor = GuiaPagamentoReboqueEstadia.Cor;

            ResultView.TextoApresentacao =
                "Este documento deverá ser apresentado no Depósito de Veículos localizado no endereço " +
                GuiaPagamentoReboqueEstadia.DepositoEndereco + ", até a data: " +
                prazoFormatado +
                ", para que a retirada do veículo seja autorizada. A não apresentação até a data informada acarretará na cobrança de estadias adicionais.";

            ResultView.TextoDeclaracaoRetirada1 =
                $@"Eu {GuiaPagamentoReboqueEstadia.AtendimentoResponsavelNome}, portador do CPF {GuiaPagamentoReboqueEstadia.AtendimentoResponsavelDocumento}, declaro que no dia {DateTime.Now.ToString("dd 'de' MMMM 'de' yyyy", CultureInfo.GetCultureInfo("pt-BR"))}, " +
                $"recebi do depósito {GuiaPagamentoReboqueEstadia.DepositoNome} o veículo de placa {VeiculoHelper.FormatPlaca(GuiaPagamentoReboqueEstadia.Placa)}, Marca/Modelo {GuiaPagamentoReboqueEstadia.MarcaModelo}, Cor {GuiaPagamentoReboqueEstadia.Cor}, recolhido às {GuiaPagamentoReboqueEstadia.DataHoraGuarda.Right(5)} do dia {GuiaPagamentoReboqueEstadia.DataHoraGuarda.Left(10)}, " +
                $"no endereco {GuiaPagamentoReboqueEstadia.DepositoEndereco}";

            ResultView.TextoDeclaracaoRetirada2 =
                $@"Declaro também que o veículo se encontrava nas mesmas condições, quando foi removido e ainda lacrado, " +
                "conforme numeração abaixo descrita, sendo estes lacres conferidos na minha presença, nada havendo para reclamar agora ou no futuro.";

            ResultView.ProprietarioProcurador = GuiaPagamentoReboqueEstadia.AtendimentoResponsavelNome;

            ResultView.ProprietarioCpf = GuiaPagamentoReboqueEstadia.AtendimentoResponsavelDocumento;

            ResultView.GrvEstacionamentoSetor = !GuiaPagamentoReboqueEstadia.EstacionamentoSetor.IsNullOrWhiteSpace()
                ? GuiaPagamentoReboqueEstadia.EstacionamentoSetor
                : "Não informado";

            ResultView.GrvEstacionamentoNumeroVaga =
                !GuiaPagamentoReboqueEstadia.EstacionamentoNumeroVaga.IsNullOrWhiteSpace()
                    ? GuiaPagamentoReboqueEstadia.EstacionamentoNumeroVaga
                    : "Não informado";

            ResultView.GrvNumeroChave = !GuiaPagamentoReboqueEstadia.NumeroChave.IsNullOrWhiteSpace()
                ? GuiaPagamentoReboqueEstadia.NumeroChave
                : "Não informado";

            ViewUsuarioModel Usuario = await _context.ViewUsuario
                .FirstOrDefaultAsync(x => x.UsuarioId == UsuarioId);

            ResultView.UsuarioNome = Usuario.NomeCompleto;

            ResultView.UsuarioMatricula = Usuario.Matricula;


            #region FORMA DE LIBERAÇÃO

            ResultView.AtendimentoFormaLiberacaoNome = GuiaPagamentoReboqueEstadia.AtendimentoFormaLiberacaoNome;

            ResultView.AtendimentoFormaLiberacaoCNH = GuiaPagamentoReboqueEstadia.AtendimentoFormaLiberacaoCNH;

            if (GuiaPagamentoReboqueEstadia.AtendimentoFormaLiberacao == "C")
            {
                ResultView.AtendimentoFormaLiberacao = "Condutor habilitado";

                ResultView.AtendimentoFormaLiberacaoCpfPlaca =
                    DocumentHelper.FormatCPF(GuiaPagamentoReboqueEstadia.AtendimentoFormaLiberacaoCPF);

                ResultView.LabelAtendimentoFormaLiberacaoCpfPlaca = "CPF:";
            }
            else if (GuiaPagamentoReboqueEstadia.AtendimentoFormaLiberacao == "R")
            {
                ResultView.AtendimentoFormaLiberacao = "Reboque";

                ResultView.AtendimentoFormaLiberacaoCpfPlaca =
                    VeiculoHelper.FormatPlaca(GuiaPagamentoReboqueEstadia.AtendimentoFormaLiberacaoPlaca);

                ResultView.LabelAtendimentoFormaLiberacaoCpfPlaca = "Placa:";
            }
            else
            {
                ResultView.AtendimentoFormaLiberacao = "Não informado";

                ResultView.AtendimentoFormaLiberacaoNome = "Não informado";

                ResultView.AtendimentoFormaLiberacaoCNH = "Não informado";
            }

            #endregion FORMA DE LIBERAÇÃO

            // LOGOMARCA
            ImageListDTO Listagem = await new ClienteService(_context, _mapper, _httpClientFactory)
                .GetLogomarcaAsync(Grv.ClienteId);

            ResultView.Logo = Listagem
                .Listagem
                .FirstOrDefault()
                .Imagem;

            string key = AppSettingsHelper.GetValue("Segredo", "QRCode");

            var agora = DateTime.Now;
            string payload = $"{GrvId}|{agora:yyyyMMdd}|{agora:HH:mm:ss}";

            string encrypted = CryptographyHelper.EncryptString(key, payload);

            string decrypted = CryptographyHelper.DecryptString(key, encrypted);

            ResultView.QRCodeString = encrypted;
            ResultView.QRCode = QRCodeHelper.CreateImageAsByteArray(encrypted, "PNG");

            if (Grv.ListagemLacre?.Count > 0)
            {
                ResultView.ListagemLacre = new();

                ResultView.ListagemLacre.AddRange(Grv.ListagemLacre.Select(x => x.Lacre).ToList());
            }

            ResultView.Mensagem = MensagemViewHelper.SetOk(ResultView.Mensagem, "Documento gerado com sucesso");

            return ResultView;
        }

        public MensagemDTO ValidarUsuario(LiberacaoEspecialValidarParameters parameters)
        {
            #region Parameters

            StringBuilder Perm = new();
            StringBuilder Usuario = new();
            parameters.Login = parameters.Login.ToUpper().Trim();
            parameters.Password = parameters.Password.ToUpper().Trim();
            const int IdPerfilAcesso = 24;
            const int IdSubModulo = 5;

            #endregion

            #region Consulta

            Usuario.AppendLine("SELECT id_usuario AS Value");
            Usuario.AppendLine("  FROM dbo.tb_dep_usuarios");
            Usuario.AppendLine(" WHERE 1 = 1");
            Usuario.Append("   AND login = @login");
            Usuario.Append("   AND senha1 = HASHBYTES('MD5', @senha)");

            SqlParameter[] sqlParameterUser = new SqlParameter[]
            {
                new SqlParameter("@login", SqlDbType.VarChar)
                {
                    Value = parameters.Login
                },

                new SqlParameter("@senha", SqlDbType.VarChar)
                {
                    Value = parameters.Password
                }
            };
            int IdUsuario = _context.Database.SqlQueryRaw<int>(Usuario.ToString(), sqlParameterUser)
                .FirstOrDefault();


            Perm.AppendLine("SELECT CAST(");
            Perm.AppendLine("IIF(");
            Perm.AppendLine("EXISTS(");
            Perm.AppendLine("SELECT 1");
            Perm.AppendLine("FROM dbo.tb_dep_usuarios u");
            Perm.AppendLine("JOIN dbo.tb_dep_sistema_perfil_acesso_usuarios spu");
            Perm.AppendLine("ON spu.id_usuario = u.id_usuario");
            Perm.AppendLine("JOIN dbo.tb_dep_sistema_perfil_acesso tdspa");
            Perm.AppendLine("ON tdspa.id_perfil_acesso = spu.id_perfil_acesso");
            Perm.AppendLine("JOIN dbo.tb_dep_sistema_perfil_acesso_sub_modulos tdspasm");
            Perm.AppendLine("ON tdspasm.id_perfil_acesso = tdspa.id_perfil_acesso");
            Perm.AppendLine("JOIN dbo.tb_dep_sistema_sub_modulos tdssm");
            Perm.AppendLine("ON tdssm.id_sub_modulo = tdspasm.id_sub_modulo");
            Perm.AppendLine("WHERE u.id_usuario = @IdUsuario");
            Perm.Append("    AND tdspa.id_perfil_acesso = @IdPerfilAcesso");
            Perm.Append("    AND tdssm.id_sub_modulo = @IdSubModulo");
            Perm.AppendLine("),");
            Perm.AppendLine("1, 0");
            Perm.AppendLine(") AS bit");
            Perm.AppendLine(") AS Value");

            SqlParameter[] sqlParameter = new SqlParameter[]
            {
                new SqlParameter("@IdUsuario", SqlDbType.Int)
                {
                    Value = IdUsuario
                },
                new SqlParameter("@IdPerfilAcesso", SqlDbType.Int)
                {
                    Value = IdPerfilAcesso
                },
                new SqlParameter("@IdSubModulo", SqlDbType.Int)
                {
                    Value = IdSubModulo
                }
            };

            bool Exists = _context.Database
                .SqlQueryRaw<bool>(Perm.ToString(), sqlParameter)
                .FirstOrDefault();

            #endregion Consulta

            #region Validação

            if (IdUsuario == 0)
                return MensagemViewHelper.SetNotFound("Usuario não Encontrado");
            if (!Exists)
                return MensagemViewHelper.SetBadRequest("Usuario não possui permissão");

            #endregion Validação


            return MensagemViewHelper.SetOk();
        }

        public async Task<ValidacaoGuiaAutorizacaoRetiradaVeiculoDTO> ValidarGuiaAutorizacaoRetiradaVeiculoAsync(
            string input, int UsuarioId)
        {
            ValidacaoGuiaAutorizacaoRetiradaVeiculoDTO ResultView = new();

            if (input.IsNullOrWhiteSpace())
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("QRCode não informado");

                return ResultView;
            }

            string key = AppSettingsHelper.GetValue("Segredo", "QRCode");

            string decrypted = string.Empty;

            try
            {
                decrypted = CryptographyHelper.DecryptString(key, input);
            }
            catch (Exception)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("QRCode inválido");

                return ResultView;
            }

            string[] splitted = decrypted.Split('|');

            if (splitted.Length != 3 || !NumberHelper.IsNumber(splitted[0]) ||
                !DateTimeHelper.IsDate(splitted[1] + splitted[2], "yyyyMMddHH:mm:ss"))
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Parâmetro inválido");

                return ResultView;
            }

            ResultView = new()
            {
                Mensagem = new GrvService(_context)
                    .ValidateInputGrv(splitted[0].ToInt(), UsuarioId)
            };

            if (ResultView.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            GrvModel Grv = await _context.Grv
                .Include(x => x.TipoVeiculo)
                .Include(x => x.StatusOperacao)
                .Include(x => x.Cliente)
                .Include(x => x.Deposito)
                .ThenInclude(x => x.Endereco)
                .Include(x => x.MarcaModelo)
                .Include(x => x.Cor)
                .Include(x => x.ListagemLacre)
                .Include(x => x.Atendimento)
                .Include(x => x.Liberacao)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.GrvId == splitted[0].ToInt());

            if (Grv == null)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest(MensagemPadraoEnum.NaoEncontradoGrv);

                return ResultView;
            }
            else if (Grv.StatusOperacaoId == "E")
            {
                if (DateTime.Now.Date > Grv.Liberacao.DataCadastro.Date)
                {
                    ResultView.Mensagem.Alertas.Add(
                        $"Este Processo foi entregue em {Grv.Liberacao.DataCadastro:dd/MM/yyyy}");
                }
            }

            string EnderecoDeposito = string.Empty;

            if (Grv.Deposito.Endereco != null)
            {
                EnderecoDeposito = new EnderecoService(_context, _mapper)
                    .FormatarEndereco(Grv.Deposito.Endereco, Grv.Deposito.NumeroEndereco,
                        Grv.Deposito.ComplementoEndereco);
            }
            else
            {
                BairroModel Bairro = new();

                if (Grv.Deposito.BairroId != null)
                {
                    Bairro = await _context.Bairro
                        .Include(x => x.Municipio)
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.BairroId == Grv.Deposito.BairroId);

                    EnderecoDeposito = new EnderecoService(_context, _mapper)
                        .FormatarEndereco(string.Empty,
                            Grv.Deposito.Logradouro,
                            Grv.Deposito.NumeroEndereco,
                            Grv.Deposito.ComplementoEndereco,
                            Bairro.NomePtbr,
                            Bairro.Municipio.NomePtbr,
                            Bairro.Municipio.UF);
                }
                else
                {
                    EnderecoDeposito = new EnderecoService(_context, _mapper)
                        .FormatarEndereco(string.Empty,
                            Grv.Deposito.Logradouro,
                            Grv.Deposito.NumeroEndereco,
                            Grv.Deposito.ComplementoEndereco,
                            string.Empty,
                            string.Empty,
                            string.Empty);
                }
            }

            ResultView = new()
            {
                IdentificadorProcesso = splitted[0].ToInt(),

                Cliente = Grv.Cliente.Nome,

                Deposito = Grv.Deposito.Nome,

                EnderecoDeposito = EnderecoDeposito,

                NumeroProcesso = Grv.NumeroFormularioGrv,

                DataHoraGuarda = Grv.DataHoraGuarda.Value,

                Placa = Grv.Placa,

                Chassi = Grv.Chassi,

                Renavam = Grv.Renavam,

                TipoVeiculo = Grv.TipoVeiculo.Descricao,

                MarcaModelo = Grv.MarcaModelo.MarcaModelo,

                Cor = Grv.Cor.CorSecundaria,

                Setor = Grv.EstacionamentoSetor,

                Vaga = Grv.EstacionamentoNumeroVaga,

                LocalizacaoChaveClaviculario = Grv.NumeroChave,

                DataHoraAutorizacaoRetirada = DateTime.Now,

                UsuarioResponsavelAtendimento = await _context.Usuario
                    .Where(x => x.UsuarioId == Grv.Atendimento.UsuarioCadastroId)
                    .Select(x => x.Login)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(),

                PessoaResponsavelLiberacao = Grv.Atendimento.ResponsavelNome.Trim(),

                DocumentoPessoaResponsavelLiberacao = Grv.Atendimento.ResponsavelDocumento.Length == 11
                    ? DocumentHelper.FormatCPF(Grv.Atendimento.ResponsavelDocumento)
                    : DocumentHelper.FormatCNPJ(Grv.Atendimento.ResponsavelDocumento),

                ResponsavelNome = Grv.Atendimento.ResponsavelNome,

                ResponsavelCPF = DocumentHelper.FormatCPF(Grv.Atendimento.ResponsavelDocumento),

                ResponsavelCNH = Grv.Atendimento.ResponsavelCnh,

                FormaLiberacaoCNH = Grv.Atendimento.FormaLiberacaoCNH,

                FormaLiberacaoCPF = Grv.Atendimento.FormaLiberacaoCPF,

                FormaLiberacaoPlaca = Grv.Atendimento.FormaLiberacaoPlaca,

                Mensagem = MensagemViewHelper.SetOk()
            };

            if (Grv.Atendimento.FormaLiberacao != null)
            {
                if (Grv.Atendimento.FormaLiberacao.Equals("C"))
                {
                    ResultView.FormaLiberacao = "Condutor Habilitado";
                }
                else
                {
                    ResultView.FormaLiberacao = "Reboque";
                }
            }
            else
            {
                ResultView.FormaLiberacao = "Especial";
            }

            if (Grv.ListagemLacre?.Count > 0)
            {
                ResultView.ListagemLacre = new();

                ResultView.ListagemLacre.AddRange(Grv.ListagemLacre
                    .Select(x => x.Lacre)
                    .OrderBy(x => x)
                    .ToList());
            }

            ImageListDTO FotoResponsavel = await new AtendimentoService(_context, _mapper, _httpClientFactory)
                .GetFotoResponsavelAsync(Grv.Atendimento.AtendimentoId, UsuarioId);

            if (FotoResponsavel.Listagem?.Count > 0)
            {
                ResultView.FotoResponsavel = FotoResponsavel.Listagem
                    .FirstOrDefault()
                    .Imagem;
            }

            return ResultView;
        }

        public async Task<MensagemDTO> EntregaSimplificadaAsync(EntregaSimplificadaParameters Parameters)
        {
            MensagemDTO ResultView = new GrvService(_context)
                .ValidateInputGrv(Parameters.IdentificadorProcesso, Parameters.IdentificadorUsuario);

            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            TipoLiberacaoModel TipoLiberacao = await _context
                .TipoLiberacao
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.TipoLiberacaoId == Parameters.IdentificadorTipoLiberacao);

            if (Parameters.IdentificadorTipoLiberacao <= 0)
                return MensagemViewHelper.SetBadRequest("Precisa ter um tipo de liberação");

            GrvModel Grv = await _context.Grv
                .Include(x => x.StatusOperacao)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.GrvId == Parameters.IdentificadorProcesso);

            if (Grv.StatusOperacao.StatusOperacaoId != "T" && Grv.StatusOperacao.StatusOperacaoId != "U" &&
                Grv.StatusOperacao.StatusOperacaoId != "R")
            {
                return MensagemViewHelper.SetBadRequest(
                    $"O Status atual deste Processo não permite o cadastro da Entrega. " +
                    $"Descrição do Status atual: {Grv.StatusOperacao.Descricao.ToUpper()}");
            }

            List<FaturamentoModel> Faturamentos = await _context.Faturamento
                .Include(x => x.Atendimento)
                .Where(x => x.Atendimento.GrvId == Parameters.IdentificadorProcesso && x.Status != "C")
                .AsNoTracking()
                .ToListAsync();

            if (Faturamentos == null)
            {
                return MensagemViewHelper.SetNotFound(MensagemPadraoEnum.NaoEncontradoFaturamento);
            }

            if (Faturamentos.Exists(x => x.Status == "N"))
            {
                return MensagemViewHelper.SetBadRequest($"Este Processo possui uma Fatura não paga");
            }

            DateTime DataHoraPorDeposito = new DepositoService(_context)
                .GetDataHoraPorDeposito(Grv.DepositoId);

            FaturamentoModel UltimoFaturamento = Faturamentos
                .OrderByDescending(x => x.DataCadastro)
                .FirstOrDefault();

            if (UltimoFaturamento.DataPrazoRetiradaVeiculo.Value.Date < DataHoraPorDeposito.Date)
            {
                return MensagemViewHelper.SetBadRequest(
                    $"O prazo para a Entrega do veículo está vencida ({UltimoFaturamento.DataPrazoRetiradaVeiculo.Value.Date:dd/MM/yyyy}), a Entrega não poderá ser realizada");
            }

            LiberacaoModel Liberacao = new()
            {
                TipoLiberacaoId = Parameters.IdentificadorTipoLiberacao,

                UsuarioCadastroId = Parameters.IdentificadorUsuario
            };


            using (IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Liberacao.AddAsync(Liberacao);

                    await _context.SaveChangesAsync();

                    if (Parameters.IdentificadorTipoLiberacao == 2)
                    {
                        await _context.LiberacaoEspecial
                            .Where(x => x.IdGrv == Parameters.IdentificadorProcesso)
                            .UpdateAsync(x => new LiberacaoEspecialModel() { DataLiberacao = Liberacao.DataCadastro });
                    }

                    if (Grv.StatusOperacaoId.Equals("R"))
                    {
                        if (Parameters.IdentificadorSaidaReparo == null)
                        {
                            ResultView = MensagemViewHelper.SetBadRequest("Propriedade obrigatória");
                            await transaction.RollbackAsync();
                            return ResultView;
                        }

                        await _context.SaidaReparo
                            .AsTracking()
                            .Where(x => x.Id == Parameters.IdentificadorSaidaReparo)
                            .UpdateAsync(x => new Domain.Models.Atendimento.AtendimentoSaidaParaReparoModel
                            {
                                DataRetorno = DateTime.Now,
                                IdUsuario = Parameters.IdentificadorUsuario
                            });
                    }

                    await _context.Grv
                        .Where(x => x.GrvId == Parameters.IdentificadorProcesso)
                        .UpdateAsync(x => new GrvModel()
                        {
                            LiberacaoId = Liberacao.LiberacaoId, StatusOperacaoId = "E",
                            UsuarioAlteracaoId = Parameters.IdentificadorUsuario
                        });

                    if (Parameters.ResponsavelFoto != null)
                    {
                        new BucketService(_context, _httpClientFactory)
                            .SendFile(BucketNomeTabelaOrigemEnum.EntregaFotoResponsavel, Liberacao.LiberacaoId,
                                Parameters.IdentificadorUsuario, Parameters.ResponsavelFoto);
                    }

                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();

                    return MensagemViewHelper.SetInternalServerError(ex);
                }
            }

            return MensagemViewHelper.SetCreateSuccess();
        }

        public async Task<MensagemDTO> EntregaAsync(EntregaSimplificadaParameters Parameters)
        {
            MensagemDTO ResultView = new GrvService(_context)
                .ValidateInputGrv(Parameters.IdentificadorProcesso, Parameters.IdentificadorUsuario);

            List<string> Errors = new List<string>();
            if (!string.IsNullOrWhiteSpace(Parameters.FormaLiberacao.FormaLiberacaoPlaca) &&
                !Parameters.FormaLiberacao.FormaLiberacaoPlaca.IsPlaca())
                Errors.Add("Placa inválida");
            if (!string.IsNullOrWhiteSpace(Parameters.FormaLiberacao.FormaLiberacaoCnh) &&
                !Parameters.FormaLiberacao.FormaLiberacaoCnh.IsCNH())
                Errors.Add("CNH inválido");
            if (!string.IsNullOrWhiteSpace(Parameters.FormaLiberacao.FormaLiberacaoCpf) &&
                !Parameters.FormaLiberacao.FormaLiberacaoCpf.IsCPF())
                Errors.Add("CPF inválido");

            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
                Errors.Add("Grv incorreto");

            if (Errors.Count > 0)
            {
                ResultView = MensagemViewHelper.SetBadRequest(Errors);
                return ResultView;
            }


            TipoLiberacaoModel TipoLiberacao = await _context
                .TipoLiberacao
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.TipoLiberacaoId == Parameters.IdentificadorTipoLiberacao);

            if (Parameters.IdentificadorTipoLiberacao <= 0)
                return MensagemViewHelper.SetBadRequest("Precisa ter um tipo de liberação");

            GrvModel Grv = await _context.Grv
                .Include(x => x.StatusOperacao)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.GrvId == Parameters.IdentificadorProcesso);

            var permitirEmissao = await _context.FaturamentoRegra
                .AnyAsync(x =>
                    x.ClienteId == Grv.ClienteId && x.DepositoId == Grv.DepositoId &&
                    x.FaturamentoRegraTipoId == 11);
            
            if (Grv.StatusOperacao.StatusOperacaoId != "T" && Grv.StatusOperacao.StatusOperacaoId != "U" &&
                Grv.StatusOperacao.StatusOperacaoId != "R")
            {
                return MensagemViewHelper.SetBadRequest(
                    $"O Status atual deste Processo não permite o cadastro da Entrega. " +
                    $"Descrição do Status atual: {Grv.StatusOperacao.Descricao.ToUpper()}");
            }

            List<FaturamentoModel> Faturamentos = await _context.Faturamento
                .Include(x => x.Atendimento)
                .Where(x => x.Atendimento.GrvId == Parameters.IdentificadorProcesso && x.Status != "C")
                .AsNoTracking()
                .ToListAsync();

            if (Faturamentos == null)
            {
                return MensagemViewHelper.SetNotFound(MensagemPadraoEnum.NaoEncontradoFaturamento);
            }

            if (Faturamentos.Exists(x => x.Status == "N"))
            {
                return MensagemViewHelper.SetBadRequest($"Este Processo possui uma Fatura não paga");
            }

            DateTime DataHoraPorDeposito = new DepositoService(_context)
                .GetDataHoraPorDeposito(Grv.DepositoId);

            FaturamentoModel UltimoFaturamento = Faturamentos
                .OrderByDescending(x => x.DataCadastro)
                .FirstOrDefault();

            if (UltimoFaturamento.DataPrazoRetiradaVeiculo.Value.Date < DataHoraPorDeposito.Date)
            {
                return MensagemViewHelper.SetBadRequest(
                    $"O prazo para a Entrega do veículo está vencida ({UltimoFaturamento.DataPrazoRetiradaVeiculo.Value.Date:dd/MM/yyyy}), a Entrega não poderá ser realizada");
            }

            LiberacaoModel Liberacao = new()
            {
                TipoLiberacaoId = Parameters.IdentificadorTipoLiberacao,

                UsuarioCadastroId = Parameters.IdentificadorUsuario
            };


            await using (IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Liberacao.AddAsync(Liberacao);

                    await _context.SaveChangesAsync();


                    await _context.Atendimento
                        .Where(x => x.AtendimentoId == Parameters.IdentificadorAtendimento)
                        .UpdateAsync(x => new AtendimentoModel()
                        {
                            FormaLiberacao = Parameters.FormaLiberacao.FormaLiberacao,
                            FormaLiberacaoNome = Parameters.FormaLiberacao.FormaLiberacaoNome,
                            FormaLiberacaoCNH = Parameters.FormaLiberacao.FormaLiberacaoCnh,
                            FormaLiberacaoCPF = Parameters.FormaLiberacao.FormaLiberacaoCpf,
                            FormaLiberacaoPlaca = Parameters.FormaLiberacao.FormaLiberacaoPlaca,
                        });

                    if (Parameters.IdentificadorTipoLiberacao == 2)
                    {
                        await _context.LiberacaoEspecial
                            .Where(x => x.IdGrv == Parameters.IdentificadorProcesso)
                            .UpdateAsync(x => new LiberacaoEspecialModel() { DataLiberacao = Liberacao.DataCadastro });
                    }

                    if (Grv.StatusOperacaoId.Equals("R"))
                    {
                        if (Parameters.IdentificadorSaidaReparo == null)
                        {
                            await transaction.RollbackAsync();
                            ResultView = MensagemViewHelper.SetBadRequest("Propriedade obrigatória");
                            return ResultView;
                        }

                        await _context.SaidaReparo
                            .AsTracking()
                            .Where(x => x.Id == Parameters.IdentificadorSaidaReparo)
                            .UpdateAsync(x => new Domain.Models.Atendimento.AtendimentoSaidaParaReparoModel
                            {
                                DataRetorno = DateTime.Now,
                                IdUsuario = Parameters.IdentificadorUsuario
                            });
                    }

                    if (_options.Value.Enable && permitirEmissao)
                    {
                        if (!string.Equals(Grv.StatusOperacaoId, "2") && !string.Equals(Grv.StatusOperacaoId, "R"))
                        {
                            await _provider
                                .GetService<WSNfseService>()
                                .CreateNfseAsync(Grv.GrvId, Parameters.IdentificadorUsuario);
                        }
                    }

                    if (string.Equals(Grv.StatusOperacaoId, "T") || string.Equals(Grv.StatusOperacaoId, "R") ||
                        string.Equals(Grv.StatusOperacaoId, "U"))
                    {
                        await _context.Grv
                            .Where(x => x.GrvId == Parameters.IdentificadorProcesso)
                            .UpdateAsync(x => new GrvModel()
                            {
                                LiberacaoId = Liberacao.LiberacaoId, StatusOperacaoId = "E",
                                UsuarioAlteracaoId = Parameters.IdentificadorUsuario
                            });
                    }
                    else if (string.Equals(Grv.StatusOperacaoId, "2") || string.Equals(Grv.StatusOperacaoId, "6"))
                    {
                        await _context.Grv
                            .Where(x => x.GrvId == Parameters.IdentificadorProcesso)
                            .UpdateAsync(x => new GrvModel()
                            {
                                LiberacaoId = Liberacao.LiberacaoId, StatusOperacaoId = "7",
                                UsuarioAlteracaoId = Parameters.IdentificadorUsuario
                            });
                    }

                    if (Parameters.ResponsavelFoto != null)
                    {
                        new BucketService(_context, _httpClientFactory)
                            .SendFile(BucketNomeTabelaOrigemEnum.EntregaFotoResponsavel, Liberacao.LiberacaoId,
                                Parameters.IdentificadorUsuario, Parameters.ResponsavelFoto);
                    }

                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();

                    return MensagemViewHelper.SetInternalServerError(ex.Message);
                }
            }

            return MensagemViewHelper.SetCreateSuccess();
        }
    }
}