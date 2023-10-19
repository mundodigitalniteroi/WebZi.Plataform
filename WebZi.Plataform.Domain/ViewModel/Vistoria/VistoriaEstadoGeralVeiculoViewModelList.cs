namespace WebZi.Plataform.Domain.ViewModel.Vistoria
{
    public class VistoriaEstadoGeralVeiculoViewModelList
    {
        public MensagemViewModel Mensagem { get; set; } = new();

        public List<VistoriaEstadoGeralVeiculoViewModel> ListagemEstadoGeralVeiculo { get; set; } = new();
    }
}