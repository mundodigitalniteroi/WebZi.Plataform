using System.ComponentModel.DataAnnotations;

namespace WebZi.Plataform.Domain.ViewModel.Leilao;

public class CadastrarArrematanteParameters
{
    public int? IdentificadorProcesso { get; set; }

    [Required(ErrorMessage = "Propriedade obrigatória")]

    public string? Nome { get; set; }

    [Required(ErrorMessage = "Propriedade obrigatória")]

    public string? CpfCnpj { get; set; }

    public string? TelefoneCelular { get; set; }

    public string? Email { get; set; }

    [Required(ErrorMessage = "Propriedade obrigatória")]

    public string? Logradouro { get; set; }

    [Required(ErrorMessage = "Propriedade obrigatória")]

    public string? Numero { get; set; }

    public string? Complemento { get; set; }

    [Required(ErrorMessage = "Propriedade obrigatória")]

    public string? Bairro { get; set; }

    [Required(ErrorMessage = "Propriedade obrigatória")]

    public string? Cidade { get; set; }

    [Required(ErrorMessage = "Propriedade obrigatória")]
    [StringLength(2, ErrorMessage = "O campo {0} deve conter no máximo {1} caracteres.")]
    public string? Estado { get; set; }

    [Required(ErrorMessage = "Propriedade obrigatória")]

    public string? Cep { get; set; }

    [Required(ErrorMessage = "Propriedade obrigatória")]

    public string? NomeLeilao { get; set; }
    [Required(ErrorMessage = "Propriedade obrigatória")]

    public string? NumeroLote { get; set; }

    [Required(ErrorMessage = "Propriedade obrigatória")]
    public string? ValorArrematacao { get; set; }

    public string? ValorTaxaAdministrativa { get; set; }

    public string? ValorOutrasTaxas { get; set; }

    public string? ValorComissao { get; set; }

    [Required(ErrorMessage = "Propriedade obrigatória")]
    public string? ValorTotal { get; set; }

    [Required(ErrorMessage = "Propriedade obrigatória")]
    public DateTime? DataLeilao { get; set; }
}
