using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Leilao.Vistoria;

public class SelecionarVistoriaPreLeilaoDTO
{
    public MensagemDTO Mensagem { get; set; } = new();
    public string? Url { get; set; }
    public string NumeroProcesso { get; set; }
    public string Placa { get; set; }
    public string Chassi { get; set; }
    public string Status { get; set; }

    public int VistoriaId { get; set; }
    public int GrvId { get; set; }
    public int? EmpresaVistoriaId { get; set; }
    public byte? VistoriaStatusId { get; set; }
    public byte? VistoriaSituacaoChassiId { get; set; }
    public int UsuarioCadastroId { get; set; }
    public int? UsuarioAlteracaoId { get; set; }
    public string MotivoNaoRealizacaoVistoria { get; set; }
    public string NumeroVistoria { get; set; }
    public string NomeVistoriador { get; set; }
    public string NumeroMotor { get; set; }
    public string ResumoVistoria { get; set; }
    public DateTime? DataVistoria { get; set; }
    public DateTime DataCadastro { get; set; }
    public DateTime? DataAlteracao { get; set; }
    public string TipoDirecao { get; set; }
    public string EstadoGeralVeiculo { get; set; }
    public string FlagPossuiRestricoes { get; set; } = "N";
    public string FlagPossuiPlaca { get; set; } = "N";
    public string FlagPossuiVidroEletrico { get; set; } = "N";
    public string FlagPossuiTravaEletrica { get; set; } = "N";
    public string MatriculaVistoriador { get; set; }
}
