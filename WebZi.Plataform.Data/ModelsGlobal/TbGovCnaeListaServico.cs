using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsGlobal;

public partial class TbGovCnaeListaServico
{
    public int CnaeListaServicoId { get; set; }

    public int CnaeId { get; set; }

    public int ListaServicoId { get; set; }

    public DateTime DataCadastro { get; set; }

    public virtual TbGovCnae Cnae { get; set; }

    public virtual TbGovListaServico ListaServico { get; set; }

    public virtual ICollection<TbGloEmpEmpresa> TbGloEmpEmpresas { get; set; } = new List<TbGloEmpEmpresa>();
}
