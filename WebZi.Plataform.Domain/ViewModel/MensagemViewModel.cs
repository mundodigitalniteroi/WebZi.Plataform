using WebZi.Plataform.CrossCutting.Web;

namespace WebZi.Plataform.Domain.ViewModel
{
    public class MensagemViewModel
    {
        public HtmlStatusCodeEnum HtmlStatusCode { get; set; }

        public List<string> AvisosInformativos { get; set; } = new List<string>();

        public List<string> AvisosImpeditivos { get; set; } = new List<string>();

        public List<string> Erros { get; set; } = new List<string>();
    }
}