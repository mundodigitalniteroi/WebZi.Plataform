using WebZi.Plataform.Domain.Models.GRV;

namespace WebZi.Plataform.Domain.Models.Faturamento
{
    public class FaturamentoProdutoModel
    {
        public string FaturamentoProdutoId { get; set; }

        public string Descricao { get; set; }

        public string FlagSolicitacaoReboque { get; set; } = "N";

        public virtual ICollection<FaturamentoServicoTipoModel> FaturamentoServicosTipos { get; set; }

        public virtual ICollection<GrvModel> Grvs { get; set; }
    }
}