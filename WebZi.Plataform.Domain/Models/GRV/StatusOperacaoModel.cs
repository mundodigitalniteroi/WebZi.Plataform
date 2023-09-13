namespace WebZi.Plataform.Domain.Models.GRV
{
    public class StatusOperacaoModel
    {
        public char StatusOperacaoId { get; set; }

        public string Descricao { get; set; }

        public byte? Sequencia { get; set; }

        public char FlagVeiculoApreendido { get; set; }

        public char FlagLeilao { get; set; }

        public virtual ICollection<GrvModel> Grvs { get; set; } // = new List<TbDepGrv>();

        // public virtual ICollection<TbDepGrvBloqueio> TbDepGrvBloqueios { get; set; } = new List<TbDepGrvBloqueio>();
    }
}