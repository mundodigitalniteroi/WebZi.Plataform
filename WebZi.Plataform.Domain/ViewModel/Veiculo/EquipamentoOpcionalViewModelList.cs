namespace WebZi.Plataform.Domain.ViewModel.Veiculo
{
    public class EquipamentoOpcionalViewModelList
    {
        public MensagemViewModel Mensagem { get; set; } = new();

        public List<EquipamentoOpcionalViewModel> ListagemEquipamentoOpcional { get; set; } = new();
    }
}