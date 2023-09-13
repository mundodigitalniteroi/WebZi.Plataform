using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbLogGrvDrfaAgendamentoRetiradum
{
    public long Id { get; set; }

    public int IdUsuarioCrud { get; set; }

    public string Crud { get; set; }

    public DateTime DatahoraLog { get; set; }

    public int IdGrvDrfaAgendamentoRetirada { get; set; }

    public int IdGrvDrfa { get; set; }

    public int IdUsuarioRegistroAgendamento { get; set; }

    public string NomeResponsavelAgendamento { get; set; }

    public string CpfResponsavelAgendamento { get; set; }

    public DateTime? DataRegistroAgendamento { get; set; }

    public DateTime? DataAgendamento { get; set; }
}
