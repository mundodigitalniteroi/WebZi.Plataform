using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepEndereco
{
    public int IdEndereco { get; set; }

    public string Logradouro { get; set; }

    public int Numero { get; set; }

    public string Complemento { get; set; }

    public string Bairro { get; set; }

    public string Municipio { get; set; }

    public string Uf { get; set; }

    public string Cep { get; set; }

    public int IdUsuarioCadastro { get; set; }

    public DateTime DataCadastro { get; set; }

    public int? IdUsuarioAlteracao { get; set; }

    public DateTime? DataAlteracao { get; set; }
}
