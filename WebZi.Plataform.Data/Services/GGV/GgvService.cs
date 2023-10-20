using AutoMapper;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Services.Sistema;
using WebZi.Plataform.Data.Services.Veiculo;
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

        public async Task<DadosMestresViewModel> ListarDadosMestres(byte TipoVeiculoId)
        {
            VistoriaService VistoriaService = new(_context);

            DadosMestresViewModel DadosMestres = new()
            {
                ListagemCorOstentada = await new SistemaService(_context, _mapper)
                    .ListarCores(),

                ListagemEquipamento = await new VeiculoService(_context, _mapper)
                    .ListarEquipamentoOpcional(TipoVeiculoId),

                ListagemEstadoGeralVeiculo = await VistoriaService
                    .ListarEstadoGeralVeiculo(),

                ListagemSituacaoChassi = await VistoriaService
                    .ListarSituacaoChassi(),

                ListagemStatusVistoria = await VistoriaService
                    .ListarStatusVistoria(),

                ListagemTipoAvaria = await new TipoAvariaService(_context, _mapper)
                    .ListarTipoAvaria(),

                ListagemTipoDirecao = await VistoriaService 
                    .ListarTipoDirecao()
            };

            return DadosMestres;
        }
    }
}