using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Usuario
{
    public class UsuarioDTO
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public int IdentificadorUsuario { get; set; }

        public string Token { get; set; }

        public string FlagPermissaoDesconto { get; set; }

        public string FlagPermissaoDataRetroativaFaturamento { get; set; }

        public string FlagAtivo { get; set; }

        public List<UsuarioClienteDepositoDTO> ListagemClienteDepositoAssociado { get; set; } = new();
    }
}