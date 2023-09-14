using WebZi.Plataform.Domain.Models.Veiculo;

namespace WebZi.Plataform.Domain.Models.Faturamento
{
    public class FaturamentoServicoTipoVeiculoModel
    {
        public int IdFaturamentoServicoTipoVeiculo { get; set; }

        public int IdFaturamentoServicoAssociado { get; set; }

        public byte IdTipoVeiculo { get; set; }

        public virtual FaturamentoServicoAssociadoModel FaturamentoServicoAssociado { get; set; }

        public virtual TipoVeiculoModel TipoVeiculo { get; set; }

        public virtual ICollection<FaturamentoComposicaoModel> FaturamentoComposicoes { get; set; }

        public virtual ICollection<FaturamentoServicoGrvModel> FaturamentoServicoGrvs { get; set; }
    }
}