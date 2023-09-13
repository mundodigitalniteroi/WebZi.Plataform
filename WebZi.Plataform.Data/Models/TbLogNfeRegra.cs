using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbLogNfeRegra
{
    public long LogId { get; set; }

    public int UsuarioCrudId { get; set; }

    public string Crud { get; set; }

    public DateTime DatahoraLog { get; set; }

    public short NfeRegraId { get; set; }

    public short NfeRegraTipoId { get; set; }

    public int ClienteId { get; set; }

    public int DepositoId { get; set; }

    public int? EmpresaId { get; set; }

    public int UsuarioCadastroId { get; set; }

    public int? UsuarioAlteracaoId { get; set; }

    public string Valor { get; set; }

    public DateTime? DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }
}
