using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsLeilao;

public partial class TbComitentesTipoImportacao
{
    public int Id { get; set; }

    public string Descricao { get; set; }

    public virtual ICollection<TbComitente> TbComitentes { get; set; } = new List<TbComitente>();
}
