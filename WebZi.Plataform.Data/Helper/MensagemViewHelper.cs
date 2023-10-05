using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Domain.Enums;
using WebZi.Plataform.Domain.ViewModel;

namespace WebZi.Plataform.Data.Helper
{
    public abstract class MensagemViewHelper
    {
        public static MensagemViewModel GetOk(string Message)
        {
            MensagemViewModel Mensagem = new()
            {
                HtmlStatusCode = HtmlStatusCodeEnum.Ok
            };

            Mensagem.AvisosInformativos.Add(Message);

            return Mensagem;
        }

        public static MensagemViewModel GetOk(List<string> Messages)
        {
            MensagemViewModel Mensagem = new()
            {
                HtmlStatusCode = HtmlStatusCodeEnum.Ok
            };

            foreach (string Message in Messages)
            {
                Mensagem.AvisosInformativos.Add(Message);
            }

            return Mensagem;
        }

        public static MensagemViewModel GetOkCreate(string Message = "Cadastro concluído com sucesso")
        {
            return GetOkCreate(1, Message);
        }

        public static MensagemViewModel GetOkCreate(int QuantidadeRegistros, string Message = "Cadastro concluído com sucesso")
        {
            MensagemViewModel Mensagem = new()
            {
                HtmlStatusCode = HtmlStatusCodeEnum.Ok,

                QuantidadeRegistros = QuantidadeRegistros
            };

            Mensagem.AvisosInformativos.Add(Message);

            return Mensagem;
        }

        public static MensagemViewModel GetOkUpdate(string Message = "Alteração concluída com sucesso")
        {
            return GetOkUpdate(1, Message);
        }

        public static MensagemViewModel GetOkUpdate(int QuantidadeRegistros, string Message = "Alteração concluída com sucesso")
        {
            MensagemViewModel Mensagem = new()
            {
                HtmlStatusCode = HtmlStatusCodeEnum.Ok,

                QuantidadeRegistros = QuantidadeRegistros
            };

            Mensagem.AvisosInformativos.Add(Message);

            return Mensagem;
        }

        public static MensagemViewModel GetOkDelete(string Message = "Exclusão concluída com sucesso")
        {
            return GetOkDelete(1, Message);
        }

        public static MensagemViewModel GetOkDelete(int QuantidadeRegistros, string Message = "Exclusão concluída com sucesso")
        {
            MensagemViewModel Mensagem = new()
            {
                HtmlStatusCode = HtmlStatusCodeEnum.Ok,

                QuantidadeRegistros = QuantidadeRegistros
            };

            Mensagem.AvisosInformativos.Add(Message);

            return Mensagem;
        }

        public static MensagemViewModel GetOkFound(string Message = "Registro encontrado com sucesso")
        {
            return GetOkFound(1, Message);
        }

        public static MensagemViewModel GetOkFound(int QuantidadeRegistros, string Message = "Registro(s) encontrado(s) com sucesso")
        {
            MensagemViewModel Mensagem = new()
            {
                HtmlStatusCode = HtmlStatusCodeEnum.Ok,

                QuantidadeRegistros = QuantidadeRegistros
            };

            if (QuantidadeRegistros == 1)
            {
                Mensagem.AvisosInformativos.Add("Registro encontrado com sucesso");
            }
            else
            {
                Mensagem.AvisosInformativos.Add(Message);
            }

            return Mensagem;
        }

        public static MensagemViewModel GetNotFound(string Message = "Registro não encontrado")
        {
            MensagemViewModel Mensagem = new()
            {
                HtmlStatusCode = HtmlStatusCodeEnum.NotFound,

                QuantidadeRegistros = 0
            };

            Mensagem.AvisosImpeditivos.Add(Message);

            return Mensagem;
        }

        public static MensagemViewModel GetUnauthorized(string Message = "Usuário desativado ou inexistente")
        {
            return GetUnauthorized(new List<string>() { Message });
        }

        public static MensagemViewModel GetUnauthorized(List<string> Messages)
        {
            MensagemViewModel Mensagem = new()
            {
                HtmlStatusCode = HtmlStatusCodeEnum.Unauthorized,

                QuantidadeRegistros = 0
            };

            foreach (string Message in Messages)
            {
                Mensagem.AvisosImpeditivos.Add(Message);
            }

            return Mensagem;
        }

        public static MensagemViewModel GetBadRequest(string Message)
        {
            return GetBadRequest(new List<string>() { Message });
        }

        public static MensagemViewModel GetBadRequest(List<string> Messages)
        {
            MensagemViewModel Mensagem = new()
            {
                HtmlStatusCode = HtmlStatusCodeEnum.BadRequest,

                QuantidadeRegistros = 0
            };

            foreach (string Message in Messages)
            {
                Mensagem.AvisosImpeditivos.Add(Message);
            }

            return Mensagem;
        }

        public static MensagemViewModel GetInternalServerError(string Message)
        {
            return GetInternalServerError(Message, null);
        }

        public static MensagemViewModel GetInternalServerError(Exception ex)
        {
            return GetInternalServerError(string.Empty, ex);
        }

        public static MensagemViewModel GetInternalServerError(string Message, Exception ex)
        {
            MensagemViewModel Mensagem = new()
            {
                HtmlStatusCode = HtmlStatusCodeEnum.InternalServerError,

                QuantidadeRegistros = 0
            };

            if (!string.IsNullOrWhiteSpace(Message))
            {
                Mensagem.Erros.Add(Message);
            }
            else
            {
                Mensagem.Erros.Add("Ocorreu um erro interno");
            }

            if (ex != null)
            {
                Mensagem.Erros.Add(ex.Message);

                if (ex.InnerException != null)
                {
                    Mensagem.Erros.Add(ex.InnerException.Message);
                }
            }

            return Mensagem;
        }

        public static MensagemViewModel GetNewMessage(string Message, MensagemTipoAvisoEnum TipoAviso, HtmlStatusCodeEnum HtmlStatusCode)
        {
            return GetNewMessage(new List<string>() { Message }, TipoAviso, HtmlStatusCode);
        }

        public static MensagemViewModel GetNewMessage(List<string> Messages, MensagemTipoAvisoEnum TipoAviso, HtmlStatusCodeEnum HtmlStatusCode)
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