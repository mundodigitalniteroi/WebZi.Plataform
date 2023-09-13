using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepFaturamentoBoletosImagen
{
    public int IdFaturamentoBoletoImagem { get; set; }

    public int IdFaturamentoBoleto { get; set; }

    public byte[] Imagem { get; set; }

    public virtual TbDepFaturamentoBoleto IdFaturamentoBoletoNavigation { get; set; }
}
