using WebZi.Plataform.Domain.Models.Cliente;
using WebZi.Plataform.Domain.Models.Deposito;

namespace WebZi.Plataform.Domain.Models.Faturamento
{
    public class FaturamentoRegraModel
    {
        public short FaturamentoRegraId { get; set; }

        public short FaturamentoRegraTipoId { get; set; }

        public int? ClienteId { get; set; }

        public int? DepositoId { get; set; }

        public int UsuarioCadastroId { get; set; }

        public int? UsuarioAlteracaoId { get; set; }

        public string Valor { get; set; }

        public DateTime DataVigenciaInicial { get; set; }

        public DateTime? DataVigenciaFinal { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public virtual ClienteModel Cliente { get; set; }

        public virtual DepositoModel Deposito { get; set; }

        public virtual FaturamentoRegraTipoModel FaturamentoRegraTipo { get; set; }

        // public virtual TbDepUsuario IdUsuarioAlteracaoNavigation { get; set; }

        // public virtual TbDepUsuario IdUsuarioCadastroNavigation { get; set; }

        // public virtual ICollection<TbDepFaturamentoServicosAssociado> TbDepFaturamentoServicosAssociados { get; set; } = new List<TbDepFaturamentoServicosAssociado>();
    }
}