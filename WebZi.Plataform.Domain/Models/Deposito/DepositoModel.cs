using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.Models.GRV;

namespace WebZi.Plataform.Domain.Models.Deposito
{
    public class DepositoModel
    {
        public int DepositoId { get; set; }

        public int? EmpresaId { get; set; }

        public int? CepId { get; set; }

        public byte? TipoLogradouroId { get; set; }

        public int? BairroId { get; set; }

        public int? SistemaExternoId { get; set; }

        public int UsuarioCadastroId { get; set; }

        public int? UsuarioAlteracaoId { get; set; }

        public string Descricao { get; set; }

        public string Logradouro { get; set; }

        public string Numero { get; set; }

        public string Complemento { get; set; }

        public string EmailNfe { get; set; }

        public byte GrvMinimoFotosExigidas { get; set; }

        public byte GrvLimiteMinimoDatahoraGuarda { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string EnderecoMob { get; set; }

        public string TelefoneMob { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public string FlagEnderecoCadastroManual { get; set; }

        public string FlagAtivo { get; set; }

        public string FlagVirtual { get; set; }

        //public virtual TbDepUsuario IdUsuarioAlteracaoNavigation { get; set; }

        //public virtual TbDepUsuario IdUsuarioCadastroNavigation { get; set; }

        //public virtual ICollection<TbDepClientesDeposito> TbDepClientesDepositos { get; set; }

        public virtual ICollection<FaturamentoRegraModel> FaturamentoRegras { get; set; }

        public virtual ICollection<FaturamentoServicoAssociadoModel> FaturamentoServicosAssociados { get; set; }

        public virtual ICollection<GrvModel> Grvs { get; set; }

        //public virtual ICollection<TbDepGtv> TbDepGtvIdDepositoEnvioNavigations { get; set; }

        //public virtual ICollection<TbDepGtv> TbDepGtvIdDepositoRecebimentoNavigations { get; set; }

        //public virtual ICollection<TbDepReboquista> TbDepReboquista { get; set; }

        //public virtual ICollection<TbDepUsuariosDeposito> TbDepUsuariosDepositos { get; set; }
    }
}