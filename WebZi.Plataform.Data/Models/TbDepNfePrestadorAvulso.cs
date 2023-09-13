using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepNfePrestadorAvulso
{
    public short NfePrestadorAvulsoId { get; set; }

    public string Cnpj { get; set; }

    public string Nome { get; set; }

    public string Token { get; set; }
}
