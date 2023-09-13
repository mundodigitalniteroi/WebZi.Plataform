using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbLogNfe
{
    public long Id { get; set; }

    public int UsuarioCrudId { get; set; }

    public string Crud { get; set; }

    public DateTime DataHoraLog { get; set; }

    public int NfeId { get; set; }

    public int GrvId { get; set; }

    public int? FaturamentoServicoTipoVeiculoId { get; set; }

    public int IdentificadorNota { get; set; }

    public int? NfeComplementarId { get; set; }

    public int UsuarioCadastroId { get; set; }

    public string Cnpj { get; set; }

    public string Numero { get; set; }

    public string CodigoVerificacao { get; set; }

    public int? CodigoRetorno { get; set; }

    public string Url { get; set; }

    public string Status { get; set; }

    public string StatusNfe { get; set; }

    public DateTime? DataEmissao { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public string NumeroRps { get; set; }

    public string NumeroNotaFiscal { get; set; }

    public string CaminhoXmlNotaFiscal { get; set; }

    public string Referencia { get; set; }

    public string SerieRps { get; set; }
}
