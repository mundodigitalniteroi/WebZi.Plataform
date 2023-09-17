using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsLeilao;

public partial class TbLeiloeiro
{
    public int Id { get; set; }

    public string Nome { get; set; }

    public DateTime DataHoraCadastro { get; set; }

    public int? Comissao { get; set; }

    public string OrgaoVinculado { get; set; }

    public int? IdUsuarioCadastro { get; set; }

    public string TipoDocumento { get; set; }

    public string Documento { get; set; }

    public virtual ICollection<TbLeilao> TbLeilaos { get; set; } = new List<TbLeilao>();
}
