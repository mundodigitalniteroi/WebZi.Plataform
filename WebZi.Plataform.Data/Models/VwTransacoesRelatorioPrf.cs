using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwTransacoesRelatorioPrf
{
    public string NumeroFormularioGrv { get; set; }

    public string Placa { get; set; }

    public string Chassi { get; set; }

    public string MarcaModelo { get; set; }

    public string TipoVeiculo { get; set; }

    public string Cor { get; set; }

    public string FlagComboio { get; set; }

    public DateTime? DataHoraRemocao { get; set; }

    public DateTime? DataHoraGuarda { get; set; }

    public string Status { get; set; }

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
}
