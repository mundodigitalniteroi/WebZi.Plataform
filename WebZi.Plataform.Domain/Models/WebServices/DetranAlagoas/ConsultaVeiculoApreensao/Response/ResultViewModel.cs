using WebZi.Plataform.Domain.ViewModel;

namespace WebZi.Plataform.Domain.Models.WebServices.DetranAlagoas.ConsultaVeiculoApreensao.Response
{
    public class ResultViewModel
    {
        public MensagemViewModel Mensagem { get; set; } = new();

        public ResultModel Result { get; set; }
    }
}