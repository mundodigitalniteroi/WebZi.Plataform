using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepNfeConfiguracaoImagem
{
    public short ConfiguracaoImagemId { get; set; }

    public int ClienteDepositoId { get; set; }

    public int UsuarioCadastroId { get; set; }

    public int? UsuarioAlteracaoId { get; set; }

    public short ValueX { get; set; }

    public short ValueY { get; set; }

    public short Width { get; set; }

    public short Height { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public virtual TbDepClientesDeposito ClienteDeposito { get; set; }

    public virtual TbDepUsuario UsuarioAlteracao { get; set; }

    public virtual TbDepUsuario UsuarioCadastro { get; set; }
}
