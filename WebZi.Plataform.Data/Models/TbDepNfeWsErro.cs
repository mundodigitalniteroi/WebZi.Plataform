using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepNfeWsErro
{
    public int ErroId { get; set; }

    public int GrvId { get; set; }

    public int? IdentificadorNota { get; set; }

    public int UsuarioId { get; set; }

    public string Acao { get; set; }

    public string OrigemErro { get; set; }

    public string Status { get; set; }

    public string CodigoErro { get; set; }

    public string MensagemErro { get; set; }

    public string CorrecaoErro { get; set; }

    public DateTime DataHoraCadastro { get; set; }

    public virtual TbDepGrv Grv { get; set; }

    public virtual TbDepUsuario Usuario { get; set; }
}
