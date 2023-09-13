using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsGlobal;

public partial class VwGloPesPessoasTiposContato
{
    public long IdPessoaTipoContato { get; set; }

    public long IdPessoa { get; set; }

    public int IdTipoContato { get; set; }

    public string PessoasTiposContatosDescricao { get; set; }

    public string PessoasTiposContatosFlagContatoPrincipal { get; set; }

    public string TiposContatosDescricao { get; set; }

    public string TiposContatosFormato { get; set; }

    public byte TiposContatosTamanhoMinimo { get; set; }

    public byte TiposContatosTamanhoMaximo { get; set; }

    public byte TiposContatosOrdemApresentacao { get; set; }

    public string TiposContatosFlagAtivo { get; set; }
}
