using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsLeilao;

public partial class TbDetranBaFtpRetornoDetalhe
{
    public int IdFtpRetornoDetalhe { get; set; }

    public int? IdGrv { get; set; }

    public int? Sequencial { get; set; }

    public string Placa { get; set; }

    public string Chassi { get; set; }

    public string TipoAtualizacao { get; set; }

    public int? IdLeilao { get; set; }

    public int? CodigoPatio { get; set; }

    public DateTime? DataNotificacao { get; set; }

    public DateTime? DataEditalNotificacao { get; set; }

    public DateTime? DataEditalLiberacao { get; set; }

    public DateTime? DataEncerramento { get; set; }

    public DateTime? DataExecucao { get; set; }

    public DateTime? DataLimiteRetirada { get; set; }

    public string NumeroTermo { get; set; }

    public decimal? ValorEstimado { get; set; }

    public decimal? ValorVenda { get; set; }

    public int? TipoDocArrematante { get; set; }

    public string CnpjCpfArrematante { get; set; }

    public string NomeArrematante { get; set; }

    public int? NumeroNotaFiscal { get; set; }

    public int? DataNotaFiscal { get; set; }

    public string EnderecoArrematante { get; set; }

    public string NumeroEnderecoArrematante { get; set; }

    public string ComplemEnderecoArrematante { get; set; }

    public int? CepArrematante { get; set; }

    public string MunicipioArrematante { get; set; }

    public string UfArrematante { get; set; }

    public int? DataApreensao { get; set; }

    public string Classificacao { get; set; }

    public int? IdFtpRetorno { get; set; }

    public int? CodigoRetorno { get; set; }

    public string EmailArrematante { get; set; }
}
