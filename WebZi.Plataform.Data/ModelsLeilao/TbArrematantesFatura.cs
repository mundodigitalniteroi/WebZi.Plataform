using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsLeilao;

public partial class TbArrematantesFatura
{
    public int Id { get; set; }

    public int? IdLeilao { get; set; }

    public string Codigo { get; set; }

    public string Token { get; set; }

    public DateTime? DataCadastro { get; set; }

    public DateTime? DataVencimento { get; set; }

    public DateTime? DataExpiracao { get; set; }

    public int? IdFormaPagamento { get; set; }

    public DateTime? DataPagamento { get; set; }

    public int? IdStatus { get; set; }

    public decimal? JurosPerc { get; set; }

    public decimal? MultaPerc { get; set; }

    public decimal? ValorDesconto { get; set; }

    public decimal? ValorJurosPago { get; set; }

    public decimal? ValorMultaPago { get; set; }

    public decimal? ValorTotalPago { get; set; }

    public decimal? ValorTaxaPaga { get; set; }

    public int? IdConfiguracaoConta { get; set; }

    public int? IdEmpresa { get; set; }

    public virtual TbFaturaFormaPagamento IdFormaPagamentoNavigation { get; set; }

    public virtual TbLeilao IdLeilaoNavigation { get; set; }

    public virtual TbFaturaStatus IdStatusNavigation { get; set; }

    public virtual ICollection<TbArrematantesFaturaItem> TbArrematantesFaturaItems { get; set; } = new List<TbArrematantesFaturaItem>();

    public virtual ICollection<TbFaturaComposicaoCobranca> TbFaturaComposicaoCobrancas { get; set; } = new List<TbFaturaComposicaoCobranca>();

    public virtual ICollection<TbNotificacaoFatura> TbNotificacaoFaturas { get; set; } = new List<TbNotificacaoFatura>();
}
