using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwDepClientesDeposito
{
    public int IdCliente { get; set; }

    public int IdDeposito { get; set; }

    public int IdClienteDeposito { get; set; }

    public string ClienteNome { get; set; }

    public string DepositoNome { get; set; }

    public short ClienteIdAgenciaBancaria { get; set; }

    public byte? ClienteIdTipoMeioCobranca { get; set; }

    public int? ClienteIdEmpresa { get; set; }

    public string ClienteCnpj { get; set; }

    public decimal? ClienteMetragemTotal { get; set; }

    public decimal? ClienteMetragemGuarda { get; set; }

    public string ClienteCodigoSap { get; set; }

    public string ClienteHoraDiaria { get; set; }

    public short ClienteMaximoDiariasParaCobranca { get; set; }

    public short ClienteMaximoDiasVencimento { get; set; }

    public string ClienteFlagUsarHoraDiaria { get; set; }

    public string ClienteFlagEmissaoNotaFiscalSap { get; set; }

    public string ClienteFlagCadastrarQuilometragem { get; set; }

    public string ClienteFlagCobrarDiariasDiasCorridos { get; set; }

    public string ClienteFlagLancarIpvaMultas { get; set; }

    public string ClienteFlagClienteRealizaFaturamentoArrecadacao { get; set; }

    public string ClienteFlagPermiteAlteracaoTipoVeiculo { get; set; }

    public string ClienteFlagAtivo { get; set; }

    public string ClienteDepositoFlagAtivo { get; set; }

    public string ClienteDepositoFlagUtilizaSistemaMobileGgv { get; set; }

    public string ClienteDepositoFlagCadastrarGrvBloqueado { get; set; }

    public byte GrvMinimoFotosExigidas { get; set; }

    public byte GrvLimiteMinimoDatahoraGuarda { get; set; }

    public string DepositoFlagAtivo { get; set; }

    public byte? IdTipoMeioCobranca { get; set; }

    public string TiposMeiosCobrancasCodigoSap { get; set; }

    public string TiposMeiosCobrancasDescricao { get; set; }

    public string TiposMeiosCobrancasAlias { get; set; }

    public string TiposMeiosCobrancasFlagBanco { get; set; }

    public string TiposMeiosCobrancasFlagPossuiCodigoAutorizacaoCartao { get; set; }

    public byte? IdSapCondicaoPagamento { get; set; }

    public string SapCondicaoPagamentoCodigo { get; set; }

    public string SapCondicaoPagamentoDescricao { get; set; }

    public string ClienteTipoLogradouro { get; set; }

    public string ClienteLogradouro { get; set; }

    public string ClienteBairro { get; set; }

    public string ClienteMunicipio { get; set; }

    public string ClienteEstado { get; set; }

    public string ClienteUf { get; set; }

    public string ClienteCep { get; set; }

    public string ClienteEnderecoCompleto { get; set; }

    public string DepositoTipoLogradouro { get; set; }

    public string DepositoLogradouro { get; set; }

    public string DepositoBairro { get; set; }

    public string DepositoMunicipio { get; set; }

    public string DepositoEstado { get; set; }

    public string DepositoUf { get; set; }

    public string DepositoCep { get; set; }

    public string DepositoEnderecoCompleto { get; set; }

    public string ClienteNomeUsuarioCadastro { get; set; }

    public DateTime ClienteDataCadastro { get; set; }

    public string ClienteNomeUsuarioAlteracao { get; set; }

    public DateTime? ClienteDataAlteracao { get; set; }
}
