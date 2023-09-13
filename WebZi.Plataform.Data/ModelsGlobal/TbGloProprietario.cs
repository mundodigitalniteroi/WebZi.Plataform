using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsGlobal;

public partial class TbGloProprietario
{
    public int IdProprietario { get; set; }

    public string CodigoUsuario { get; set; }

    public string SenhaUsuario { get; set; }

    public string CodigoOperacao { get; set; }

    public string ParamPlaca { get; set; }

    public string ParamUf { get; set; }

    public string ParamChassi { get; set; }

    public string ParamRenavam { get; set; }

    public int? CodigoRetExec { get; set; }

    public string CodigoMunicipio { get; set; }

    public string NomeMunicipio { get; set; }

    public string UnidadeFederacao { get; set; }

    public string NomeUf { get; set; }

    public string IndRouboFurto { get; set; }

    public string CodigoMarcaMod { get; set; }

    public string MarcaModelo { get; set; }

    public string MotorDifAcesso { get; set; }

    public string ComunicacaoVenda { get; set; }

    public string CodigoCarroceria { get; set; }

    public string Carroceria { get; set; }

    public string CodigoCor { get; set; }

    public string Cor { get; set; }

    public string CodigoCategoria { get; set; }

    public string CategoriaVeiculo { get; set; }

    public string CodigoEspecie { get; set; }

    public string DescricaoEspecie { get; set; }

    public string CodigoTipVeic { get; set; }

    public string TipoVeiculo { get; set; }

    public string AnoFabricacao { get; set; }

    public string AnoModelo { get; set; }

    public string Potencia { get; set; }

    public string Cilindrada { get; set; }

    public string CodigoCombustive { get; set; }

    public string Combustivel { get; set; }

    public string NumeroMotor { get; set; }

    public decimal? TracaoMax { get; set; }

    public decimal? PesoBrutoTotal { get; set; }

    public decimal? CapacidadeCarga { get; set; }

    public string Procedencia { get; set; }

    public string CapacidadePassag { get; set; }

    public string NumeroEixos { get; set; }

    public string Restricao1 { get; set; }

    public string Restricao2 { get; set; }

    public string Restricao3 { get; set; }

    public string Restricao4 { get; set; }

    public string Restricao5 { get; set; }

    public string Restricao6 { get; set; }

    public string TipoDocumento { get; set; }

    public string NumeroCpfCgc { get; set; }

    public string NomeProprietario { get; set; }

    public string Endereco { get; set; }

    public string NumeroEndereco { get; set; }

    public string ComplemEndereco { get; set; }

    public string NomeBairro { get; set; }

    public string NumeroCep { get; set; }

    public string NumeroTelefone { get; set; }

    public string RegravChassi { get; set; }

    public string Situacao { get; set; }

    public string DataAtualizacao { get; set; }

    public decimal? ValorDebIpva { get; set; }

    public decimal? ValorDebLicenc { get; set; }

    public decimal? ValorDebMulta { get; set; }

    public decimal? ValorDebDpvat { get; set; }

    public decimal? ValorDebInfTrami { get; set; }

    public DateTime? DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public string WebService { get; set; }

    public int? Sequencial { get; set; }

    public int? TipoRegistro { get; set; }

    public int? CodigoOcoExec { get; set; }

    public string TipoAtualizacao { get; set; }

    public int? IdLeilao { get; set; }

    public int? CodigoPatio { get; set; }

    public string NumeroTermo { get; set; }

    public int? NumeroTrave { get; set; }

    public DateTime? DataApreensao { get; set; }

    public string Status { get; set; }

    public string StatusApreensao { get; set; }

    public string FinanceiraCnpj { get; set; }

    public string FinanceiraCnpjSng { get; set; }

    public int? FinanceiraDataSng { get; set; }

    public int? FinanceiraHoraSng { get; set; }

    public string FinanceiraNome { get; set; }

    public string FinanceiraEndereco { get; set; }

    public string FinanceiraComplemEndereco { get; set; }

    public string FinanceiraNumeroEndereco { get; set; }

    public string FinanceiraBairro { get; set; }

    public string FinanceiraMunicipio { get; set; }

    public string FinanceiraUf { get; set; }

    public int? FinanceiraCep { get; set; }

    public int? QuantidadeDiaria { get; set; }

    public decimal? ValorDiaria { get; set; }

    public decimal? ValorReboque { get; set; }

    public string CompradorNumeroDoc { get; set; }

    public string CompradorNome { get; set; }

    public string CompradorEndereco { get; set; }

    public string CompradorNumeroEndereco { get; set; }

    public string CompradorComplemEndereco { get; set; }

    public int? CompradorCep { get; set; }

    public string CompradorBairro { get; set; }

    public string CompradorMunicipio { get; set; }

    public string CompradorUf { get; set; }

    public string NotifFiscalSefaz { get; set; }

    public int? IdLeilaoAnterior { get; set; }

    public DateTime? DataLimiteRestricao { get; set; }

    public int? TipoDocComunicVenda { get; set; }

    public DateTime? CompradorDataVenda { get; set; }

    public int? FinanceiraTipoDocumento { get; set; }

    public DateTime? FinanceiraData { get; set; }

    public int? FinanceiraHora { get; set; }

    public int? FinanceiraTipoDocumentoSng { get; set; }

    public int? TipoDocAgenteFinanceiro { get; set; }

    public string CpfCnpjAgenteFinanceiro { get; set; }

    public int? IndicacaoFinanciamento { get; set; }

    public string PlacaAnterior { get; set; }

    public string PlacaNova { get; set; }

    public string DescricaoMunicipioEmplacamento { get; set; }

    public string DescricaoSerie { get; set; }

    public string IndicacaoMultasRenainf { get; set; }

    public string IndicacaoDividaAtiva { get; set; }

    public string IndicacaoVeiculoBaixado { get; set; }

    public string NomeFinanciadoSng { get; set; }

    public string NomeAgenteFinanceiro { get; set; }

    public string Observacao { get; set; }

    public string NomeProprietarioAnterior { get; set; }

    public string DiaJuliano { get; set; }

    public int? Transacao { get; set; }

    public string NumeroDocProprietario { get; set; }

    public string CepProprietario { get; set; }

    public virtual ICollection<TbGloVeiculosRestrico> TbGloVeiculosRestricos { get; set; } = new List<TbGloVeiculosRestrico>();
}
