using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbLogFuncionario
{
    public long Id { get; set; }

    public int? IdFuncionario { get; set; }

    public long? IdPessoa { get; set; }

    public int? IdEmpresa { get; set; }

    public short? IdDepartamento { get; set; }

    public short? IdCargo { get; set; }

    public int? IdUsuarioCadastro { get; set; }

    public int? IdUsuarioAlteracao { get; set; }

    public string Matricula { get; set; }

    public DateTime? DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public string FlagAtivo { get; set; }
}
