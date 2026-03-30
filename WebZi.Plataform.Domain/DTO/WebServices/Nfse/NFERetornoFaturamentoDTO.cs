namespace WebZi.Plataform.Domain.DTO.WebServices.Nfse
{
    public class NFERetornoFaturamentoDTO
    {
        public int NfeId { get; set; }
        public string NumeroNotaFiscal { get; set; }
        public decimal Valor { get; set; }
        public string Servico { get; set; }
        public string Url { get; set; }
    }
}
