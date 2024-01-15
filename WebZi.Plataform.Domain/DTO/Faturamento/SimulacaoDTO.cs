using WebZi.Plataform.Domain.DTO.Sistema;
using WebZi.Plataform.Domain.DTO.WebServices.DetranRio;

namespace WebZi.Plataform.Domain.DTO.Faturamento
{
    public class SimulacaoDTO
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public int IdentificadorProcesso { get; set; }

        public string NumeroProcesso { get; set; }

        public int IdentificadorAtendimento { get; set; }

        public DateTime? DataHoraRemocao { get; set; }

        public DateTime? DataHoraGuarda { get; set; }

        public DateTime DataHoraInicialParaCalculo { get; set; }

        public DateTime DataHoraFinalParaCalculo { get; set; }

        public int QuantidadeDiarias { get; set; }

        public FaturamentoProdutoDTO Produto { get; set; }

        public FaturamentoDTO Faturamento { get; set; }

        public DetranRioVeiculoDTO Veiculo { get; set; }

        public DateTime DataHoraSimulacao { get; set; }
    }
}