using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsGlobal;

public partial class TbGloPesPessoasFoto
{
    public long IdPessoaFoto { get; set; }

    public long IdPessoa { get; set; }

    public byte[] Foto { get; set; }

    public virtual TbGloPesPessoa IdPessoaNavigation { get; set; }
}
