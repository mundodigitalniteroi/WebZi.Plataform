using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsLeilao;

public partial class TbNotificacaoFatura
{
    public int Id { get; set; }

    public int? IdFatura { get; set; }

    public string Destinatario { get; set; }

    public bool? Enviado { get; set; }

    public string MsgErro { get; set; }

    public DateTime? DataEnvio { get; set; }

    public virtual TbArrematantesFatura IdFaturaNavigation { get; set; }
}
