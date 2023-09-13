using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepNfeRegra
{
    public short NfeRegraId { get; set; }

    public short NfeRegraTipoId { get; set; }

    public int ClienteDepositoId { get; set; }

    public int UsuarioCadastroId { get; set; }

    public int? UsuarioAlteracaoId { get; set; }

    public string Valor { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public bool? Ativo { get; set; }

    public virtual TbDepClientesDeposito ClienteDeposito { get; set; }

    public virtual TbDepNfeRegrasTipo NfeRegraTipo { get; set; }

    public virtual TbDepUsuario UsuarioAlteracao { get; set; }

    public virtual TbDepUsuario UsuarioCadastro { get; set; }
}
