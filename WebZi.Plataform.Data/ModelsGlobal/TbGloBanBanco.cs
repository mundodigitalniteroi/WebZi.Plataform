using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsGlobal;

public partial class TbGloBanBanco
{
    public short IdBanco { get; set; }

    public int IdUsuarioCadastro { get; set; }

    public int? IdUsuarioAlteracao { get; set; }

    public string CodigoFebraban { get; set; }

    public string Nome { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public string FlagAtivo { get; set; }
}
