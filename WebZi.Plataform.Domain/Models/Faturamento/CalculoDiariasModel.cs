namespace WebZi.Plataform.Domain.Models.Faturamento
{
    public class CalculoDiariasModel
    {
        public int ClienteId { get; set; }

        public int DepositoId { get; set; }

        public int AtendimentoId { get; set; }

        public int MunicipioId { get; set; }

        public string Uf { get; set; }


        public short MaximoDiariasParaCobranca { get; set; }

        public short MaximoDiasVencimento { get; set; }

        public int Diarias { get; set; }


        public string HoraDiaria { get; set; }

        public DateTime DataHoraDiaria { get; set; }

        public DateTime DataHoraLiberacao { get; set; }

        public DateTime DataHoraInicialParaCalculo { get; set; }

        public DateTime? DataHoraFinalParaCalculo { get; set; }


        public string FlagUsarHoraDiaria { get; set; }

        public string FlagEmissaoNotaFiscalSap { get; set; }

        public string FlagClienteRealizaFaturamentoArrecadacao { get; set; }

        public string FlagCobrarDiariasDiasCorridos { get; set; }

        public string FlagPrimeiroFaturamento { get; set; }

        public string FlagComboio { get; set; }

        public DateTime DataHoraOntem { get; set; }

        public int QuantidadeDiariasPagas { get; set; }

        public bool CobrarTodasDiarias { get; set; }

        public List<DateTime> Feriados { get; set; }

        public int QuantidadeDiasUteis { get; set; }
    }
}