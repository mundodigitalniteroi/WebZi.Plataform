using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Text;
using WebZi.Plataform.CrossCutting.Localizacao;
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
                ResultView.Mensagem = MensagemViewHelper.GetBadRequest("Identificador do CEP inválido");

                return ResultView;
            }

            ViewEnderecoCompletoModel result = _context.Endereco
               .Where(w => w.CEPId == CEPId)
               .AsNoTracking()
               .FirstOrDefault();

            if (result != null)
            {
                ResultView = _mapper.Map<EnderecoViewModel>(result);

                ResultView.Mensagem = MensagemViewHelper.GetOkFound();
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound();
            }

            return ResultView;
        }

        public EnderecoViewModel GetByCEP(string CEP)
        {
            List<string> erros = new();

            if (string.IsNullOrWhiteSpace(CEP))
            {
                erros.Add("Informe o CEP");
            }
            else if (!LocalizacaoHelper.IsCEP(CEP))
            {
                erros.Add("CEP inválido");
            }

            EnderecoViewModel ResultView = new();

            if (erros.Count > 0)
            {
                ResultView.Mensagem = MensagemViewHelper.GetBadRequest(erros);

                return ResultView;
            }

            ViewEnderecoCompletoModel result = _context.Endereco
               .Where(w => w.CEP == CEP)
               .AsNoTracking()
               .FirstOrDefault();

            if (result != null)
            {
                ResultView = _mapper.Map<EnderecoViewModel>(result);

                ResultView.Mensagem = MensagemViewHelper.GetOkFound();
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound();
            }

            return ResultView;
        }

        public string FormatarEndereco(ViewEnderecoCompletoModel input, string Numero, string Complemento)
        {
            StringBuilder Endereco = new();

            if (!string.IsNullOrWhiteSpace(input.TipoLogradouro) && !string.IsNullOrWhiteSpace(input.Logradouro))
            {
                Endereco.Append(input.TipoLogradouro);

                Endereco.Append(' ');
            }

            if (!string.IsNullOrWhiteSpace(input.Logradouro))
            {
                Endereco.Append(input.Logradouro);

                if (string.IsNullOrWhiteSpace(Numero))
                {
                    Endereco.Append(", S/N");
                }
                else
                {
                    Endereco.Append(", " + Numero);
                }

                if (!string.IsNullOrWhiteSpace(Complemento))
                {
                    Endereco.Append(", " + Complemento);
                }
            }

            if (!string.IsNullOrWhiteSpace(input.Bairro))
            {
                if (string.IsNullOrWhiteSpace(Endereco.ToString()))
                {
                    Endereco.Append(input.Bairro);
                }
                else
                {
                    Endereco.Append(", " + input.Bairro);
                }
            }

            if (string.IsNullOrWhiteSpace(Endereco.ToString()))
            {
                Endereco.Append(input.Municipio);
            }
            else
            {
                Endereco.Append(" - " + input.Municipio);
            }

            Endereco.Append("/" + input.UF);


            return Endereco.ToString();
        }
    }
}