namespace WebZi.Plataform.Domain.ViewModel.Faturamento
{
    public class ServicoAssociadoTipoVeiculoViewModel
    {
        public int IdentificadorServicoAssociadoTipoVeiculo { get; set; }

        public string DescricaoServico { get; set; }

        public string TipoCobranca { get; set; }

        public string DescricaoTipoCobranca { get; set; }

        public string FlagPermiteAlteracaoValor { get; set; }

        public decimal PrecoPadrao { get; set; }

        public decimal PrecoMinimoObrigatorio { get; set; }

        public DateTime DataVigenciaInicial { get; set; }
    }
}