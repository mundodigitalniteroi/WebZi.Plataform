using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepGrvBloqueioMotivo
{
    public byte IdGrvBloqueioMotivo { get; set; }

    public int IdUsuarioCadastro { get; set; }

    public int? IdUsuarioAlteracao { get; set; }

    public string Descricao { get; set; }

    public string Tipo { get; set; }

    public string FlagAtivo { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public virtual TbDepUsuario IdUsuarioCadastroNavigation { get; set; }

    public virtual ICollection<TbDepGrvBloqueio> TbDepGrvBloqueios { get; set; } = new List<TbDepGrvBloqueio>();
}
