using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsGlobal;

public partial class TbGovListaServico
{
    public int ListaServicoId { get; set; }

    public string ItemLista { get; set; }

    public string Descricao { get; set; }

    public decimal AliquotaIss { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public virtual ICollection<TbGovCnaeListaServico> TbGovCnaeListaServicos { get; set; } = new List<TbGovCnaeListaServico>();
}
