using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepGrvDocumento
{
    public int GrvDocumentosId { get; set; }

    public int GrvId { get; set; }

    public byte GrvDocumentosQualificacaoId { get; set; }

    public int UsuarioCadastroId { get; set; }

    public int? UsuarioExclusaoId { get; set; }

    public string DiretorioRemoto { get; set; }

    public string ArquivoRemoto { get; set; }

    public string ArquivoNomeReal { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataExclusao { get; set; }

    public virtual TbDepGrv Grv { get; set; }

    public virtual TbDepGrvDocumentosQualificacao GrvDocumentosQualificacao { get; set; }

    public virtual TbDepUsuario UsuarioCadastro { get; set; }

    public virtual TbDepUsuario UsuarioExclusao { get; set; }
}
