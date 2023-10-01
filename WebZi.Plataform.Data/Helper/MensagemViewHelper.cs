using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Domain.Enums;
using WebZi.Plataform.Domain.ViewModel;

namespace WebZi.Plataform.Data.Helper
{
    public abstract class MensagemViewHelper
    {
        public static MensagemViewModel GetOkMessage(string Message)
        {
            MensagemViewModel Mensagem = new()
            {
                HtmlStatusCode = HtmlStatusCodeEnum.Ok
            };

            Mensagem.AvisosInformativos.Add(Message);

            return Mensagem;
        }

        public static MensagemViewModel GetNotFound(string Message)
        {
            MensagemViewModel Mensagem = new()
            {
                HtmlStatusCode = HtmlStatusCodeEnum.NotFound
            };

            Mensagem.AvisosImpeditivos.Add(Message);

            return Mensagem;
        }

        public static MensagemViewModel GetUnauthorized(string Message)
        {
            MensagemViewModel Mensagem = new()
            {
                HtmlStatusCode = HtmlStatusCodeEnum.Unauthorized
            };

            Mensagem.AvisosImpeditivos.Add(Message);

            return Mensagem;
        }

        public static MensagemViewModel GetBadRequest(string Message)
        {
            MensagemViewModel Mensagem = new()
            {
                HtmlStatusCode = HtmlStatusCodeEnum.BadRequest
            };

            Mensagem.AvisosImpeditivos.Add(Message);

            return Mensagem;
        }

        public static MensagemViewModel GetNewMessage(string Message, MensagemTipoAvisoEnum TipoAviso, HtmlStatusCodeEnum HtmlStatusCode = HtmlStatusCodeEnum.BadRequest)
        {
            MensagemViewModel Mensagem = new()
            {
                HtmlStatusCode = HtmlStatusCode
            };

            if (TipoAviso == MensagemTipoAvisoEnum.Informativo)
            {
                Mensagem.AvisosInformativos.Add(Message);
            }
            else if (TipoAviso == MensagemTipoAvisoEnum.Impeditivo)
            {
                Mensagem.AvisosImpeditivos.Add(Message);
            }
            else if (TipoAviso == MensagemTipoAvisoEnum.Erro)
            {
                Mensagem.Erros.Add(Message);
            }

            return Mensagem;
        }

        public static MensagemViewModel GetNewMessage(List<string> Messages, MensagemTipoAvisoEnum TipoAviso, HtmlStatusCodeEnum HtmlStatusCode = HtmlStatusCodeEnum.BadRequest)
        {
            MensagemViewModel Mensagem = new()
            {
                HtmlStatusCode = HtmlStatusCode
            };

            if (TipoAviso == MensagemTipoAvisoEnum.Informativo)
            {
                foreach (string Message in Messages)
                {
                    Mensagem.AvisosInformativos.Add(Message);
                }
            }
            else if (TipoAviso == MensagemTipoAvisoEnum.Impeditivo)
            {
                foreach (string Message in Messages)
                {
                    Mensagem.AvisosImpeditivos.Add(Message);
                }
            }
            else if (TipoAviso == MensagemTipoAvisoEnum.Erro)
            {
                foreach (string Message in Messages)
                {
                    Mensagem.Erros.Add(Message);
                }
            }

            return Mensagem;
        }
    }
}