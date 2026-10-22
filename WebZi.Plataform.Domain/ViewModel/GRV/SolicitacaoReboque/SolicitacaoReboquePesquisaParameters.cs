namespace WebZi.Plataform.Domain.ViewModel.GRV.SolicitacaoReboque;

public class SolicitacaoReboquePesquisaParameters
{
    public short? Skip { get; set; }
    public short? Take { get; set; }
    public string? Placa { get; set; }
    public string? NumeroProcesso { get; set; }
    public DateTime? DataInicial { get; set; }
    public DateTime? DataFinal { get; set; }
    public int IdentificadorUsuario { get; set; }
}