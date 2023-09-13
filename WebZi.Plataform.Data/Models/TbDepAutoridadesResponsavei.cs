using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepAutoridadesResponsavei
{
    public int IdAutoridadeResponsavel { get; set; }

    public short IdOrgaoEmissor { get; set; }

    public int IdUsuarioCadastro { get; set; }

    public int? IdUsuarioAlteracao { get; set; }

    public string Divisao { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public string FlagAtivo { get; set; }

    public int? IdExterno { get; set; }

    public virtual TbDepUsuario IdUsuarioAlteracaoNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioCadastroNavigation { get; set; }

    public virtual ICollection<TbDepAgente> TbDepAgentes { get; set; } = new List<TbDepAgente>();

    public virtual ICollection<TbDepGrv> TbDepGrvs { get; set; } = new List<TbDepGrv>();

    public virtual ICollection<TbDepSolicitacaoReboqueGrv> TbDepSolicitacaoReboqueGrvs { get; set; } = new List<TbDepSolicitacaoReboqueGrv>();
}
