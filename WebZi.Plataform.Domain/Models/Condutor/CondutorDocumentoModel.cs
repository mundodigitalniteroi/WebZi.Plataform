using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Models.Pessoa.Documento;
using WebZi.Plataform.Domain.Models.Usuario;

namespace WebZi.Plataform.Domain.Models.Condutor
{
    public class CondutorDocumentoModel
    {
        public int CondutorDocumentoId { get; set; }

        public int GrvId { get; set; }

        public byte TipoDocumentoIdentificacaoId { get; set; }

        public int UsuarioCadastroId { get; set; }

        public DateTime DataCadastro { get; set; }

        public virtual GrvModel Grv { get; set; }

        public virtual TipoDocumentoIdentificacaoModel TipoDocumentoIdentificacao { get; set; }

//        public virtual UsuarioModel UsuarioCadastro { get; set; }
    }
}