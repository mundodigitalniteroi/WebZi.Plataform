using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebZi.Plataform.Domain.ViewModel.GGV;

public class AtualizarSetorParameters
{
    [JsonIgnore] public int IdentificadorProcesso { get; set; }
    [JsonIgnore] public int IdentificadorUsuario { get; set; }

    [MaxLength(32, ErrorMessage = "O setor do estacionamento deve possuir no máximo 32 caracteres.")]
    public string EstacionamentoSetor { get; set; }

    [MaxLength(10, ErrorMessage = "O número da vaga deve possuir no máximo 10 caracteres.")]
    public string EstacionamentoNumeroVaga { get; set; }
}