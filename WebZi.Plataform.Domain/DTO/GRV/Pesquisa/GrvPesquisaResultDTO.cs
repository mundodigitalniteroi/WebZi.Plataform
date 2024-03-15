namespace WebZi.Plataform.Domain.DTO.GRV.Pesquisa
{
    public class GrvPesquisaResultDTO
    {
        public int IdentificadorProcesso { get; set; }

        public string NumeroProcesso { get; set; }

        public string Placa { get; set; }

        public string Chassi { get; set; }

        public string Renavam { get; set; }

        public string MarcaModelo { get; set; }

        public string StatusOperacaoId { get; set; }

        public string StatusOperacao { get; set; }

        public DateTime DataHoraRemocao { get; set; }

        public DateTime DataHoraGuarda { get; set; }

        public string Cliente { get; set; }

        public string Deposito { get; set; }
    }
}