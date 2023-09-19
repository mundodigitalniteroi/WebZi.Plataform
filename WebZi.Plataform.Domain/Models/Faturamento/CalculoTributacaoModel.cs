namespace WebZi.Plataform.Domain.Models.Faturamento
{
    public class CalculoTributacaoModel : FaturamentoComposicaoModel
    {
        public string CodigoMaterial { get; set; }

        public int FaturamentoServicoAssociadoId { get; set; }
    }
}