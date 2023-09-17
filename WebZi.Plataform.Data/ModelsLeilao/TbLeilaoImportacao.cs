using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsLeilao;

public partial class TbLeilaoImportacao
{
    public int Id { get; set; }

    public int? IdLeilao { get; set; }

    public DateTime DataImportacao { get; set; }

    public string Tipo { get; set; }

    public string Arquivo { get; set; }

    public int? QtdItens { get; set; }

    public int? QtdErro { get; set; }

    public string Status { get; set; }

    public int? IdUsuario { get; set; }

    public string Obs { get; set; }
}
