using System.ComponentModel.DataAnnotations;

namespace WebZi.Plataform.Domain.ViewModel.NFe;

public class AtualizarDadosNFeParameters
{
    [Required(ErrorMessage = "Propriedade obrigatória")]
    public int IdentificadorUsuario { get; set; }

    [Required(ErrorMessage = "Propriedade obrigatória")]
    public int IdentificadorProcesso { get; set; }

    [Required(ErrorMessage = "Propriedade obrigatória")]
    public int IdentificadorCliente { get; set; }

    [Required(ErrorMessage = "Propriedade obrigatória")]
    public int IdentificadorDeposito { get; set; }

    public string Nome { get; set; }
    public string InscricaoMTS { get; set; }
    public string Cpf { get; set; }
    public string Cnpj { get; set; }
    public string Cep { get; set; }
    public string Logradouro { get; set; }
    public string Numero { get; set; }
    public string Complemento { get; set; }
    public string Bairro { get; set; }
    public string Municipio { get; set; }
    public string UF { get; set; }
    public string DDD { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }
}