namespace WebZi.Plataform.Domain.Models.Usuario
{
    public class SistemaPerfilAcessoUsuariosModel
    {
        public int PerfilUsuarioAcessoId{ get; set; }
        public int PerfilAcessoId { get; set; }
        public int UsuarioId { get; set; }

        public SistemaPerfilAcessoModel PerfilAcesso { get; set; }
        public UsuarioModel Usuario { get; set; }
    }
}
