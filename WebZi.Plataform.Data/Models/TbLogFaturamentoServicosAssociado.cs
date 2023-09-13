using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbLogFaturamentoServicosAssociado
{
    public long Id { get; set; }

    public int IdUsuarioCrud { get; set; }

    public string Crud { get; set; }

    public DateTime DatahoraLog { get; set; }

    public int? IdFaturamentoServicoAssociado { get; set; }

    public int? IdCliente { get; set; }

    public int? IdDeposito { get; set; }

    public int? IdFaturamentoServicoTipo { get; set; }

    public int? IdSapTipoComposicao { get; set; }

    public short? IdFaturamentoRegra { get; set; }

    public int? IdUsuarioCadastro { get; set; }

    public int? IdUsuarioAlteracao { get; set; }

    public string Descricao { get; set; }

    public decimal? PrecoPadrao { get; set; }

    public decimal? PrecoValorMinimo { get; set; }

    public DateTime? DataVigenciaInicial { get; set; }

    public DateTime? DataVigenciaFinal { get; set; }

    public DateTime? DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public string FormaCobranca { get; set; }

    public string FlagServicoObrigatorio { get; set; }

    public string FlagPermiteAlteracaoValor { get; set; }

    public string FlagPermiteDesconto { get; set; }

    public string FlagCobrarSomentePrimeiraFatura { get; set; }

    public int? CnaeId { get; set; }

    public int? ListaServicoId { get; set; }

    public string DescricaoConfiguracaoNfe { get; set; }

    public string FlagEnviarValorIss { get; set; }

    public string FlagEnviarInscricaoEstadual { get; set; }
}
