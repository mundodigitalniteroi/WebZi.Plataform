using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepNfeImagen
{
    public int NfeImagemId { get; set; }

    public int NfeId { get; set; }

    public byte[] Imagem { get; set; }

    public DateTime DataCadastro { get; set; }

    public virtual TbDepNfe Nfe { get; set; }
}
