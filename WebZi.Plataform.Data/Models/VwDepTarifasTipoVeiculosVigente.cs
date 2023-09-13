using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwDepTarifasTipoVeiculosVigente
{
    public int IdGrv { get; set; }

    public int IdTarifaTipoVeiculoVigente { get; set; }

    public int IdTarifa { get; set; }

    public string TarifaDescricao { get; set; }

    public decimal TarifaPrecoDiaria { get; set; }

    public decimal TarifaPrecoRebocada { get; set; }

    public decimal TarifaPrecoQuilometragem { get; set; }

    public DateTime DataVigenciaInicial { get; set; }

    public DateTime? DataVigenciaFinal { get; set; }

    public byte IdTipoVeiculo { get; set; }

    public string TipoVeiculosNome { get; set; }

    public string TipoVeiculosFlagNaoRequerCnhNaLiberacao { get; set; }
}
