using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepTiposCombustivei
{
    public decimal IdTipoCombustivel { get; set; }

    public string Descricao { get; set; }

    public int IdUsuario { get; set; }

    public DateTime DataCadastro { get; set; }

    public string Status { get; set; }

    public virtual TbDepUsuario IdUsuarioNavigation { get; set; }
}
