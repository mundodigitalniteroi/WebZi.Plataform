using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepConsultaVeiculoAl
{
    public int Id { get; set; }

    public int IdGrv { get; set; }

    public string JsonEnvio { get; set; }

    public string JsonRetorno { get; set; }

    public DateTime DataCadastro { get; set; }

    public string Placa { get; set; }

    public string Chassi { get; set; }
}
