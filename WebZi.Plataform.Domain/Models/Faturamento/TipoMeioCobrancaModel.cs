namespace WebZi.Plataform.Domain.Models.Faturamento
{
    public class TipoMeioCobrancaModel
    {
        public byte TipoMeioCobrancaId { get; set; }

        public string Descricao { get; set; }

        public string Alias { get; set; }

        public string DocumentoImpressao { get; set; }

        public string FlagBanco { get; set; }

        public string FlagPossuiCodigoAutorizacaoCartao { get; set; }

        public string FlagAtivo { get; set; }
    }
}