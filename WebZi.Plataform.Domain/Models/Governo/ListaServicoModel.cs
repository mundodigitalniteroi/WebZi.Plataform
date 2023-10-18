namespace WebZi.Plataform.Domain.Models.Governo
{
    public class ListaServicoModel
    {
        public int ListaServicoId { get; set; }

        public string ItemLista { get; set; }

        public string Descricao { get; set; }

        public decimal AliquotaIss { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public virtual ICollection<AssociacaoCnaeListaServicoModel> CnaeListaServicos { get; set; }
    }
}