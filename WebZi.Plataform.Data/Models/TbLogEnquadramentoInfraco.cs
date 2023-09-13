using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbLogEnquadramentoInfraco
{
    public long Id { get; set; }

    public decimal? IdEnquadramentoInfracao { get; set; }

    public int? IdUsuario { get; set; }

    public string CodigoInfracao { get; set; }

    public short? Artigo { get; set; }

    public string Inciso { get; set; }

    public string Descricao { get; set; }

    public DateTime? DataCadastro { get; set; }

    public string Status { get; set; }
}
