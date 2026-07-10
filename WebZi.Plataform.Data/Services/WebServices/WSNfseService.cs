using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.ServiceModel;
using Microsoft.IdentityModel.Tokens;
using WebZi.Plataform.CrossCutting.Documents;
using WebZi.Plataform.CrossCutting.Localizacao;
using WebZi.Plataform.CrossCutting.Strings;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Cliente;
using WebZi.Plataform.Data.WSnfse;
using WebZi.Plataform.Domain.DTO.Sistema;
using WebZi.Plataform.Domain.DTO.WebServices.Nfe;
using WebZi.Plataform.Domain.DTO.WebServices.Nfse;
using WebZi.Plataform.Domain.Models.Nfe;
using WebZi.Plataform.Domain.Models.Sistema;
using WebZi.Plataform.Domain.Options;
using WebZi.Plataform.Domain.Services.GRV;
using WebZi.Plataform.Domain.ViewModel.NFe;
using static System.Net.WebRequestMethods;

namespace WebZi.Plataform.Data.Services.WebServices
{
    public class WSNfseService
    {
        private readonly AppDbContext _context;
        private readonly IOptions<WSNfseOptions> _options;
        private readonly IMapper _mapper;

        public WSNfseService(AppDbContext context)
        {
            _context = context;
        }

        public WSNfseService(AppDbContext context, IOptions<WSNfseOptions> options)
        {
            _context = context;
            _options = options;
        }

        public WSNfseService(AppDbContext context, IOptions<WSNfseOptions> options, IMapper mapper)
        {
            _context = context;
            _options = options;
            _mapper = mapper;
        }

        public async Task<NFERetornoFaturamentoDTOList> ConsultarNfeAsync(int grvId, int usuarioId)
        {
            NFERetornoFaturamentoDTOList ResultView = new();
            List<string> Erros = new();


            ResultView.Mensagem = new GrvService(_context).ValidateInputGrv(grvId, usuarioId);

            if (ResultView.Mensagem.Erros.Count > 0)
            {
                return ResultView;
            }

            #region Consulta

            var notas = await _context.Nfe
                .Where(x =>
                    x.GrvId == grvId &&
                    !_context.Nfe.Any(j =>
                        j.GrvId == x.GrvId &&
                        j.NfeComplementarId == x.NfeId))
                .Select(x => new
                {
                    Nfe = x,
                    Composicoes = x.NfeFaturamentoComposicao
                        .Select(nfc => new
                        {
                            Valor = nfc.FaturamentoComposicao != null
                                ? nfc.FaturamentoComposicao.ValorComposicao
                                : 0,

                            Servico = nfc.FaturamentoComposicao != null &&
                                      nfc.FaturamentoComposicao.FaturamentoServicoTipoVeiculo != null &&
                                      nfc.FaturamentoComposicao.FaturamentoServicoTipoVeiculo
                                          .FaturamentoServicoAssociado != null
                                ? nfc.FaturamentoComposicao
                                    .FaturamentoServicoTipoVeiculo
                                    .FaturamentoServicoAssociado
                                    .Descricao
                                : null
                        })
                        .ToList()
                })
                .AsNoTracking()
                .ToListAsync();

            #endregion

            if (notas.Count <= 0)
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound("Não possui nota");
                return ResultView;
            }

            List<NFERetornoFaturamentoDTO> notasDto = new();

            var nfeIdentificadoresComErro = notas
                .Where(x => x.Nfe.Status == "E")
                .Select(x => x.Nfe.IdentificadorNota)
                .Distinct()
                .ToList();

            var erroPorIdentificadorNota = new Dictionary<int, NfeWsErrosModel>();

            if (nfeIdentificadoresComErro.Count > 0)
            {
                var errosNfe = await _context.NfeWsErros
                    .Where(x =>
                        x.GrvId == grvId &&
                        x.IdentificadorNota != null &&
                        nfeIdentificadoresComErro.Contains(x.IdentificadorNota.ToString()))
                    .AsNoTracking()
                    .ToListAsync();

                erroPorIdentificadorNota = errosNfe
                    .Where(x => x.IdentificadorNota.HasValue)
                    .GroupBy(x => x.IdentificadorNota.Value)
                    .ToDictionary(
                        g => g.Key,
                        g => g
                            .OrderByDescending(x => x.DataHoraCadastro)
                            .First());
            }

            foreach (var item in notas)
            {
                var nfe = item.Nfe;

                if (nfe.Status == "E")
                {
                    var nfDto = _mapper.Map<NFERetornoFaturamentoDTO>(nfe);

                    if (int.TryParse(nfe.IdentificadorNota, out var identificadorNota) &&
                        erroPorIdentificadorNota.TryGetValue(identificadorNota, out var erro))
                    {
                        nfDto.StatusErro = erro.Status;
                        nfDto.MensagemErro = erro.MensagemErro;
                        nfDto.CorrecaoErro = erro.CorrecaoErro;
                    }

                    if (item.Composicoes != null && item.Composicoes.Any())
                    {
                        nfDto.Valor = item.Composicoes.Sum(x => x.Valor);
                        nfDto.Servico = item.Composicoes.Count > 1
                            ? "Vários"
                            : item.Composicoes.FirstOrDefault()?.Servico;
                    }

                    notasDto.Add(nfDto);
                }
                else if (item.Composicoes != null && item.Composicoes.Any())
                {
                    foreach (var composicao in item.Composicoes)
                    {
                        var nfDto = _mapper.Map<NFERetornoFaturamentoDTO>(nfe);

                        nfDto.Valor = composicao.Valor;
                        nfDto.Servico = composicao.Servico;

                        notasDto.Add(nfDto);
                    }
                }
                else
                {
                    notasDto.Add(_mapper.Map<NFERetornoFaturamentoDTO>(nfe));
                }
            }

            ResultView.Listagem = notasDto;

            ResultView.Mensagem = MensagemViewHelper.SetFound(ResultView.Listagem.Count);

            return ResultView;
        }

        public async Task<MensagemDTO> ReprocessNfseAsync(int grvId, string notaId, int usuarioId)
        {
            MensagemDTO ResultView = new GrvService(_context).ValidateInputGrv(grvId, usuarioId);

            #region Consulta

            var nfeDB = await _context.Nfe
                .AsNoTracking()
                .AnyAsync(x => x.GrvId == grvId && x.IdentificadorNota == notaId);

            #endregion

            if (!nfeDB)
            {
                ResultView = MensagemViewHelper.SetOk("Nota não existe");
                return ResultView;
            }

            if (_options.Value.Enable)
            {
                var result = await ReprocessNfseFromWSAsync(grvId, notaId, usuarioId);
                if (result.Mensagem.Erros.Count > 0 || result.Mensagem.AvisosImpeditivos.Count > 0)
                {
                    ResultView = MensagemViewHelper.SetBadRequest(
                        result.Mensagem.Erros.Count > 0
                            ? string.Join(" | ", result.Mensagem.Erros)
                            : string.Join(" | ", result.Mensagem.AvisosImpeditivos)
                    );
                    return ResultView;
                }
            }

            return MensagemViewHelper.SetCreateSuccess("Nota Fiscal Emitida");
        }

        public async Task<MensagemDTO> CreateNfseAsync(int grvId, int usuarioId)
        {
            MensagemDTO ResultView = new GrvService(_context).ValidateInputGrv(grvId, usuarioId);

            #region Consulta

            var grv = await _context.Grv
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.GrvId == grvId);
            NfeModel nfeDB = await _context.Nfe
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.GrvId == grvId);

            var permitirEmissao = await _context.FaturamentoRegra
                .AnyAsync(x =>
                    x.ClienteId == grv.ClienteId && x.DepositoId == grv.DepositoId &&
                    x.FaturamentoRegraTipoId == 11);

            #endregion

            if (grv is null)
            {
                ResultView = MensagemViewHelper.SetNotFound("Não foi encontrado este grv");
                return ResultView;
            }

            if (nfeDB is not null)
            {
                ResultView = MensagemViewHelper.SetOk("Nota fiscal já foi emitida");
                return ResultView;
            }

            if (!_options.Value.Enable)
            {
                ResultView = MensagemViewHelper.SetCreateSuccess("Emissão de nota desativada");
                return ResultView;
            }

            if (_options.Value.Enable && !permitirEmissao)
            {
                ResultView = MensagemViewHelper.SetCreateSuccess("Não possui permissão para emitir nota");
                return ResultView;
            }


            var result = await CreateNfseFromWSAsync(grvId, usuarioId);
            if (result.Mensagem.Erros.Count > 0 || result.Mensagem.AvisosImpeditivos.Count > 0)
            {
                ResultView = MensagemViewHelper.SetBadRequest(
                    result.Mensagem.Erros.Count > 0
                        ? string.Join(" | ", result.Mensagem.Erros)
                        : string.Join(" | ", result.Mensagem.AvisosImpeditivos)
                );
                return ResultView;
            }

            return MensagemViewHelper.SetCreateSuccess("Nota Fiscal Emitida");
        }


        private async Task<WSNfseGerarNotaFiscalDTO> ReprocessNfseFromWSAsync(int grvId, string identificadorNota,
            int usuarioId)
        {
            WSNfseGerarNotaFiscalDTO ResultView = new();
            var config = _options?.Value;
            if (config == null)
            {
                throw new ArgumentException($"Falha ao achar a configuração de dev");
            }

            WebServiceUrlModel WebServiceUrl = await _context.WebServiceUrl
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Name == "Wsnfse");
            //var localhost = "http://localhost:8655/WSnfse.asmx";
            List<string>? avisos = new List<string>();
            List<string>? erros = new List<string>();

            List<string> result;
            try
            {
                // var response = await ClientConfig("http://localhost:8655/WSnfse.asmx")
                //     .GerarNovaNotaFiscalAsync(grvId, identificadorNota, usuarioId, config.IsDev);
                var response = await ClientConfig(WebServiceUrl.Url)
                    .GerarNovaNotaFiscalAsync(grvId, identificadorNota, usuarioId, config.IsDev);

                result = response?.Body?.GerarNovaNotaFiscalResult;
            }
            catch (Exception ex)
            {
                ResultView.Mensagem.Erros.Add($"Falha ao chamar WSNFSe: {ex.Message}");
                return ResultView;
            }

            foreach (var item in result.Where(x => !string.IsNullOrWhiteSpace(x)))
            {
                var txt = item.Trim();

                if (txt.StartsWith("ERRO:", StringComparison.OrdinalIgnoreCase))
                {
                    erros.Add(txt);
                    continue;
                }

                if (txt.StartsWith("AVISO:", StringComparison.OrdinalIgnoreCase))
                {
                    avisos.Add(txt);
                    continue;
                }

                try
                {
                    var value = JsonConvert.DeserializeObject<WSNfseGerarNotaFiscalDTO>(txt);
                    if (value != null)
                        ResultView = value;
                }
                catch
                {
                    avisos.Add($"Retorno não reconhecido: {txt}");
                }
            }

            if (erros?.Count > 0)
            {
                ResultView.Mensagem.Erros.Add(string.Join(" | ", erros));
            }

            if (avisos?.Count > 0)
            {
                ResultView.Mensagem.AvisosImpeditivos.Add(string.Join(" | ", avisos));
            }

            return ResultView;
        }

        private async Task<WSNfseGerarNotaFiscalDTO> CreateNfseFromWSAsync(int grvId, int usuarioId)
        {
            WSNfseGerarNotaFiscalDTO ResultView = new();
            var config = _options?.Value;
            if (config == null)
            {
                throw new ArgumentException($"Falha ao achar a configuração de dev");
            }

            WebServiceUrlModel WebServiceUrl = await _context.WebServiceUrl
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Name == "Wsnfse");
            // var localhost = "http://localhost:8655/WSnfse.asmx";
            List<string>? avisos = new List<string>();
            List<string>? erros = new List<string>();

            List<string> result;
            try
            {
                // var response = await ClientConfig("http://localhost:8655/WSnfse.asmx")
                //     .GerarNotaFiscalAsync(grvId, usuarioId, config.IsDev);
                var response = await ClientConfig(WebServiceUrl.Url)
                    .GerarNotaFiscalAsync(grvId, usuarioId, config.IsDev);

                result = response?.Body?.GerarNotaFiscalResult;
            }
            catch (Exception ex)
            {
                ResultView.Mensagem.Erros.Add($"Falha ao chamar WSNFSe: {ex.Message}");
                return ResultView;
            }

            foreach (var item in result.Where(x => !string.IsNullOrWhiteSpace(x)))
            {
                var txt = item.Trim();

                if (txt.StartsWith("ERRO:", StringComparison.OrdinalIgnoreCase))
                {
                    erros.Add(txt);
                    continue;
                }

                if (txt.StartsWith("AVISO:", StringComparison.OrdinalIgnoreCase))
                {
                    avisos.Add(txt);
                    continue;
                }

                try
                {
                    var value = JsonConvert.DeserializeObject<WSNfseGerarNotaFiscalDTO>(txt);
                    if (value != null)
                        ResultView = value;
                }
                catch
                {
                    avisos.Add($"Retorno não reconhecido: {txt}");
                }
            }

            if (erros?.Count > 0)
            {
                ResultView.Mensagem.Erros.Add(string.Join(" | ", erros));
            }

            if (avisos?.Count > 0)
            {
                ResultView.Mensagem.AvisosImpeditivos.Add(string.Join(" | ", avisos));
            }

            return ResultView;
        }

        public async Task<NfeJsonEnvioDTO> GetJsonNfeAsync(long nfeId)
        {
            NfeJsonEnvioDTO ResultView = new();

            #region Consulta

            var nfe = await _context.NfeRetornoSolicitacao.AsNoTracking().FirstOrDefaultAsync(x => x.NfeId == nfeId);

            #endregion

            if (nfe == null)
            {
                ResultView.Mensagem.Erros.Add("Nenhuma NFe correspondente encontrada para este identificador.");
                return ResultView;
            }

            if (string.IsNullOrEmpty(nfe.Json))
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();
                return ResultView;
            }

            try
            {
                var parsedJson = JsonConvert.DeserializeObject(nfe.Json);
                ResultView.Json = JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
                ResultView.Mensagem = MensagemViewHelper.SetFound();
                return ResultView;
            }
            catch (Exception e)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest(e.Message);
                return ResultView;
            }
        }

        public async Task<MensagemDTO> UpdateNFeAsync(AtualizarDadosNFeParameters parameters)
        {
            MensagemDTO ResultView = await ValidateInputsAsync(parameters);

            if (ResultView.AvisosImpeditivos.Count > 0 || ResultView.Erros.Count > 0)
            {
                return ResultView;
            }

            #region Consulta

            var atendimento = await _context.Atendimento.AsTracking()
                .FirstOrDefaultAsync(x => x.GrvId == parameters.IdentificadorProcesso);
            var permitirEmissao = await _context.FaturamentoRegra
                .AnyAsync(x =>
                    x.ClienteId == parameters.IdentificadorCliente &&
                    x.DepositoId == parameters.IdentificadorDeposito &&
                    x.FaturamentoRegraTipoId == 11);
            var permiteEdicaoNota = await _context.PerfilAcessoUsuario
                .AsNoTracking()
                .AnyAsync(x => x.UsuarioId == parameters.IdentificadorUsuario
                               // && x.PerfilAcessoId == 81
                               && x.PerfilAcessoId == 81 // AMBIENTE DE HOMOLOG
                               && _context.SistemaPerfilAcessoSubModulos
                                   .Any(s => s.IdPerfilAcesso == 81 && s.IdSubModulo == 167)); // AMBIENTE DE HOMOLOG 
            // .Any(s => s.IdPerfilAcesso == 81 && s.IdSubModulo == 164));

            #endregion

            if (atendimento == null)
            {
                ResultView = MensagemViewHelper.SetNotFound();
                return ResultView;
            }

            try
            {
                var documento = string.Empty;
                if (!string.IsNullOrEmpty(parameters.Cpf) && parameters.Cpf.IsCPF())
                {
                    documento = parameters.Cpf;
                }
                else if (!string.IsNullOrEmpty(parameters.Cnpj) && parameters.Cnpj.IsCNPJ())
                {
                    documento = parameters.Cnpj;
                }

                if (permitirEmissao && permiteEdicaoNota)
                {
                    atendimento.NotaFiscalNome = parameters.Nome.ToUpperTrim();
                    atendimento.NotaFiscalDocumento = documento.Replace(".", "")
                        .Replace("/", "")
                        .Replace("-", "");
                    atendimento.NotaFiscalEndereco = parameters.Logradouro.ToUpperTrim();
                    atendimento.NotaFiscalNumero = parameters.Numero.ToUpperTrim();
                    atendimento.NotaFiscalCEP = parameters.Cep.Replace("-", "");
                    atendimento.NotaFiscalComplemento = parameters.Complemento.ToUpperTrim();
                    atendimento.NotaFiscalBairro = parameters.Bairro.ToUpperTrim();
                    atendimento.NotaFiscalMunicipio = parameters.Municipio.ToUpperTrim();
                    atendimento.NotaFiscalUF = parameters.UF.ToUpperTrim();
                    atendimento.NotaFiscalDDD = parameters.DDD;
                    atendimento.NotaFiscalTelefone = parameters.Telefone.Replace("-", "");
                    atendimento.NotaFiscalEmail = parameters.Email.ToLowerTrim();
                    atendimento.NotaFiscalInscricaoMunicipal =
                        parameters.InscricaoMTS.ToUpperTrim();
                }
                else
                {
                    if (!permitirEmissao)
                    {
                        ResultView.AvisosImpeditivos.Add(
                            "Emissão de nota fiscal não permitida para as configurações do cliente/depósito atuais.");
                    }

                    if (!permiteEdicaoNota)
                    {
                        ResultView.AvisosImpeditivos.Add("Usuário sem permissão para editar dados da Nota Fiscal.");
                    }

                    ResultView.HtmlStatusCode = CrossCutting.Web.HtmlStatusCodeEnum.BadRequest;
                    return ResultView;
                }

                atendimento.UsuarioAlteracaoId = parameters.IdentificadorUsuario;
                atendimento.DataAlteracao = DateTime.Now;

                await _context.SaveChangesAsync();

                ResultView = MensagemViewHelper.SetUpdateSuccess();
                return ResultView;
            }
            catch (Exception e)
            {
                ResultView = MensagemViewHelper.SetBadRequest(e.Message);
                return ResultView;
            }
        }

        private async Task<MensagemDTO> ValidateInputsAsync(AtualizarDadosNFeParameters parameters)
        {
            MensagemDTO ResultView = new();

            var clienteResult =
                await new ClienteService(_context).ValidateClienteAsync(parameters.IdentificadorCliente);
            if (clienteResult.AvisosImpeditivos.Count > 0)
            {
                ResultView.AvisosImpeditivos.AddRange(clienteResult.AvisosImpeditivos);
            }

            if (clienteResult.Erros.Count > 0)
            {
                ResultView.Erros.AddRange(clienteResult.Erros);
            }

            var GrvResult =
                new GrvService(_context).ValidateInputGrv(parameters.IdentificadorProcesso,
                    parameters.IdentificadorUsuario);
            if (GrvResult.AvisosImpeditivos.Count > 0)
            {
                ResultView.AvisosImpeditivos.AddRange(GrvResult.AvisosImpeditivos);
            }

            if (GrvResult.Erros.Count > 0)
            {
                ResultView.Erros.AddRange(GrvResult.Erros);
            }

            if (string.IsNullOrWhiteSpace(parameters.Nome))
            {
                ResultView.AvisosImpeditivos.Add("Informe o Nome do Receptor da Nota Fiscal.");
            }

            if (string.IsNullOrWhiteSpace(parameters.Cpf) && string.IsNullOrWhiteSpace(parameters.Cnpj))
            {
                ResultView.AvisosImpeditivos.Add("Informe o CPF ou CNPJ do Receptor da Nota Fiscal.");
            }
            else
            {
                if (!string.IsNullOrEmpty(parameters.Cpf) && !parameters.Cpf.IsCPF())
                {
                    ResultView.AvisosImpeditivos.Add("CPF do Receptor da Nota Fiscal inválido.");
                }

                if (!string.IsNullOrEmpty(parameters.Cnpj) && !parameters.Cnpj.IsCNPJ())
                {
                    ResultView.AvisosImpeditivos.Add("CNPJ do Receptor da Nota Fiscal inválido.");
                }
            }

            if (string.IsNullOrWhiteSpace(parameters.Cep))
            {
                ResultView.AvisosImpeditivos.Add("Informe o CEP do Receptor da Nota Fiscal.");
            }
            else if (!parameters.Cep.IsCEP())
            {
                ResultView.AvisosImpeditivos.Add("CEP do Receptor da Nota Fiscal inválido.");
            }

            if (ResultView.AvisosImpeditivos.Count > 0 || ResultView.Erros.Count > 0)
            {
                ResultView.HtmlStatusCode = WebZi.Plataform.CrossCutting.Web.HtmlStatusCodeEnum.BadRequest;
            }

            return ResultView;
        }


        private WSnfseSoapClient ClientConfig(string WebServiceUrl)
        {
            BasicHttpBinding httpBinding = new()
            {
                MaxReceivedMessageSize = int.MaxValue,
                MaxBufferSize = int.MaxValue
            };

            // httpBinding.Security.Mode = BasicHttpSecurityMode.Transport;
            httpBinding.Security.Mode = BasicHttpSecurityMode.None;
            // httpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
            WSnfseSoapClient client = new(httpBinding, new(new Uri(WebServiceUrl)));

            client.ChannelFactory.CreateChannel();

            return client;
        }
    }
}