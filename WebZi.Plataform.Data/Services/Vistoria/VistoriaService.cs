using AutoMapper;
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
        private readonly IMapper _mapper;

        public VistoriaService(AppDbContext context)
        {
            _context = context;
        }

        public VistoriaService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<VistoriaStatusViewModelList> ListarStatusVistoriaAsync()
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
                ResultView.Listagem.Add(new()
                {
                    IdentificadorStatus = item.VistoriaStatusId,
                    Descricao = item.Descricao
                });
            }

            ResultView.Mensagem = MensagemViewHelper.GetOkFound(result.Count);

            return ResultView;
        }

        public async Task<VistoriaSituacaoChassiViewModelList> ListarSituacaoChassiAsync()
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
                ResultView.Listagem.Add(new()
                {
                    IdentificadorSituacaoChassi = item.VistoriaSituacaoChassiId,
                    Descricao = item.Descricao
                });
            }

            ResultView.Mensagem = MensagemViewHelper.GetOkFound(result.Count);

            return ResultView;
        }

        public async Task<VistoriaTipoDirecaoViewModelList> ListarTipoDirecaoAsync()
        {
            VistoriaTipoDirecaoViewModelList ResultView = new();

            List<TabelaGenericaModel> result = await new SistemaService(_context).ListarTabelaGenerica("VISTORIA_TIPO_DIRECAO");

            if (result?.Count == 0)
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound();

                return ResultView;
            }

            foreach (var item in result)
            {
                ResultView.Listagem.Add(new()
                {
                    Sigla = item.Sigla,
                    Descricao = item.Valor1
                });
            }

            ResultView.Mensagem = MensagemViewHelper.GetOkFound(result.Count);

            return ResultView;
        }

        public async Task<VistoriaEstadoGeralVeiculoViewModelList> ListarEstadoGeralVeiculoAsync()
        {
            VistoriaEstadoGeralVeiculoViewModelList ResultView = new();

            List<TabelaGenericaModel> result = await new SistemaService(_context).ListarTabelaGenerica("VISTORIA_ESTADO_GERAL_VEICULO");

            if (result?.Count == 0)
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound();

                return ResultView;
            }

            foreach (var item in result)
            {
                ResultView.Listagem.Add(new()
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