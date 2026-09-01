namespace WebZi.Plataform.Domain.Models.GRV.SolicitacaoReboque;

public class SolicitacaoReboqueGrvModel
{
    public int Id { get; set; }
    public int SolicitacaoReboqueId { get; set; }
    public int AutoridadeResponsavelId { get; set; }
    public string MatriculaAutoridadeResponsavel { get; set; }
    public string NomeAutoridadeResponsavel { get; set; }

    public byte? TipoVeiculoId { get; set; }
    public int? CorId { get; set; }
    public int? MarcaModeloId { get; set; }
    public string? Placa { get; set; }
    public string? Chassi { get; set; }
    public string? Renavam { get; set; }
    public string? VeiculoUF { get; set; }

    public SolicitacaoReboqueModel SolicitacaoReboque { get; set; }
    public AutoridadeResponsavelModel AutoridadeResponsavel { get; set; }
}