using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwDepNfeImagen
{
    public int NfeId { get; set; }

    public int GrvId { get; set; }

    public int UsuarioCadastroId { get; set; }

    public string Cnpj { get; set; }

    public int? CodigoRetorno { get; set; }

    public string Status { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public int NfeImagemId { get; set; }

    public byte[] Imagem { get; set; }
}
