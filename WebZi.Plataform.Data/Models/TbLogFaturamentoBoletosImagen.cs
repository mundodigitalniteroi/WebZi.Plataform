using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbLogFaturamentoBoletosImagen
{
    public long Id { get; set; }

    public int IdUsuarioCrud { get; set; }

    public string Crud { get; set; }

    public DateTime DatahoraLog { get; set; }

    public int? IdFaturamentoBoletoImagem { get; set; }

    public int? IdFaturamentoBoleto { get; set; }

    public byte[] Imagem { get; set; }
}
