using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepGrvLacre
{
    public int IdLacre { get; set; }

    public int IdGrv { get; set; }

    public byte? IdLacreMotivoDesassociacao { get; set; }

    public int? IdUsuarioCadastro { get; set; }

    public int? IdUsuarioAtualizacao { get; set; }

    public string Lacre { get; set; }

    public string LacreAnterior { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAtualizacao { get; set; }

    public virtual TbDepGrv IdGrvNavigation { get; set; }

    public virtual TbDepGrvLacresMotivosDesassociacao IdLacreMotivoDesassociacaoNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioAtualizacaoNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioCadastroNavigation { get; set; }
}
