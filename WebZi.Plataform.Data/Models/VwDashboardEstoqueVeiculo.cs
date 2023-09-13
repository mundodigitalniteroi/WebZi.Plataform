using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwDashboardEstoqueVeiculo
{
    public int IdGrv { get; set; }

    public string NumeroFormularioGrv { get; set; }

    public string IdStatusOperacao { get; set; }

    public string StatusOperacao { get; set; }

    public string Renavam { get; set; }

    public string Placa { get; set; }

    public string PlacaOstentada { get; set; }

    public string Chassi { get; set; }

    public DateTime? DataHoraRemocao { get; set; }

    public DateTime? DataHoraGuarda { get; set; }

    public string FlagComboio { get; set; }

    public int IdCliente { get; set; }

    public string Cliente { get; set; }

    public int IdDeposito { get; set; }

    public string Deposito { get; set; }

    public string MarcaModelo { get; set; }

    public string Cor { get; set; }

    public string AutoridadeDivisao { get; set; }

    public string MatriculaAutoridadeResponsavel { get; set; }

    public string PlacaReboque { get; set; }

    public int? IdReboquista { get; set; }

    public string NomeReboquista { get; set; }

    public string NumeroChave { get; set; }

    public string EstacionamentoSetor { get; set; }

    public string EstacionamentoNumeroVaga { get; set; }
}
