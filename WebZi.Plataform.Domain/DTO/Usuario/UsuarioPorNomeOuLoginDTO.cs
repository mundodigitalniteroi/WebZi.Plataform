using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Usuario
{
    public class UsuarioPorNomeOuLoginDTO
    {
        public MensagemDTO Mensagem { get; set; } = new();
        public int IdentificadorUsuario { get; set; }
        public string Login { get; set; }
        public string Nome { get; set; }
        public string DataUltimoAcesso { get; set; }
        public string FlagAtivo { get; set; }

    }
}
