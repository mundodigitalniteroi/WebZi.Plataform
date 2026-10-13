
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebZi.Plataform.Domain.Models.GRV;

namespace WebZi.Plataform.Domain.Models.Arrematantes;

public class ArrematantesModel
{
    public int ArrematanteId { get; set; }
    public int? GrvId { get; set; }
    public string? NumeroProcesso { get; set; }
    public string? Nome { get; set; }
    public string? CpfCnpj { get; set; }
    public string? TelefoneCelular { get; set; }
    public string? Email { get; set; }
    public string? Logradouro { get; set; }
    public string? Numero { get; set; }
    public string? Complemento { get; set; }
    public string? Bairro { get; set; }
    public string? Cidade { get; set; }
    public string? Estado { get; set; }
    public string? Cep { get; set; }
    public string? NomeLeilao { get; set; }
    public string? NumeroLote { get; set; }
    public string? ValorArrematacao { get; set; }
    public string? ValorTaxaAdministrativa { get; set; }
    public string? ValorOutrasTaxas { get; set; }
    public string? ValorComissao { get; set; }
    public string? ValorTotal { get; set; }
    public DateTime? DataLeilao { get; set; }
    public DateTime? DataCadastro { get; set; }
    public GrvModel Grv { get; set; }
}
