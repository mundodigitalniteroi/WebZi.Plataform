using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepUsuariosPermisso
{
    public int IdUsuarioPermissao { get; set; }

    public short IdTipoPermissao { get; set; }

    public int IdUsuario { get; set; }

    public int IdUsuarioCadastro { get; set; }

    public int? IdUsuarioAlteracao { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public string FlagAtivo { get; set; }

    public virtual TbDepUsuariosTiposPermisso IdTipoPermissaoNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioAlteracaoNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioCadastroNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioNavigation { get; set; }
}
