using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepFaturamento
{
    public int IdFaturamento { get; set; }

    public int IdAtendimento { get; set; }

    public byte IdTipoMeioCobranca { get; set; }

    public int IdUsuarioCadastro { get; set; }

    public int? IdUsuarioAlteracao { get; set; }

    public string NumeroIdentificacao { get; set; }

    public decimal ValorFaturado { get; set; }

    public decimal? ValorPagamento { get; set; }

    public string HoraDiaria { get; set; }

    public short MaximoDiariasParaCobranca { get; set; }

    public short MaximoDiasVencimento { get; set; }

    public byte Sequencia { get; set; }

    public string NumeroNotaFiscal { get; set; }

    public DateTime? DataCalculo { get; set; }

    public DateTime? DataRetroativa { get; set; }

    public DateTime DataVencimento { get; set; }

    public DateTime? DataPrazoRetiradaVeiculo { get; set; }

    public DateTime? DataPagamento { get; set; }

    public DateTime? DataEmissaoDocumento { get; set; }

    public DateTime? DataEmissaoNotaFiscal { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public string Status { get; set; }

    public string FlagUsarHoraDiaria { get; set; }

    public string FlagLimitacaoJudicial { get; set; }

    public string FlagClienteRealizaFaturamentoArrecadacao { get; set; }

    public string FlagCobrarDiariasDiasCorridos { get; set; }

    public string FlagPermissaoDataRetroativaFaturamento { get; set; }

    public virtual TbDepAtendimento IdAtendimentoNavigation { get; set; }

    public virtual TbDepTiposMeiosCobranca IdTipoMeioCobrancaNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioAlteracaoNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioCadastroNavigation { get; set; }

    public virtual ICollection<TbDepFaturamentoBoleto> TbDepFaturamentoBoletos { get; set; } = new List<TbDepFaturamentoBoleto>();

    public virtual ICollection<TbDepFaturamentoCodigoAutorizacaoCartao> TbDepFaturamentoCodigoAutorizacaoCartaos { get; set; } = new List<TbDepFaturamentoCodigoAutorizacaoCartao>();

    public virtual ICollection<TbDepFaturamentoComposicao> TbDepFaturamentoComposicaos { get; set; } = new List<TbDepFaturamentoComposicao>();

    public virtual ICollection<TbDepLiberacaoEspecial> TbDepLiberacaoEspecials { get; set; } = new List<TbDepLiberacaoEspecial>();

    public virtual ICollection<TbDepPixDinamicoSenhaConfirmacaoTranferencium> TbDepPixDinamicoSenhaConfirmacaoTranferencia { get; set; } = new List<TbDepPixDinamicoSenhaConfirmacaoTranferencium>();

    public virtual ICollection<TbDepPixDinamico> TbDepPixDinamicos { get; set; } = new List<TbDepPixDinamico>();

    public virtual ICollection<TbDepPix> TbDepPixes { get; set; } = new List<TbDepPix>();
}
