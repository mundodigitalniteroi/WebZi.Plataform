using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwTransacoesDetranRjStatus
{
    public int IdGrv { get; set; }

    public int IdCliente { get; set; }

    public int IdDeposito { get; set; }

    public string IdStatusOperacao { get; set; }

    public int? IdLiberacao { get; set; }

    public int IdUsuarioGrv { get; set; }

    public string LoginGrv { get; set; }

    public int IdUsuarioGgv { get; set; }

    public string LoginGgv { get; set; }

    public DateTime? DataHoraRemocao { get; set; }

    public DateTime? DataHoraGuarda { get; set; }

    public string DataRemocao { get; set; }

    public string DataGuarda { get; set; }

    public int? ARec { get; set; }

    public int? AGua { get; set; }

    public int Recolher { get; set; }

    public int Guardar { get; set; }

    public int ConsultarLiberacao { get; set; }

    public int Liberar { get; set; }

    public DateTime? DataEnvioLog { get; set; }

    public string NumeroGrv { get; set; }

    public string Placa { get; set; }

    public string Chassi { get; set; }
}
