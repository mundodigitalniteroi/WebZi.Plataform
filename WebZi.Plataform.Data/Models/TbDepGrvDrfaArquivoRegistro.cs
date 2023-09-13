using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepGrvDrfaArquivoRegistro
{
    public int IdGrvDrfaArquivoRegistro { get; set; }

    public int IdGrvDrfa { get; set; }

    public string ArquivoNome { get; set; }

    public byte[] ArquivoRegistro { get; set; }

    public string TipoArquivo { get; set; }

    public virtual TbDepGrvDrfa IdGrvDrfaNavigation { get; set; }
}
