using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepDetranTransacaoConsultarPendenciasLiberacaoVeiculo
{
    public int IdDetranTransacaoConsultarPendenciasLiberacaoVeiculo { get; set; }

    public int IdDetranGrvTransacao { get; set; }

    public string Placa { get; set; }

    public string Chassi { get; set; }

    public string Operador { get; set; }

    public string TipoDocLiberador { get; set; }

    public string DocumentoLiberador { get; set; }

    public string NumeroGuiaRecolhimento { get; set; }

    public virtual TbDepDetranGrvStatusTransacao IdDetranGrvTransacaoNavigation { get; set; }
}
