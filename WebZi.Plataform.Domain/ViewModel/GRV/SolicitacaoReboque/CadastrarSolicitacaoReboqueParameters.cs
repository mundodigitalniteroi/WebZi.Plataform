using WebZi.Plataform.Domain.ViewModel.GRV.Cadastro;

namespace WebZi.Plataform.Domain.ViewModel.GRV.SolicitacaoReboque;

public class CadastrarSolicitacaoReboqueParameters
{
    public int IdentificadorCliente { get; set; }
    public int IdentificadorDeposito { get; set; }
    public byte IdentificadorMotivoApreensao { get; set; }
    public int IdentificadorUsuario { get; set; }
    public string LocalRemocaoEnderecoCompleto { get; set; }
    public string LocalRemocaoEnderecoReferencia { get; set; }
    public string LocalRemocaoEnderecoLatitude { get; set; }
    public string LocalRemocaoEnderecoLongitude { get; set; }

    public byte? IdentificadorTipoVeiculo { get; set; }
    public int? IdentificadorCor { get; set; }
    public int? IdentificadorMarcaModelo { get; set; }
    public string? Placa { get; set; }
    public string? Chassi { get; set; }
    public string? Renavam { get; set; }
    public string? VeiculoUF { get; set; }
    public SolicitacaoReboqueGrvParameters? SolicitacaoReboqueGrv { get; set; }
    public SolicitacaoReboqueDRFAParameters? SolicitacaoReboqueDRFA { get; set; }
    public CondutorParameters? Condutor { get; set; }

    public List<EnquadramentoInfracaoParameters>? ListagemEnquadramentoInfracao { get; set; }

    public List<byte[]>? ListagemFoto { get; set; }
}

public class SolicitacaoReboqueGrvParameters
{
    public int IdentificadorAutoridadeResponsavel { get; set; }
    public string MatriculaAgenteResponsavel { get; set; }
    public string? NomeAgenteResponsavel { get; set; }

}

public class SolicitacaoReboqueDRFAParameters
{
    public byte TipoRegistroId { get; set; }
    public short? OrgaoEmissorId { get; set; }
    public byte? AutoridadeDivisaoId { get; set; }
    public string? AutoridadeDivisaoComplemento { get; set; }
    public string? NumeroRegistroRouboFurto { get; set; }
    public string? RegistroRouboFurtoMatriculaAgente { get; set; }
    public string? RegistroRouboFurtoNomeAgente { get; set; }
    public string? EstadoGeralVeiculo { get; set; }

}