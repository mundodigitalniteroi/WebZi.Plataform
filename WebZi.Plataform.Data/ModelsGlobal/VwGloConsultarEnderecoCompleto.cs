using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsGlobal;

public partial class VwGloConsultarEnderecoCompleto
{
    public int IdCep { get; set; }

    public int IdMunicipio { get; set; }

    public int? IdBairro { get; set; }

    public byte? IdTipoLogradouro { get; set; }

    public string Cep { get; set; }

    public string TipoLogradouro { get; set; }

    public string CodigoLogradouro { get; set; }

    public string Logradouro { get; set; }

    public string Bairro { get; set; }

    public string BairroPtbr { get; set; }

    public string Municipio { get; set; }

    public string MunicipioPtbr { get; set; }

    public string CodigoMunicipio { get; set; }

    public string CodigoMunicipioIbge { get; set; }

    public string Estado { get; set; }

    public string EstadoPtbr { get; set; }

    public string Uf { get; set; }

    public string Regiao { get; set; }

    public string RegiaoNome { get; set; }

    public string FlagSanitizado { get; set; }
}
