using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepContasTemporaria
{
    public int IdContaTemporaria { get; set; }

    public int IdClienteDeposito { get; set; }

    public short IdAgenciaBancaria { get; set; }

    public DateTime DataVigenciaInicial { get; set; }

    public DateTime DataVigenciaFinal { get; set; }

    public string FlagAtivo { get; set; }

    public virtual TbDepAgenciasBancaria IdAgenciaBancariaNavigation { get; set; }

    public virtual TbDepClientesDeposito IdClienteDepositoNavigation { get; set; }
}
