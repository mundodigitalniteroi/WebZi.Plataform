using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbLogReboquista
{
    public long Id { get; set; }

    public int? IdReboquista { get; set; }

    public int? IdCliente { get; set; }

    public int? IdDeposito { get; set; }

    public int? IdUsuarioCadastro { get; set; }

    public int? IdUsuarioAlteracao { get; set; }

    public string Nome { get; set; }

    public DateTime? DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public string FlagAtivo { get; set; }
}
