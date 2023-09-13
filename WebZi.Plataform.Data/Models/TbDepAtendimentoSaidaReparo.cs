using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepAtendimentoSaidaReparo
{
    public int Id { get; set; }

    public int AtendimentoId { get; set; }

    public DateTime DataSaida { get; set; }

    public DateTime DataPrevisaoRetorno { get; set; }

    public string MotivoSaida { get; set; }

    public virtual TbDepAtendimento Atendimento { get; set; }
}
