using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsGlobal;

public partial class TbGloLocCep
{
    public int IdCep { get; set; }

    public int IdMunicipio { get; set; }

    public int? IdBairro { get; set; }

    public byte? IdTipoLogradouro { get; set; }

    public string Cep { get; set; }

    public string Logradouro { get; set; }

    public string FlagSanitizado { get; set; }

    public virtual TbGloLocBairro IdBairroNavigation { get; set; }

    public virtual TbGloLocMunicipio IdMunicipioNavigation { get; set; }

    public virtual TbGloLocTiposLogradouro IdTipoLogradouroNavigation { get; set; }

    public virtual ICollection<TbGloEmpEmpresa> TbGloEmpEmpresas { get; set; } = new List<TbGloEmpEmpresa>();
}
