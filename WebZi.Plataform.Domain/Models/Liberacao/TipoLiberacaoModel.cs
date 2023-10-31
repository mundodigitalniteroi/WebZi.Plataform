namespace WebZi.Plataform.Domain.Models.Liberacao
{
    public class TipoLiberacaoModel
    {
        public byte TipoLiberacaoId { get; set; }

        public string Descricao { get; set; }

        public virtual ICollection<LiberacaoModel> ListagemLiberacao { get; set; }
    }
}