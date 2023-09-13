using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepSistemaPerfilAcesso
{
    public int IdPerfilAcesso { get; set; }

    public int IdUsuarioCadastro { get; set; }

    public int? IdUsuarioAlteracao { get; set; }

    public string Descricao { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public string FlagAtivo { get; set; }

    public virtual TbDepUsuario IdUsuarioAlteracaoNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioCadastroNavigation { get; set; }

    public virtual ICollection<TbDepSistemaPerfilAcessoSubModulo> TbDepSistemaPerfilAcessoSubModulos { get; set; } = new List<TbDepSistemaPerfilAcessoSubModulo>();

    public virtual ICollection<TbDepSistemaPerfilAcessoUsuario> TbDepSistemaPerfilAcessoUsuarios { get; set; } = new List<TbDepSistemaPerfilAcessoUsuario>();
}
