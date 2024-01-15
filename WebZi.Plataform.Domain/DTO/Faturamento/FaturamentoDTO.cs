using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Faturamento
{
    public class FaturamentoDTO
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public int IdentificadorFaturamento { get; set; }

        public byte IdentificadorTipoMeioCobranca { get; set; }

        public string NumeroIdentificacao { get; set; }

        public decimal ValorFaturado { get; set; }

        public decimal? ValorPagamento { get; set; }

        public string HoraDiaria { get; set; }

        public short MaximoDiariasParaCobranca { get; set; }

        public short MaximoDiasVencimento { get; set; }

        public byte Sequencia { get; set; } = 1;

        public string NumeroNotaFiscal { get; set; }

        public DateTime? DataCalculo { get; set; }

        public DateTime? DataRetroativa { get; set; }

        public DateTime DataVencimento { get; set; }

        public DateTime? DataPrazoRetiradaVeiculo { get; set; }

        public DateTime? DataPagamento { get; set; }

        public DateTime? DataEmissaoDocumento { get; set; }

        public DateTime? DataEmissaoNotaFiscal { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public string Status { get; set; }

        public string FlagUsarHoraDiaria { get; set; }

        public string FlagLimitacaoJudicial { get; set; }

        public string FlagClienteRealizaFaturamentoArrecadacao { get; set; }

        public string FlagCobrarDiariasDiasCorridos { get; set; }

        public string FlagPermissaoDataRetroativaFaturamento { get; set; }

        // public virtual TipoMeioCobrancaModel TipoMeioCobranca { get; set; }

        public List<FaturamentoComposicaoDTO> ListagemFaturamentoServico { get; set; }
    }
}