using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepSolicitacaoReboquePsv
{
    public int IdSolicitacaoReboquePsv { get; set; }

    public int IdSolicitacaoReboque { get; set; }

    public byte? IdTipoVeiculo { get; set; }

    public int? IdMarcaModelo { get; set; }

    public int? IdCor { get; set; }

    public int? IdCep { get; set; }

    public byte? IdTipoLogradouro { get; set; }

    public string Placa { get; set; }

    public string Chassi { get; set; }

    public string Renavam { get; set; }

    public string NomeSolicitante { get; set; }

    public string CpfSolicitante { get; set; }

    public string Logradouro { get; set; }

    public string Numero { get; set; }

    public string Complemento { get; set; }

    public string Bairro { get; set; }

    public string Municipio { get; set; }

    public string Uf { get; set; }

    public string Cep { get; set; }

    public string Tel1 { get; set; }

    public string Tel2 { get; set; }

    public string Email { get; set; }

    public virtual TbDepMarcasModelo IdMarcaModeloNavigation { get; set; }

    public virtual TbDepSolicitacaoReboque IdSolicitacaoReboqueNavigation { get; set; }

    public virtual TbDepTipoVeiculo IdTipoVeiculoNavigation { get; set; }
}
