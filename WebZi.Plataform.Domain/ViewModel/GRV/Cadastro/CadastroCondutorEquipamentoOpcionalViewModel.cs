namespace WebZi.Plataform.Domain.ViewModel.GRV.Cadastro
{
    public class CadastroCondutorEquipamentoOpcionalViewModel
    {
        public int IdentificadorGrv { get; set; }

        public decimal IdentificadorEquipamentoOpcional { get; set; }

        public int? IdentificadorUsuarioCadastro { get; set; }

        public int? CodigoAvaria { get; set; }

        public string Avariado { get; set; }

        public string FlagPossuiEquipamento { get; set; }
    }
}