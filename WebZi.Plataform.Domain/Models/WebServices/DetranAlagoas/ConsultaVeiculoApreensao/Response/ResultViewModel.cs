using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.Models.WebServices.DetranAlagoas.ConsultaVeiculoApreensao.Response
{
    public class ResultViewModel
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public ResultModel Result { get; set; }
    }
}