using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbSapRetorno
{
    public int IdSapRetorno { get; set; }

    public int IdTransacaoSap { get; set; }

    public string IdDocumento { get; set; }

    public string Mensagens { get; set; }

    public string Nota { get; set; }

    public string UsuarioAut { get; set; }

    public string IpLocal { get; set; }

    public string IpRemoto { get; set; }

    public int? PortaAcesso { get; set; }

    public string Url { get; set; }

    public string Link { get; set; }

    public string Metodo { get; set; }

    public string Http { get; set; }

    public string Verbo { get; set; }

    public DateTime DataHoraRegistro { get; set; }

    public string DataEmissaoNota { get; set; }
}
