using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbLogFaturamentoRegra
{
    public long Id { get; set; }

    public short? IdFaturamentoRegra { get; set; }

    public short? IdFaturamentoRegraTipo { get; set; }

    public int? IdCliente { get; set; }

    public int? IdDeposito { get; set; }

    public int? IdUsuarioCadastro { get; set; }

    public int? IdUsuarioAlteracao { get; set; }

    public string Valor { get; set; }

    public DateTime? DataVigenciaInicial { get; set; }

    public DateTime? DataVigenciaFinal { get; set; }

    public DateTime? DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public DateTime DatahoraLog { get; set; }
}
