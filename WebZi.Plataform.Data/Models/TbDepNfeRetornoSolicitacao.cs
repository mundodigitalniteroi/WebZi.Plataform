using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepNfeRetornoSolicitacao
{
    public int RetornoSolicitacaoId { get; set; }

    public int NfeId { get; set; }

    public int? NfePrestadorId { get; set; }

    public string NaturezaOperacao { get; set; }

    public string OptanteSimplesNacional { get; set; }

    public string TomadorCpfCnpj { get; set; }

    public string TomadorCnpj { get; set; }

    public string TomadorNomeRazaoSocial { get; set; }

    public string TomadorTelefone { get; set; }

    public string TomadorEmail { get; set; }

    public string TomadorEnderecoLogradouro { get; set; }

    public string TomadorEnderecoNumero { get; set; }

    public string TomadorEnderecoComplemento { get; set; }

    public string TomadorEnderecoBairro { get; set; }

    public string TomadorEnderecoCodigoMunicipio { get; set; }

    public string TomadorEnderecoUf { get; set; }

    public string TomadorEnderecoCep { get; set; }

    public string ServicoAliquota { get; set; }

    public string ServicoDiscriminacao { get; set; }

    public string ServicoIssRetido { get; set; }

    public string ServicoValorIss { get; set; }

    public string ServicoCodigoCnae { get; set; }

    public string ServicoItemListaServico { get; set; }

    public string ServicoValorServicos { get; set; }

    public string ServicoCodigoTributarioMunicipio { get; set; }

    public string RespostaEnvio { get; set; }

    public DateTime? DataEmissao { get; set; }

    public DateTime DataCadastro { get; set; }

    public string Json { get; set; }

    public virtual TbDepNfe Nfe { get; set; }
}
