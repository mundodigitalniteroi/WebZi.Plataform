using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepGrvBloqueio
{
    public int IdGrvBloqueio { get; set; }

    public int IdGrv { get; set; }

    public string IdStatusOperacaoAnterior { get; set; }

    public byte IdGrvBloqueioMotivo { get; set; }

    public int IdUsuarioCadastro { get; set; }

    public int? IdUsuarioAlteracao { get; set; }

    public string Motivo { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public virtual TbDepGrvBloqueioMotivo IdGrvBloqueioMotivoNavigation { get; set; }

    public virtual TbDepGrv IdGrvNavigation { get; set; }

    public virtual TbDepStatusOperaco IdStatusOperacaoAnteriorNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioCadastroNavigation { get; set; }
}
