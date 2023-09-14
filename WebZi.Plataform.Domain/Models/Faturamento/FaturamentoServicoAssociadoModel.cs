using WebZi.Plataform.Domain.Models.Cliente;
using WebZi.Plataform.Domain.Models.Deposito;

namespace WebZi.Plataform.Domain.Models.Faturamento
{
    public class FaturamentoServicoAssociadoModel
    {
        public int IdFaturamentoServicoAssociado { get; set; }

        public int IdCliente { get; set; }

        public int IdDeposito { get; set; }

        public int IdFaturamentoServicoTipo { get; set; }

        public int IdSapTipoComposicao { get; set; }

        public short? IdFaturamentoRegra { get; set; }

        public int IdUsuarioCadastro { get; set; }

        public int? IdUsuarioAlteracao { get; set; }

        public string Descricao { get; set; }

        public decimal PrecoPadrao { get; set; }

        public decimal PrecoValorMinimo { get; set; }

        public DateTime DataVigenciaInicial { get; set; }

        public DateTime? DataVigenciaFinal { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        /// <summary>
        /// AM: Ambos;
        /// VA: Vigência Atual (Valor Padrão);
        /// VI: Vigência Inicial.
        /// </summary>
        public string FormaCobranca { get; set; }

        public string FlagServicoObrigatorio { get; set; }

        public string FlagPermiteAlteracaoValor { get; set; }

        public string FlagPermiteDesconto { get; set; }

        public string FlagCobrarSomentePrimeiraFatura { get; set; }

        public int? CnaeId { get; set; }

        public int? ListaServicoId { get; set; }

        public string DescricaoConfiguracaoNfe { get; set; }

        public string FlagEnviarValorIss { get; set; }

        public string FlagEnviarInscricaoEstadual { get; set; }

        public virtual ClienteModel Cliente { get; set; }

        public virtual DepositoModel Deposito { get; set; }

        //public virtual TbDepFaturamentoRegra IdFaturamentoRegraNavigation { get; set; }

        //public virtual TbDepFaturamentoServicosTipo IdFaturamentoServicoTipoNavigation { get; set; }

        //public virtual TbDepSapTipoComposicao IdSapTipoComposicaoNavigation { get; set; }

        //public virtual TbDepUsuario IdUsuarioAlteracaoNavigation { get; set; }

        //public virtual TbDepUsuario IdUsuarioCadastroNavigation { get; set; }

        // public virtual ICollection<TbDepFaturamentoServicosTipoVeiculo> TbDepFaturamentoServicosTipoVeiculos { get; set; } = new List<TbDepFaturamentoServicosTipoVeiculo>();
    }
}