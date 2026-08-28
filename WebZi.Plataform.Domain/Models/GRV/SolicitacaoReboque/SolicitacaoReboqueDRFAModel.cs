using WebZi.Plataform.Domain.Models.Documento;

namespace WebZi.Plataform.Domain.Models.GRV.SolicitacaoReboque;

public class SolicitacaoReboqueDRFAModel
{
    public int Id { get; set; }
    public int SolicitacaoReboqueId { get; set; }
    public byte TipoRegistroId { get; set; }
    public short? OrgaoEmissorId { get; set; }
    public byte? AutoridadeDivisaoId { get; set; }
    public string? AutoridadeDivisaoComplemento { get; set; }
    public string? NumeroRegistroRouboFurto { get; set; }
    public string? RegistroRouboFurtoMatriculaAgente { get; set; }
    public string? RegistroRouboFurtoNomeAgente { get; set; }
    public string? EstadoGeralVeiculo { get; set; }
    public SolicitacaoReboqueModel SolicitacaoReboque { get; set; }
    public TipoRegistroModel TipoRegistro { get; set; }
    public OrgaoEmissorModel? OrgaoEmissor { get; set; }
    public AutoridadeDivisaoModel? AutoridadeDivisao { get; set; }
}