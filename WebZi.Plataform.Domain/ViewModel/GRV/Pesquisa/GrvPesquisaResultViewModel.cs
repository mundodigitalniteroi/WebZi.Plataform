namespace WebZi.Plataform.Domain.ViewModel.GRV.Pesquisa
{
    public class GrvPesquisaResultViewModel
    {
        public int IdentificadorGrv { get; set; }

        public string NumeroProcesso { get; set; }

        public string Placa { get; set; }

        public string Chassi { get; set; }

        public string StatusOperacaoId { get; set; }

        public string StatusOperacao { get; set; }

        public DateTime DataHoraRemocao { get; set; }

        public DateTime DataHoraGuarda { get; set; }

        public string Cliente { get; set; }

        public string Deposito { get; set; }
    }
}