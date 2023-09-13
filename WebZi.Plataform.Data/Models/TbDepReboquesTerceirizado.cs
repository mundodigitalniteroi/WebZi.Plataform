using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepReboquesTerceirizado
{
    public int IdReboqueTerceirizado { get; set; }

    public int IdEmpresa { get; set; }

    public int? IdReboque { get; set; }

    public virtual TbDepReboque IdReboqueNavigation { get; set; }

    public virtual ICollection<TbDepReboquesTerceirizadosTarifa> TbDepReboquesTerceirizadosTarifas { get; set; } = new List<TbDepReboquesTerceirizadosTarifa>();
}
