namespace WebZi.Plataform.Domain.ViewModel.GRV.Cadastro
{
    public class CondutorEquipamentoOpcionalCadastroViewModel
    {
        public int GrvId { get; set; }

        public decimal EquipamentoOpcionalId { get; set; }

        public int? UsuarioCadastroId { get; set; }

        public int? CodigoAvaria { get; set; }

        public string Avariado { get; set; }

        public string FlagPossuiEquipamento { get; set; }
    }
}