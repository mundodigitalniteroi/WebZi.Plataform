namespace WebZi.Plataform.Domain.Models.GRV.SolicitacaoReboque;

public class SolicitacaoReboqueTipoModel
{
    public byte Id { get; set; }
    public byte MotivoApreensaoId { get; set; }
    public string FaturamentoProdutoCodigo { get; set; }
    public string Descricao { get; set; }
    public MotivoApreensaoModel MotivoApreensao { get; set; }
}