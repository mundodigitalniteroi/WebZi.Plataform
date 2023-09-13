using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwTransacoesDetranBaDadosProprietario
{
    public int IdCliente { get; set; }

    public int IdDeposito { get; set; }

    public int IdGrv { get; set; }

    public string Placa { get; set; }

    public string Chassi { get; set; }

    public int IdUsuario { get; set; }

    public string NumeroFormularioGrv { get; set; }

    public string IdStatusOperacao { get; set; }

    public string CodigoRetExec { get; set; }

    public DateTime DataCadastro { get; set; }
}
