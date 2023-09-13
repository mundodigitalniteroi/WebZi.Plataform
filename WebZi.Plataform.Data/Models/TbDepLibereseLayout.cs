using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepLibereseLayout
{
    public int IdLibereseLayout { get; set; }

    public int IdCliente { get; set; }

    public int Ordem { get; set; }

    public string Texto { get; set; }

    public byte[] Imagem { get; set; }
}
