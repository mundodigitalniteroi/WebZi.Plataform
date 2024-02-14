using Newtonsoft.Json;

namespace WebZi.Plataform.Domain.Models.Banco.PIX.Dinamico.Consulta.Retorno
{
    public class PixConsultaRetornoModel
    {
        [JsonProperty("txId")]
        public string TransactionId { get; set; }

        public int Revisao { get; set; }

        public string Status { get; set; }

        public PixConsultaRetornoPixModel[] Pix { get; set; }

        public string QrString { get; set; }

        public string QrCode { get; set; }

        public PixConsultaRetornoCalendarioModel Calendario { get; set; }

        public PixConsultaRetornoDevedorModel Devedor { get; set; }

        public string Location { get; set; }

        public PixConsultaRetornoLocalizacaoModel Loc { get; set; }

        public string Chave { get; set; }

        public string SolicitacaoPagador { get; set; }

        public PixConsultaRetornoInfoAdicionaisModel[] InfoAdicionais { get; set; }

        public PixConsultaRetornoValorModel Valor { get; set; }

        public PixConsultaRetornoMerchantModel Merchant { get; set; }
    }
}