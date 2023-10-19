namespace WebZi.Plataform.Domain.ViewModel.Veiculo
{
    public class EquipamentoOpcionalViewModel
    {
        public decimal EquipamentoOpcionalId { get; set; }

        public int? OrdemVistoria { get; set; } = 0;

        public string Descricao { get; set; }

        public string ItemObrigatorio { get; set; }

        public string Status { get; set; }
    }
}