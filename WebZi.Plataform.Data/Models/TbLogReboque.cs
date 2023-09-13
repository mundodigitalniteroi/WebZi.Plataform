using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbLogReboque
{
    public long Id { get; set; }

    public int? IdReboque { get; set; }

    public int? IdCliente { get; set; }

    public short? IdDeposito { get; set; }

    public decimal? IdUsuarioCadastro { get; set; }

    public decimal? IdUsuarioAlteracao { get; set; }

    public string Codigo { get; set; }

    public string Placa { get; set; }

    public string Chassi { get; set; }

    public string Renavam { get; set; }

    public string Marca { get; set; }

    public string Modelo { get; set; }

    public decimal? Ano { get; set; }

    public DateTime? DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public string FlagAtivo { get; set; }
}
