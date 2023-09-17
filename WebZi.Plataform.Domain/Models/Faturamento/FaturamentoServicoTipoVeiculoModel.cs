using WebZi.Plataform.Domain.Models.Veiculo;

namespace WebZi.Plataform.Domain.Models.Faturamento
{
    public class FaturamentoServicoTipoVeiculoModel
    {
        public int FaturamentoServicoTipoVeiculoId { get; set; }

        public int FaturamentoServicoAssociadoId { get; set; }

        public byte TipoVeiculoId { get; set; }

        public virtual FaturamentoServicoAssociadoModel FaturamentoServicoAssociado { get; set; }

        public virtual TipoVeiculoModel TipoVeiculo { get; set; }

        public virtual ICollection<FaturamentoServicoGrvModel> FaturamentoServicosGrvs { get; set; }
    }
}