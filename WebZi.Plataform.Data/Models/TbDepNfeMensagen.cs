using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepNfeMensagen
{
    public int NfeMensagemId { get; set; }

    public int NfeId { get; set; }

    public string Mensagem { get; set; }

    /// <summary>
    /// E: Envio;
    /// R: Reenvio.
    /// </summary>
    public string Tipo { get; set; }

    public DateTime DataCadastro { get; set; }

    public virtual TbDepNfe Nfe { get; set; }
}
