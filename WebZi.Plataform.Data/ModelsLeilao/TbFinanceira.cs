using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsLeilao;

public partial class TbFinanceira
{
    public int Id { get; set; }

    public string Cnpj { get; set; }

    public string Nome { get; set; }

    public string Segmento { get; set; }

    public string Endereco { get; set; }

    public string Complemento { get; set; }

    public string Bairro { get; set; }

    public string Cep { get; set; }

    public string Municipio { get; set; }

    public string Uf { get; set; }

    public string Ddd { get; set; }

    public string Fone { get; set; }

    public string Email { get; set; }

    public string Site { get; set; }

    public string Numero { get; set; }
}
