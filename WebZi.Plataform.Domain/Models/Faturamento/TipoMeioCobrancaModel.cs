namespace WebZi.Plataform.Domain.Models.Faturamento
{
    public class TipoMeioCobrancaModel
    {
        public byte TipoMeioCobrancaId { get; set; }

        public string Descricao { get; set; }

        public string Alias { get; set; }

        public string DocumentoImpressao { get; set; }

        public string CodigoERP { get; set; }

        public string FlagBanco { get; set; } = "S";

        public string FlagPossuiCodigoAutorizacaoCartao { get; set; } = "N";

        public string FlagAtivo { get; set; } = "S";
    }
}