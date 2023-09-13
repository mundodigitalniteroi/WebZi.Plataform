using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwTransacoesDetranRjDadosProprietario
{
    public int IdGrv { get; set; }

    public string NumeroFormularioGrv { get; set; }

    public string Placa { get; set; }

    public string Chassi { get; set; }

    public string Login { get; set; }

    public string Operador { get; set; }

    public string Uf { get; set; }

    public string IdStatusOperacao { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataGuarda { get; set; }
}
