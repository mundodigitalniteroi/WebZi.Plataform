using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsLeilao;

public partial class VwDetranBaArquivoFtpLeilaoSelecao
{
    public string Tipo { get; set; }

    public int IdFtpEnvio { get; set; }

    public int? Sequencial { get; set; }

    public int? SequencialRemessa { get; set; }

    public string TipoRegistro { get; set; }

    public int? CodigoPatio { get; set; }

    public int? IdCliente { get; set; }

    public int? IdDeposito { get; set; }

    public int? IdLeilao { get; set; }

    public string Arquivo { get; set; }
}
