using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepReboque
{
    public int IdReboque { get; set; }

    public int IdCliente { get; set; }

    public int IdDeposito { get; set; }

    public int IdUsuarioCadastro { get; set; }

    public int? IdUsuarioAlteracao { get; set; }

    public string Codigo { get; set; }

    public string Placa { get; set; }

    public string Chassi { get; set; }

    public string Renavam { get; set; }

    public string Marca { get; set; }

    public string Modelo { get; set; }

    public decimal? Ano { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public string FlagAtivo { get; set; }

    public virtual TbDepCliente IdClienteNavigation { get; set; }

    public virtual ICollection<TbDepGrv> TbDepGrvs { get; set; } = new List<TbDepGrv>();

    public virtual ICollection<TbDepGtv> TbDepGtvs { get; set; } = new List<TbDepGtv>();

    public virtual ICollection<TbDepReboquesTerceirizado> TbDepReboquesTerceirizados { get; set; } = new List<TbDepReboquesTerceirizado>();

    public virtual ICollection<TbDepSolicitacaoReboque> TbDepSolicitacaoReboques { get; set; } = new List<TbDepSolicitacaoReboque>();
}
