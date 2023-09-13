using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepGrvFoto
{
    public int IdFoto { get; set; }

    public int IdGrv { get; set; }

    public int? IdUsuarioCadastro { get; set; }

    public string TipoFoto { get; set; }

    public byte[] Foto { get; set; }

    public DateTime DataCadastro { get; set; }

    public virtual TbDepGrv IdGrvNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioCadastroNavigation { get; set; }
}
