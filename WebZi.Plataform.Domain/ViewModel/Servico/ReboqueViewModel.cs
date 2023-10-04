namespace WebZi.Plataform.Domain.ViewModel.Servico
{
    public class ReboqueViewModel
    {
        public int ReboqueId { get; set; }

        public int ClienteId { get; set; }

        public int DepositoId { get; set; }

        public string Codigo { get; set; }

        public string Placa { get; set; }

        public string Chassi { get; set; }

        public string Renavam { get; set; }

        public string Marca { get; set; }

        public string Modelo { get; set; }

        public decimal? Ano { get; set; }

        public string FlagAtivo { get; set; }
    }
}