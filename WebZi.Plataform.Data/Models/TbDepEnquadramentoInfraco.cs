using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepEnquadramentoInfraco
{
    public decimal IdEnquadramentoInfracao { get; set; }

    public int IdUsuario { get; set; }

    public string CodigoInfracao { get; set; }

    public short? Artigo { get; set; }

    public string Inciso { get; set; }

    public string Descricao { get; set; }

    public DateTime DataCadastro { get; set; }

    public string Status { get; set; }

    public virtual TbDepUsuario IdUsuarioNavigation { get; set; }

    public virtual ICollection<TbDepCondutor> TbDepCondutors { get; set; } = new List<TbDepCondutor>();

    public virtual ICollection<TbDepGrvEnquadramentoInfraco> TbDepGrvEnquadramentoInfracos { get; set; } = new List<TbDepGrvEnquadramentoInfraco>();
}
