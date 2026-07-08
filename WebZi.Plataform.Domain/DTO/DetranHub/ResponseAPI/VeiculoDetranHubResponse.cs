namespace WebZi.Plataform.Domain.DTO.DetranHub.ResponseAPI;

public class VeiculoDetranHubResponse
{
    public int? IdentificadorVeiculo { get; set; }
    public string Placa { get; set; }
    public string Chassi { get; set; }
    public bool? ChassiRemarcado { get; set; }
    public string ChassiRemarcadoCodigoOrigem { get; set; }
    public string Renavam { get; set; }
    public string Uf { get; set; }
    public string Municipio { get; set; }
    public int? AnoFabricacao { get; set; }
    public int? AnoModelo { get; set; }
    public int? AnoUltimaLicenca { get; set; }
    public int? AnoIpva { get; set; }
    public string CategoriaCodigo { get; set; }
    public string CategoriaDescricao { get; set; }
    public string Classificacao { get; set; }
    public string MarcaModeloCodigo { get; set; }
    public string MarcaModelo { get; set; }
    public string TipoVeiculoCodigo { get; set; }
    public string TipoVeiculo { get; set; }
    public string EspecieCodigo { get; set; }
    public string EspecieDescricao { get; set; }
    public string CarroceriaCodigo { get; set; }
    public string CarroceriaDescricao { get; set; }
    public string CorCodigo { get; set; }
    public string CorPrimaria { get; set; }
    public string CorSecundaria { get; set; }
    public int? CapacidadePassageiros { get; set; }
    public decimal? CapacidadeCarga { get; set; }
    public decimal? PesoBrutoTotal { get; set; }
    public int? NumeroEixos { get; set; }
    public int? Potencia { get; set; }
    public int? Cilindrada { get; set; }
    public decimal? TracaoMaxima { get; set; }
    public string Motor { get; set; }
    public string MotorRemarcado { get; set; }
    public string CombustivelCodigo { get; set; }
    public string CombustivelDescricao { get; set; }
    public string ProcedenciaCodigo { get; set; }
    public string ProcedenciaDescricao { get; set; }
    public string TipoDocumentoProprietario { get; set; }
    public string DocumentoProprietario { get; set; }
    public string NomeProprietario { get; set; }
    public string CpfProprietario { get; set; }
    public string EnderecoProprietario { get; set; }
    public string NumeroEnderecoProprietario { get; set; }
    public string ComplementoEnderecoProprietario { get; set; }
    public string BairroProprietario { get; set; }
    public string InformacaoRoubo { get; set; }
    public string RestricaoEstelionato { get; set; }
    public string CodigoRestricao { get; set; }
    public string NumeroCrv { get; set; }
    public string NovaPlaca { get; set; }
    public DateTime? DataAquisicao { get; set; }
    public DateTime? DataAtualizacao { get; set; }
    public DebitosDetranHubResponse Debitos { get; set; }
    public RestricoesOperacionaisDetranHubResponse RestricoesOperacionais { get; set; }
    public List<RestricaoDetranHubResponse> Restricoes { get; set; }
}

