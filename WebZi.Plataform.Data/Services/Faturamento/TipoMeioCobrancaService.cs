using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.ViewModel.Faturamento;

namespace WebZi.Plataform.Data.Services.Faturamento
{
    public class TipoMeioCobrancaService
    {
        private readonly AppDbContext _context;

        public TipoMeioCobrancaService(AppDbContext context)
        {
            _context = context;
        }

        public TipoMeioCobrancaViewModel GetById(byte TipoMeioCobrancaId)
        {
            TipoMeioCobrancaViewModel ResultView = new();

            TipoMeioCobrancaModel result = _context.TipoMeioCobranca
                .Where(w => w.TipoMeioCobrancaId == TipoMeioCobrancaId)
                .AsNoTracking()
                .FirstOrDefault();

            if (result != null)
            {
                ResultView.TiposMeiosCobrancas.Add(result);

                ResultView.Mensagem = MensagemViewHelper.GetOkFound();
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound();
            }

            return ResultView;
        }

        public TipoMeioCobrancaViewModel List()
        {
            TipoMeioCobrancaViewModel ResultView = new();

            List<TipoMeioCobrancaModel> result = _context.TipoMeioCobranca
                .Where(w => w.FlagAtivo.Equals("S"))
                .AsNoTracking()
                .ToList();

            if (result?.Count > 0)
            {
                result = result
                    .OrderBy(o => o.Descricao)
                    .ToList();

                ResultView.TiposMeiosCobrancas = result;

                ResultView.Mensagem = MensagemViewHelper.GetOkFound(result.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound();
            }

            return ResultView;
        }
    }
}