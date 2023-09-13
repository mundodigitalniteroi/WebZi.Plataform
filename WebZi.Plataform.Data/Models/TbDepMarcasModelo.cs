using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepMarcasModelo
{
    public int IdMarcaModelo { get; set; }

    public string Descricao { get; set; }

    public int IdUsuario { get; set; }

    public DateTime DataCadastro { get; set; }

    public string Status { get; set; }

    public virtual TbDepUsuario IdUsuarioNavigation { get; set; }

    public virtual ICollection<TbDepSolicitacaoReboquePsv> TbDepSolicitacaoReboquePsvs { get; set; } = new List<TbDepSolicitacaoReboquePsv>();
}
