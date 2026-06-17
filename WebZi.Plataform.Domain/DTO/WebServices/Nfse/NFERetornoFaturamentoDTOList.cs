using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.WebServices.Nfse
{
    public class NFERetornoFaturamentoDTOList
    {
        public MensagemDTO Mensagem { get; set; } = new(); 
        public List<NFERetornoFaturamentoDTO> Listagem { get; set; }
    }
}