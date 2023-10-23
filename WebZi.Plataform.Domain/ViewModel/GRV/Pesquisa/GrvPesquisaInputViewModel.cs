namespace WebZi.Plataform.Domain.ViewModel.GRV.Pesquisa
{
    public class GrvPesquisaInputViewModel
    {
        public List<string> ListaCodigoProduto { get; set; } = new();

        public List<string> ListaStatusOperacao { get; set; } = new();

        public string NumeroProcesso { get; set; }

        public string Placa { get; set; }

        public string Chassi { get; set; }

        public string FlagVeiculoNaoIdentificado { get; set; }

        public DateTime? DataInicialRemocao { get; set; }

        public DateTime? DataFinalRemocao { get; set; }

        public int? ClienteId { get; set; }

        public int? DepositoId { get; set; }

        public string ReboquePlaca { get; set; }

        public string ReboquistaNome { get; set; }

        public int UsuarioId { get; set; }
    }
}