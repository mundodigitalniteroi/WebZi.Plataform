using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsLeilao;

public partial class TbDetranBaFtpEnvio
{
    public int IdFtpEnvio { get; set; }

    public int? Sequencial { get; set; }

    public int? IdCliente { get; set; }

    public int? IdDeposito { get; set; }

    public DateTime? DataGeracaoArquivo { get; set; }

    public int? CodigoOrgaoSolic { get; set; }

    public int? QuantRegDetalhe { get; set; }

    public int? TipoArquivo { get; set; }

    public string TipoArquivoDescricao { get; set; }

    public string NomeArquivo { get; set; }

    public string ExtensaoArquivo { get; set; }

    public int? TamanhoArquivo { get; set; }

    public byte[] Arquivo { get; set; }

    public int? CodigoPatio { get; set; }

    public string FlagRetornado { get; set; }

    public string FlagLeilaoTerceiros { get; set; }

    public int? IdLeilao { get; set; }
}
