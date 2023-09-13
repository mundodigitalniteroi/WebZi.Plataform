using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepAlterdataDocumento
{
    public int AlterDataDocumentoId { get; set; }

    public int NfeId { get; set; }

    public string Identificador { get; set; }

    public string JsonEnvio { get; set; }

    public string JsonRetorno { get; set; }

    public DateTime DataCadastro { get; set; }

    public string IdentificadorLoteEstoque { get; set; }

    public string ItemIdentificador { get; set; }

    public string ItemIdentificadorProduto { get; set; }

    public int? Numero { get; set; }

    public string PagamentoIdentificador { get; set; }

    public string IdentificadorFormaPagamento { get; set; }

    public string TituloReceberEnviado { get; set; }

    public DateTime? DataDeBaixa { get; set; }

    public virtual TbDepNfe Nfe { get; set; }

    public virtual ICollection<TbDepAlterdataTituloReceber> TbDepAlterdataTituloRecebers { get; set; } = new List<TbDepAlterdataTituloReceber>();
}
