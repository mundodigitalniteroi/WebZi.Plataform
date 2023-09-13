using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwDetranBaAnaliseApreensaoEnvio
{
    public string Cliente { get; set; }

    public string Deposito { get; set; }

    public int IdGrv { get; set; }

    public string NumeroGrv { get; set; }

    public string Placa { get; set; }

    public string Chassi { get; set; }

    public string Status { get; set; }

    public short? CodigoRetExec { get; set; }

    public short? CodigoErro { get; set; }

    public string DescricaoErro { get; set; }

    public string MotivoApreensao { get; set; }

    public int IdUsuario { get; set; }

    public string Usuario { get; set; }

    public string CodigoOperacao { get; set; }

    public string TipoArgumento { get; set; }

    public string Argumento { get; set; }

    public string CodigoPatio { get; set; }

    public string IndNaoEmplacado { get; set; }

    public string DataEntrada { get; set; }

    public string HoraEntrada { get; set; }

    public string Artigo { get; set; }

    public string IndChassiAdulter { get; set; }

    public string CodigoMunicipio { get; set; }

    public string LocalInfracao { get; set; }

    public string SituacaoVeicP2 { get; set; }

    public string SituacaoVeicP3 { get; set; }

    public string NovaPlaca { get; set; }

    public string NovoChassi { get; set; }

    public string NumeroTrav { get; set; }

    public string StatusRouboFurto { get; set; }

    public string OrigemApreensao { get; set; }

    public string NumeroOficio { get; set; }

    public string DataOficio { get; set; }

    public string MatricComand { get; set; }

    public string OcorrItens { get; set; }

    public string CodigoReboque { get; set; }

    public string AitAssinado { get; set; }

    public DateTime? DataCadastro { get; set; }
}
