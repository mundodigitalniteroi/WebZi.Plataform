using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsGlobal;

public partial class TbGloLocEstado
{
    public string Uf { get; set; }

    public string PaisNumcode { get; set; }

    public string Regiao { get; set; }

    public string Nome { get; set; }

    public string NomePtbr { get; set; }

    public string Capital { get; set; }

    public byte IdUtc { get; set; }

    public byte? IdUtcVerao { get; set; }

    public byte EstadoId { get; set; }

    public virtual TbGloLocPaise PaisNumcodeNavigation { get; set; }

    public virtual TbGloLocRegio RegiaoNavigation { get; set; }

    public virtual ICollection<TbGloDocOrgaosEmissore> TbGloDocOrgaosEmissores { get; set; } = new List<TbGloDocOrgaosEmissore>();

    public virtual ICollection<TbGloLocFeriado> TbGloLocFeriados { get; set; } = new List<TbGloLocFeriado>();

    public virtual ICollection<TbGloLocMunicipio> TbGloLocMunicipios { get; set; } = new List<TbGloLocMunicipio>();
}
