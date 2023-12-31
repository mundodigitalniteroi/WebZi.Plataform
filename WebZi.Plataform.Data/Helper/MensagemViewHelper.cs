using WebZi.Plataform.CrossCutting.Strings;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Domain.DTO.Sistema;
using WebZi.Plataform.Domain.Enums;

namespace WebZi.Plataform.Data.Helper
{
    public abstract class MensagemViewHelper
    {
        public static MensagemDTO SetBadRequest()
        {
            return SetBadRequest(new List<string>() { "BadRequest" });
        }

        public static MensagemDTO SetBadRequest(string Message)
        {
            return SetBadRequest(new List<string>() { Message });
        }

        public static MensagemDTO SetBadRequest(List<string> Messages)
        {
            MensagemDTO Mensagem = new()
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

        public static MensagemDTO SetOk()
        {
            MensagemDTO Mensagem = new()
            {
                HtmlStatusCode = HtmlStatusCodeEnum.Ok
            };

            return Mensagem;
        }

        public static MensagemDTO SetOk(string Message)
        {
            MensagemDTO Mensagem = new()
            {
                HtmlStatusCode = HtmlStatusCodeEnum.Ok
            };

            Mensagem.AvisosInformativos.Add(Message);

            return Mensagem;
        }

        public static MensagemDTO SetOk(List<string> Messages)
        {
            MensagemDTO Mensagem = new()
            {
                HtmlStatusCode = HtmlStatusCodeEnum.Ok
            };

            foreach (string Message in Messages)
            {
                Mensagem.AvisosInformativos.Add(Message);
            }

            return Mensagem;
        }

        public static MensagemDTO SetOk(MensagemDTO Mensagem)
        {
            Mensagem.HtmlStatusCode = HtmlStatusCodeEnum.Ok;

            return Mensagem;
        }

        public static MensagemDTO SetOk(MensagemDTO Mensagem, string Message)
        {
            Mensagem.HtmlStatusCode = HtmlStatusCodeEnum.Ok;

            Mensagem.AvisosInformativos.Add(Message);

            return Mensagem;
        }

        public static MensagemDTO SetOk(MensagemDTO Mensagem, List<string> Messages)
        {
            Mensagem.HtmlStatusCode = HtmlStatusCodeEnum.Ok;

            foreach (string Message in Messages)
            {
                Mensagem.AvisosInformativos.Add(Message);
            }

            return Mensagem;
        }

        public static MensagemDTO SetCreateSuccess(string Message = "Cadastro concluído com sucesso")
        {
            return SetCreateSuccess(1, Message);
        }

        public static MensagemDTO SetCreateSuccess(int QuantidadeRegistros, string Message = "Cadastro concluído com sucesso")
        {
            MensagemDTO Mensagem = new()
            {
                HtmlStatusCode = HtmlStatusCodeEnum.Ok,

                QuantidadeRegistros = QuantidadeRegistros
            };

            Mensagem.AvisosInformativos.Add(Message);

            return Mensagem;
        }

        public static MensagemDTO SetDeleteSuccess(string Message = "Exclusão concluída com sucesso")
        {
            return SetDeleteSuccess(1, Message);
        }

        public static MensagemDTO SetDeleteSuccess(int QuantidadeRegistros, string Message = "Exclusão concluída com sucesso")
        {
            MensagemDTO Mensagem = new()
            {
                HtmlStatusCode = HtmlStatusCodeEnum.Ok,

                QuantidadeRegistros = QuantidadeRegistros
            };

            Mensagem.AvisosInformativos.Add(Message);

            return Mensagem;
        }

        public static MensagemDTO SetFound(string Message = "Registro encontrado com sucesso")
        {
            return SetFound(1, Message);
        }

        public static MensagemDTO SetFound(int QuantidadeRegistros, string Message = "Registro(s) encontrado(s) com sucesso")
        {
            MensagemDTO Mensagem = new()
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

        public static MensagemDTO SetInternalServerError(string Message)
        {
            return SetInternalServerError(Message, null);
        }

        public static MensagemDTO SetInternalServerError(Exception ex)
        {
            return SetInternalServerError(string.Empty, ex);
        }

        public static MensagemDTO SetInternalServerError(string Message, Exception ex)
        {
            MensagemDTO Mensagem = new()
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

        public static MensagemDTO SetNewMessage(MensagemDTO Message, string NewMessage, MensagemTipoAvisoEnum TipoAviso)
        {
            if (TipoAviso == MensagemTipoAvisoEnum.Alerta)
            {
                Message.Alertas.Add(NewMessage);
            }
            else if (TipoAviso == MensagemTipoAvisoEnum.Erro)
            {
                Message.Erros.Add(NewMessage);
            }
            else if (TipoAviso == MensagemTipoAvisoEnum.Impeditivo)
            {
                Message.AvisosImpeditivos.Add(NewMessage);
            }
            else if (TipoAviso == MensagemTipoAvisoEnum.Informativo)
            {
                Message.AvisosInformativos.Add(NewMessage);
            }

            return Message;
        }

        public static MensagemDTO SetNewMessage(string Message, MensagemTipoAvisoEnum TipoAviso, HtmlStatusCodeEnum HtmlStatusCode)
        {
            return SetNewMessage(new List<string>() { Message }, TipoAviso, HtmlStatusCode);
        }

        public static MensagemDTO SetNewMessage(List<string> Messages, MensagemTipoAvisoEnum TipoAviso, HtmlStatusCodeEnum HtmlStatusCode)
        {
            MensagemDTO Mensagem = new()
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

        public static MensagemDTO SetNewMessages(MensagemDTO Message, MensagemDTO NewMessages)
        {
            foreach (string item in NewMessages.Alertas)
            {
                Message.Alertas.Add(item);
            }

            foreach (string item in NewMessages.AvisosImpeditivos)
            {
                Message.Alertas.Add(item);
            }

            foreach (string item in NewMessages.AvisosInformativos)
            {
                Message.Alertas.Add(item);
            }

            foreach (string item in NewMessages.Erros)
            {
                Message.Erros.Add(item);
            }

            return Message;
        }

        public static MensagemDTO SetNotFound(string Message = "Registro não encontrado")
        {
            MensagemDTO Mensagem = new()
            {
                HtmlStatusCode = HtmlStatusCodeEnum.NotFound,

                QuantidadeRegistros = 0
            };

            Mensagem.AvisosImpeditivos.Add(Message);

            return Mensagem;
        }

        public static MensagemDTO SetServiceUnavailable()
        {
            return SetServiceUnavailable(string.Empty, null);
        }

        public static MensagemDTO SetServiceUnavailable(string Message)
        {
            return SetServiceUnavailable(Message, null);
        }

        public static MensagemDTO SetServiceUnavailable(Exception ex)
        {
            return SetServiceUnavailable(string.Empty, ex);
        }

        public static MensagemDTO SetServiceUnavailable(string Message, Exception ex)
        {
            MensagemDTO Mensagem = new()
            {
                HtmlStatusCode = HtmlStatusCodeEnum.ServiceUnavailable,

                QuantidadeRegistros = 0
            };

            if (!Message.IsNullOrWhiteSpace())
            {
                Mensagem.Erros.Add(Message);
            }
            else
            {
                Mensagem.Erros.Add("O Serviço de dependência requisitado está inoperante ou indisponível");
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

        public static MensagemDTO SetUnauthorized(string Message = "Usuário desativado ou inexistente")
        {
            return SetUnauthorized(new List<string>() { Message });
        }

        public static MensagemDTO SetUnauthorized(List<string> Messages)
        {
            MensagemDTO Mensagem = new()
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

        public static MensagemDTO SetUpdateSuccess(string Message = "Alteração concluída com sucesso")
        {
            return SetUpdateSuccess(1, Message);
        }

        public static MensagemDTO SetUpdateSuccess(int QuantidadeRegistros, string Message = "Alteração concluída com sucesso")
        {
            MensagemDTO Mensagem = new()
            {
                HtmlStatusCode = HtmlStatusCodeEnum.Ok,

                QuantidadeRegistros = QuantidadeRegistros
            };

            Mensagem.AvisosInformativos.Add(Message);

            return Mensagem;
        }
    }
}