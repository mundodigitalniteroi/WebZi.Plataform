using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepGrvDrfaRegistroRecuperacao
{
    public int IdGrvDrfaRegistroRecuperacao { get; set; }

    public int IdGrvDrfa { get; set; }

    public byte IdAutoridadeDivisao { get; set; }

    public string NumeroRegistroRecuperacao { get; set; }

    public string RegistroRecuperacaoMatriculaAgente { get; set; }

    public string RegistroRecuperacaoNomeAgente { get; set; }

    public DateTime DataRegistroRecuperacao { get; set; }

    public virtual TbDepGrvDrfa IdGrvDrfaNavigation { get; set; }
}
