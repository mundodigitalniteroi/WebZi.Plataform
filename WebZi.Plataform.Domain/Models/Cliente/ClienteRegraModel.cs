using WebZi.Plataform.Domain.Models.Usuario;

namespace WebZi.Plataform.Domain.Models.Cliente
{
    public class ClienteRegraModel
    {
        public short ClienteRegraId { get; set; }

        public short ClienteRegraTipoId { get; set; }

        public int ClienteId { get; set; }

        public int UsuarioCadastroId { get; set; }

        public int? UsuarioAlteracaoId { get; set; }

        public string Valor { get; set; }

        public DateTime DataVigenciaInicial { get; set; }

        public DateTime? DataVigenciaFinal { get; set; }

        public virtual ClienteModel Cliente { get; set; }

        public virtual ClienteRegraTipoModel ClienteRegraTipo { get; set; }

        public virtual UsuarioModel UsuarioCadastro { get; set; }

        public virtual UsuarioModel UsuarioAlteracao { get; set; }
    }
}