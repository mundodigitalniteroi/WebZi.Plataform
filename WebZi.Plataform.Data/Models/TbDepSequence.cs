using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepSequence
{
    public string SequenceName { get; set; }

    public int Seed { get; set; }

    public int Incremental { get; set; }

    public int? CurrentValue { get; set; }
}
