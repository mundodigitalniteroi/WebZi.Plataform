using WebZi.Plataform.Domain.DTO.Atendimento;
using WebZi.Plataform.Domain.DTO.Faturamento.Simulacao;
using WebZi.Plataform.Domain.DTO.Liberacao;
using WebZi.Plataform.Domain.DTO.Sistema;
using WebZi.Plataform.Domain.DTO.WebServices.DetranRio;
using WebZi.Plataform.Domain.DTO.WebServices.Nfe;
using WebZi.Plataform.Domain.DTO.WebServices.Nfse;

namespace WebZi.Plataform.Domain.DTO.Faturamento
{
    public class FaturamentoConsultaDTO
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public SimulacaoClienteDTO Cliente { get; set; }

        public SimulacaoDepositoDTO Deposito { get; set; }

        public SimulacaoProdutoDTO Produto { get; set; }

        public int IdentificadorFaturamento { get; set; }

        public int IdentificadorProcesso { get; set; }

        public string NumeroProcesso { get; set; }

        public int IdentificadorAtendimento { get; set; }

        public DateTime? DataHoraRemocao { get; set; }

        public DateTime? DataHoraGuarda { get; set; }

        public string StatusOperacaoId { get; set; }
        public string StatusOperacaoDescricao { get; set; }

        public int TipoMeioCobrancaId { get; set; }
        public List<NFERetornoFaturamentoDTO?> NotaFiscal { get; set; }
        public AtendimentoDTO Atendimento { get; set; }

        public SimulacaoFaturamentoDTO Faturamento { get; set; }

        public LiberacaoEspecialDTO? LiberacaoEspecial { get; set; }

        public DetranRioVeiculoDTO Veiculo { get; set; }
    }
}