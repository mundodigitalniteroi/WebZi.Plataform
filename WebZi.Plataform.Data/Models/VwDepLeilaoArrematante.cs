using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwDepLeilaoArrematante
{
    public int IdGrv { get; set; }

    public string ArrematanteNomeArrematante { get; set; }

    public string ArrematanteCpfCnpj { get; set; }

    public string ArrematanteTelefoneFixo { get; set; }

    public string ArrematanteTelefoneCelular { get; set; }

    public string ArrematanteEmail { get; set; }

    public string ArrematanteLogradouro { get; set; }

    public string ArrematanteNumero { get; set; }

    public string ArrematanteComplemento { get; set; }

    public string ArrematanteBairro { get; set; }

    public string ArrematanteCidade { get; set; }

    public string ArrematanteEstado { get; set; }

    public string ArrematanteCep { get; set; }
}
