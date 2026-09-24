using Microsoft.Extensions.Options;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Sistema;
using WebZi.Plataform.Domain.DTO.Banco;
using WebZi.Plataform.Domain.DTO.GGV;
using WebZi.Plataform.Domain.DTO.Sistema;
using WebZi.Plataform.Domain.DTO.Transalvador;
using WebZi.Plataform.Domain.DTO.Transalvador.DAT.Consultar;
using WebZi.Plataform.Domain.DTO.Transalvador.DAT.Gerar;
using WebZi.Plataform.Domain.DTO.Transalvador.Entrada;
using WebZi.Plataform.Domain.DTO.Transalvador.Liberacao;
using WebZi.Plataform.Domain.Options;
using WebZi.Plataform.Domain.ViewModel.Transalvador.DAT.Consultar;
using WebZi.Plataform.Domain.ViewModel.Transalvador.DAT.Gerar;
using WebZi.Plataform.Domain.ViewModel.Transalvador.Entrada;
using WebZi.Plataform.Domain.ViewModel.Transalvador.Liberacao;

namespace WebZi.Plataform.Data.Services.Transalvador;

public class TransalvadorService
{
    private readonly IHttpClientFactory _http;
    private readonly IOptions<TransalvadorApiOptions> _options;

    public TransalvadorService(IOptions<TransalvadorApiOptions> options,
        IHttpClientFactory http)
    {
        _options = options;
        _http = http;
    }

    private const string EEntrada = "patio/veiculos/entrada";
    private const string EEmitirDAT = "dat";
    private const string EEmitirSegundaViaDAT = "dat/segunda-via";
    private const string EConsultarStatusBancario = "dats/consultar-status";
    private const string ELiberacao = "";

    public async Task<EntradaVeiculorTransalvadorDTO> EntradaVeiculoAsync(EntradaPatioParameters parameters,
        CancellationToken ct = default)
    {
        EntradaPatioDTO entradaPatioResponse = await new HttpClientFactoryService(_http)
            .PostBearerAuthAsync<EntradaPatioDTO>(
                _options.Value.Url + EEntrada,
                _options.Value.EntradaPatioToken, parameters, ct);

        if (entradaPatioResponse is not { Success: true })
        {
            return new EntradaVeiculorTransalvadorDTO
            {
                Mensagem = TratarRespostaErro(entradaPatioResponse,
                    "Erro ao registrar entrada de veículo no pátio da Transalvador.")
            };
        }

        return new EntradaVeiculorTransalvadorDTO
        {
            Mensagem = MensagemViewHelper.SetCreateSuccess(),
            Id = entradaPatioResponse.Data.Id
        };
    }

    public async Task<MensagemDTO> LiberacaoVeiculoAsync(LiberacaoPatioParameters parameters,
        CancellationToken ct = default)
    {
        LiberacaoPatioDTO liberacaoPatioResponse = await new HttpClientFactoryService(_http)
            .PostBearerAuthAsync<LiberacaoPatioDTO>(
                _options.Value.Url + ELiberacao,
                _options.Value.LiberacaoToken, parameters, ct);

        if (liberacaoPatioResponse is not { Success: true })
        {
            return TratarRespostaErro(liberacaoPatioResponse,
                "Erro ao registrar liberação de veículo no pátio da Transalvador.");
        }

        return MensagemViewHelper.SetCreateSuccess();
    }

    public async Task<DATDTO> GerarDATAsync(GerarDATParameters parameters, CancellationToken ct = default)
    {
        var gerarDatResponse = await new HttpClientFactoryService(_http)
            .PostBearerAuthAsync<GerarDATDTO>(
                _options.Value.Url + EEmitirDAT,
                _options.Value.GerarDATToken, parameters, ct);

        return MapearParaDATDTO(gerarDatResponse, "Erro ao gerar DAT na Transalvador.");
    }


    /*
     * <summary>
     * Emite a 2ª via de um DAT existente. Se vencido, aplica encargos de juros e multas de mora,
     * recalculando os valores e atualizando o PIX.
     * </summary>
     */
    public async Task<DATDTO> EmitirSegundaViaAsync(string numdat, CancellationToken ct = default)
    {
        GerarDATDTO gerarDatResponse = await new HttpClientFactoryService(_http)
            .PostBearerAuthAsync<GerarDATDTO>(
                _options.Value.Url + EEmitirSegundaViaDAT,
                _options.Value.GerarDATToken, numdat, ct);

        return MapearParaDATDTO(gerarDatResponse, "Erro ao emitir 2ª via do DAT na Transalvador.");
    }

    public async Task<MensagemDTO> ConsultarStatusBancarioAsync(ConsultarStatusBancarioParameters parameters,
        CancellationToken ct = default)
    {
        RetornoBancarioDTO retornoBancarioResponse = await new HttpClientFactoryService(_http)
            .PostBearerAuthAsync<RetornoBancarioDTO>(
                _options.Value.Url + EConsultarStatusBancario,
                _options.Value.RetornoBancarioToken, parameters, ct);

        if (retornoBancarioResponse is not { Success: true })
        {
            return TratarRespostaErro(retornoBancarioResponse, "Erro ao consultar DAT na Transalvador.");
        }

        return MensagemViewHelper.SetCreateSuccess();
    }

    #region Mapeamento DAT

    private static DATDTO MapearParaDATDTO(GerarDATDTO gerarDatResponse,
        string defaultErrorMessage = "Erro ao gerar DAT na Transalvador.")
    {
        DATDTO result = new();

        if (gerarDatResponse is not { Success: true } || gerarDatResponse.Data == null)
        {
            result.Mensagem = TratarRespostaErro(gerarDatResponse, defaultErrorMessage);
            return result;
        }

        GerarDATDataDTO data = gerarDatResponse.Data;

        result.Mensagem = MensagemViewHelper.SetCreateSuccess();

        result.TituloDocumento = data.TituloDocumento;
        result.NumeroDocumentoDat = data.NumeroDocumentoDat;
        result.NumeroProcesso = data.NumeroProcesso;
        result.DataDocumento = data.DataDocumento;
        result.DataVencimento = data.DataVencimento;
        result.Referencia = data.Referencia;
        result.TipoSaldo = data.TipoSaldo;
        result.PdfUrl = data.PdfUrl;
        result.Instrucoes = data.Instrucoes;

        if (data.Cedente != null)
        {
            result.CedenteRazaoSocial = data.Cedente.RazaoSocial;
            result.CedenteSigla = data.Cedente.Sigla;
            result.CedenteCnpj = data.Cedente.Cnpj;
        }

        if (data.Sacado != null)
        {
            result.SacadoNome = data.Sacado.Nome;
            result.SacadoCpfCnpj = data.Sacado.CpfCnpj;
            result.SacadoEndereco = data.Sacado.Endereco;
            result.SacadoCep = data.Sacado.Cep;
            result.SacadoCidade = data.Sacado.Cidade;
            result.SacadoUf = data.Sacado.Uf;
        }

        if (data.Servico != null)
        {
            result.ServicoCodigo = data.Servico.Codigo;
            result.ServicoDescricao = data.Servico.Descricao;
            result.ServicoRotuloCompleto = data.Servico.RotuloCompleto;
        }

        if (data.DiscriminacaoIss != null)
        {
            result.PossuiIss = data.DiscriminacaoIss.PossuiIss;
            result.IssAliquotaPercentual = data.DiscriminacaoIss.AliquotaPercentual;
            result.IssPercentualBaseServico = data.DiscriminacaoIss.PercentualBaseServico;
            result.IssValorBaseServicoDesconto = data.DiscriminacaoIss.ValorBaseServicoDesconto;
            result.IssValorRetido = data.DiscriminacaoIss.ValorIssRetido;
            result.IssTextoReferencia = data.DiscriminacaoIss.TextoReferencia;
        }

        if (data.Valores != null)
        {
            result.ValorDocumento = data.Valores.ValorDocumento;
            result.DescontoAbatimento = data.Valores.DescontoAbatimento;
            result.OutrasDeducoes = data.Valores.OutrasDeducoes;
            result.JurosMultas = data.Valores.JurosMultas;
            result.OutrosAcrescimos = data.Valores.OutrosAcrescimos;
            result.ValorCobrado = data.Valores.ValorCobrado;
        }

        if (data.Pagamento != null)
        {
            result.LinhaDigitavel = data.Pagamento.LinhaDigitavel;
            result.CodigoBarras = data.Pagamento.CodigoBarras;
            result.PixCopiaECola = data.Pagamento.PixCopiaECola;
            result.TxId = data.Pagamento.TxId;
            result.QrCodeBase64 = data.Pagamento.QrCodeBase64;

            if (!string.IsNullOrWhiteSpace(data.Pagamento.QrCodeBase64))
            {
                try
                {
                    string cleanBase64 = data.Pagamento.QrCodeBase64.Contains(",")
                        ? data.Pagamento.QrCodeBase64.Split(',')[1]
                        : data.Pagamento.QrCodeBase64;

                    result.QrCode = Convert.FromBase64String(cleanBase64);
                }
                catch
                {
                    // Mantém QrCode nulo se houver erro ao converter Base64
                }
            }
        }

        return result;
    }

    #endregion Mapeamento DAT

    #region Tratamento na Resposta de Erro

    private static MensagemDTO TratarRespostaErro(TransalvadorBaseDTO response,
        string defaultMessage = "Erro ao processar requisição na API da Transalvador.")
    {
        if (response == null)
        {
            return MensagemViewHelper.SetBadRequest("Não foi possível obter resposta da API Transalvador.");
        }

        List<string> erros = ExtrairErros(response.Errors, response.Message);

        if (erros.Count == 0)
        {
            erros.Add(defaultMessage);
        }

        return MensagemViewHelper.SetBadRequest(erros);
    }

    private static List<string> ExtrairErros(object errors, string messageFallback = null)
    {
        List<string> listaErros = new();

        if (errors != null)
        {
            if (errors is Newtonsoft.Json.Linq.JObject jObj)
            {
                foreach (var prop in jObj.Properties())
                {
                    if (prop.Value is Newtonsoft.Json.Linq.JArray jArr)
                    {
                        foreach (var item in jArr)
                        {
                            string valor = item?.ToString();
                            if (!string.IsNullOrWhiteSpace(valor))
                            {
                                listaErros.Add(valor);
                            }
                        }
                    }
                    else if (prop.Value != null)
                    {
                        string valor = prop.Value.ToString();
                        if (!string.IsNullOrWhiteSpace(valor))
                            listaErros.Add(valor);
                    }
                }
            }
            else if (errors is Newtonsoft.Json.Linq.JArray jArr)
            {
                foreach (var item in jArr)
                {
                    string valor = item?.ToString();
                    if (!string.IsNullOrWhiteSpace(valor))
                    {
                        listaErros.Add(valor);
                    }
                }
            }
            else if (errors is System.Text.Json.JsonElement jsonElement)
            {
                if (jsonElement.ValueKind == System.Text.Json.JsonValueKind.Object)
                {
                    foreach (var prop in jsonElement.EnumerateObject())
                    {
                        if (prop.Value.ValueKind == System.Text.Json.JsonValueKind.Array)
                        {
                            foreach (var item in prop.Value.EnumerateArray())
                            {
                                string valor = item.GetString();
                                if (!string.IsNullOrWhiteSpace(valor))
                                    listaErros.Add(valor);
                            }
                        }
                        else
                        {
                            string valor = prop.Value.GetString() ?? prop.Value.GetRawText();
                            if (!string.IsNullOrWhiteSpace(valor))
                                listaErros.Add(valor);
                        }
                    }
                }
                else if (jsonElement.ValueKind == System.Text.Json.JsonValueKind.Array)
                {
                    foreach (var item in jsonElement.EnumerateArray())
                    {
                        string valor = item.GetString() ?? item.GetRawText();
                        if (!string.IsNullOrWhiteSpace(valor))
                            listaErros.Add(valor);
                    }
                }
                else if (jsonElement.ValueKind == System.Text.Json.JsonValueKind.String)
                {
                    string valor = jsonElement.GetString();
                    if (!string.IsNullOrWhiteSpace(valor))
                        listaErros.Add(valor);
                }
            }
            else if (errors is System.Collections.IDictionary dict)
            {
                foreach (System.Collections.DictionaryEntry entry in dict)
                {
                    if (entry.Value is System.Collections.IEnumerable list && entry.Value is not string)
                    {
                        foreach (var item in list)
                        {
                            if (item != null && !string.IsNullOrWhiteSpace(item.ToString()))
                            {
                                listaErros.Add(item.ToString());
                            }
                        }
                    }
                    else if (entry.Value != null)
                    {
                        listaErros.Add(entry.Value.ToString());
                    }
                }
            }
            else if (errors is IEnumerable<string> strList)
            {
                listaErros.AddRange(strList.Where(s => !string.IsNullOrWhiteSpace(s)));
            }
            else if (errors is string str && !string.IsNullOrWhiteSpace(str))
            {
                listaErros.Add(str);
            }
        }

        if (listaErros.Count == 0 && !string.IsNullOrWhiteSpace(messageFallback))
        {
            listaErros.Add(messageFallback);
        }

        return listaErros;
    }

    #endregion
}