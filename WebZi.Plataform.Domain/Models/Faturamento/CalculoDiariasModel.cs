namespace WebZi.Plataform.Domain.Models.Faturamento
{
    public class CalculoDiariasModel
    {
        public int ClienteId { get; set; }

        public int DepositoId { get; set; }

        public int AtendimentoId { get; set; }

        public int MunicipioId { get; set; }

        public string Uf { get; set; }


        public int MaximoDiariasParaCobranca { get; set; }

        public int MaximoDiasVencimento { get; set; }

        public int Diarias { get; set; }


        public string HoraDiaria { get; set; }

        public DateTime DataHoraGuarda { get; set; }

        public DateTime DataHoraDiaria { get; set; }

        public DateTime DataHoraLiberacao { get; set; }

        public DateTime DataHoraInicialParaCalculo { get; set; }

        public DateTime? DataFinalParaCalculo { get; set; }


        public string FlagUsarHoraDiaria { get; set; }

        public string IsEmissaoNotaFiscalSap { get; set; }

        public string IsClienteRealizaFaturamentoArrecadacao { get; set; }

        public string IsCobrarDiariasDiasCorridos { get; set; }

        public string IsPrimeiroFaturamento { get; set; }

        public string IsComboio { get; set; }

        public DateTime DataHoraOntem { get; set; }

        public int QuantidadeDiariasPagas { get; set; }

        public bool CobrarTodasDiarias { get; set; }

        public List<DateTime> Feriados { get; set; }

        public int QuantidadeDiasUteis { get; set; }
    }
}