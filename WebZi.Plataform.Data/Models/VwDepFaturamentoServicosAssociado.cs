using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwDepFaturamentoServicosAssociado
{
    public int IdCliente { get; set; }

    public short ClienteIdAgenciaBancaria { get; set; }

    public byte? ClienteIdTipoMeioCobranca { get; set; }

    public int? ClienteIdEmpresa { get; set; }

    public string ClienteNome { get; set; }

    public string ClienteCnpj { get; set; }

    public decimal? ClienteMetragemTotal { get; set; }

    public decimal? ClienteMetragemGuarda { get; set; }

    public string ClienteCodigoSap { get; set; }

    public string ClienteHoraDiaria { get; set; }

    public short ClienteMaximoDiariasParaCobranca { get; set; }

    public short ClienteMaximoDiasVencimento { get; set; }

    public string ClienteFlagUsarHoraDiaria { get; set; }

    public string ClienteFlagEmissaoNotaFiscalSap { get; set; }

    public string ClienteFlagCobrarDiariasDiasCorridos { get; set; }

    public string ClienteFlagClienteRealizaFaturamentoArrecadacao { get; set; }

    public string ClienteFlagAtivo { get; set; }

    public int IdDeposito { get; set; }

    public string DepositoNome { get; set; }

    public byte GrvMinimoFotosExigidas { get; set; }

    public byte GrvLimiteMinimoDatahoraGuarda { get; set; }

    public string DepositoFlagAtivo { get; set; }

    public byte? IdTipoMeioCobranca { get; set; }

    public string TiposMeiosCobrancasCodigoSap { get; set; }

    public string TiposMeiosCobrancasDescricao { get; set; }

    public string TiposMeiosCobrancasFlagBanco { get; set; }

    public string TiposMeiosCobrancasFlagPossuiCodigoAutorizacaoCartao { get; set; }

    public int IdFaturamentoServicoTipo { get; set; }

    public int IdSapTipoComposicao { get; set; }

    public string ServicoDescricao { get; set; }

    public string CodigoMaterial { get; set; }

    public string SapDescricao { get; set; }

    public string SapCodigoDescricao { get; set; }

    public byte? IdSapCondicaoPagamento { get; set; }

    public string SapCondicaoPagamentoCodigo { get; set; }

    public string SapCondicaoPagamentoDescricao { get; set; }

    public string TipoCobranca { get; set; }

    public byte OrdemImpressao { get; set; }

    public string FlagServicoObrigatorioGlobal { get; set; }

    public string FlagCobrarTelaGrv { get; set; }

    public string FlagNaoCobrarSeNaoUsouReboque { get; set; }

    public string FlagRebocada { get; set; }

    public string FlagImpressaoAgrupada { get; set; }

    public string FlagTributacao { get; set; }

    public string FaturamentoProdutoCodigo { get; set; }

    public string FaturamentoProdutoDescricao { get; set; }

    public int IdFaturamentoServicoAssociado { get; set; }

    public short? IdFaturamentoRegra { get; set; }

    public string Descricao { get; set; }

    public decimal PrecoPadrao { get; set; }

    public decimal PrecoValorMinimo { get; set; }

    public DateTime DataVigenciaInicial { get; set; }

    public DateTime? DataVigenciaFinal { get; set; }

    public string FormaCobranca { get; set; }

    public string FlagServicoObrigatorio { get; set; }

    public string FlagPermiteAlteracaoValor { get; set; }

    public string FlagPermiteDesconto { get; set; }

    public string FlagCobrarSomentePrimeiraFatura { get; set; }

    public short? IdFaturamentoRegraTipo { get; set; }

    public string FaturamentoRegraTipoCodigo { get; set; }

    public string FaturamentoRegraTipoDescricao { get; set; }

    public string FaturamentoRegraTipoFlagPossuiValor { get; set; }

    public string FaturamentoRegraTipoFlagAtivo { get; set; }

    public string NomeUsuarioCadastro { get; set; }

    public DateTime DataCadastro { get; set; }

    public string NomeUsuarioAlteracao { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public int? CnaeId { get; set; }

    public string CnaeCodigo { get; set; }

    public string CnaeCodigoFormatado { get; set; }

    public string CnaeDescricao { get; set; }

    public int? ListaServicoId { get; set; }

    public string ListaServicoItemLista { get; set; }

    public string ListaServicoDescricao { get; set; }

    public decimal? ListaServicoAliquotaIss { get; set; }

    public string DescricaoConfiguracaoNfe { get; set; }

    public string FlagEnviarValorIss { get; set; }

    public string FlagEnviarInscricaoEstadual { get; set; }
}
