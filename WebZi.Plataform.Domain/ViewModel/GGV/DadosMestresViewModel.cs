using WebZi.Plataform.Domain.ViewModel.Sistema;
using WebZi.Plataform.Domain.ViewModel.Veiculo;
using WebZi.Plataform.Domain.ViewModel.Vistoria;

namespace WebZi.Plataform.Domain.ViewModel.GGV
{
    public class DadosMestresViewModel
    {
        public CorViewModelList ListagemCorOstentada { get; set; }

        public EquipamentoOpcionalViewModelList ListagemEquipamento { get; set; }

        public VistoriaEstadoGeralVeiculoViewModelList ListagemEstadoGeralVeiculo { get; set; }

        public VistoriaSituacaoChassiViewModelList ListagemSituacaoChassi { get; set; }

        public VistoriaStatusViewModelList ListagemStatusVistoria { get; set; }

        public TipoAvariaViewModelList ListagemTipoAvaria { get; set; }

        public VistoriaTipoDirecaoViewModelList ListagemTipoDirecao { get; set; }
    }
}