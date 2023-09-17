using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsLeilao;

public partial class TbLeilaoLotesPericiaArquivo
{
    public int Id { get; set; }

    public int IdLote { get; set; }

    public string Nome { get; set; }

    public int Tamanho { get; set; }

    public string Tipo { get; set; }

    public string Path { get; set; }

    public DateTime? DataHoraCadastro { get; set; }

    public string Usuario { get; set; }
}
