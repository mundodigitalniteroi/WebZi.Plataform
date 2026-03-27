namespace WebZi.Plataform.Domain.Models.Usuario
{
    public class SistemaPerfilAcessoModel
    {
        public int PerfilAcessoId{ get; set; }
        public int UsuarioCadastroId { get; set; }
        public int? UsuarioAlteracaoId { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public char FlagAtivo { get; set; }

        public UsuarioModel UsuarioCadastro { get; set; }
        public UsuarioModel? UsuarioAlteracao { get; set; }
    }
}
