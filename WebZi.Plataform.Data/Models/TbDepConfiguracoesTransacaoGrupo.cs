using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepConfiguracoesTransacaoGrupo
{
    public int IdConfigGrupo { get; set; }

    public string Descricao { get; set; }

    public string FlagServicoLiberado { get; set; }

    public virtual ICollection<TbDepConfiguracoesTransacao> TbDepConfiguracoesTransacaos { get; set; } = new List<TbDepConfiguracoesTransacao>();
}
