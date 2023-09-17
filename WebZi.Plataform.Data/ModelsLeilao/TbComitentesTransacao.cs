using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsLeilao;

public partial class TbComitentesTransacao
{
    public int Id { get; set; }

    public int? IdComitente { get; set; }

    public int? IdTipoTransacao { get; set; }

    public int? OrdemExecucao { get; set; }

    public bool? ProximaTransacao { get; set; }

    public virtual TbLeilaoLotesTiposTransacao IdTipoTransacaoNavigation { get; set; }
}
