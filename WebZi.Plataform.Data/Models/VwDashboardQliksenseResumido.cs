using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwDashboardQliksenseResumido
{
    public int? IdDeposito { get; set; }

    public string Processo { get; set; }

    public string Placa { get; set; }

    public string Chassi { get; set; }

    public string MarcaModelo { get; set; }

    public string StatusLote { get; set; }

    public string TipoVeiculo { get; set; }

    public string CategoriaVeiculo { get; set; }

    public string ResponsavelApreensao { get; set; }

    public string OrgaoApreensor { get; set; }

    public string DataRecolhimento { get; set; }

    public string DataGuarda { get; set; }

    public DateTime? DataLiberacao { get; set; }

    public string Reboque { get; set; }

    public string PlacaReboque { get; set; }

    public string Reboquista { get; set; }

    public int? DiasConsiderados { get; set; }

    public string TipoCobranca { get; set; }

    public string FormaLiberacao { get; set; }

    public decimal? ValorTotal { get; set; }

    public string Unidade { get; set; }

    public string Estado { get; set; }
}
