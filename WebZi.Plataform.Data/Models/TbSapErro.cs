using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbSapErro
{
    public int Id { get; set; }

    public string Erro { get; set; }

    public string Mensagem { get; set; }

    public DateTime Datahora { get; set; }

    public string Metodo { get; set; }
}
