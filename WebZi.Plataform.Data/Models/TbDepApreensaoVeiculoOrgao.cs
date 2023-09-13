using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepApreensaoVeiculoOrgao
{
    public int IdApreensaoVeiculoOrgao { get; set; }

    public int IdGrv { get; set; }

    public int IdUsuario { get; set; }

    public string CodigoDetran { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public string Placa { get; set; }

    public string Chassi { get; set; }

    public string CodDeposito { get; set; }

    public string CodMotivo { get; set; }

    public string TipoEntrada { get; set; }

    public string DataApre { get; set; }

    public string HoraApre { get; set; }

    public string NumeroAutoRetirada { get; set; }

    public string Matricula { get; set; }

    public string JsonEnvio { get; set; }

    public string JsonRetorno { get; set; }

    public DateTime DataCadastro { get; set; }

    public string Status { get; set; }

    public int? CodigoRetorno { get; set; }

    public string MensagemRetorno { get; set; }

    public string Observacao { get; set; }
}
