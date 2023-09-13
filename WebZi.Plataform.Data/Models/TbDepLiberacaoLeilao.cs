using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepLiberacaoLeilao
{
    public int IdLiberacaoLeilao { get; set; }

    public int IdGrv { get; set; }

    public string IdStatusOperacaoLeilao { get; set; }

    public int IdUsuarioCadastro { get; set; }

    public DateTime DataCadastro { get; set; }

    public virtual TbDepGrv IdGrvNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioCadastroNavigation { get; set; }
}
