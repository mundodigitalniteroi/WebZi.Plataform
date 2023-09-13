using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepMetasClienteDepositoRealizado
{
    public int? IdDepMetasClienteDepositoRealizado { get; set; }

    public int? IdMetaClienteDeposito { get; set; }

    public decimal? MesMetaRealizada { get; set; }

    public string AtingiuPe { get; set; }

    public string AtingiuMeta { get; set; }

    public DateTime? DataCadastro { get; set; }
}
