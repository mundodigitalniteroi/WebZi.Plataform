namespace WebZi.Plataform.Domain.Models.WebServices.DetranAlagoas.ConsultaVeiculoApreensao.Envio
{
    public class AutorizarRetiradaModel
    {
        public string Placa { get; set; }

        public string CodigoDeposito { get; set; }

        public DateTime DataSaida { get; set; }

        public string CodigoTipoSaida { get; set; }

        public string NumeroAutoRetirada { get; set; }

        public string Observacoes { get; set; }

        public string Matricula { get; set; }
    }
}