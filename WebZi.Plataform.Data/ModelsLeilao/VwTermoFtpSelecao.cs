using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsLeilao;

public partial class VwTermoFtpSelecao
{
    public string NumeroTermo { get; set; }

    public int? Id { get; set; }

    public string TipoAtualizacao { get; set; }

    public int? IdLeilao { get; set; }

    public int? IdLeilaoLote { get; set; }

    public int? IdGrv { get; set; }

    public string Placa { get; set; }

    public string Chassi { get; set; }
}
