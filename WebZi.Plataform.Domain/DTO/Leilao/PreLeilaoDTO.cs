namespace WebZi.Plataform.Domain.DTO.Leilao;

public class PreLeilaoDTO
{
    public string NumeroFormularioGrv { get; set; }
    public string Placa { get; set; }
    public string Chassi { get; set; }
    public string Renavam { get; set; }
    public string MarcaModelo { get; set; }
    public string TipoVeiculo { get; set; }
    public string Cor { get; set; }
    public char FlagComboio { get; set; }
    public DateTime? DataHoraRemocao { get; set; }
    public DateTime? DataHoraGuarda { get; set; }
    public string IdStatusOperacao { get; set; }
    public int IdGrv { get; set; }
    public int? IdTarifaTipoVeiculo { get; set; }
    public int IdCliente { get; set; }
    public int IdDeposito { get; set; }
    public int? IdReboquista { get; set; }
    public int? IdReboque { get; set; }
    public int? IdAutoridadeResponsavel { get; set; }
    public int? IdCor { get; set; }
    public int? IdDetranMarcaModelo { get; set; }
    public DateTime DataCadastro { get; set; }
    public string Municipio { get; set; }
    public string Uf { get; set; }
    public string? DescLeilaoAnterior { get; set; }
    public string? DescStatusLoteAnterior { get; set; }
    public int? IdLeilaoAnterior { get; set; }
    public int? IdLoteAnterior { get; set; }
    public DateTime? DataLeilaoAnterior { get; set; }
}