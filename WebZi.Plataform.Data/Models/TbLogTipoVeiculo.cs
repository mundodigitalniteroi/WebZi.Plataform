using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbLogTipoVeiculo
{
    public long Id { get; set; }

    public byte? IdTipoVeiculo { get; set; }

    public int? IdUsuarioCadastro { get; set; }

    public int? IdUsuarioAlteracao { get; set; }

    public string Descricao { get; set; }

    public DateTime? DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public string FlagNaoRequerCnhNaLiberacao { get; set; }

    public string FlagAtivo { get; set; }
}
