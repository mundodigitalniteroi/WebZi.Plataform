using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwDepNfeRegra
{
    public short NfeRegraId { get; set; }

    public short NfeRegraTipoId { get; set; }

    public int ClienteId { get; set; }

    public int DepositoId { get; set; }

    public int ClienteDepositoId { get; set; }

    public int UsuarioCadastroId { get; set; }

    public int? UsuarioAlteracaoId { get; set; }

    public string Valor { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public bool Ativo { get; set; }

    public string RegraCodigo { get; set; }

    public string RegraDescricao { get; set; }

    public bool RegraPossuiValor { get; set; }

    public bool RegraAtivo { get; set; }

    public string Cliente { get; set; }

    public string Deposito { get; set; }

    public string UsuarioCadastro { get; set; }

    public string UsuarioAlteracao { get; set; }
}
