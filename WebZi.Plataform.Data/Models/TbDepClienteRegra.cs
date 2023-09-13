using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepClienteRegra
{
    public short ClienteRegraId { get; set; }

    public short ClienteRegraTipoId { get; set; }

    public int? ClienteId { get; set; }

    public int UsuarioCadastroId { get; set; }

    public int? UsuarioAlteracaoId { get; set; }

    public string Valor { get; set; }

    public DateTime DataVigenciaInicial { get; set; }

    public DateTime? DataVigenciaFinal { get; set; }

    public virtual TbDepCliente Cliente { get; set; }

    public virtual TbDepClienteRegrasTipo ClienteRegraTipo { get; set; }

    public virtual TbDepUsuario UsuarioAlteracao { get; set; }

    public virtual TbDepUsuario UsuarioCadastro { get; set; }
}
