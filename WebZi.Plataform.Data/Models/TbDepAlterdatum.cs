using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepAlterdatum
{
    public int AlterDataId { get; set; }

    public string Identificador { get; set; }

    public int GrvOrigemId { get; set; }

    public int UsuarioCadastroId { get; set; }

    public byte TipoDocumentoIdentificacaoId { get; set; }

    public string Documento { get; set; }

    public string JsonEnvio { get; set; }

    public string JsonRetorno { get; set; }

    public DateTime DataCadastro { get; set; }

    public virtual TbDepGrv GrvOrigem { get; set; }

    public virtual TbDepUsuario UsuarioCadastro { get; set; }
}
