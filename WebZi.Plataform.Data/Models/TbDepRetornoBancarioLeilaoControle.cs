using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepRetornoBancarioLeilaoControle
{
    public short Id { get; set; }

    public string NomeArquivo { get; set; }

    public int IdUsuario { get; set; }

    public DateTime DataImportacao { get; set; }
}
