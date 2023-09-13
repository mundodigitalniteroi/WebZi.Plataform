using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepLiberacao
{
    public int IdLiberacao { get; set; }

    public byte IdLiberacaoTipo { get; set; }

    public int IdUsuarioCadastro { get; set; }

    public DateTime DataCadastro { get; set; }

    public virtual TbDepLiberacaoTipo IdLiberacaoTipoNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioCadastroNavigation { get; set; }

    public virtual ICollection<TbDepGrv> TbDepGrvs { get; set; } = new List<TbDepGrv>();
}
