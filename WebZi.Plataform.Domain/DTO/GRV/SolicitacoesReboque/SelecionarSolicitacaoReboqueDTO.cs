using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.GRV.SolicitacoesReboque;

public class SelecionarSolicitacaoReboqueDTO
{
    public MensagemDTO Mensagem { get; set; } = new();
    public string? NumeroProcesso { get; set; }
    public string Cliente { get; set; }
    public string Deposito { get; set; }
    public string MotivoApreensao { get; set; }
    public SolicitacaoReboqueVeiculoDTO Veiculo { get; set; }
    public string? Reboque { get; set; }

    public string? Reboquista { get; set; }

    public string? SolicitacaoReboqueTipo { get; set; }

    public string? SolicitacaoReboqueStatus { get; set; }

    public string? LocalRemocaoCompleto { get; set; }
    public string? LocalRemocaoReferencia { get; set; }
    public string? LocalRemocaoLatitude { get; set; }
    public string? LocalRemocaoLongitude { get; set; }

    public string? UsuarioCadastro { get; set; }

    public DateTime DataCadastro { get; set; }
    public DateTime? DataSolicitacaoAceito { get; set; }
}