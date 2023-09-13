using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepDetroGrvMotivoNaoAutorizado
{
    public int IdDetroGrvMotivoNaoAutorizado { get; set; }

    public int IdDetroGrv { get; set; }

    public int IdUsuarioCadastro { get; set; }

    public DateTime DataCadastro { get; set; }

    public string MotivoNaoAutorizado { get; set; }

    public virtual TbDepDetroGrv IdDetroGrvNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioCadastroNavigation { get; set; }
}
