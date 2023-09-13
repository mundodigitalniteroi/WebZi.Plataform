using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsGlobal;

public partial class TbGloPesPessoasTiposContato
{
    public long IdPessoaTipoContato { get; set; }

    public long IdPessoa { get; set; }

    public int IdTipoContato { get; set; }

    public string Descricao { get; set; }

    public string FlagContatoPrincipal { get; set; }

    public virtual TbGloPesPessoa IdPessoaNavigation { get; set; }

    public virtual TbGloDocTiposContato IdTipoContatoNavigation { get; set; }
}
