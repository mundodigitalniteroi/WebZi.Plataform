using WebZi.Plataform.Domain.DTO.Pessoa;
using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Usuario
{
    public class InformacoesUsuarioDTO
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public List<TiposContatosPessoaDTO> Contatos { get; set; }
        public List<PerfisAcessoUsuarioDTO> Perfis { get; set; }

    }
}
