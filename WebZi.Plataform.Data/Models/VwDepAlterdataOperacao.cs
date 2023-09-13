using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwDepAlterdataOperacao
{
    public int CnaeListaServicoId { get; set; }

    public int CnaeId { get; set; }

    public int ListaServicoId { get; set; }

    public int MunicipioId { get; set; }

    public int ParametroMunicipioId { get; set; }

    public string CnaeCodigo { get; set; }

    public string CnaeCodigoFormatado { get; set; }

    public string CnaeDescricao { get; set; }

    public string ListaServico { get; set; }

    public string ListaServicoDescricao { get; set; }

    public decimal AliquotaIss { get; set; }

    public string Uf { get; set; }

    public string Municipio { get; set; }

    public string CodigoMunicipioIbge { get; set; }

    public string CodigoTributarioMunicipio { get; set; }

    public int ClienteId { get; set; }

    public string Cliente { get; set; }

    public int DepositoId { get; set; }

    public string Deposito { get; set; }

    public int ClienteDepositoId { get; set; }

    public int? AlterDataConfiguracaoId { get; set; }

    public string CodigoEmpresa { get; set; }

    public bool? Status { get; set; }

    public short? IdentificadorNaturezaLancamentoId { get; set; }

    public string IdentificadorNaturezaLancamentoCodigo { get; set; }

    public string IdentificadorNaturezaLancamentoDescricao { get; set; }

    public byte? AlterDataOperacaoId { get; set; }

    public int? UsuarioCadastroId { get; set; }

    public int? UsuarioAlteracaoId { get; set; }

    public DateTime? DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public short? OperacaoId { get; set; }

    public string OperacaoCodigo { get; set; }

    public string OperacaoDescricao { get; set; }

    public short? CfopId { get; set; }

    public string CfopCodigo { get; set; }

    public string CfopDescricao { get; set; }

    public short? IdentificadorProdutoId { get; set; }

    public string IdentificadorProdutoCodigo { get; set; }

    public string IdentificadorProdutoDescricao { get; set; }
}
