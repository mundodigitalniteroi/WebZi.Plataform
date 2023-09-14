namespace WebZi.Plataform.Domain.Models.Faturamento
{
    public class FaturamentoComposicaoModel
    {
        public int FaturamentoComposicaoId { get; set; }

        public int FaturamentoId { get; set; }

        public int? FaturamentoServicoTipoVeiculoId { get; set; }

        public byte? FaturamentoTipoComposicaoId { get; set; }

        public string DocumentoSapId { get; set; }

        public int? UsuarioDescontoId { get; set; }

        public int? UsuarioAlteracaoQuantidadeId { get; set; }

        public string TipoLancamento { get; set; }

        /// <summary>
        /// TIPOS DE COBRANÇA:
        /// D = Diárias;
        /// H = Quantidade de HH:MM vezes o Preço;
        /// P = Porcentagem;
        /// Q = Quantidade;
        /// T = Tempo entre duas Datas;
        /// V = Valor.
        /// </summary>
        public string TipoComposicao { get; set; }

        public decimal ValorTipoComposicao { get; set; }

        public decimal? QuantidadeComposicao { get; set; }

        public decimal ValorComposicao { get; set; }

        public string TipoDesconto { get; set; }

        public int? QuantidadeDesconto { get; set; }

        public decimal? ValorDesconto { get; set; }

        public string ObservacaoDesconto { get; set; }

        public decimal ValorFaturado { get; set; }

        public decimal? QuantidadeAlterada { get; set; }

        public string ObservacaoQuantidadeAlterada { get; set; }

        public virtual FaturamentoModel Faturamento { get; set; }

        //public virtual TbDepFaturamentoServicosTipoVeiculo IdFaturamentoServicoTipoVeiculoNavigation { get; set; }

        //public virtual TbDepFaturamentoTipoComposicao IdFaturamentoTipoComposicaoNavigation { get; set; }

        //public virtual TbDepUsuario IdUsuarioAlteracaoQuantidadeNavigation { get; set; }

        //public virtual TbDepUsuario IdUsuarioDescontoNavigation { get; set; }

        //public virtual ICollection<TbDepFaturamentoComposicaoNf> TbDepFaturamentoComposicaoNfs { get; set; } = new List<TbDepFaturamentoComposicaoNf>();

        //public virtual ICollection<TbDepNfeFaturamentoComposicao> TbDepNfeFaturamentoComposicaos { get; set; } = new List<TbDepNfeFaturamentoComposicao>();
    }
}