using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepSapTipoComposicao
{
    public int IdSapTipoComposicao { get; set; }

    public byte IdSapTipoComposicaoGrupos { get; set; }

    public int IdUsuarioCadastro { get; set; }

    public int? IdUsuarioAlteracao { get; set; }

    public string CodigoMaterial { get; set; }

    public string TipoDocumentoVenda { get; set; }

    public string Descricao { get; set; }

    public string DescricaoNotaFiscal { get; set; }

    public string Observacao { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public string FlagAgrupamento { get; set; }

    public virtual TbDepSapTipoComposicaoGrupo IdSapTipoComposicaoGruposNavigation { get; set; }

    public virtual ICollection<TbDepFaturamentoServicosAssociado> TbDepFaturamentoServicosAssociados { get; set; } = new List<TbDepFaturamentoServicosAssociado>();
}
