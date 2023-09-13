namespace WebZi.Plataform.Domain.Models.Pessoa.Documento
{
    public class TipoDocumentoIdentificacaoModel
    {
        public byte TipoDocumentoIdentificacaoId { get; set; }

        public string Codigo { get; set; }

        public string Descricao { get; set; }

        public string Formato { get; set; }

        public byte TamanhoMinimo { get; set; }

        public byte TamanhoMaximo { get; set; }

        public byte OrdemApresentacao { get; set; }

        public char FlagPrincipal { get; set; }

        public char FlagPossuiDataEmissao { get; set; }

        public char FlagPossuiDataValidade { get; set; }

        public char FlagPossuiComplemento { get; set; }

        public char FlagAtivo { get; set; }
    }
}