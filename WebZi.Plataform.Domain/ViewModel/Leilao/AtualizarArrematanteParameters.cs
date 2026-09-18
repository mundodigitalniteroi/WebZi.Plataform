using System.ComponentModel.DataAnnotations;

namespace WebZi.Plataform.Domain.ViewModel.Leilao;

public class AtualizarArrematanteParameters
{
    [Required(ErrorMessage = "O identificador do arrematante é obrigatório.")]
    public int IdentificadorArrematante { get; set; }

    public int? IdentificadorProcesso { get; set; }

    [StringLength(255, ErrorMessage = "O campo {0} deve conter no máximo {1} caracteres.")]
    public string? Nome { get; set; }

    [StringLength(14, ErrorMessage = "O campo {0} deve conter no máximo {1} caracteres.")]
    public string? CpfCnpj { get; set; }

    [StringLength(20, ErrorMessage = "O campo {0} deve conter no máximo {1} caracteres.")]
    public string? TelefoneFixo { get; set; }

    [StringLength(20, ErrorMessage = "O campo {0} deve conter no máximo {1} caracteres.")]
    public string? TelefoneCelular { get; set; }

    [StringLength(255, ErrorMessage = "O campo {0} deve conter no máximo {1} caracteres.")]
    public string? Email { get; set; }

    [StringLength(500, ErrorMessage = "O campo {0} deve conter no máximo {1} caracteres.")]
    public string? Logradouro { get; set; }

    [StringLength(10, ErrorMessage = "O campo {0} deve conter no máximo {1} caracteres.")]
    public string? Numero { get; set; }

    [StringLength(500, ErrorMessage = "O campo {0} deve conter no máximo {1} caracteres.")]
    public string? Complemento { get; set; }

    [StringLength(500, ErrorMessage = "O campo {0} deve conter no máximo {1} caracteres.")]
    public string? Bairro { get; set; }

    [StringLength(500, ErrorMessage = "O campo {0} deve conter no máximo {1} caracteres.")]
    public string? Cidade { get; set; }

    [StringLength(2, ErrorMessage = "O campo {0} deve conter no máximo {1} caracteres.")]
    public string? Estado { get; set; }

    [StringLength(10, ErrorMessage = "O campo {0} deve conter no máximo {1} caracteres.")]
    public string? Cep { get; set; }

    [StringLength(15, ErrorMessage = "O campo {0} deve conter no máximo {1} caracteres.")]
    public string? NomeLeilao { get; set; }

    [StringLength(10, ErrorMessage = "O campo {0} deve conter no máximo {1} caracteres.")]
    public string? NumeroLote { get; set; }

    [StringLength(15, ErrorMessage = "O campo {0} deve conter no máximo {1} caracteres.")]
    public string? ValorArrematacao { get; set; }

    [StringLength(15, ErrorMessage = "O campo {0} deve conter no máximo {1} caracteres.")]
    public string? ValorTaxaAdministrativa { get; set; }

    [StringLength(15, ErrorMessage = "O campo {0} deve conter no máximo {1} caracteres.")]
    public string? ValorOutrasTaxas { get; set; }

    [StringLength(15, ErrorMessage = "O campo {0} deve conter no máximo {1} caracteres.")]
    public string? ValorComissao { get; set; }

    [StringLength(15, ErrorMessage = "O campo {0} deve conter no máximo {1} caracteres.")]
    public string? ValorTotal { get; set; }

    public DateTime? DataLeilao { get; set; }
}
