using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepGrvDrfaAgendamentoRetiradum
{
    public int IdGrvDrfaAgendamentoRetirada { get; set; }

    public int IdGrvDrfa { get; set; }

    public int IdUsuarioRegistroAgendamento { get; set; }

    public string NomeResponsavelAgendamento { get; set; }

    public string CpfResponsavelAgendamento { get; set; }

    public DateTime DataRegistroAgendamento { get; set; }

    public DateTime DataAgendamento { get; set; }

    public virtual TbDepGrvDrfa IdGrvDrfaNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioRegistroAgendamentoNavigation { get; set; }
}
