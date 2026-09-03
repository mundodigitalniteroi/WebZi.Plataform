namespace WebZi.Plataform.Domain.Models.VLock;

public class RecolhimentoModel
{
    public int Id { get; set; }
    public int IdDispositivo { get; set; }
    public int IdGrv { get; set; }
    public bool Ativo { get; set; }
    public bool CertaVirtual { get; set; }
}