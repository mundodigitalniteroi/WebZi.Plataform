using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepFaturamentoServicosTipo
{
    public int IdFaturamentoServicoTipo { get; set; }

    public int IdUsuarioCadastro { get; set; }

    public int? IdUsuarioAlteracao { get; set; }

    public string Descricao { get; set; }

    /// <summary>
    /// TIPOS DE COBRANÇA:
    /// D = Diárias;
    /// H = Quantidade de HH:MM vezes o Preço;
    /// P = Porcentagem;
    /// Q = Quantidade;
    /// T = Tempo entre duas Datas;
    /// V = Valor.
    /// </summary>
    public string TipoCobranca { get; set; }

    public string FaturamentoProdutoCodigo { get; set; }

    public byte OrdemImpressao { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public string FlagCobrarTelaGrv { get; set; }

    public string FlagNaoCobrarSeNaoUsouReboque { get; set; }

    public string FlagServicoObrigatorio { get; set; }

    public string FlagRebocada { get; set; }

    public string FlagImpressaoAgrupada { get; set; }

    public string FlagTributacao { get; set; }

    public string FlagCobrancaPorHora { get; set; }

    public string FlagAtivo { get; set; }

    public virtual TbDepFaturamentoProduto FaturamentoProdutoCodigoNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioAlteracaoNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioCadastroNavigation { get; set; }

    public virtual ICollection<TbDepFaturamentoServicosAssociado> TbDepFaturamentoServicosAssociados { get; set; } = new List<TbDepFaturamentoServicosAssociado>();
}
