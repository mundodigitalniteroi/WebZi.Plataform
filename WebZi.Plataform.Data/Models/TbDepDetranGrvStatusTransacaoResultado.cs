using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepDetranGrvStatusTransacaoResultado
{
    public int IdDetranGrvTransacaoResultado { get; set; }

    public int? IdDetranGrvTransacao { get; set; }

    public short? IdTransacaoClienteDeposito { get; set; }

    public string Sucesso { get; set; }

    public string Resultado { get; set; }

    public DateTime? DataCadastro { get; set; }
}
