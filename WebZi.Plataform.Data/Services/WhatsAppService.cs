using Microsoft.Extensions.Http;
using WebZi.Plataform.Data.Services.Sistema;

namespace WebZi.Plataform.Data.Services;

public class WhatsAppService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public WhatsAppService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<string> SendTextMessageAsync(string telefone, string message)
    {
        // return new HttpClientFactoryService(_httpClientFactory)
        //     .Post<string>("", new()
        //     {
        //         telefone,
        //         text = message,
        //         delay = 1200
        //     });
        // TODO: Implementação envio do codigo por whatsapp
        return string.Empty;
    }
}