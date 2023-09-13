using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepDetranTransacaoLiberarVeiculoPatio
{
    public int IdDetranTransacaoLiberarVeiculoPatio { get; set; }

    public int IdDetranGrvTransacao { get; set; }

    public string Placa { get; set; }

    public string Chassi { get; set; }

    public string Operador { get; set; }

    public string TipoDocumentoLiberador { get; set; }

    public string CpfCnpjLiberador { get; set; }

    public string DataLiberacao { get; set; }

    public string HoraLiberacao { get; set; }

    public string MotivoLiberacao { get; set; }

    public virtual TbDepDetranGrvStatusTransacao IdDetranGrvTransacaoNavigation { get; set; }
}
