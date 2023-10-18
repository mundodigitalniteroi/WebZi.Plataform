using WebZi.Plataform.Domain.Models.Cliente;
using WebZi.Plataform.Domain.Models.Governo;
using WebZi.Plataform.Domain.Models.Localizacao;
using WebZi.Plataform.Domain.Views.Localizacao;

namespace WebZi.Plataform.Domain.Models.Empresa
{
    public class EmpresaModel
    {
        public int EmpresaId { get; set; }

        public int? EmpresaMatrizId { get; set; }

        public byte EmpresaClassificacaoId { get; set; }

        public int? CEPId { get; set; }

        public byte? TipoLogradouroId { get; set; }

        public int? CnaeId { get; set; }

        public int? CnaeListaServicoId { get; set; }

        public int UsuarioCadastroId { get; set; }

        public int? UsuarioAlteracaoId { get; set; }

        public string CNPJ { get; set; }

        public string Nome { get; set; }

        public string NomeFantasia { get; set; }

        public string Logradouro { get; set; }

        public string Numero { get; set; }

        public string Complemento { get; set; }

        public string Bairro { get; set; }

        public string Municipio { get; set; }

        public string UF { get; set; }

        public short? CodigoAlterdata { get; set; }

        public string CodigoSap { get; set; }

        public string InscricaoEstadual { get; set; }

        public string InscricaoMunicipal { get; set; }

        public short? CodigoTributarioMunicipio { get; set; }

        public string Token { get; set; }

        public string FlagOptanteSimplesNacional { get; set; } = "N";

        public string FlagIssRetido { get; set; } = "N";

        public string FlagAtivo { get; set; } = "S";

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public virtual ClienteModel Cliente { get; set; }

        public virtual EmpresaClassificacaoModel EmpresaClassificacao { get; set; }

        public virtual ViewEnderecoCompletoModel Endereco { get; set; }

        public virtual TipoLogradouroModel TipoLogradouro { get; set; }

        public virtual CnaeModel Cnae { get; set; }

        public virtual AssociacaoCnaeListaServicoModel CnaeListaServico { get; set; }

        // public virtual EmpresaModel EmpresaMatriz { get; set; }

        //public virtual ICollection<TbGloEmpEmpresa> InverseIdEmpresaMatrizNavigation { get; set; }
    }
}