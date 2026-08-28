namespace WebZi.Plataform.Domain.Models.GRV.SolicitacaoReboque;

public class SolicitacaoReboqueGrvModel
{
    public int Id { get; set; }
    public int SolicitacaoReboqueId { get; set; }
    public int AutoridadeResponsavelId { get; set; }
    public string MatriculaAutoridadeResponsavel { get; set; }
    public string NomeAutoridadeResponsavel { get; set; }

    public SolicitacaoReboqueModel SolicitacaoReboque { get; set; }
    public AutoridadeResponsavelModel AutoridadeResponsavel { get; set; }
}