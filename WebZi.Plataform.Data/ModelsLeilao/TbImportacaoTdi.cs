using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsLeilao;

public partial class TbImportacaoTdi
{
    public int Id { get; set; }

    public int? IdLeilao { get; set; }

    public DateTime? Data { get; set; }

    public string Url { get; set; }

    public string Usuario { get; set; }

    public string Senha { get; set; }

    public string JsonRetorno { get; set; }

    public int? QtdDados { get; set; }

    public int? IdUsuario { get; set; }
}
