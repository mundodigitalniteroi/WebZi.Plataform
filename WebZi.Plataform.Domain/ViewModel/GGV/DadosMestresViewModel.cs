using WebZi.Plataform.Domain.ViewModel.Veiculo;
using WebZi.Plataform.Domain.ViewModel.Vistoria;

namespace WebZi.Plataform.Domain.ViewModel.GGV
{
    public class DadosMestresViewModel
    {
        public VistoriaEstadoGeralVeiculoViewModelList EstadoGeralVeiculos { get; set; }

        public VistoriaSituacaoChassiViewModelList SituacoesChassi { get; set; }

        public VistoriaStatusViewModelList StatusVistoria { get; set; }

        public TipoAvariaViewModelList TiposAvarias { get; set; }

        public VistoriaTipoDirecaoViewModelList TiposDirecoes { get; set; }
    }
}