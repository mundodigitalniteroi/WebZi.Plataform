using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepPixDinamicoConsultum
{
    public int PixDinamicoConsultaId { get; set; }

    public int PixDinamicoId { get; set; }

    public byte PixDinamicoTipoStatusGeracaoId { get; set; }

    public string Json { get; set; }

    public DateTime DataCadastro { get; set; }

    public virtual TbDepPixDinamico PixDinamico { get; set; }

    public virtual TbDepPixDinamicoTipoStatusGeracao PixDinamicoTipoStatusGeracao { get; set; }
}
