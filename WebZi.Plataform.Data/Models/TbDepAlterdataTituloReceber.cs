using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepAlterdataTituloReceber
{
    public int AlterDataTituloReceberId { get; set; }

    public int AlterDataDocumentoId { get; set; }

    public string CodigoEmpresa { get; set; }

    public string Identificador { get; set; }

    public string JsonEnvio { get; set; }

    public string JsonRetorno { get; set; }

    public string LoteBaixaEnviado { get; set; }

    public DateTime DataCadastro { get; set; }

    public virtual TbDepAlterdataDocumento AlterDataDocumento { get; set; }

    public virtual ICollection<TbDepAlterdataLoteBaixa> TbDepAlterdataLoteBaixas { get; set; } = new List<TbDepAlterdataLoteBaixa>();
}
