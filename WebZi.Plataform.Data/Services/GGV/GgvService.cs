using AutoMapper;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Services.Vistoria;
using WebZi.Plataform.Domain.ViewModel.GGV;

namespace WebZi.Plataform.Data.Services.GGV
{
    public class GgvService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public GgvService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DadosMestresViewModel> ListarDadosMestres()
        {
            VistoriaService VistoriaService = new(_context);

            DadosMestresViewModel DadosMestres = new()
            {
                EstadoGeralVeiculos = await VistoriaService.ListarEstadoGeralVeiculo(),

                SituacoesChassi = await VistoriaService.ListarSituacaoChassi(),

                StatusVistoria = await VistoriaService.ListarStatusVistoria(),

                TiposAvarias = await new TipoAvariaService(_context, _mapper).ListarTipoAvaria(),

                TiposDirecoes = await VistoriaService.ListarTipoDirecao()
            };

            return DadosMestres;
        }
    }
}