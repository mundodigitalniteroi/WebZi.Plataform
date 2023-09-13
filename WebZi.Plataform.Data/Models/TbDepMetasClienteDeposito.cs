using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepMetasClienteDeposito
{
    public int IdMetaClienteDeposito { get; set; }

    public int IdDepMetasGrupo { get; set; }

    public int IdCliente { get; set; }

    public int IdDeposito { get; set; }

    public int IdUsuarioCadastro { get; set; }

    public DateTime MesRef { get; set; }

    public decimal MesCusto { get; set; }

    public DateTime MesMeta { get; set; }

    public DateTime DataCadastro { get; set; }
}
