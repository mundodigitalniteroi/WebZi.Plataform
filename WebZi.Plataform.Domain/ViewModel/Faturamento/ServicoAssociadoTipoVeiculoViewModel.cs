namespace WebZi.Plataform.Domain.ViewModel.Faturamento
{
    public class ServicoAssociadoTipoVeiculoViewModel
    {
        public int FaturamentoServicoTipoVeiculoId { get; set; }

        public string ServicoDescricao { get; set; }

        public string TipoCobranca { get; set; }

        public string FlagPermiteAlteracaoValor { get; set; }

        public decimal PrecoPadrao { get; set; }

        public decimal PrecoValorMinimo { get; set; }

        public DateTime DataVigenciaInicial { get; set; }
    }
}