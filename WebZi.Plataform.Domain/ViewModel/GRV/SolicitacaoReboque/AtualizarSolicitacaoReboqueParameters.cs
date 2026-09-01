using WebZi.Plataform.Domain.ViewModel.GRV.Cadastro;

namespace WebZi.Plataform.Domain.ViewModel.GRV.SolicitacaoReboque;

public class AtualizarSolicitacaoReboqueParameters
{
    public int SolicitacaoReboqueId { get; set; }
    public byte SolicitacaoReboqueStatusId { get; set; }
    public int? IdentificadorReboque { get; set; }
    public int? IdentificadorReboquista { get; set; }
    public int? IdentificadorGrv { get; set; }
    public int IdentificadorUsuario { get; set; }
    public List<byte[]> ListagemFoto { get; set; }

    public List<string> ListagemLacre { get; set; }
    public CondutorParameters Condutor { get; set; }
    public List<CondutorDocumentoParameters> ListagemDocumentoCondutor { get; set; }

    public List<EnquadramentoInfracaoParameters>? ListagemEnquadramentoInfracao { get; set; }
}