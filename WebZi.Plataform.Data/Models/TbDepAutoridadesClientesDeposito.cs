using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepAutoridadesClientesDeposito
{
    public int IdAutoridadeClienteDeposito { get; set; }

    public int IdAutoridadeResponsavel { get; set; }

    public int IdCliente { get; set; }

    public int IdDeposito { get; set; }

    public int IdUsuarioCadastro { get; set; }

    public DateTime DataCadastro { get; set; }

    public string FlagAtivo { get; set; }
}
