﻿using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwDepRepGuiaPagamentoEstadiaReboqueSite
{
    public int IdGrv { get; set; }

    public int IdAtendimento { get; set; }

    public string GrvPlaca { get; set; }

    public string GrvChassi { get; set; }

    public string GrvRenavam { get; set; }

    public string GrvNumeroFormulario { get; set; }

    public DateTime? GrvDataHoraRemocao { get; set; }

    public DateTime? GrvDataHoraGuarda { get; set; }

    public string GrvEstacionamentoSetor { get; set; }

    public string GrvEstacionamentoNumeroVaga { get; set; }

    public string GrvNumeroChave { get; set; }

    public string ReboquistaNome { get; set; }

    public string ReboquePlaca { get; set; }

    public string MarcaModelo { get; set; }

    public string Cor { get; set; }

    public string TipoVeiculo { get; set; }

    public string QualificacaoResponsavelDescricao { get; set; }

    public int IdTarifaTipoVeiculo { get; set; }

    public int IdTarifa { get; set; }

    public string TarifasDescricao { get; set; }

    public string AtendimentoResponsavelNome { get; set; }

    public string AtendimentoResponsavelDocumento { get; set; }

    public string AtendimentoFormaLiberacao { get; set; }

    public string AtendimentoFormaLiberacaoNome { get; set; }

    public string AtendimentoFormaLiberacaoCnh { get; set; }

    public string AtendimentoFormaLiberacaoCpf { get; set; }

    public string AtendimentoFormaLiberacaoPlaca { get; set; }

    public string ClienteNome { get; set; }

    public string ClienteCnpj { get; set; }

    public string ClienteTipoLogradouro { get; set; }

    public string ClienteLogradouro { get; set; }

    public string ClienteNumeroLogradouro { get; set; }

    public string ClienteComplementoLogradouro { get; set; }

    public string ClienteBairro { get; set; }

    public string ClienteMunicipio { get; set; }

    public string ClienteEstado { get; set; }

    public string ClienteUf { get; set; }

    public string ClienteCep { get; set; }

    public string ClienteBanco { get; set; }

    public string CodigoAgencia { get; set; }

    public string ContaCorrente { get; set; }

    public string DepositoDescricao { get; set; }

    public string DepositoTipoLogradouro { get; set; }

    public string DepositoLogradouro { get; set; }

    public string DepositoNumeroLogradouro { get; set; }

    public string DepositoComplementoLogradouro { get; set; }

    public string DepositoBairro { get; set; }

    public string DepositoMunicipio { get; set; }

    public string DepositoEstado { get; set; }

    public string DepositoUf { get; set; }

    public string DepositoCep { get; set; }

    public DateTime DataHoraAtual { get; set; }

    public int IdCliente { get; set; }

    public int IdDeposito { get; set; }
}
