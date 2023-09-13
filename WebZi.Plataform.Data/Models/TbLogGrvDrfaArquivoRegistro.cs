using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbLogGrvDrfaArquivoRegistro
{
    public long Id { get; set; }

    public int IdUsuarioCrud { get; set; }

    public string Crud { get; set; }

    public DateTime DatahoraLog { get; set; }

    public int IdGrvDrfaArquivoRegistro { get; set; }

    public int IdGrvDrfa { get; set; }

    public string ArquivoNome { get; set; }

    public byte[] ArquivoRegistro { get; set; }

    public string TipoArquivo { get; set; }
}
