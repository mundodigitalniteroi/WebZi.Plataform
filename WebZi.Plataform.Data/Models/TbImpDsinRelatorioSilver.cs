using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbImpDsinRelatorioSilver
{
    public int Id { get; set; }

    public int? ClienteId { get; set; }

    public int? DepositoId { get; set; }

    public string Regional { get; set; }

    public string Delegacia { get; set; }

    public string PatioNome { get; set; }

    public string PatioMunicipio { get; set; }

    public string PatioUf { get; set; }

    public string PatioSitConvenio { get; set; }

    public string LicExterior { get; set; }

    public string NumRecolhimento { get; set; }

    public DateTime? DataHoraRecolhimento { get; set; }

    public string Br { get; set; }

    public string Km { get; set; }

    public string Placa { get; set; }

    public string UfEmplacamento { get; set; }

    public string MarcaModelo { get; set; }

    public string TipoVeiculo { get; set; }

    public string Renavam { get; set; }

    public string Chassi { get; set; }

    public string Motor { get; set; }

    public string AnoFabricacao { get; set; }

    public string AnoModelo { get; set; }

    public string Cor { get; set; }

    public string MotivoRecolhimento { get; set; }

    public DateTime? UltimaConsulta { get; set; }

    public string Status { get; set; }

    public string MatrResponsavel { get; set; }

    public string Auxiliares { get; set; }

    public string RestrArrendamento { get; set; }

    public string RestrReservaDominio { get; set; }

    public string RestrAlienacaoFiduciaria { get; set; }

    public string RestrJudicial { get; set; }

    public string RestrExecucao { get; set; }

    public string RestrAdministrativa { get; set; }

    public string RestrRouboFurto { get; set; }

    public string RestrReceitaFederal { get; set; }

    public string RestrBaixaAlieOrdemJudicial { get; set; }

    public string RestrPenhorVeiculo { get; set; }

    public string RestrPenhorMercantil { get; set; }

    public string RestrVeiculoBaixado { get; set; }

    public string RestrComunicacaoVenda { get; set; }

    public string RestrIndicadorRecallAtivo { get; set; }

    public string RestrAcidenteMediaMonta { get; set; }

    public string RestrAcidenteGrandeMonta { get; set; }

    public string RestrAcidentePerdaTotal { get; set; }

    public string RestrAlertaMenos72H { get; set; }

    public string RestrAlertaMais72H { get; set; }

    public string OcorrenciasDiversas { get; set; }

    public string TipoProprietario { get; set; }

    public string CpfCnpjProprietario { get; set; }

    public string NomeProprietario { get; set; }

    public string EnderecoProprietario { get; set; }

    public string EndPropBairro { get; set; }

    public string EndPropCidade { get; set; }

    public string EndPropUf { get; set; }

    public string EndPropCep { get; set; }

    public string EndPropCepErrado { get; set; }

    public string Arquivo { get; set; }

    public string FlagNormalizado { get; set; }

    public string FlagImportado { get; set; }
}
