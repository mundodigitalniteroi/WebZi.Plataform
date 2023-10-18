using WebZi.Plataform.Domain.Models.Faturamento;

namespace WebZi.Plataform.Domain.Models.Banco.PIX
{
    public class PixEstaticoModel
    {
        public int PixId { get; set; }

        public int FaturamentoId { get; set; }

        public string Chave { get; set; }

        public string SolicitacaoPagador { get; set; }

        public decimal Valor { get; set; }

        public string MerchantName { get; set; }

        public string MerchantCity { get; set; }

        public string QRString { get; set; }

        public string QRCode { get; set; }

        public DateTime DataCadastro { get; set; }

        public virtual FaturamentoModel Faturamento { get; set; }
    }
}