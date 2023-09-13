using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbLogGrvDrfaRegistroRecuperacao
{
    public long Id { get; set; }

    public int IdUsuarioCrud { get; set; }

    public string Crud { get; set; }

    public DateTime DatahoraLog { get; set; }

    public int IdGrvDrfaRegistroRecuperacao { get; set; }

    public int IdGrvDrfa { get; set; }

    public byte IdAutoridadeDivisao { get; set; }

    public string NumeroRegistroRecuperacao { get; set; }

    public string RegistroRecuperacaoMatriculaAgente { get; set; }

    public string RegistroRecuperacaoNomeAgente { get; set; }

    public DateTime? DataRegistroRecuperacao { get; set; }
}
