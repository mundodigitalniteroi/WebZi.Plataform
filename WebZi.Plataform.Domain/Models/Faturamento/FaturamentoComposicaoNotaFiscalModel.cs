namespace WebZi.Plataform.Domain.Models.Faturamento
{
    public class FaturamentoComposicaoNotaFiscalModel
    {
        public int FaturamentoComposicaoNotaFiscalId { get; set; }

        public int FaturamentoComposicaoId { get; set; }

        public string Nota { get; set; }

        public DateTime? DataEmissaoNota { get; set; }
    }
}