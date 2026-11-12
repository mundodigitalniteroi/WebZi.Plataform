using WebZi.Plataform.Domain.DTO.GRV;

namespace WebZi.Plataform.Domain.Models.GRV.SolicitacaoReboque;

public class EnquadramentoInfracaoSolicitacaoDTO
{
    public int IdentificadorEnquadramentoGrv { get; set; }
    public string NumeroInfracao { get; set; }
    public EnquadramentoInfracaoDTO Infracao { get; set; }
}