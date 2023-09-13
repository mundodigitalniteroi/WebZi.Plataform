using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsGlobal;

public partial class VwGloConsultarMunicipioPorCep
{
    public int IdCep { get; set; }

    public string Cep { get; set; }

    public int IdMunicipio { get; set; }

    public string Municipio { get; set; }

    public string MunicipioPtbr { get; set; }

    public string Uf { get; set; }

    public string CodigoMunicipio { get; set; }
}
