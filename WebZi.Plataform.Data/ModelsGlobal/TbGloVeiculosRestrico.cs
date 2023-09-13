using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsGlobal;

public partial class TbGloVeiculosRestrico
{
    public int Sequencial { get; set; }

    public string Placa { get; set; }

    public string Chassi { get; set; }

    public int? IdProprietario { get; set; }

    public string Descricao { get; set; }

    public DateTime? DataCadastro { get; set; }

    public string FlagOrigem { get; set; }

    public virtual TbGloProprietario IdProprietarioNavigation { get; set; }
}
