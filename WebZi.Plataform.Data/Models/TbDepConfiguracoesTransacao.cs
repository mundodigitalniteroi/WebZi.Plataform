using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepConfiguracoesTransacao
{
    public int IdConfigTransacao { get; set; }

    public int? IdCliente { get; set; }

    public int? IdDeposito { get; set; }

    public int? IdConfigGrupo { get; set; }

    public string FlagServicoLiberado { get; set; }

    public DateTime? FlagDataInicio { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public double? FlagIntervaloLogHoras { get; set; }

    public double? FlagIntervaloCadastroHoras { get; set; }

    public int? IdConfigAcao { get; set; }

    public DateTime? FlagDataApreensaoStart { get; set; }

    public virtual TbDepConfiguracoesTransacaoAcao IdConfigAcaoNavigation { get; set; }

    public virtual TbDepConfiguracoesTransacaoGrupo IdConfigGrupoNavigation { get; set; }
}
