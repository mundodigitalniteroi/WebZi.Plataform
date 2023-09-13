using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsGlobal;

public partial class TbGloDocTiposDocumento
{
    public byte IdTipoDocumento { get; set; }

    public string Codigo { get; set; }

    public string Descricao { get; set; }

    public string Formato { get; set; }

    public byte TamanhoMaximo { get; set; }

    public byte OrdemApresentacao { get; set; }

    public string FlagAtivo { get; set; }
}
