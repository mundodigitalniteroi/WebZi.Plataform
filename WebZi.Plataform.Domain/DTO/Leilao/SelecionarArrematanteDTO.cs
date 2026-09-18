using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Leilao;

public class SelecionarArrematanteDTO
{
    public MensagemDTO Mensagem { get; set; } = new();

    public ArrematanteProcessoDTO Processo { get; set; } = new();

    public ArrematanteVeiculoDTO Veiculo { get; set; } = new();

    public ArrematanteDadosDTO Arrematante { get; set; } = new();
}

public class ArrematanteProcessoDTO
{
    public int? IdentificadorProcesso { get; set; }
    public string? NumeroProcesso { get; set; }
    public string? StatusOperacaoId { get; set; }
    public string? StatusOperacaoDescricao { get; set; }
}

public class ArrematanteVeiculoDTO
{
    public string? Placa { get; set; }
    public string? PlacaOstentada { get; set; }
    public string? Chassi { get; set; }
    public string? Renavam { get; set; }
    public string? MarcaModelo { get; set; }
    public string? Cor { get; set; }
    public string? TipoVeiculo { get; set; }
    public string? VeiculoUF { get; set; }
    public string? AnoFabricacao { get; set; }
    public string? AnoModelo { get; set; }
    public DateTime? DataHoraGuarda { get; set; }
    public string? ClienteNome { get; set; }
    public string? DepositoNome { get; set; }
    public string? DepositoEndereco { get; set; }
    public string? DepositoTelefone { get; set; }
}

public class ArrematanteDadosDTO
{
    public int IdentificadorArrematante { get; set; }
    public string? Nome { get; set; }
    public string? CpfCnpj { get; set; }
    public string? TelefoneFixo { get; set; }
    public string? TelefoneCelular { get; set; }
    public string? Email { get; set; }
    public string? Logradouro { get; set; }
    public string? Numero { get; set; }
    public string? Complemento { get; set; }
    public string? Bairro { get; set; }
    public string? Cidade { get; set; }
    public string? Estado { get; set; }
    public string? Cep { get; set; }
    public DateTime? DataCadastro { get; set; }
    public ArrematanteLeilaoDTO Leilao { get; set; } = new();
}

public class ArrematanteLeilaoDTO
{
    public string? NomeLeilao { get; set; }
    public string? NumeroLote { get; set; }
    public string? ValorArrematacao { get; set; }
    public string? ValorTaxaAdministrativa { get; set; }
    public string? ValorOutrasTaxas { get; set; }
    public string? ValorComissao { get; set; }
    public string? ValorTotal { get; set; }
    public DateTime? DataLeilao { get; set; }
}
