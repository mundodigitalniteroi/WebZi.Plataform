using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepGrvVistoriaArquivoLaudo
{
    public int IdGrvVistoriaArquivoLaudo { get; set; }

    public int IdGrvVistoria { get; set; }

    public string NomeArquivo { get; set; }

    public byte[] ArquivoLaudoVistoria { get; set; }

    public virtual TbDepGrvVistorium IdGrvVistoriaNavigation { get; set; }
}
