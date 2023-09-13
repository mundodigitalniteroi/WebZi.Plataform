using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwGloDetranBaDadosProprietario
{
    public int IdProprietario { get; set; }

    public int IdGrv { get; set; }

    public string NumeroFormularioGrv { get; set; }

    public string IdStatusOperacao { get; set; }

    public string Placa { get; set; }

    public string Chassi { get; set; }

    public string CodigoUsuario { get; set; }

    public string SenhaUsuario { get; set; }

    public string CodigoOperacao { get; set; }

    public string ParamPlaca { get; set; }

    public string ParamUf { get; set; }

    public string ParamChassi { get; set; }

    public string ParamRenavam { get; set; }

    public string CodigoRetExec { get; set; }

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
}
