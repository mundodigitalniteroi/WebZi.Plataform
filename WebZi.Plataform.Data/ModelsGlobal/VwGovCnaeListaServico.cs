using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsGlobal;

public partial class VwGovCnaeListaServico
{
    public int CnaeListaServicoId { get; set; }

    public int CnaeId { get; set; }

    public string CnaeCodigo { get; set; }

    public string CnaeCodigoFormatado { get; set; }

    public string CnaeDescricao { get; set; }

    public int ListaServicoId { get; set; }

    public string ListaServico { get; set; }

    public string ListaServicoDescricao { get; set; }

    public decimal AliquotaIss { get; set; }
}
