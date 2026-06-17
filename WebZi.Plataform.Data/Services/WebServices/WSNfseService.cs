using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.ServiceModel;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.WSnfse;
using WebZi.Plataform.Domain.DTO.Sistema;
using WebZi.Plataform.Domain.DTO.WebServices.Nfse;
using WebZi.Plataform.Domain.Models.Nfe;
using WebZi.Plataform.Domain.Models.Sistema;
using WebZi.Plataform.Domain.Options;
using WebZi.Plataform.Domain.Services.GRV;
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