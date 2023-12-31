namespace WebZi.Plataform.Domain.DTO.Banco
{
    public class TipoMeioCobrancaDTO
    {
        public byte IdentificadorTipoMeioCobranca { get; set; }

        public string Descricao { get; set; }

        public string Alias { get; set; }

        public string DocumentoImpressao { get; set; }

        public string CodigoERP { get; set; }

        public string FlagBanco { get; set; }

        public string FlagPossuiCodigoAutorizacaoCartao { get; set; }

        public string FlagAtivo { get; set; }
    }
}