using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsLeilao;

public partial class TbLeilaoLotesArrematante
{
    public int Id { get; set; }

    public int? IdGrv { get; set; }

    public string NumeroProcesso { get; set; }

    public string NumeroBoleto { get; set; }

    public string NotaArrematacao { get; set; }

    public string Cartela { get; set; }

    public string Status { get; set; }

    public string Lote { get; set; }

    public string Leilao { get; set; }

    public string Placa { get; set; }

    public string Chassi { get; set; }

    public string NomeArrematante { get; set; }

    public string Cpf { get; set; }

    public string Cnpj { get; set; }

    public string Fone1 { get; set; }

    public string Fone2 { get; set; }

    public string Email { get; set; }

    public string Logradouro { get; set; }

    public string Numero { get; set; }

    public string Complemento { get; set; }

    public string Bairro { get; set; }

    public string Cidade { get; set; }

    public string Estado { get; set; }

    public string Cep { get; set; }

    public string LinhaDigitavel { get; set; }

    public string DataEmissaoBoleto { get; set; }

    public string DataVencimentoBoleto { get; set; }

    public string DataPagamentoBoleto { get; set; }

    public string Avaliacao { get; set; }

    public string Arrematacao { get; set; }

    public string Descontos { get; set; }

    public string TaxaAdministrativa { get; set; }

    public string OutrasTaxas { get; set; }

    public string TarifaBancaria { get; set; }

    public string ValorTotal { get; set; }

    public string Iss { get; set; }

    public string Comissao { get; set; }

    public string StatusCadastroClienteSap { get; set; }

    public string StatusCadastroFb70Sap { get; set; }

    public string IdDocumentoClienteSap { get; set; }

    public string IdDocumentoFb70Sap { get; set; }

    public DateTime DataHoraCadastro { get; set; }

    public string FlgConfirmado { get; set; }

    public DateTime? DataNotaFiscal { get; set; }

    public string ValorPago { get; set; }

    public int? IdLote { get; set; }

    public string MsgErroFb70 { get; set; }

    public string CodigoSapBanco { get; set; }
}
