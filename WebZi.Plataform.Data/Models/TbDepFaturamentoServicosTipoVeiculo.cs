using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepFaturamentoServicosTipoVeiculo
{
    public int IdFaturamentoServicoTipoVeiculo { get; set; }

    public int IdFaturamentoServicoAssociado { get; set; }

    public byte IdTipoVeiculo { get; set; }

    public virtual TbDepFaturamentoServicosAssociado IdFaturamentoServicoAssociadoNavigation { get; set; }

    public virtual TbDepTipoVeiculo IdTipoVeiculoNavigation { get; set; }

    public virtual ICollection<TbDepFaturamentoComposicao> TbDepFaturamentoComposicaos { get; set; } = new List<TbDepFaturamentoComposicao>();

    public virtual ICollection<TbDepFaturamentoServicosGrv> TbDepFaturamentoServicosGrvs { get; set; } = new List<TbDepFaturamentoServicosGrv>();
}
