using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsGlobal;

public partial class TbGovCnae
{
    public int CnaeId { get; set; }

    public string Codigo { get; set; }

    public string CodigoFormatado { get; set; }

    public string Descricao { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public string FlagPrincipal { get; set; }

    public virtual ICollection<TbGloEmpEmpresa> TbGloEmpEmpresas { get; set; } = new List<TbGloEmpEmpresa>();

    public virtual ICollection<TbGovCnaeListaServico> TbGovCnaeListaServicos { get; set; } = new List<TbGovCnaeListaServico>();
}
