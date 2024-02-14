using Newtonsoft.Json;

namespace WebZi.Plataform.Domain.Models.Banco.PIX.Dinamico.Consulta.Envio
{
    public class PixConsultaEnvioModel
    {
        public PixConsultaEnvioCalendarioModel Calendario { get; set; }

        public PixConsultaEnvioDevedorModel Devedor { get; set; }

        public PixConsultaEnvioParametrosModel Parametros { get; set; }

        [JsonProperty("txId")]
        public string TransactionId { get; set; }

        public string DataInicio { get; set; }

        public string DataFim { get; set; }

        public string Chave { get; set; }

        public string SolicitacaoPagador { get; set; }

        public PixConsultaEnvioInfoAdicionalModel[] InfoAdicionais { get; set; }

        public PixConsultaEnvioValorModel Valor { get; set; }

        public PixConsultaEnvioMerchantModel Merchant { get; set; }

        public string QrString { get; set; }

        public string QrCode { get; set; }
    }
}