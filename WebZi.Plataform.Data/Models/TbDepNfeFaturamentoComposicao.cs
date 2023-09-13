using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepNfeFaturamentoComposicao
{
    public int NfeFaturamentoComposicaoId { get; set; }

    public int NfeId { get; set; }

    public int FaturamentoComposicaoId { get; set; }

    /// <summary>
    /// P = PENDENTE DE CADASTRO;
    /// F = CADASTRADO FINALIZADO;
    /// E = ERRO NO CADASTRO.
    /// </summary>
    public string StatusCadastroErp { get; set; }

    public virtual TbDepFaturamentoComposicao FaturamentoComposicao { get; set; }

    public virtual TbDepNfe Nfe { get; set; }
}
