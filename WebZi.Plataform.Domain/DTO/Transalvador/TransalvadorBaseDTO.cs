using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace WebZi.Plataform.Domain.DTO.Transalvador;

public class TransalvadorBaseDTO
{
    private bool? _success;
    private string _message;
    private object _errors;

    [JsonProperty("success")]
    [JsonPropertyName("success")]
    public bool Success
    {
        get => _success ?? false;
        set => _success = value;
    }

    [JsonProperty("sucesso")]
    [JsonPropertyName("sucesso")]
    public bool Sucesso
    {
        get => Success;
        set => _success = value;
    }

    [JsonProperty("message")]
    [JsonPropertyName("message")]
    public string Message
    {
        get => _message;
        set => _message = value;
    }

    [JsonProperty("mensagem")]
    [JsonPropertyName("mensagem")]
    public string Mensagem
    {
        get => _message;
        set => _message = value;
    }

    [JsonProperty("errors")]
    [JsonPropertyName("errors")]
    public object Errors
    {
        get => _errors;
        set => _errors = value;
    }

    [JsonProperty("erros")]
    [JsonPropertyName("erros")]
    public object Erros
    {
        get => _errors;
        set => _errors = value;
    }
}
