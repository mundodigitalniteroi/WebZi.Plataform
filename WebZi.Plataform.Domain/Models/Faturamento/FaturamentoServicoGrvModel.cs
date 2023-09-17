using WebZi.Plataform.Domain.Models.GRV;

namespace WebZi.Plataform.Domain.Models.Faturamento
{
    public class FaturamentoServicoGrvModel
    {
        public int FaturamentoServicoGrvId { get; set; }

        public int GrvId { get; set; }

        public int FaturamentoServicoTipoVeiculoId { get; set; }

        public int? UsuarioDescontoId { get; set; }

        public decimal Valor { get; set; }

        public string TempoTrabalhado { get; set; }

        public string OrigemCadastro { get; set; } = "G";

        public string TipoDesconto { get; set; }

        public int? QuantidadeDesconto { get; set; }

        public decimal? ValorDesconto { get; set; }

        public string ObservacaoDesconto { get; set; }

        public string FlagRealizarCobranca { get; set; } = "S";

        public virtual FaturamentoServicoTipoVeiculoModel FaturamentoServicoTipoVeiculo { get; set; }

        public virtual GrvModel Grv { get; set; }
    }
}