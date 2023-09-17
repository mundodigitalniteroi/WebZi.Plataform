using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsLeilao;

public partial class TbLeilaoLotesFoto
{
    public int Id { get; set; }

    public int IdLote { get; set; }

    public byte[] Foto { get; set; }

    public string NomeFoto { get; set; }

    public string Observacao { get; set; }
}
