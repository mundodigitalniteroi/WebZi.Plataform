using WebZi.Plataform.Domain.DTO.Report;
using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Banco.PIX
{
    public class PixDinamicoCompletoDTO
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public PixDinamicoDTO PixDinamico { get; set; }
        public GuiaPagamentoReboqueEstadiaDTO GuiaPagamentoReboqueEstadia { get; set; }
    }
}