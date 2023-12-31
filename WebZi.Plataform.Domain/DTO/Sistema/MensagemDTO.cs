using WebZi.Plataform.CrossCutting.Web;

namespace WebZi.Plataform.Domain.DTO.Sistema
{
    public class MensagemDTO
    {
        public HtmlStatusCodeEnum HtmlStatusCode { get; set; }

        public int QuantidadeRegistros { get; set; } = 1;

        public List<string> AvisosInformativos { get; set; } = new List<string>();

        public List<string> Alertas { get; set; } = new List<string>();

        public List<string> AvisosImpeditivos { get; set; } = new List<string>();

        public List<string> Erros { get; set; } = new List<string>();
    }
}