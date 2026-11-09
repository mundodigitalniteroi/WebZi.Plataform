using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebZi.Plataform.Domain.ViewModel.GRV.SolicitacaoReboque;

public class ListarSolicitacoesParameters
{
    [JsonIgnore]
    [Required(ErrorMessage = "Propriedade obrigatória")]
    public int IdentificadorUsuario { get; set; }

    public int? IdentificadorCliente { get; set; }
    public int? IdentificadorDeposito { get; set; }

    public int? Skip { get; set; }
    public int? Take { get; set; }

    public DateTime? DataInicial { get; set; }
    public DateTime? DataFinal { get; set; }
}