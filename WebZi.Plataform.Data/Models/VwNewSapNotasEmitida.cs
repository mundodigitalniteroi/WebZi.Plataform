using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwNewSapNotasEmitida
{
    public int IdGrv { get; set; }

    public int IdCliente { get; set; }

    public int IdDeposito { get; set; }

    public int? Patio { get; set; }

    public string Processo { get; set; }

    public string Placa { get; set; }

    public string Chassi { get; set; }

    public string OrgaoEmissor { get; set; }

    public string DataLiberacao { get; set; }

    public string Documento { get; set; }

    public string CodigoClienteSap { get; set; }

    public string Nome { get; set; }

    public string Endereco { get; set; }

    public string FormaPagamento { get; set; }

    public string Identificacao { get; set; }

    public decimal? ValorDiarias { get; set; }

    public decimal? ValorRebocada { get; set; }

    public decimal? ValorQuilometragem { get; set; }

    public decimal? ValorTotal { get; set; }

    public string DocSapDiaria { get; set; }

    public string DocSapRemocao { get; set; }

    public string DocSapQuilometragem { get; set; }

    public string NotaSap { get; set; }
}
