using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsGlobal;

public partial class TbGloLocTiposLogradouro
{
    public byte IdTipoLogradouro { get; set; }

    public string Codigo { get; set; }

    public string Descricao { get; set; }

    public virtual ICollection<TbGloEmpEmpresa> TbGloEmpEmpresas { get; set; } = new List<TbGloEmpEmpresa>();

    public virtual ICollection<TbGloLocCep> TbGloLocCeps { get; set; } = new List<TbGloLocCep>();

    public virtual ICollection<TbGloPesPessoasLogradouro> TbGloPesPessoasLogradouros { get; set; } = new List<TbGloPesPessoasLogradouro>();
}
