using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepRetornoBancarioLeilaoT
{
    public int IdRetornoBancarioLeilaoT { get; set; }

    public int? Agencia { get; set; }

    public int? AgenciaCobradoraRecebedora { get; set; }

    public int? BancoCobradorRecebedor { get; set; }

    public int? CodigoBanco { get; set; }

    public int? CodigoCarteira { get; set; }

    public int? CodigoCedente { get; set; }

    public int? CodigoMovimento { get; set; }

    public int? CodigoRegistro { get; set; }

    public string CodigoSegmento { get; set; }

    public int? ContaCorrente { get; set; }

    public DateTime? DataVencimento { get; set; }

    public string DigitoAgencia { get; set; }

    public string DigitoContacorrente { get; set; }

    public string DvagenciaCobradoraRecebedora { get; set; }

    public string DvagenciaConta { get; set; }

    public string IdentificacaoTitulo { get; set; }

    public string IdentificacaoTitulonaEmpresa { get; set; }

    public string LoteServico { get; set; }

    public int? ModalidaDenossonumero { get; set; }

    public int? Moeda { get; set; }

    public string MotivoOcorrencia { get; set; }

    public string NomeSacado { get; set; }

    public string NossoNumero { get; set; }

    public int? NumeroBanco { get; set; }

    public string NumeroContrato { get; set; }

    public string NumeroDocumento { get; set; }

    public string NumeroInscricaosacado { get; set; }

    public int? NumeroRegistro { get; set; }

    public int? TipoInscricaoSacado { get; set; }

    public string UsoFebraban { get; set; }

    public decimal? ValorTarifas { get; set; }

    public decimal? RetornoBancarioCef { get; set; }

    public int IdArquivo { get; set; }
}
