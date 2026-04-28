using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.ViewModel.Liberacao;

public class ProcessosPreLeilaoParameters
{
    public int ClienteId { get; set; }
    public List<int> DepositosIds { get; set; }
    public int NumDiasPatio { get; set; }
    public int NumDiasLeilao { get; set; }
    public string Data { get; set; }
    public int? IdLeilao { get; set; }
    public int Sobra { get; set; } = 0;
    public List<string>? StatusLote { get; set; }
    public List<string>? Leiloes { get; set; }
    public int NumLotes { get; set; } = 1000;
    public string? NumeroProcesso { get; set; }
}