using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwDepGrvEquipamento
{
    public int IdGrv { get; set; }

    public string NumeroFormularioGrv { get; set; }

    public byte IdTipoVeiculo { get; set; }

    public string TipoVeiculoDescricao { get; set; }

    public int? IdCondutorEquipamentoOpcional { get; set; }

    public string Avariado { get; set; }

    public string FlagPossuiEquipamento { get; set; }

    public decimal IdEquipamentoOpcional { get; set; }

    public string EquipamentoOpcionalDescricao { get; set; }

    public string EquipamentoItemObrigatorio { get; set; }

    public int? EquipamentoOrdemVistoria { get; set; }

    public int? IdPreGrvAvarias { get; set; }

    public string AvariaDescricao { get; set; }

    public string NaoConformidadeExplicacao { get; set; }

    public DateTime? NaoConformidadeDataCadastro { get; set; }

    public int? IdUsuarioNaoConformidade { get; set; }

    public string UsuarioNaoConformidadeLogin { get; set; }

    public string UsuarioNaoConformidadeNome { get; set; }
}
