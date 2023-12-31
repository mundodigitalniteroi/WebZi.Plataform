using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.GRV.Cadastro
{
    public class ResultadoCadastroGrvDTO
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public int IdentificadorProcesso { get; set; }
    }
}