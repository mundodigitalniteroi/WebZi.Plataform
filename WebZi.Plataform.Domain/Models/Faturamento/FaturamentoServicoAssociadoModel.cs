using WebZi.Plataform.Domain.Models.Cliente;
using WebZi.Plataform.Domain.Models.Deposito;
using WebZi.Plataform.Domain.Models.Usuario;

namespace WebZi.Plataform.Domain.Models.Faturamento
{
    public class FaturamentoServicoAssociadoModel
    {
        public int FaturamentoServicoAssociadoId { get; set; }

        public int ClienteId { get; set; }

        public int DepositoId { get; set; }

        public int FaturamentoServicoTipoId { get; set; }

        public int SapTipoComposicaoId { get; set; }

        public short? FaturamentoRegraId { get; set; }

        public int UsuarioCadastroId { get; set; }

        public int? UsuarioAlteracaoId { get; set; }

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
        public string FormaCobranca { get; set; } = "VA";

        public string FlagServicoObrigatorio { get; set; } = "N";

        public string FlagPermiteAlteracaoValor { get; set; } = "N";

        public string FlagPermiteDesconto { get; set; } = "N";

        public string FlagCobrarSomentePrimeiraFatura { get; set; } = "N";

        public int? CnaeId { get; set; }

        public int? ListaServicoId { get; set; }

        public string DescricaoConfiguracaoNfe { get; set; }

        public string FlagEnviarValorIss { get; set; }

        public string FlagEnviarInscricaoEstadual { get; set; } = "N";

        public virtual ClienteModel Cliente { get; set; }

        public virtual DepositoModel Deposito { get; set; }

        public virtual FaturamentoRegraModel FaturamentoRegra { get; set; }

        public virtual FaturamentoServicoTipoModel FaturamentoServicoTipo { get; set; }

        // public virtual SapTipoComposicao IdSapTipoComposicaoNavigation { get; set; }

        public virtual UsuarioModel UsuarioCadastro { get; set; }

        public virtual UsuarioModel UsuarioAlteracao { get; set; }

        public virtual ICollection<FaturamentoServicoTipoVeiculoModel> FaturamentoServicosTiposVeiculos { get; set; }
    }
}