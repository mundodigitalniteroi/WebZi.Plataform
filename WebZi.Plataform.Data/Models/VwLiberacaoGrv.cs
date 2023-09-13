using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwLiberacaoGrv
{
    public string Descricao { get; set; }

    public string Nome { get; set; }

    public string NumeroFormularioGrv { get; set; }

    public string Expr1 { get; set; }

    public decimal ValorTipoComposicao { get; set; }

    public int QuantidadeComposicao { get; set; }

    public DateTime? DataPagamento { get; set; }
}
