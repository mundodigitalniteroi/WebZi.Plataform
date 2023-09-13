using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbLogTiposCombustivei
{
    public long Id { get; set; }

    public decimal? IdTipoCombustivel { get; set; }

    public string Descricao { get; set; }

    public int? IdUsuario { get; set; }

    public DateTime? DataCadastro { get; set; }

    public string Status { get; set; }
}
