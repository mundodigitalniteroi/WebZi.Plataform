using WebZi.Plataform.Domain.DTO.Pessoa;
using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Usuario
{
    public class UsuarioDTO
    {
        public MensagemDTO Mensagem { get; set; } = new();
        public int IdentificadorUsuario { get; set; }
        public string Login { get; set; }
        public string Nome { get; set; }

        public string Token { get; set; }

        public string FlagPermissaoDesconto { get; set; }

        public string FlagPermissaoDataRetroativaFaturamento { get; set; }

        public string DataUltimoAcesso { get; set; }
        public string FlagAtivo { get; set; }

        public InformacoesUsuarioDTO InformacoesUsuario { get; set; }        
        //public List<UsuarioClienteDepositoDTO> ListagemClienteDepositoAssociado { get; set; } = new();
    }
}