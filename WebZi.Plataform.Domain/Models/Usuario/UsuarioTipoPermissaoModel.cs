namespace WebZi.Plataform.Domain.Models.Usuario
{
    public class UsuarioTipoPermissaoModel
    {
        public short TipoPermissaoId { get; set; }

        public string Codigo { get; set; }

        public string Descricao { get; set; }

        public virtual ICollection<UsuarioPermissaoModel> ListagemUsuarioPermissao { get; set; }
    }
}