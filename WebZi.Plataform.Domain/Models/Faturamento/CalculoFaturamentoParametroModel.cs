using WebZi.Plataform.Domain.Models.Atendimento;
using WebZi.Plataform.Domain.Models.ClienteDeposito;

namespace WebZi.Plataform.Domain.Models.Faturamento
{
    public class CalculoFaturamentoParametroModel
    {
        public string FaturamentoProdutoId { get; set; }

        //public int ClienteId { get; set; }

        //public int DepositoId { get; set; }

        public int GrvId { get; set; }

        public string NumeroFormularioGrv { get; set; }

        public int TipoVeiculoId { get; set; }

        public byte TipoMeioCobrancaId { get; set; }

        public int UsuarioCadastroId { get; set; }

        public bool IsComboio { get; set; }

        public string StatusOperacaoId { get; set; }

        public string StatusOperacaoLeilaoId { get; set; }

        public bool IsSimulacao { get; set; }

        public bool FaturarSemGrv { get; set; }

        public bool FlagPermissaoDataRetroativaFaturamento { get; set; }

        /// <summary>
        /// Quando for um Atendimento deve-se usar a Data/Hora da Guarda.
        /// Se for um Faturamento adicional, deve-se usar a Data do dia anterior à Data final do Faturamento anterior.
        /// </summary>
        public DateTime DataHoraInicialParaCalculo { get; set; }

        public DateTime DataHoraFinalParaCalculo { get; set; }

        public DateTime DataHoraPorDeposito { get; set; }

        public bool FlagFaturamentoCompleto { get; set; } = true;

        public bool FaturamentoAdicional { get; set; }

        // public AtendimentoModel Atendimento { get; set; }

        public ClienteDepositoModel ClienteDeposito { get; set; }

        // public FaturamentoModel Faturamento { get; set; }

        public List<CalculoFaturamentoDescontoModel> FaturamentoDescontos { get; set; }

        public List<CalculoFaturamentoQuantidadeAlteradaModel> FaturamentoQuantidadesAlteradas { get; set; }
    }
}