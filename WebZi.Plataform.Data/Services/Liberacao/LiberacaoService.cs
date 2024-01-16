using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
using WebZi.Plataform.Data.Services.Faturamento;
using WebZi.Plataform.Data.Services.Localizacao;
using WebZi.Plataform.Data.Services.Report;
using WebZi.Plataform.Domain.DTO.Generic;
using WebZi.Plataform.Domain.DTO.Report;
using WebZi.Plataform.Domain.Enums;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Models.Localizacao;
using WebZi.Plataform.Domain.Services.GRV;
using WebZi.Plataform.Domain.Views.Usuario;

namespace WebZi.Plataform.Data.Services.Liberacao
{
    public class LiberacaoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;

        public LiberacaoService(AppDbContext context)
        {
            _context = context;
        }

        public LiberacaoService(AppDbContext context, IMapper mapper, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<GuiaAutorizacaoRetiradaVeiculoDTO> CreateGuiaAutorizacaoRetiradaVeiculoAsync(int GrvId, int UsuarioId)
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
                .Include(x => x.StatusOperacao)
                .Include(x => x.Cliente)
                .Include(x => x.Liberacao)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.GrvId == GrvId);

            if (Grv.StatusOperacaoId is not "R" and not "T" and not "E")
            {
                ResultView.Mensagem = MensagemViewHelper
                    .SetBadRequest($"O Status atual deste Processo não permite a geração do Documento. Status atual: {Grv.StatusOperacao.Descricao}");

                return ResultView;
            }
            else if (Grv.StatusOperacaoId == "E")
            {
                if (DateTime.Now.Date > Grv.Liberacao.DataCadastro.Date)
                {
                    ResultView.Mensagem.Alertas
                        .Add($"Este Processo foi entregue em {Grv.Liberacao.DataCadastro:dd/MM/yyyy}, as informações impressas no Documento estão desatualizadas");
                }
            }

            int FaturamentoId = await new FaturamentoService(_context).GetUltimoFaturamentoIdAsync(GrvId);

            if (FaturamentoId == 0)
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound("Faturamento não encontrado");

                return ResultView;
            }

            GuiaPagamentoReboqueEstadiaDTO GuiaPagamentoReboqueEstadia = await new GuiaPagamentoReboqueEstadiaService(_context, _mapper, _httpClientFactory)
                .GetGuiaPagamentoReboqueEstadiaAsync(FaturamentoId, UsuarioId, true);

            if (GuiaPagamentoReboqueEstadia == null)
            {
                ResultView.Mensagem = MensagemViewHelper
                    .SetNotFound("Informações para a geração da Guia de Autorização para a Retirada do Veículo não encontradas");

                return ResultView;
            }

            ResultView.ClienteNome = GuiaPagamentoReboqueEstadia.ClienteNome;

            ResultView.ClienteEndereco = GuiaPagamentoReboqueEstadia.ClienteEndereco;

            ResultView.DadosCodigoAutorizacao = "Link para validação";

            ResultView.DadosProcessoGrv = "Dados do Processo Processo: " + GuiaPagamentoReboqueEstadia.NumeroFormularioGrv + " - " + "Depósito: " + GuiaPagamentoReboqueEstadia.DepositoNome;

            ResultView.DadosTipoProcesso = "REGISTRO DE APREENSÃO";

            ResultView.DadosReboque = !string.IsNullOrWhiteSpace(GuiaPagamentoReboqueEstadia.ReboquePlaca) ?
                VeiculoHelper.FormatPlaca(GuiaPagamentoReboqueEstadia.ReboquePlaca) :
                string.Empty;

            ResultView.DadosDataEntrada = GuiaPagamentoReboqueEstadia.DataHoraGuarda.Left(10);

            ResultView.DadosHoraEntrada = GuiaPagamentoReboqueEstadia.DataHoraGuarda.Right(5);

            ResultView.DadosPermanencia = GuiaPagamentoReboqueEstadia.QuantidadeEstadias == 1 ?
                GuiaPagamentoReboqueEstadia.QuantidadeEstadias.ToString() + " dia" :
                GuiaPagamentoReboqueEstadia.QuantidadeEstadias.ToString() + " dias";

            ResultView.DadosAutorizadaRetiradaVeiculoEm = GuiaPagamentoReboqueEstadia.FaturamentoDataVencimento.Left(10);

            ResultView.VeiculoMarcaModelo = GuiaPagamentoReboqueEstadia.MarcaModelo;

            ResultView.VeiculoPlaca = VeiculoHelper.FormatPlaca(GuiaPagamentoReboqueEstadia.Placa);

            ResultView.VeiculoRenavam = GuiaPagamentoReboqueEstadia.Renavam;

            ResultView.VeiculoChassi = GuiaPagamentoReboqueEstadia.Chassi;

            ResultView.VeiculoCor = GuiaPagamentoReboqueEstadia.Cor;

            ResultView.TextoApresentacao = "Este documento deverá ser apresentado no Depósito de Veículos localizado no endereço " +
                GuiaPagamentoReboqueEstadia.DepositoEndereco + ", até a data: " +
                GuiaPagamentoReboqueEstadia.PrazoRetiradaVeiculo.Left(10) + ", para que a retirada do veículo seja autorizada. A não apresentação até a data informada acarretará na cobrança de estadias adicionais.";

            ResultView.ProprietarioProcurador = "Proprietário/Procurador: " + GuiaPagamentoReboqueEstadia.AtendimentoResponsavelNome;

            ResultView.ProprietarioCpf = "CPF: " + GuiaPagamentoReboqueEstadia.AtendimentoResponsavelDocumento;

            ResultView.GrvEstacionamentoSetor = !GuiaPagamentoReboqueEstadia.EstacionamentoSetor.IsNullOrWhiteSpace() ? GuiaPagamentoReboqueEstadia.EstacionamentoSetor : "Não informado";

            ResultView.GrvEstacionamentoNumeroVaga = !GuiaPagamentoReboqueEstadia.EstacionamentoNumeroVaga.IsNullOrWhiteSpace() ? GuiaPagamentoReboqueEstadia.EstacionamentoNumeroVaga : "Não informado";

            ResultView.GrvNumeroChave = GuiaPagamentoReboqueEstadia.NumeroChave.IsNullOrWhiteSpace() ? GuiaPagamentoReboqueEstadia.NumeroChave : "Não informado";

            ViewUsuarioModel Usuario = await _context.ViewUsuario
                .FirstOrDefaultAsync(x => x.UsuarioId == UsuarioId);

            ResultView.UsuarioNome = Usuario.NomeCompleto;

            ResultView.UsuarioMatricula = "Matrícula: " + Usuario.Matricula;


            #region FORMA DE LIBERAÇÃO

            ResultView.AtendimentoFormaLiberacaoNome = GuiaPagamentoReboqueEstadia.AtendimentoFormaLiberacaoNome;

            ResultView.AtendimentoFormaLiberacaoCNH = GuiaPagamentoReboqueEstadia.AtendimentoFormaLiberacaoCNH;

            if (GuiaPagamentoReboqueEstadia.AtendimentoFormaLiberacao == "C")
            {
                ResultView.AtendimentoFormaLiberacao = "Condutor habilitado";

                ResultView.AtendimentoFormaLiberacaoCpfPlaca = DocumentHelper.FormatCPF(GuiaPagamentoReboqueEstadia.AtendimentoFormaLiberacaoCPF);

                ResultView.LabelAtendimentoFormaLiberacaoCpfPlaca = "CPF:";
            }
            else if (GuiaPagamentoReboqueEstadia.AtendimentoFormaLiberacao == "R")
            {
                ResultView.AtendimentoFormaLiberacao = "Reboque";

                ResultView.AtendimentoFormaLiberacaoCpfPlaca = VeiculoHelper.FormatPlaca(GuiaPagamentoReboqueEstadia.AtendimentoFormaLiberacaoPlaca);

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

            string encrypted = CryptographyHelper.EncryptString(key, GrvId + "|" + DateTime.Now.ToString("yyyyMMdd") + "|" + DateTime.Now.ToString("HH:mm:ss"));

            string decrypted = CryptographyHelper.DecryptString(key, encrypted);

            ResultView.QRCode = QRCodeHelper.CreateImageAsByteArray(encrypted, "PNG");

            ResultView.Mensagem = MensagemViewHelper.SetOk(ResultView.Mensagem, "Documento gerado com sucesso");

            return ResultView;
        }

        public async Task<ValidacaoGuiaAutorizacaoRetiradaVeiculoDTO> ValidarGuiaAutorizacaoRetiradaVeiculoAsync(string input, int UsuarioId)
        {
            ValidacaoGuiaAutorizacaoRetiradaVeiculoDTO ResultView = new();

            if (input.IsNullOrWhiteSpace())
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Parâmetro inválido");

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
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Parâmetro inválido");

                return ResultView;
            }

            string[] splitted = decrypted.Split('|');

            if (splitted.Length != 3 || !NumberHelper.IsNumber(splitted[0]) || !DateTimeHelper.IsDate(splitted[1] + splitted[2], "yyyyMMddHH:mm:ss"))
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
                .Include(x => x.StatusOperacao)
                .Include(x => x.Cliente)
                .Include(x => x.Deposito)
                .ThenInclude(x => x.Endereco)
                .Include(x => x.MarcaModelo)
                .Include(x => x.Cor)
                .Include(x => x.ListagemLacre)
                .Include(x => x.Atendimento)
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
                    ResultView.Mensagem.Alertas.Add($"Este Processo foi entregue em {Grv.Liberacao.DataCadastro:dd/MM/yyyy}");
                }
            }

            string EnderecoDeposito = string.Empty;

            if (Grv.Deposito.Endereco != null)
            {
                EnderecoDeposito = new EnderecoService(_context, _mapper)
                    .FormatarEndereco(Grv.Deposito.Endereco, Grv.Deposito.NumeroEndereco, Grv.Deposito.ComplementoEndereco);
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

                MarcaModelo = Grv.MarcaModelo.MarcaModelo,

                Cor = Grv.Cor.CorSecundaria,

                Setor = Grv.EstacionamentoSetor,

                Vaga = Grv.EstacionamentoNumeroVaga,

                LocalizacaoChaveClaviculario = Grv.NumeroChave,

                DataAutorizacaoRetirada = DateTime.Now,

                UsuarioResponsavelAtendimento = await _context.Usuario
                    .Where(x => x.UsuarioId == Grv.Atendimento.UsuarioCadastroId)
                    .Select(x => x.Login)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(),

                PessoaResponsavelLiberacao = Grv.Atendimento.ResponsavelNome,

                DocumentoPessoaResponsavelLiberacao = Grv.Atendimento.ResponsavelDocumento.Length == 11 ?
                    DocumentHelper.FormatCPF(Grv.Atendimento.ResponsavelDocumento) :
                    DocumentHelper.FormatCNPJ(Grv.Atendimento.ResponsavelDocumento),

                ResponsavelNome = Grv.Atendimento.ResponsavelNome,

                ResponsavelCPF = DocumentHelper.FormatCPF(Grv.Atendimento.ResponsavelDocumento),

                ResponsavelCNH = Grv.Atendimento.ResponsavelCnh,

                FormaLiberacao = Grv.Atendimento.FormaLiberacao.Equals("C") ? "Condutor Habilitado" : "Reboque",

                FormaLiberacaoCNH = Grv.Atendimento.FormaLiberacaoCNH,

                FormaLiberacaoCPF = Grv.Atendimento.FormaLiberacaoCPF,

                FormaLiberacaoPlaca = Grv.Atendimento.FormaLiberacaoPlaca,

                Mensagem = MensagemViewHelper.SetOk()
            };

            if (Grv.ListagemLacre?.Count > 0)
            {
                Grv.ListagemLacre = Grv.ListagemLacre
                    .OrderBy(x => x.Lacre)
                    .ToList();

                foreach (LacreModel item in Grv.ListagemLacre)
                {
                    ResultView.ListagemLacre.Add(item.Lacre);
                }
            }

            ImageListDTO FotoResponsavel = await new AtendimentoService(_context, _mapper, _httpClientFactory)
                .GetResponsavelFotoAsync(Grv.Atendimento.AtendimentoId, UsuarioId);

            if (FotoResponsavel.Listagem?.Count > 0)
            {
                ResultView.FotoResponsavel = FotoResponsavel.Listagem
                    .FirstOrDefault()
                    .Imagem;
            }

            return ResultView;
        }
    }
}