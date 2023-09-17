using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsLeilao;

public partial class TbDetranBaFtpRetorno
{
    public int IdFtpRetorno { get; set; }

    public int? IdFtpEnvio { get; set; }

    public int? Sequencial { get; set; }

    public int? TipoRegistro { get; set; }

    public int? CodigoRetorno { get; set; }

    public int? DataGeracaoArquivo { get; set; }

    public int? HoraGeracaoArquivo { get; set; }

    public int? CodigoPatio { get; set; }

    public int? TipoArquivo { get; set; }

    public int? CodigoOrgaoSolic { get; set; }

    public int? QuantRegDetalhe { get; set; }

    public string TipoArquivoDescricao { get; set; }

    public string NomeArquivo { get; set; }

    public string ExtensaoArquivo { get; set; }

    public int? TamanhoArquivo { get; set; }

    public byte[] Arquivo { get; set; }
}
