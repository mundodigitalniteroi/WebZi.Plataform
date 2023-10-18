using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Sistema;
using WebZi.Plataform.Domain.Models.Sistema;
using WebZi.Plataform.Domain.Models.Vistoria;
using WebZi.Plataform.Domain.ViewModel.Vistoria;

namespace WebZi.Plataform.Data.Services.Vistoria
{
    public class VistoriaService
    {
        private readonly AppDbContext _context;

        public VistoriaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<VistoriaStatusViewModelList> ListarStatusVistoria()
        {
            VistoriaStatusViewModelList ResultView = new();

            List<VistoriaStatusModel> result = await _context.VistoriaStatus
                .AsNoTracking()
                .ToListAsync();

            result = result
                .OrderBy(x => x.Descricao)
                .ToList();

            foreach (var item in result)
            {
                ResultView.StatusVistoria.Add(new()
                {
                    VistoriaStatusId = item.VistoriaStatusId,
                    Descricao = item.Descricao
                });
            }

            ResultView.Mensagem = MensagemViewHelper.GetOkFound(result.Count);

            return ResultView;
        }

        public async Task<VistoriaSituacaoChassiViewModelList> ListarSituacaoChassi()
        {
            VistoriaSituacaoChassiViewModelList ResultView = new();

            List<VistoriaSituacaoChassiModel> result = await _context.VistoriaSituacaoChassi
                .AsNoTracking()
                .ToListAsync();

            result = result
                .OrderBy(x => x.Descricao)
                .ToList();

            foreach (var item in result)
            {
                ResultView.SituacaoChassi.Add(new()
                {
                    VistoriaSituacaoChassiId = item.VistoriaSituacaoChassiId,
                    Descricao = item.Descricao
                });
            }

            ResultView.Mensagem = MensagemViewHelper.GetOkFound(result.Count);

            return ResultView;
        }

        public async Task<VistoriaTipoDirecaoViewModelList> ListarTipoDirecao()
        {
            VistoriaTipoDirecaoViewModelList ResultView = new();

            List<TabelaGenericaModel> result = await new TabelaGenericaService(_context).List("VISTORIA_TIPO_DIRECAO");

            if (result?.Count == 0)
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound();

                return ResultView;
            }

            foreach (var item in result)
            {
                ResultView.TipoDirecao.Add(new()
                {
                    Sigla = item.Sigla,
                    Descricao = item.Valor1
                });
            }

            ResultView.Mensagem = MensagemViewHelper.GetOkFound(result.Count);

            return ResultView;
        }

        public async Task<VistoriaEstadoGeralVeiculoViewModelList> ListarEstadoGeralVeiculo()
        {
            VistoriaEstadoGeralVeiculoViewModelList ResultView = new();

            List<TabelaGenericaModel> result = await new TabelaGenericaService(_context).List("VISTORIA_ESTADO_GERAL_VEICULO");

            if (result?.Count == 0)
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound();

                return ResultView;
            }

            foreach (var item in result)
            {
                ResultView.EstadoGeralVeiculo.Add(new()
                {
                    Sigla = item.Sigla,
                    Descricao = item.Valor1
                });
            }

            ResultView.Mensagem = MensagemViewHelper.GetOkFound(result.Count);

            return ResultView;
        }
    }
}