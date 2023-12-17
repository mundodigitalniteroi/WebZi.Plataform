using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.CrossCutting.Number;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Sistema;
using WebZi.Plataform.Domain.Models.Sistema;
using WebZi.Plataform.Domain.Models.WebServices.DetranAlagoas.ConsultaVeiculoApreensao.Envio;
using WebZi.Plataform.Domain.Models.WebServices.DetranAlagoas.ConsultaVeiculoApreensao.Response;

namespace WebZi.Plataform.Data.Services.WebServices
{
    public class DetranAlagoasService
    {
        private readonly AppDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;

        public DetranAlagoasService(AppDbContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResultViewModel> ConsultarVeiculoApreensao(AutorizarRetiradaModel AutorizarRetirada)
        {
            WebServiceUrlModel WebServiceUrl = await _context.WebServiceUrl
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Name == "DetranAlagoas");

            EnvioModel Envio = new();

            Envio.Login.Username = WebServiceUrl.Username;
            Envio.Login.Password = WebServiceUrl.Password;
            Envio.AutorizarRetirada = AutorizarRetirada;

            HttpClientFactoryService HttpClientFactoryService = new(_httpClientFactory);

            ResultViewModel ResultView = new();

            try
            {
                ResultView.Result = await HttpClientFactoryService.PostAsync<ResultModel>(WebServiceUrl.Url, Envio);

                if (ResultView.Result.Codigo.ToInt() == (int)HtmlStatusCodeEnum.Ok)
                {
                    ResultView.Mensagem = MensagemViewHelper.SetFound();
                }
                else
                {
                    ResultView.Mensagem = MensagemViewHelper.SetServiceUnavailable();
                }
            }
            catch (Exception)
            {
                // TODO: Gravar o erro em Log

                ResultView.Mensagem = MensagemViewHelper.SetServiceUnavailable();
            }

            return ResultView;
        }
    }
}