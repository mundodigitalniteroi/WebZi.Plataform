using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsGlobal;

public partial class TbGloDocTiposContato
{
    public int IdTipoContato { get; set; }

    public string Descricao { get; set; }

    public string Formato { get; set; }

    public byte TamanhoMinimo { get; set; }

    public byte TamanhoMaximo { get; set; }

    public byte OrdemApresentacao { get; set; }

    public string FlagAtivo { get; set; }

    public virtual ICollection<TbGloPesPessoasTiposContato> TbGloPesPessoasTiposContatos { get; set; } = new List<TbGloPesPessoasTiposContato>();
}
