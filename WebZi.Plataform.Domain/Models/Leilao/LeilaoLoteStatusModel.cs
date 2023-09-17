namespace WebZi.Plataform.Domain.Models.Leilao
{
    public class LeilaoLoteStatusModel
    {
        public int LeilaoLoteStatusId { get; set; }

        public string Descricao { get; set; }

        public string ValidaLote { get; set; }

        public string CorrelacaoDsin { get; set; }

        public string FlagReaproveitavel { get; set; } = "N";

        public string FlagAtivo { get; set; } = "S";

        public string FlagPermiteAlteracao { get; set; } = "S";

        public int Codigo { get; set; }

        public int CodigoGrupo { get; set; }

        public int? LeiloadoId { get; set; }

        public int? NaoLeiloadoId { get; set; }

        public int? PrefixoLote { get; set; }

        public int? ReaproveitavelId { get; set; }

        //public virtual TbLotesStatusGrupo CodigoGrupoNavigation { get; set; }

        //public virtual TbLotesStatus IdLeiloadoNavigation { get; set; }

        //public virtual TbLotesStatus IdNaoLeiloadoNavigation { get; set; }

        //public virtual TbLotesStatus IdReaproveitavelNavigation { get; set; }

        //public virtual ICollection<TbLotesStatus> InverseIdLeiloadoNavigation { get; set; }

        //public virtual ICollection<TbLotesStatus> InverseIdNaoLeiloadoNavigation { get; set; }

        //public virtual ICollection<TbLotesStatus> InverseIdReaproveitavelNavigation { get; set; }
    }
}