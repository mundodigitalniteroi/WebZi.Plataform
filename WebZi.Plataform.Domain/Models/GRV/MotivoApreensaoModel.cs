namespace WebZi.Plataform.Domain.Models.GRV
{
    public class MotivoApreensaoModel
    {
        public byte MotivoApreensaoId { get; set; }

        public string Codigo { get; set; }

        public string Descricao { get; set; }

        public string FlagDefault { get; set; } = "N";

        // public virtual ICollection<TbDepSolicitacaoReboqueTipo> TbDepSolicitacaoReboqueTipos { get; set; } = new List<TbDepSolicitacaoReboqueTipo>();
    }
}