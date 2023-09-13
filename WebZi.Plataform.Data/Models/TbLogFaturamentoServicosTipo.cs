using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbLogFaturamentoServicosTipo
{
    public long Id { get; set; }

    public int? IdFaturamentoServicoTipo { get; set; }

    public int? IdUsuarioCadastro { get; set; }

    public int? IdUsuarioAlteracao { get; set; }

    public string Descricao { get; set; }

    public string TipoCobranca { get; set; }

    public string FaturamentoProdutoCodigo { get; set; }

    public byte? OrdemImpressao { get; set; }

    public DateTime? DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public string FlagCobrarTelaGrv { get; set; }

    public string FlagNaoCobrarSeNaoUsouReboque { get; set; }

    public string FlagServicoObrigatorio { get; set; }

    public string FlagImpressaoAgrupada { get; set; }

    public string FlagRebocada { get; set; }

    public string FlagTributacao { get; set; }

    public string FlagCobrancaPorHora { get; set; }

    public string FlagAtivo { get; set; }

    public DateTime DatahoraLog { get; set; }
}
