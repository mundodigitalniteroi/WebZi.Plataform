using WebZi.Plataform.Domain.Models.Documento;

namespace WebZi.Plataform.Domain.Models.Pessoa.Documento
{
    public class PessoaDocumentoIdentificacaoModel
    {
        public long PessoaDocumentoIdentificacaoId { get; set; }

        public long IdPessoa { get; set; }

        public short? IdOrgaoEmissor { get; set; }

        public byte IdTipoDocumentoIdentificacao { get; set; }

        public string Descricao { get; set; }

        public DateTime? DataEmissao { get; set; }

        public DateTime? DataValidade { get; set; }

        public string Complemento { get; set; }

        public virtual PessoaModel Pessoa { get; set; }

        public virtual TipoDocumentoIdentificacaoModel TipoDocumentoIdentificacao { get; set; }

        public virtual OrgaoEmissorModel OrgaoEmissor { get; set; }
    }
}
