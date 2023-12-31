namespace WebZi.Plataform.Domain.Models.Faturamento
{
    public class CalculoDiariasModel
    {
        public int ClienteId { get; set; }

        public int DepositoId { get; set; }

        public int AtendimentoId { get; set; }

        public int MunicipioId { get; set; }

        public string UF { get; set; }


        public short MaximoDiariasParaCobranca { get; set; }

        public short MaximoDiasVencimento { get; set; }

        public int Diarias { get; set; }


        public string HoraDiaria { get; set; }

        public DateTime DataHoraDiaria { get; set; }

        public DateTime DataHoraLiberacao { get; set; }

        public DateTime DataHoraInicialParaCalculo { get; set; }

        public DateTime? DataHoraFinalParaCalculo { get; set; }


        public bool FlagUsarHoraDiaria { get; set; }

        public bool FlagEmissaoNotaFiscalERP { get; set; }

        public bool FlagClienteRealizaFaturamentoArrecadacao { get; set; }

        public bool FlagCobrarDiariasDiasCorridos { get; set; }

        public bool IsPrimeiroFaturamento { get; set; }

        public bool IsComboio { get; set; }

        public DateTime DataHoraOntem { get; set; }

        public int QuantidadeDiariasPagas { get; set; }

        public bool FlagCobrarTodasDiarias { get; set; }

        public int QuantidadeDiasUteis { get; set; }

        public List<DateTime> Feriados { get; set; }
    }
}