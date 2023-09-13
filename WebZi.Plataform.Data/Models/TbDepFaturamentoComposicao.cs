using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepFaturamentoComposicao
{
    public int IdFaturamentoComposicao { get; set; }

    public int IdFaturamento { get; set; }

    public int? IdFaturamentoServicoTipoVeiculo { get; set; }

    public byte? IdFaturamentoTipoComposicao { get; set; }

    public int? IdUsuarioDesconto { get; set; }

    public string IdDocumentoSap { get; set; }

    public string TipoLancamento { get; set; }

    /// <summary>
    /// TIPOS DE COBRANÇA:
    /// D = Diárias;
    /// H = Quantidade de HH:MM vezes o Preço;
    /// P = Porcentagem;
    /// Q = Quantidade;
    /// T = Tempo entre duas Datas;
    /// V = Valor.
    /// </summary>
    public string TipoComposicao { get; set; }

    public decimal ValorTipoComposicao { get; set; }

    public decimal? QuantidadeComposicao { get; set; }

    public decimal ValorComposicao { get; set; }

    public string TipoDesconto { get; set; }

    public int? QuantidadeDesconto { get; set; }

    public decimal? ValorDesconto { get; set; }

    public string ObservacaoDesconto { get; set; }

    public decimal ValorFaturado { get; set; }

    public int? IdUsuarioAlteracaoQuantidade { get; set; }

    public decimal? QuantidadeAlterada { get; set; }

    public string ObservacaoQuantidadeAlterada { get; set; }

    public virtual TbDepFaturamento IdFaturamentoNavigation { get; set; }

    public virtual TbDepFaturamentoServicosTipoVeiculo IdFaturamentoServicoTipoVeiculoNavigation { get; set; }

    public virtual TbDepFaturamentoTipoComposicao IdFaturamentoTipoComposicaoNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioAlteracaoQuantidadeNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioDescontoNavigation { get; set; }

    public virtual ICollection<TbDepFaturamentoComposicaoNf> TbDepFaturamentoComposicaoNfs { get; set; } = new List<TbDepFaturamentoComposicaoNf>();

    public virtual ICollection<TbDepNfeFaturamentoComposicao> TbDepNfeFaturamentoComposicaos { get; set; } = new List<TbDepNfeFaturamentoComposicao>();
}
