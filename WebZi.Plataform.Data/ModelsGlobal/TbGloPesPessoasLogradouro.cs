using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsGlobal;

public partial class TbGloPesPessoasLogradouro
{
    public long IdPessoaLogradouro { get; set; }

    public long IdPessoa { get; set; }

    public int IdCep { get; set; }

    public byte? IdTipoLogradouro { get; set; }

    public string Cep { get; set; }

    public string TipoLogradouro { get; set; }

    public string Logradouro { get; set; }

    public string Numero { get; set; }

    public string Complemento { get; set; }

    public string Bairro { get; set; }

    public string Municipio { get; set; }

    public string Uf { get; set; }

    public string FlagLogradouroPrincipal { get; set; }

    public string FlagAtivo { get; set; }

    public virtual TbGloPesPessoa IdPessoaNavigation { get; set; }

    public virtual TbGloLocTiposLogradouro IdTipoLogradouroNavigation { get; set; }
}
