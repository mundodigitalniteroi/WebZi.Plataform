namespace WebZi.Plataform.Domain.Models.Faturamento
{
    public class FaturamentoRegraTipoModel
    {
        public short FaturamentoRegraTipoId { get; set; }

        public string Codigo { get; set; }

        public string Descricao { get; set; }

        public char FlagPossuiValor { get; set; }

        public char FlagAtivo { get; set; }

        public virtual ICollection<FaturamentoRegraModel> FaturamentoRegras { get; set; }
    }
}