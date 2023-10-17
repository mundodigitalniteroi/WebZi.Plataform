using WebZi.Plataform.Domain.Models.GRV;

namespace WebZi.Plataform.Domain.Models.Cliente
{
    public class ClienteCodigoIdentificacaoModel
    {
        public int ClienteCodigoIdentificacaoId { get; set; }

        public int GrvId { get; set; }

        public int UsuarioCadastroId { get; set; }

        public int? UsuarioAlteracaoId { get; set; }

        public string CodigoIdentificacao { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public virtual GrvModel Grv { get; set; }

        //public virtual UsuarioModel UsuarioCadastro { get; set; }

        //public virtual UsuarioModel UsuarioAlteracao { get; set; }
    }
}