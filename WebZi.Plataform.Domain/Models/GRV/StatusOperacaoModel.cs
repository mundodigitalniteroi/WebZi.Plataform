namespace WebZi.Plataform.Domain.Models.GRV
{
    public class StatusOperacaoModel
    {
        public string StatusOperacaoId { get; set; }

        public string Descricao { get; set; }

        public byte? Sequencia { get; set; }

        public string FlagVeiculoApreendido { get; set; } = "S";

        public string FlagLeilao { get; set; } = "N";

        public virtual ICollection<GrvModel> Grvs { get; set; }
    }
}