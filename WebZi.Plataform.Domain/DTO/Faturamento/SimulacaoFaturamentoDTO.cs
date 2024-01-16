using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Faturamento
{
    public class SimulacaoFaturamentoDTO
    {
        public decimal ValorFaturado { get; set; }

        public string HoraDiaria { get; set; }

        public short MaximoDiariasParaCobranca { get; set; }

        public short MaximoDiasVencimento { get; set; }

        public DateTime? DataCalculo { get; set; }

        public DateTime DataVencimento { get; set; }

        public string FlagUsarHoraDiaria { get; set; }

        public string FlagLimitacaoJudicial { get; set; }

        public string FlagClienteRealizaFaturamentoArrecadacao { get; set; }

        public string FlagCobrarDiariasDiasCorridos { get; set; }

        public string FlagPermissaoDataRetroativaFaturamento { get; set; }

        public List<SimulacaoFaturamentoComposicaoDTO> ListagemFaturamentoServico { get; set; }
    }
}