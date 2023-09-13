using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwConsultarGrv
{
    public int IdGrv { get; set; }

    public string NumeroFormularioGrv { get; set; }

    public string Placa { get; set; }

    public string Chassi { get; set; }

    public string DataHoraRemocao { get; set; }

    public string DataHoraGuarda { get; set; }

    public string IdStatusOperacao { get; set; }

    public string StatusOperacaoDescricao { get; set; }

    public int IdUsuarioCadastro { get; set; }

    public string NomeUsuarioCadastro { get; set; }

    public int? IdUsuarioAlteracao { get; set; }

    public string NomeUsuarioAlteracao { get; set; }
}
