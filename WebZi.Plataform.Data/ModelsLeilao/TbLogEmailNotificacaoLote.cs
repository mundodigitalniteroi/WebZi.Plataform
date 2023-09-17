using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsLeilao;

public partial class TbLogEmailNotificacaoLote
{
    public int Id { get; set; }

    public int? IdLogEmailNotificacao { get; set; }

    public int? IdLote { get; set; }
}
