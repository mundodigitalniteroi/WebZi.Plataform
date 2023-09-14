using WebZi.Plataform.Domain.Models.GRV;

namespace WebZi.Plataform.Domain.Models.Faturamento
{
    public class FaturamentoServicoGrvModel
    {
        public int IdFaturamentoServicoGrv { get; set; }

        public int IdGrv { get; set; }

        public int IdFaturamentoServicoTipoVeiculo { get; set; }

        public int? IdUsuarioDesconto { get; set; }

        public decimal Valor { get; set; }

        public string TempoTrabalhado { get; set; }

        public string OrigemCadastro { get; set; }

        public string TipoDesconto { get; set; }

        public int? QuantidadeDesconto { get; set; }

        public decimal? ValorDesconto { get; set; }

        public string ObservacaoDesconto { get; set; }

        public string FlagRealizarCobranca { get; set; }

        // public virtual TbDepFaturamentoServicosTipoVeiculo IdFaturamentoServicoTipoVeiculoNavigation { get; set; }

        public virtual GrvModel Grv { get; set; }
    }
}