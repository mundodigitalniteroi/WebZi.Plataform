using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsLeilao;

public partial class TbLeiaoPrestacaoContasIten
{
    public int Id { get; set; }

    public int? IdPrestacaoContas { get; set; }

    public int? Grupo { get; set; }

    public string Descricao { get; set; }

    public decimal? Devido { get; set; }

    public decimal? NaoPago { get; set; }

    public decimal? Pago { get; set; }

    public decimal? Saldo { get; set; }
}
