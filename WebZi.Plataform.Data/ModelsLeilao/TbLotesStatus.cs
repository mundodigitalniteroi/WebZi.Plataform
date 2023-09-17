using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsLeilao;

public partial class TbLotesStatus
{
    public int Id { get; set; }

    public string Descricao { get; set; }

    public string ValidaLote { get; set; }

    public string CorrelacaoDsin { get; set; }

    public string Reaproveitavel { get; set; }

    public string Ativo { get; set; }

    public string PermiteAlteracao { get; set; }

    public int Codigo { get; set; }

    public int CodigoGrupo { get; set; }

    public int? IdLeiloado { get; set; }

    public int? IdNaoLeiloado { get; set; }

    public int? PrefixoLote { get; set; }

    public int? IdReaproveitavel { get; set; }

    public virtual TbLotesStatusGrupo CodigoGrupoNavigation { get; set; }

    public virtual TbLotesStatus IdLeiloadoNavigation { get; set; }

    public virtual TbLotesStatus IdNaoLeiloadoNavigation { get; set; }

    public virtual TbLotesStatus IdReaproveitavelNavigation { get; set; }

    public virtual ICollection<TbLotesStatus> InverseIdLeiloadoNavigation { get; set; } = new List<TbLotesStatus>();

    public virtual ICollection<TbLotesStatus> InverseIdNaoLeiloadoNavigation { get; set; } = new List<TbLotesStatus>();

    public virtual ICollection<TbLotesStatus> InverseIdReaproveitavelNavigation { get; set; } = new List<TbLotesStatus>();

    public virtual ICollection<TbLeilaoLote> TbLeilaoLotes { get; set; } = new List<TbLeilaoLote>();
}
