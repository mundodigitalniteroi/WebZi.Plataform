using Newtonsoft.Json;

namespace WebZi.Plataform.Domain.Models.WhatsApp;

public class WhatsAppEnvioModel
{
    [JsonProperty("number")]
    public string Telefone { get; set; }

    [JsonProperty("text")]
    public string Message { get; set; }

    [JsonProperty("delay")]
    public int Delay { get; set; }
}