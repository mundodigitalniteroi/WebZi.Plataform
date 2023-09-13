using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsGlobal;

public partial class VwGovCnaeListaServicoParametroMunicipio
{
    public int CnaeListaServicoId { get; set; }

    public int CnaeId { get; set; }

    public int ListaServicoId { get; set; }

    public int MunicipioId { get; set; }

    public int ParametroMunicipioId { get; set; }

    public string CnaeCodigo { get; set; }

    public string CnaeCodigoFormatado { get; set; }

    public string CnaeDescricao { get; set; }

    public string ListaServico { get; set; }

    public string ListaServicoDescricao { get; set; }

    public decimal AliquotaIss { get; set; }

    public string Uf { get; set; }

    public string Municipio { get; set; }

    public string CodigoMunicipioIbge { get; set; }

    public string CodigoTributarioMunicipio { get; set; }
}
