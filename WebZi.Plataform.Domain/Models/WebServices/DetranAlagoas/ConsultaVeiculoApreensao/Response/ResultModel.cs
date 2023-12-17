namespace WebZi.Plataform.Domain.Models.WebServices.DetranAlagoas.ConsultaVeiculoApreensao.Response
{
    public class ResultModel
    {
        public string Profile { get; set; }

        public string Codigo { get; set; }

        public string Mensagem { get; set; }

        public RetornoModel AutorizacaoRetirada { get; set; }
    }
}