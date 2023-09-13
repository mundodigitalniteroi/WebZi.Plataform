using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbSapCliente
{
    public int IdSapClientes { get; set; }

    public int IdTransacaoSap { get; set; }

    public int IdGrv { get; set; }

    public string CodigoEmpresa { get; set; }

    public string OrganizacaoVendas { get; set; }

    public string NomeCliente { get; set; }

    public string EnderecoRua { get; set; }

    public string EnderecoNumero { get; set; }

    public string Edificio { get; set; }

    public string Andar { get; set; }

    public string Bairro { get; set; }

    public string Cep { get; set; }

    public string Cidade { get; set; }

    public string Regiao { get; set; }

    public string ContatoTelefone { get; set; }

    public string ContatoEmail { get; set; }

    public string Cnpj { get; set; }

    public string Cpf { get; set; }

    public string InscricaoEstadual { get; set; }

    public string InscricaoMunicipal { get; set; }

    public string FormaPagamento { get; set; }

    public string CondicaoPagamento { get; set; }

    public string TipoParceiro { get; set; }

    public string CodParceiro { get; set; }

    public string CodOrg { get; set; }

    public string FlagErroWsSap { get; set; }

    public DateTime DataCadastro { get; set; }
}
