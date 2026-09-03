namespace WebZi.Plataform.Domain.Models.VLock;

public class DispositivosModel
{
    public int Id { get; set; }
    public string Imei { get; set; }
    public bool? Ativo { get; set; }
    public int? StatusId { get; set; }
    public string Telefone { get; set; }
    public string Fabricante { get; set; }

    public string Modelo { get; set; }

    public string NotaFiscal { get; set; }
    public int? ClienteId { get; set; }
    public int? AutoridadeId { get; set; }
    public int? AgenteId { get; set; }
    public int? ParceiroId { get; set; }

    public StatusDispositivoModel StatusDispositivo { get; set; }
}