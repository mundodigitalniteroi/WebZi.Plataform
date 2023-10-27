using WebZi.Plataform.Domain.Models.Condutor;

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

        public string FlagPrincipal { get; set; } = "N";

        public string FlagPossuiDataEmissao { get; set; } = "N";

        public string FlagPossuiDataValidade { get; set; } = "N";

        public string FlagPossuiComplemento { get; set; } = "N";

        public string FlagAtivo { get; set; } = "S";
    }
}