using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwDetranBaAnaliseLiberacaoEnvio
{
    public int? IdCliente { get; set; }

    public string Cliente { get; set; }

    public int? IdDeposito { get; set; }

    public string Deposito { get; set; }

    public int IdGrv { get; set; }

    public string NumeroGrv { get; set; }

    public string Placa { get; set; }

    public string Chassi { get; set; }

    public string Status { get; set; }

    public short CodigoRetExec { get; set; }

    public short? CodigoErro { get; set; }

    public string DescricaoErro { get; set; }

    public byte? CdMeioPag { get; set; }

    public int IdUsuario { get; set; }

    public string Usuario { get; set; }

    public string CodigoOperacao { get; set; }

    public string NumeroTermo { get; set; }

    public string NumeroCnh { get; set; }

    public string CodigoPatio { get; set; }

    public string DataEntrega { get; set; }

    public string HoraEntrega { get; set; }

    public string NomeCondutor { get; set; }

    public string NomeLibera { get; set; }

    public string IndProprietario { get; set; }

    public string NumDocLibera { get; set; }

    public DateTime? DataCadastro { get; set; }
}
