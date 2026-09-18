using System.ComponentModel.DataAnnotations;

namespace WebZi.Plataform.Domain.ViewModel.Leilao;

public class IngressarLoteParameters
{
    public List<int>? IdentificadoresProcesso { get; set; }
    public List<string>? NumerosDeProcesso { get; set; }
}
