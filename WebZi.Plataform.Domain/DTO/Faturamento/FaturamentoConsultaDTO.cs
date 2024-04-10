using WebZi.Plataform.Domain.DTO.Faturamento.Simulacao;
using WebZi.Plataform.Domain.DTO.Sistema;
using WebZi.Plataform.Domain.DTO.WebServices.DetranRio;

namespace WebZi.Plataform.Domain.DTO.Faturamento
{
    public class FaturamentoConsultaDTO
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public SimulacaoClienteDTO Cliente { get; set; }

        public SimulacaoDepositoDTO Deposito { get; set; }

        public SimulacaoProdutoDTO Produto { get; set; }

        public int IdentificadorProcesso { get; set; }

        public string NumeroProcesso { get; set; }

        public int IdentificadorAtendimento { get; set; }

        public DateTime? DataHoraRemocao { get; set; }

        public DateTime? DataHoraGuarda { get; set; }

        public string Status { get; set; }
        public int TipoMeioCobrancaId { get; set; }

        public SimulacaoFaturamentoDTO Faturamento { get; set; }

        public DetranRioVeiculoDTO Veiculo { get; set; }
    }
}