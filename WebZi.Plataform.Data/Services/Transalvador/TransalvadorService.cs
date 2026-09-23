using Microsoft.Extensions.Options;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Sistema;
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

    public async Task<MensagemDTO> EntradaVeiculoAsync(EntradaPatioParameters parameters, CancellationToken ct = default)
    {
        EntradaPatioDTO entradaPatioResponse = await new HttpClientFactoryService(_http)
            .PostBearerAuthAsync<EntradaPatioDTO>(
                _options.Value.Url + "patio/veiculos/entrada",
                _options.Value.EntradaPatioToken, parameters, ct);

        if (entradaPatioResponse == null || !entradaPatioResponse.Success)
        {
            return TratarRespostaErro(entradaPatioResponse, "Erro ao registrar entrada de veículo no pátio da Transalvador.");
        }

        return MensagemViewHelper.SetCreateSuccess();
    }

    public async Task<MensagemDTO> LiberacaoVeiculoAsync(LiberacaoPatioParameters parameters, CancellationToken ct = default)
    {
        LiberacaoPatioDTO liberacaoPatioResponse = await new HttpClientFactoryService(_http)
            .PostBearerAuthAsync<LiberacaoPatioDTO>(
                _options.Value.Url,
                _options.Value.LiberacaoToken, parameters, ct);

        if (liberacaoPatioResponse == null || !liberacaoPatioResponse.Success)
        {
            return TratarRespostaErro(liberacaoPatioResponse, "Erro ao registrar liberação de veículo no pátio da Transalvador.");
        }

        return MensagemViewHelper.SetCreateSuccess();
    }

    public async Task<MensagemDTO> GerarDATAsync(GerarDATParameters parameters, CancellationToken ct = default)
    {
        GerarDATDTO gerarDatResponse = await new HttpClientFactoryService(_http)
            .PostBearerAuthAsync<GerarDATDTO>(
                _options.Value.Url,
                _options.Value.GerarDATToken, parameters, ct);

        if (gerarDatResponse == null || !gerarDatResponse.Success)
        {
            return TratarRespostaErro(gerarDatResponse, "Erro ao gerar DAT na Transalvador.");
        }

        return MensagemViewHelper.SetCreateSuccess();
    }

    public async Task<MensagemDTO> ConsultarDATAsync(ConsultarDATParameters parameters, CancellationToken ct = default)
    {
        RetornoBancarioDTO retornoBancarioResponse = await new HttpClientFactoryService(_http)
            .PostBearerAuthAsync<RetornoBancarioDTO>(
                _options.Value.Url,
                _options.Value.RetornoBancarioToken, parameters, ct);

        if (retornoBancarioResponse == null || !retornoBancarioResponse.Success)
        {
            return TratarRespostaErro(retornoBancarioResponse, "Erro ao consultar DAT na Transalvador.");
        }

        return MensagemViewHelper.SetCreateSuccess();
    }

    private static MensagemDTO TratarRespostaErro(TransalvadorBaseDTO response, string defaultMessage = "Erro ao processar requisição na API da Transalvador.")
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
}