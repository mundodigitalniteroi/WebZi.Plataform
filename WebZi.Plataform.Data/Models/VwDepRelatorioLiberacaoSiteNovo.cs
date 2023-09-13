using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwDepRelatorioLiberacaoSiteNovo
{
    public int IdGrv { get; set; }

    public string NumeroFormularioGrv { get; set; }

    public string Placa { get; set; }

    public string Chassi { get; set; }

    public string MarcaModelo { get; set; }

    public string TipoVeiculo { get; set; }

    public string Divisao { get; set; }

    public DateTime? DataHoraRemocao { get; set; }

    public DateTime? DataHoraGuarda { get; set; }

    public DateTime DataLiberacao { get; set; }

    public string FlagComboio { get; set; }

    public int IdCliente { get; set; }

    public int IdDeposito { get; set; }

    public string LiberacaoTipoDescricao { get; set; }

    public decimal ValorFaturado { get; set; }

    public decimal ValorPagamento { get; set; }

    public DateTime DataVencimento { get; set; }

    public int IdAutoridadeResponsavel { get; set; }

    public string FormaLiberacaoCpf { get; set; }

    public string Status { get; set; }

    public string NomeNotafiscal { get; set; }

    public string Renavam { get; set; }

    public string ProprietarioDocumento { get; set; }

    public string IdDocumentoSap { get; set; }

    public string IdStatusOperacao { get; set; }

    public string ClassificacaoNome { get; set; }

    public string Nome { get; set; }

    public string Frota { get; set; }

    public DateTime? DataTransbordo { get; set; }

    public string FlagTransbordo { get; set; }

    public string FlagPagamentoFinanciado { get; set; }

    public string BoletoCdx { get; set; }
}
