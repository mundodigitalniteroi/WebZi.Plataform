using WebZi.Plataform.CrossCutting.Strings;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Domain.Enums;
using WebZi.Plataform.Domain.ViewModel;

namespace WebZi.Plataform.Data.Helper
{
    public abstract class MensagemViewHelper
    {
        public static MensagemViewModel SetBadRequest()
        {
            return SetBadRequest(new List<string>() { "BadRequest" });
        }

        public static MensagemViewModel SetBadRequest(string Message)
        {
            return SetBadRequest(new List<string>() { Message });
        }

        public static MensagemViewModel SetBadRequest(List<string> Messages)
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

        public static MensagemViewModel SetOk()
        {
            MensagemViewModel Mensagem = new()
            {
                HtmlStatusCode = HtmlStatusCodeEnum.Ok
            };

            return Mensagem;
        }

        public static MensagemViewModel SetOk(string Message)
        {
            MensagemViewModel Mensagem = new()
            {
                HtmlStatusCode = HtmlStatusCodeEnum.Ok
            };

            Mensagem.AvisosInformativos.Add(Message);

            return Mensagem;
        }

        public static MensagemViewModel SetOk(List<string> Messages)
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

        public static MensagemViewModel SetOk(MensagemViewModel Mensagem)
        {
            Mensagem.HtmlStatusCode = HtmlStatusCodeEnum.Ok;

            return Mensagem;
        }

        public static MensagemViewModel SetOk(MensagemViewModel Mensagem, string Message)
        {
            Mensagem.HtmlStatusCode = HtmlStatusCodeEnum.Ok;

            Mensagem.AvisosInformativos.Add(Message);

            return Mensagem;
        }

        public static MensagemViewModel SetOk(MensagemViewModel Mensagem, List<string> Messages)
        {
            Mensagem.HtmlStatusCode = HtmlStatusCodeEnum.Ok;

            foreach (string Message in Messages)
            {
                Mensagem.AvisosInformativos.Add(Message);
            }

            return Mensagem;
        }

        public static MensagemViewModel SetCreateSuccess(string Message = "Cadastro concluído com sucesso")
        {
            return SetCreateSuccess(1, Message);
        }

        public static MensagemViewModel SetCreateSuccess(int QuantidadeRegistros, string Message = "Cadastro concluído com sucesso")
        {
            MensagemViewModel Mensagem = new()
            {
                HtmlStatusCode = HtmlStatusCodeEnum.Ok,

                QuantidadeRegistros = QuantidadeRegistros
            };

            Mensagem.AvisosInformativos.Add(Message);

            return Mensagem;
        }

        public static MensagemViewModel SetDeleteSuccess(string Message = "Exclusão concluída com sucesso")
        {
            return SetDeleteSuccess(1, Message);
        }

        public static MensagemViewModel SetDeleteSuccess(int QuantidadeRegistros, string Message = "Exclusão concluída com sucesso")
        {
            MensagemViewModel Mensagem = new()
            {
                HtmlStatusCode = HtmlStatusCodeEnum.Ok,

                QuantidadeRegistros = QuantidadeRegistros
            };

            Mensagem.AvisosInformativos.Add(Message);

            return Mensagem;
        }

        public static MensagemViewModel SetFound(string Message = "Registro encontrado com sucesso")
        {
            return SetFound(1, Message);
        }

        public static MensagemViewModel SetFound(int QuantidadeRegistros, string Message = "Registro(s) encontrado(s) com sucesso")
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

        public static MensagemViewModel SetInternalServerError(string Message)
        {
            return SetInternalServerError(Message, null);
        }

        public static MensagemViewModel SetInternalServerError(Exception ex)
        {
            return SetInternalServerError(string.Empty, ex);
        }

        public static MensagemViewModel SetInternalServerError(string Message, Exception ex)
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

        public static MensagemViewModel SetNewMessage(string Message, MensagemTipoAvisoEnum TipoAviso, HtmlStatusCodeEnum HtmlStatusCode)
        {
            return SetNewMessage(new List<string>() { Message }, TipoAviso, HtmlStatusCode);
        }

        public static MensagemViewModel SetNewMessage(List<string> Messages, MensagemTipoAvisoEnum TipoAviso, HtmlStatusCodeEnum HtmlStatusCode)
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

        public static MensagemViewModel SetNotFound(string Message = "Registro não encontrado")
        {
            MensagemViewModel Mensagem = new()
            {
                HtmlStatusCode = HtmlStatusCodeEnum.NotFound,

                QuantidadeRegistros = 0
            };

            Mensagem.AvisosImpeditivos.Add(Message);

            return Mensagem;
        }

        public static MensagemViewModel SetServiceUnavailable()
        {
            return SetServiceUnavailable(string.Empty, null);
        }

        public static MensagemViewModel SetServiceUnavailable(string Message)
        {
            return SetServiceUnavailable(Message, null);
        }

        public static MensagemViewModel SetServiceUnavailable(Exception ex)
        {
            return SetServiceUnavailable(string.Empty, ex);
        }

        public static MensagemViewModel SetServiceUnavailable(string Message, Exception ex)
        {
            MensagemViewModel Mensagem = new()
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

        public static MensagemViewModel SetUnauthorized(string Message = "Usuário desativado ou inexistente")
        {
            return SetUnauthorized(new List<string>() { Message });
        }

        public static MensagemViewModel SetUnauthorized(List<string> Messages)
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

        public static MensagemViewModel SetUpdateSuccess(string Message = "Alteração concluída com sucesso")
        {
            return SetUpdateSuccess(1, Message);
        }

        public static MensagemViewModel SetUpdateSuccess(int QuantidadeRegistros, string Message = "Alteração concluída com sucesso")
        {
            MensagemViewModel Mensagem = new()
            {
                HtmlStatusCode = HtmlStatusCodeEnum.Ok,

                QuantidadeRegistros = QuantidadeRegistros
            };

            Mensagem.AvisosInformativos.Add(Message);

            return Mensagem;
        }
    }
}