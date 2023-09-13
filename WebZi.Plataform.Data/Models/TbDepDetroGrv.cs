using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepDetroGrv
{
    public int IdDetroGrv { get; set; }

    public int IdGrv { get; set; }

    public int IdUsuarioCadastro { get; set; }

    public int? IdUsuarioAlteracao { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    /// <summary>
    /// A = AUTORIZADOS;
    /// B = BLOQUEIO AUTOMÁTICO (Valor Padrão);
    /// N = NÃO AUTORIZADO.
    /// </summary>
    public string Status { get; set; }

    public virtual TbDepGrv IdGrvNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioAlteracaoNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioCadastroNavigation { get; set; }

    public virtual ICollection<TbDepDetroGrvMotivoNaoAutorizado> TbDepDetroGrvMotivoNaoAutorizados { get; set; } = new List<TbDepDetroGrvMotivoNaoAutorizado>();
}
