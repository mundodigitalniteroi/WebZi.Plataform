using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsLeilao;

public partial class TbNotificaco
{
    public int Id { get; set; }

    public int IdNotificacaoTipo { get; set; }

    public int IdCliente { get; set; }

    public int IdDeposito { get; set; }

    public int IdUsuario { get; set; }

    public DateTime DataHora { get; set; }

    public virtual TbNotificacoesTipo IdNotificacaoTipoNavigation { get; set; }
}
