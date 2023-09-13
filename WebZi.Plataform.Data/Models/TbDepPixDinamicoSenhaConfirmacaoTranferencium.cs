using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepPixDinamicoSenhaConfirmacaoTranferencium
{
    public int PixDinamicoSenhaConfirmacaoTranferenciaId { get; set; }

    public int FaturamentoId { get; set; }

    public int UsuarioCadastroId { get; set; }

    public int? UsuarioFinanceiroId { get; set; }

    public string Senha { get; set; }

    public string SenhaFinanceiro { get; set; }

    public string FlagConfirmado { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public DateTime? DataHoraAutorizacaoFinanceiro { get; set; }

    public virtual TbDepFaturamento Faturamento { get; set; }

    public virtual TbDepUsuario UsuarioCadastro { get; set; }

    public virtual TbDepUsuario UsuarioFinanceiro { get; set; }
}
