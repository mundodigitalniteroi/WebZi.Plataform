﻿using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbLogAtendimentoExclusao
{
    public long Id { get; set; }

    public int? IdGrv { get; set; }

    public int IdAtendimento { get; set; }

    public int IdUsuario { get; set; }

    public string Motivo { get; set; }

    public DateTime DatahoraLog { get; set; }
}
