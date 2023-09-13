using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsGlobal;

public partial class TbGloLocMunicipio
{
    public int IdMunicipio { get; set; }

    public string Uf { get; set; }

    public string Nome { get; set; }

    public string NomePtbr { get; set; }

    public string CodigoMunicipio { get; set; }

    public string CodigoMunicipioIbge { get; set; }

    public byte? EstadoId { get; set; }

    public virtual TbGloLocEstado Estado { get; set; }

    public virtual ICollection<TbGloLocBairro> TbGloLocBairros { get; set; } = new List<TbGloLocBairro>();

    public virtual ICollection<TbGloLocCep> TbGloLocCeps { get; set; } = new List<TbGloLocCep>();

    public virtual ICollection<TbGloLocFeriado> TbGloLocFeriados { get; set; } = new List<TbGloLocFeriado>();
}
