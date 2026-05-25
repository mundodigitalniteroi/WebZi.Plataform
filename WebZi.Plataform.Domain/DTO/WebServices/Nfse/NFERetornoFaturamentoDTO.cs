namespace WebZi.Plataform.Domain.DTO.WebServices.Nfse
{
    public class NFERetornoFaturamentoDTO
    {
        public int NfeId { get; set; }
        public string NumeroNotaFiscal { get; set; }
        public string StatusId { get; set; }
        public string Status { get; set; }
        public string? StatusNfe { get; set; }
        public string? Url { get; set; }
        public DateTime? DataEmissao { get; set; }
        public string? Servico { get; set; }
        public decimal? Valor { get; set; }
        public string? StatusErro { get; set; }
        public string? MensagemErro { get; set; }
        public string? CorrecaoErro { get; set; }
    }
}