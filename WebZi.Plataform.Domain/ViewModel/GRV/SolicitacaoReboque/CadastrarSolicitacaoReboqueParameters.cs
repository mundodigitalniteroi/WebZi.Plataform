using WebZi.Plataform.Domain.ViewModel.GRV.Cadastro;

namespace WebZi.Plataform.Domain.ViewModel.GRV.SolicitacaoReboque;

public class CadastrarSolicitacaoReboqueParameters
{
    public int IdentificadorCliente { get; set; }
    public int IdentificadorDeposito { get; set; }
    public byte IdentificadorTipoSolicitacao { get; set; }
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

    public int? IdentificadorAutoridadeResponsavel { get; set; }
    public string? MatriculaAutoridadeResponsavel { get; set; }
    public string? NomeAutoridadeResponsavel { get; set; }

    public CondutorParameters? Condutor { get; set; }

    public List<EnquadramentoInfracaoParameters>? ListagemEnquadramentoInfracao { get; set; }

    public List<byte[]>? ListagemFoto { get; set; }
}