using WebZi.Plataform.Domain.Models.Atendimento;
using WebZi.Plataform.Domain.Models.ClienteDeposito;
using WebZi.Plataform.Domain.Models.GRV;

namespace WebZi.Plataform.Domain.Models.Faturamento
{
    public class CalculoFaturamentoParametroModel
    {
        public int UsuarioCadastroId { get; set; }

        public char OrigemTipoComposicao { get; set; } = 'P'; // P = PÁTIO, L = LEILÃO

        public string StatusOperacaoId { get; set; }

        public bool FlagCadastrarFaturamento { get; set; } = true;

        public bool FlagPermissaoDataRetroativaFaturamento { get; set; }

        public int Diarias { get; set; }

        public string StatusOperacaoLeilaoId { get; set; }

        /// <summary>
        /// Quando for um Atendimento deve-se usar a Data/Hora da Guarda.
        /// Se for um Faturamento adicional, deve-se usar a Data do dia anterior à Data final do Faturamento anterior.
        /// </summary>
        public DateTime DataHoraInicialParaCalculo { get; set; }

        public DateTime DataHoraFinalParaCalculo { get; set; }

        public DateTime DataPrazoRetiradaVeiculo { get; set; }

        public DateTime DataHoraPorDeposito { get; set; }

        public bool FlagFaturamentoCompleto { get; set; } = true;

        public bool FaturamentoAdicional { get; set; }

        public AtendimentoModel Atendimento { get; set; }

        public ClienteDepositoModel ClienteDeposito { get; set; }

        public FaturamentoModel Faturamento { get; set; }

        public GrvModel Grv { get; set; }

        public TipoMeioCobrancaModel TipoMeioCobranca { get; set; }

        public List<CalculoFaturamentoDescontoModel> FaturamentoDescontos { get; set; }

        public List<FaturamentoRegraModel> FaturamentoRegras { get; set; }

        public List<CalculoFaturamentoQuantidadeAlteradaModel> FaturamentoQuantidadesAlteradas { get; set; }
    }
}