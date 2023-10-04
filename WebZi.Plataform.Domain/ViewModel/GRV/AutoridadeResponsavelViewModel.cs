namespace WebZi.Plataform.Domain.ViewModel.GRV
{
    public class AutoridadeResponsavelViewModel
    {
        public int AutoridadeResponsavelId { get; set; }

        public short OrgaoEmissorId { get; set; }

        public string Divisao { get; set; }

        public string FlagAtivo { get; set; } = "S";

        public int? SistemaExternoId { get; set; }
    }
}