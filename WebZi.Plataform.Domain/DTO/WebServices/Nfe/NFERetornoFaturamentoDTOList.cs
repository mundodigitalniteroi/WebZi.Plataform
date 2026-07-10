using WebZi.Plataform.Domain.DTO.Sistema;
using WebZi.Plataform.Domain.DTO.WebServices.Nfe;

namespace WebZi.Plataform.Domain.DTO.WebServices.Nfse
{
    public class NFERetornoFaturamentoDTOList
    {
        public MensagemDTO Mensagem { get; set; } = new(); 
        public List<NFERetornoFaturamentoDTO> Listagem { get; set; }
    }
}