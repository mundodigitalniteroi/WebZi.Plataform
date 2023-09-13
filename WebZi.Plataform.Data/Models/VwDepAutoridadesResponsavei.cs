using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwDepAutoridadesResponsavei
{
    public int IdAutoridadeResponsavel { get; set; }

    public string AutoridadesResponsaveisDivisao { get; set; }

    public string AutoridadesResponsaveisFlagAtivo { get; set; }

    public short IdOrgaoEmissor { get; set; }

    public string OrgaoEmissorSigla { get; set; }

    public string OrgaoEmissorDescricao { get; set; }

    public string OrgaoEmissorUf { get; set; }

    public string OrgaoEmissorFlagAtivo { get; set; }
}
