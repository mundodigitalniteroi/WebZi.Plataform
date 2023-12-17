using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Text;
using WebZi.Plataform.CrossCutting.Localizacao;
using WebZi.Plataform.CrossCutting.Number;
using WebZi.Plataform.CrossCutting.Strings;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Domain.ViewModel.Localizacao;
using WebZi.Plataform.Domain.Views.Localizacao;

namespace WebZi.Plataform.Data.Services.Localizacao
{
    public class EnderecoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public EnderecoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public EnderecoViewModel GetById(int CEPId)
        {
            EnderecoViewModel ResultView = new();

            if (CEPId <= 0)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Identificador do CEP inválido");

                return ResultView;
            }

            ViewEnderecoCompletoModel result = _context.EnderecoCompleto
               .AsNoTracking()
               .FirstOrDefault(x => x.CEPId == CEPId);

            if (result != null)
            {
                ResultView = _mapper.Map<EnderecoViewModel>(result);

                ResultView.Mensagem = MensagemViewHelper.SetFound();
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();
            }

            return ResultView;
        }

        public EnderecoViewModel GetByCEP(string CEP)
        {
            EnderecoViewModel ResultView = new();

            if (CEP.IsNullOrWhiteSpace())
            {
                ResultView.Mensagem.AvisosImpeditivos.Add("Informe o CEP");
            }
            else if (!LocalizacaoHelper.IsCEP(CEP))
            {
                ResultView.Mensagem.AvisosImpeditivos.Add("CEP inválido");
            }

            if (ResultView.Mensagem.AvisosImpeditivos.Count > 0)
            {
                ResultView.Mensagem.HtmlStatusCode = HtmlStatusCodeEnum.BadRequest;

                return ResultView;
            }

            ViewEnderecoCompletoModel result = _context.EnderecoCompleto
               .AsNoTracking()
               .FirstOrDefault(x => x.CEP == CEP);

            if (result != null)
            {
                ResultView = _mapper.Map<EnderecoViewModel>(result);

                ResultView.Mensagem = MensagemViewHelper.SetFound();
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();
            }

            return ResultView;
        }

        public string FormatarEndereco(ViewEnderecoCompletoModel EnderecoCompleto, string Numero, string Complemento)
        {
            StringBuilder Endereco = new();

            if (!string.IsNullOrWhiteSpace(EnderecoCompleto.TipoLogradouro) && !string.IsNullOrWhiteSpace(EnderecoCompleto.Logradouro))
            {
                Endereco.Append(EnderecoCompleto.TipoLogradouro);

                Endereco.Append(' ');
            }

            if (!string.IsNullOrWhiteSpace(EnderecoCompleto.Logradouro))
            {
                Endereco.Append(EnderecoCompleto.Logradouro);

                if (string.IsNullOrWhiteSpace(Numero))
                {
                    Endereco.Append(", S/N");
                }
                else
                {
                    if (NumberHelper.IsNumber(Numero))
                    {
                        Endereco.Append(", nº " + Numero.ToInt());
                    }
                    else
                    {
                        Endereco.Append(", " + Numero);
                    }
                }

                if (!string.IsNullOrWhiteSpace(Complemento))
                {
                    Endereco.Append(", " + Complemento);
                }
            }

            if (!string.IsNullOrWhiteSpace(EnderecoCompleto.Bairro))
            {
                if (Endereco.ToString().IsNullOrWhiteSpace())
                {
                    Endereco.Append(EnderecoCompleto.Bairro);
                }
                else
                {
                    Endereco.Append(", " + EnderecoCompleto.Bairro);
                }
            }

            if (Endereco.ToString().IsNullOrWhiteSpace())
            {
                Endereco.Append(EnderecoCompleto.Municipio);
            }
            else
            {
                Endereco.Append(" - " + EnderecoCompleto.Municipio);
            }

            Endereco.Append("/" + EnderecoCompleto.UF);

            return Endereco.ToString();
        }

        public string FormatarEndereco(string TipoLogradouro, string Logradouro, string Numero, string Complemento, string Bairro, string Municipio, string UF)
        {
            StringBuilder Endereco = new();

            if (!string.IsNullOrWhiteSpace(TipoLogradouro))
            {
                Endereco.Append(TipoLogradouro);

                Endereco.Append(' ');
            }

            Endereco.Append(Logradouro);

            if (string.IsNullOrWhiteSpace(Numero))
            {
                Endereco.Append(", S/N");
            }
            else
            {
                if (NumberHelper.IsNumber(Numero))
                {
                    Endereco.Append(", nº " + Numero.ToInt());
                }
                else
                {
                    Endereco.Append(", " + Numero);
                }
            }

            if (!string.IsNullOrWhiteSpace(Complemento))
            {
                Endereco.Append(", " + Complemento);
            }

            if (!string.IsNullOrWhiteSpace(Bairro))
            {
                if (Endereco.ToString().IsNullOrWhiteSpace())
                {
                    Endereco.Append(Bairro);
                }
                else
                {
                    Endereco.Append(", " + Bairro);
                }
            }

            if (Endereco.ToString().IsNullOrWhiteSpace())
            {
                Endereco.Append(Municipio);
            }
            else
            {
                Endereco.Append(" - " + Municipio);
            }

            Endereco.Append("/" + UF);

            return Endereco.ToString();
        }
    }
}