using Newtonsoft.Json;

namespace WebZi.Plataform.Domain.Models.Banco.PIX.Dinamico.Consulta.Retorno
{
    public class PixConsultaRetornoPixModel
    {
        public string EndToEndId { get; set; }

        [JsonProperty("txId")]
        public string TransactionId { get; set; }

        public string Valor { get; set; }

        public DateTime Horario { get; set; }

        public string InfoPagador { get; set; }

        public PixConsultaRetornoPagadorModel Pagador { get; set; }

        public PixConsultaRetornoDevolucaoModel[] Devolucoes { get; set; }
    }
}