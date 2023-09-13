using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.ModelsGlobal;

public partial class TbGloEmpEmpresa
{
    public int IdEmpresa { get; set; }

    public int? IdEmpresaMatriz { get; set; }

    public byte IdEmpresaClassificacao { get; set; }

    public int? IdCep { get; set; }

    public byte? IdTipoLogradouro { get; set; }

    public int IdUsuarioCadastro { get; set; }

    public int? IdUsuarioAlteracao { get; set; }

    public string Cnpj { get; set; }

    public string Nome { get; set; }

    public string NomeFantasia { get; set; }

    public string Logradouro { get; set; }

    public string Numero { get; set; }

    public string Complemento { get; set; }

    public string Bairro { get; set; }

    public string Municipio { get; set; }

    public string Uf { get; set; }

    public short? CodigoAlterdata { get; set; }

    public string CodigoSap { get; set; }

    public int? CnaeId { get; set; }

    public int? CnaeListaServicoId { get; set; }

    public string InscricaoEstadual { get; set; }

    public string InscricaoMunicipal { get; set; }

    public short? CodigoTributarioMunicipio { get; set; }

    public string Token { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public string FlagIssRetido { get; set; }

    public string FlagAtivo { get; set; }

    public string OptanteSimplesNacional { get; set; }

    public virtual TbGovCnae Cnae { get; set; }

    public virtual TbGovCnaeListaServico CnaeListaServico { get; set; }

    public virtual TbGloLocCep IdCepNavigation { get; set; }

    public virtual TbGloEmpEmpresasClassificacao IdEmpresaClassificacaoNavigation { get; set; }

    public virtual TbGloEmpEmpresa IdEmpresaMatrizNavigation { get; set; }

    public virtual TbGloLocTiposLogradouro IdTipoLogradouroNavigation { get; set; }

    public virtual ICollection<TbGloEmpEmpresa> InverseIdEmpresaMatrizNavigation { get; set; } = new List<TbGloEmpEmpresa>();
}
