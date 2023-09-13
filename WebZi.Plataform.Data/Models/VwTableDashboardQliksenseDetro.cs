using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwTableDashboardQliksenseDetro
{
    public int? IdDeposito { get; set; }

    public string Processo { get; set; }

    public string Placa { get; set; }

    public string Chassi { get; set; }

    public string MarcaModelo { get; set; }

    public string TipoVeiculo { get; set; }

    public string CategoriaVeiculo { get; set; }

    public string ResponsavelApreensao { get; set; }

    public string OrgaoApreensor { get; set; }

    public string DataRecolhimento { get; set; }

    public string DataGuarda { get; set; }

    public DateTime? DataLiberacao { get; set; }

    public string Reboque { get; set; }

    public string PlacaReboque { get; set; }

    public string Frota { get; set; }

    public string Reboquista { get; set; }

    public int? Dias { get; set; }

    public int? DiasConsiderados { get; set; }

    public string TipoCobranca { get; set; }

    public decimal? ValorTotal { get; set; }

    public string Unidade { get; set; }

    public string Regiao { get; set; }

    public string Estado { get; set; }

    public int? HoraCheiaApreensao { get; set; }

    public string DiaSemanaApreensao { get; set; }

    public string DiaSemanaLiberacao { get; set; }

    public int? DiaMesApreensao { get; set; }

    public int? DiaMesLiberacao { get; set; }

    public decimal? ValorDiaria { get; set; }

    public decimal? ValorDiariaDesconto { get; set; }

    public decimal? ValorReboque { get; set; }

    public decimal? ValorReboqueDesconto { get; set; }

    public decimal? ValorOutrosServicos { get; set; }

    public decimal? ValorOutrosServicosDesconto { get; set; }

    public decimal? ValorTotalSemDesconto { get; set; }

    public decimal? ValorPago { get; set; }

    public string FormaLiberacao { get; set; }
}
