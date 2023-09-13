using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbLogGrvClientesCodigoIdentificacao
{
    public long Id { get; set; }

    public int IdClienteCodigoIdentificacao { get; set; }

    public int IdGrv { get; set; }

    public int IdUsuarioCadastro { get; set; }

    public int? IdUsuarioAtualizacao { get; set; }

    public string ClienteCodigoIdentificacao { get; set; }

    public DateTime? DataCadastro { get; set; }

    public DateTime? DataAtualizacao { get; set; }

    public string FlagAtivo { get; set; }

    public DateTime DatahoraLog { get; set; }
}
