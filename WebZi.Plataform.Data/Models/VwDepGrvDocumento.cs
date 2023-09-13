using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwDepGrvDocumento
{
    public int GrvDocumentosId { get; set; }

    public int GrvId { get; set; }

    public string NumeroFormularioGrv { get; set; }

    public string Qualificacao { get; set; }

    public int UsuarioCadastroId { get; set; }

    public int? UsuarioExclusaoId { get; set; }

    public string DiretorioRemoto { get; set; }

    public string ArquivoRemoto { get; set; }

    public string ArquivoNomeReal { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataExclusao { get; set; }
}
