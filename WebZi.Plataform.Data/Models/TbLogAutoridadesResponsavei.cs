using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbLogAutoridadesResponsavei
{
    public long Id { get; set; }

    public int? IdAutoridadeResponsavel { get; set; }

    public short? IdOrgaoEmissor { get; set; }

    public int? IdUsuarioCadastro { get; set; }

    public int? IdUsuarioAlteracao { get; set; }

    public string Divisao { get; set; }

    public DateTime? DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public string FlagAtivo { get; set; }
}
