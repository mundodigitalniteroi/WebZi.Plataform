using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepConfiguracoesTransacaoAcao
{
    public int IdConfigAcao { get; set; }

    public string Descricao { get; set; }

    public string HoraExecucao { get; set; }

    public virtual ICollection<TbDepConfiguracoesTransacao> TbDepConfiguracoesTransacaos { get; set; } = new List<TbDepConfiguracoesTransacao>();
}
