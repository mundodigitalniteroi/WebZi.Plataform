namespace WebZi.Plataform.Domain.ViewModel.Faturamento
{
    public class TipoMeioCobrancaViewModel
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