using WebZi.Plataform.Domain.Models.Cliente;
using WebZi.Plataform.Domain.Models.Deposito;

namespace WebZi.Plataform.Domain.Models.ClienteDeposito
{
    public class ClienteDepositoModel
    {
        public int ClienteDepositoId { get; set; }

        public int ClienteId { get; set; }

        public int DepositoId { get; set; }

        public short? OrgaoEmissorId { get; set; }

        public int EmpresaId { get; set; }

        public string SistemaExternoId { get; set; }

        public int UsuarioCadastroId { get; set; }

        public int? UsuarioAlteracaoId { get; set; }

        public string CodigoDetran { get; set; }

        public string CodigoSap { get; set; }

        public string CodigoERPOrdemVenda { get; set; }

        public string FlagUtilizaSistemaMobileGgv { get; set; } = "N";

        public string FlagCadastrarGrvComStatusOperacaoBloqueado { get; set; } = "S";

        public string FlagValorIssIgualProdutoBaseCalculoAliquota { get; set; } = "N";

        public string FlagAtivo { get; set; } = "S";

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public decimal AliquotaIss { get; set; }

        public virtual ClienteModel Cliente { get; set; }

        public virtual DepositoModel Deposito { get; set; }

        //public virtual TbDepUsuario IdUsuarioAlteracaoNavigation { get; set; }

        //public virtual TbDepUsuario IdUsuarioCadastroNavigation { get; set; }

        //public virtual TbDepAlterdataConfiguracao TbDepAlterdataConfiguracao { get; set; }

        public virtual ICollection<ClienteDepositoTipoVeiculoModel> ClienteDepositoTiposVeiculos { get; set; }

        //public virtual ICollection<TbDepComunicacaoEmail> TbDepComunicacaoEmails { get; set; } = new List<TbDepComunicacaoEmail>();

        //public virtual ICollection<TbDepContasTemporaria> TbDepContasTemporaria { get; set; } = new List<TbDepContasTemporaria>();

        //public virtual ICollection<TbDepDetranAssociacaoTransacaoClienteDeposito> TbDepDetranAssociacaoTransacaoClienteDepositos { get; set; } = new List<TbDepDetranAssociacaoTransacaoClienteDeposito>();

        //public virtual ICollection<TbDepNfeConfiguracaoImagem> TbDepNfeConfiguracaoImagems { get; set; } = new List<TbDepNfeConfiguracaoImagem>();

        //public virtual ICollection<TbDepNfeRegra> TbDepNfeRegras { get; set; } = new List<TbDepNfeRegra>();

        //public virtual ICollection<TbDepSolicitacaoReboque> TbDepSolicitacaoReboques { get; set; } = new List<TbDepSolicitacaoReboque>();

        //public virtual ICollection<TbDepTarifa> TbDepTarifas { get; set; } = new List<TbDepTarifa>();
    }
}