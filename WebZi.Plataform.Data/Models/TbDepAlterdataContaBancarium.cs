using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepAlterdataContaBancarium
{
    public int AlterDataContaBancariaId { get; set; }

    public int? ClienteId { get; set; }

    public string Identificador { get; set; }

    public string Codigo { get; set; }

    public short CodigoEmpresa { get; set; }

    public string Agencia { get; set; }

    public string NumeroConta { get; set; }

    public string Descricao { get; set; }

    public bool? Ativa { get; set; }

    public DateTime DataCadastro { get; set; }

    public virtual TbDepCliente Cliente { get; set; }
}
