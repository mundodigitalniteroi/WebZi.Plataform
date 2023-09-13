using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbLogErrosSistema
{
    public int ErroSistemaId { get; set; }

    public byte SistemaId { get; set; }

    public int UsuarioId { get; set; }

    public string Log { get; set; }

    public DateTime DataCadastro { get; set; }
}
