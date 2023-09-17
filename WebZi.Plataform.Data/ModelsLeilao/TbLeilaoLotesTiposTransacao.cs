using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsLeilao;

public partial class TbLeilaoLotesTiposTransacao
{
    public int Id { get; set; }

    public string Descricao { get; set; }

    public int Ordem { get; set; }

    public string Flag { get; set; }

    public virtual ICollection<TbComitentesTransacao> TbComitentesTransacaos { get; set; } = new List<TbComitentesTransacao>();
}
