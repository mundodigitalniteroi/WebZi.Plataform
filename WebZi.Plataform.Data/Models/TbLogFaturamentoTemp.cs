using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbLogFaturamentoTemp
{
    public long Id { get; set; }

    public int? IdFaturamento { get; set; }

    public int? IdAtendimento { get; set; }

    public byte? IdTipoMeioCobranca { get; set; }

    public int? IdUsuarioCadastro { get; set; }

    public int? IdUsuarioAlteracao { get; set; }

    public string NumeroIdentificacao { get; set; }

    public decimal? ValorFaturado { get; set; }

    public decimal? ValorPagamento { get; set; }

    public string HoraDiaria { get; set; }

    public short? MaximoDiariasParaCobranca { get; set; }

    public short? MaximoDiasVencimento { get; set; }

    public byte? Sequencia { get; set; }

    public string NumeroNotaFiscal { get; set; }

    public DateTime? DataCalculo { get; set; }

    public DateTime? DataRetroativa { get; set; }

    public DateTime? DataVencimento { get; set; }

    public DateTime? DataPrazoRetiradaVeiculo { get; set; }

    public DateTime? DataPagamento { get; set; }

    public DateTime? DataEmissaoDocumento { get; set; }

    public DateTime? DataEmissaoNotaFiscal { get; set; }

    public DateTime? DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public string Status { get; set; }

    public string FlagUsarHoraDiaria { get; set; }

    public string FlagLimitacaoJudicial { get; set; }

    public string FlagClienteRealizaFaturamentoArrecadacao { get; set; }

    public string FlagCobrarDiariasDiasCorridos { get; set; }

    public string FlagPermissaoDataRetroativaFaturamento { get; set; }

    public DateTime DatahoraLog { get; set; }
}
