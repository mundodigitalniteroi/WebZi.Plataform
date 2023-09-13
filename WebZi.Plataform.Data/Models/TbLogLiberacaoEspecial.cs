using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbLogLiberacaoEspecial
{
    public long Id { get; set; }

    public int? IdLiberacaoEspecial { get; set; }

    public int? IdGrv { get; set; }

    public int? IdFaturamento { get; set; }

    public byte? IdLiberacaoEspecialTipo { get; set; }

    public int? IdUsuarioCadastro { get; set; }

    public string NumeroDocumento { get; set; }

    public string TipoDocumento { get; set; }

    public string NumeroProcesso { get; set; }

    public string OrgaoEmissor { get; set; }

    public string PortadorNome { get; set; }

    public string PortadorCargo { get; set; }

    public string PortadorMatricula { get; set; }

    public string SignatarioNomeDocumento { get; set; }

    public string SignatarioMatricula { get; set; }

    public string SignatarioTitulo { get; set; }

    public DateTime? DataEmissaoDocumento { get; set; }

    public DateTime? DataLiberacao { get; set; }
}
