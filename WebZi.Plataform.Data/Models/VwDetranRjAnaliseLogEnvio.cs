using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwDetranRjAnaliseLogEnvio
{
    public string Cliente { get; set; }

    public string Deposito { get; set; }

    public int IdGrv { get; set; }

    public byte? IdMotivoApreensao { get; set; }

    public short IdTransacaoClienteDeposito { get; set; }

    public int IdUsuarioCadastro { get; set; }

    public string Status { get; set; }

    public string Resultado { get; set; }

    public string Placa { get; set; }

    public string Chassi { get; set; }

    public string CodigoOperacao { get; set; }

    public DateTime? DataCadastro { get; set; }
}
