using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsLeilao;

public partial class TbLogEmailArrematacaoBoleto
{
    public int Id { get; set; }

    public int IdBoleto { get; set; }

    public string Destinatario { get; set; }

    public string Assunto { get; set; }

    public string Mensagem { get; set; }

    public bool? Erro { get; set; }

    public string MsgErro { get; set; }

    public int? IdUsuario { get; set; }

    public DateTime? DataEnvio { get; set; }
}
