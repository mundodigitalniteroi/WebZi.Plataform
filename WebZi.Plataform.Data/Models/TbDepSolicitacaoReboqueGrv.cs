using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepSolicitacaoReboqueGrv
{
    public int IdSolicitacaoReboqueGrv { get; set; }

    public int IdSolicitacaoReboque { get; set; }

    public int? IdAutoridadeResponsavel { get; set; }

    public string MatriculaAutoridadeResponsavel { get; set; }

    public string NomeAutoridadeResponsavel { get; set; }

    public virtual TbDepAutoridadesResponsavei IdAutoridadeResponsavelNavigation { get; set; }

    public virtual TbDepSolicitacaoReboque IdSolicitacaoReboqueNavigation { get; set; }
}
