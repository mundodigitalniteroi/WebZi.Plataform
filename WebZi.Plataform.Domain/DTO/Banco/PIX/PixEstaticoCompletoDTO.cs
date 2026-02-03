using WebZi.Plataform.Domain.DTO.Report;
using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Banco.PIX
{
    public class PixEstaticoCompletoDTO
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public PixEstaticoDTO PixEstatico { get; set; }
        public GuiaPagamentoReboqueEstadiaDTO GuiaPagamentoReboqueEstadia { get; set; }
    }
}