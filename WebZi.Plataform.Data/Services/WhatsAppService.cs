using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Services.Sistema;
using WebZi.Plataform.Domain.Models.WhatsApp;
using WebZi.Plataform.Domain.Options;

namespace WebZi.Plataform.Data.Services;

public class WhatsAppService
{
    private readonly IOptions<EvolutionOptions> _evolutionOptions;

    public WhatsAppService(IOptions<EvolutionOptions> evolutionOptions)
    {
        _evolutionOptions = evolutionOptions;
    }

    public async Task SendTextMessageAsync(string telefone, string message)
    {
        const string route = "message/sendText/{instance}";
        telefone = NormalizarNumeroEvolution(telefone);
        WhatsAppEnvioModel whatsappBody = new()
        {
            Telefone = telefone,
            Message = message,
            Delay = 1200
        };
        var baseUrl = _evolutionOptions.Value.BaseUrl?.Trim();
        if (string.IsNullOrWhiteSpace(baseUrl))
            throw new Exception("Configuração EvolutionApi:BaseUrl não definida");

        var apiKey = _evolutionOptions.Value.ApiKey?.Trim();
        if (string.IsNullOrWhiteSpace(apiKey))
            throw new Exception("Configuração EvolutionApi:ApiKey não definida");

        var instance = _evolutionOptions.Value.Instance?.Trim();
        if (!string.IsNullOrWhiteSpace(route) && route.Contains("{instance}") && string.IsNullOrWhiteSpace(instance))
            throw new Exception("Configuração EvolutionApi:Instance não definida");


        await PostToEvolutionApiAsync(baseUrl, route, apiKey, instance, whatsappBody);
    }

    private async Task<string> PostToEvolutionApiAsync(string baseUrl, string route, string apiKey, string instance,
        object body)
    {
        var url = BuildUrl(baseUrl, route, instance);

        using var http = new HttpClient();
        http.DefaultRequestHeaders.Accept.Clear();
        http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        if (http.DefaultRequestHeaders.Contains("apikey"))
            http.DefaultRequestHeaders.Remove("apikey");

        http.DefaultRequestHeaders.Add("apikey", apiKey);

        var payload = JsonConvert.SerializeObject(body ?? new { });
        var content = new StringContent(payload, Encoding.UTF8, "application/json");

        var response = await http.PostAsync(url, content);
        var responseBody = await response.Content.ReadAsStringAsync();

        return !response.IsSuccessStatusCode
            ? throw new Exception($"Erro Evolution API ({(int)response.StatusCode}): {responseBody}")
            : responseBody;
    }

    private string BuildUrl(string baseUrl, string route, string instance)
    {
        var normalizedBase = baseUrl.TrimEnd('/');
        var normalizedRoute = (route ?? string.Empty).TrimStart('/');

        if (normalizedRoute.Contains("{instance}"))
        {
            var inst = Uri.EscapeDataString((instance ?? string.Empty).Trim());
            normalizedRoute = normalizedRoute.Replace("{instance}", inst);
        }

        return $"{normalizedBase}/{normalizedRoute}";
    }
    
    private static string NormalizarNumeroEvolution(string telefone)
    {
        if (string.IsNullOrWhiteSpace(telefone))
            throw new Exception("Telefone inválido");

        var digits = new StringBuilder(telefone.Length);
        foreach (var ch in telefone)
        {
            if (ch >= '0' && ch <= '9')
                digits.Append(ch);
        }

        var number = digits.ToString().TrimStart('0');

        if (number.StartsWith("55"))
            return number;

        if (number.Length == 10 || number.Length == 11)
            return "55" + number;

        return number;
    }
}