using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Domain.Services.GRV;
using WebZi.Plataform.Domain.ViewModel;

namespace WebZi.Plataform.Data.Services.Liberacao
{
    public class LiberacaoService
    {
        private readonly AppDbContext _context;

        public LiberacaoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<MensagemViewModel> CreateGuiaAutorizacaoRetiradaVeiculoAsync(int GrvId, int UsuarioId)
        {
            MensagemViewModel ResultView = new GrvService(_context)
                .ValidarInputGrv(GrvId, UsuarioId);

            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            return MensagemViewHelper.GetOkCreate(1);
        }
    }
}