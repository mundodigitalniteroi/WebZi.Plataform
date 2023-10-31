namespace WebZi.Plataform.Domain.Models.Usuario
{
    public class UsuarioPermissaoModel
    {
        public int UsuarioPermissaoId { get; set; }

        public short TipoPermissaoId { get; set; }

        public int UsuarioId { get; set; }

        public int UsuarioCadastroId { get; set; }

        public int? UsuarioAlteracaoId { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public string FlagAtivo { get; set; } = "S";

        public virtual UsuarioTipoPermissaoModel TipoPermissao { get; set; }

        public virtual UsuarioModel Usuario { get; set; }

        public virtual UsuarioModel UsuarioCadastro { get; set; }

        public virtual UsuarioModel UsuarioAlteracao { get; set; }
    }
}