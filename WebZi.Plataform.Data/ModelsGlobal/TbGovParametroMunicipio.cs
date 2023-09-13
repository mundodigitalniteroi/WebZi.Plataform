using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsGlobal;

public partial class TbGovParametroMunicipio
{
    public int ParametroMunicipioId { get; set; }

    public int CnaeListaServicoId { get; set; }

    public int MunicipioId { get; set; }

    public string CodigoCnae { get; set; }

    public string ItemListaServico { get; set; }

    public string CodigoMunicipioIbge { get; set; }

    public string CodigoTributarioMunicipio { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public virtual TbGovCnaeListaServico CnaeListaServico { get; set; }

    public virtual TbGloLocMunicipio Municipio { get; set; }
}
