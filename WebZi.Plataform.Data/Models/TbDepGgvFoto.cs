using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepGgvFoto
{
    public int IdFoto { get; set; }

    public int IdGrv { get; set; }

    public int? IdUsuarioCadastro { get; set; }

    public string TipoFoto { get; set; }

    public byte[] Foto { get; set; }

    public DateTime DataCadastro { get; set; }

    /// <summary>
    /// E: Entrada no Pátio;
    /// V: Vistoria;
    /// R: Regularização.
    /// </summary>
    public string TipoCadastro { get; set; }
}
