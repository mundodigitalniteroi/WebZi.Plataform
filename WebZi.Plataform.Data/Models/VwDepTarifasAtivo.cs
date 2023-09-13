using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwDepTarifasAtivo
{
    public short IdCliente { get; set; }

    public string ClienteNome { get; set; }

    public string ClienteHoraDiaria { get; set; }

    public short ClienteMaximoDiariasParaCobranca { get; set; }

    public short ClienteMaximoDiasVencimento { get; set; }

    public string ClienteFlagUsarHoraDiaria { get; set; }

    public string ClienteFlagEmissaoNotaFiscalSap { get; set; }

    public string ClienteFlagCadastrarQuilometragem { get; set; }

    public string ClienteFlagCobrarDiariasDiasCorridos { get; set; }

    public string ClienteFlagAtivo { get; set; }

    public int IdClienteDeposito { get; set; }

    public string ClienteDepositoFlagAtivo { get; set; }

    public short IdDeposito { get; set; }

    public string DepositoNome { get; set; }

    public string DepositoFlagAtivo { get; set; }

    public int IdTarifa { get; set; }

    public string TarifaDescricao { get; set; }

    public decimal TarifaPrecoDiaria { get; set; }

    public decimal TarifaPrecoRebocada { get; set; }

    public decimal TarifaPrecoQuilometragem { get; set; }

    public DateTime DataVigenciaInicial { get; set; }

    public DateTime? DataVigenciaFinal { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public int IdUsuarioCadastro { get; set; }

    public string NomeUsuarioCadastro { get; set; }

    public int? IdUsuarioAlteracao { get; set; }

    public string NomeUsuarioAlteracao { get; set; }
}
