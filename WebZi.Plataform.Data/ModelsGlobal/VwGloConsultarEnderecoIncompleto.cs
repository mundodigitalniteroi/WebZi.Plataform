using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsGlobal;

public partial class VwGloConsultarEnderecoIncompleto
{
    public int IdCep { get; set; }

    public int IdMunicipio { get; set; }

    public int? IdBairro { get; set; }

    public int? IdTipoLogradouro { get; set; }

    public string Cep { get; set; }

    public int? TipoLogradouro { get; set; }

    public int? Logradouro { get; set; }

    public int? Bairro { get; set; }

    public int? BairroPtbr { get; set; }

    public string Municipio { get; set; }

    public string MunicipioPtbr { get; set; }

    public string CodigoMunicipio { get; set; }

    public string Estado { get; set; }

    public string EstadoPtbr { get; set; }

    public string Uf { get; set; }

    public string Regiao { get; set; }

    public string RegiaoNome { get; set; }

    public string FlagSanitizado { get; set; }
}
