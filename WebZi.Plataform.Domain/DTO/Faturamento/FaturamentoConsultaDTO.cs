using WebZi.Plataform.Domain.DTO.Atendimento;
using WebZi.Plataform.Domain.DTO.Faturamento.Simulacao;
using WebZi.Plataform.Domain.DTO.Liberacao;
using WebZi.Plataform.Domain.DTO.Sistema;
using WebZi.Plataform.Domain.DTO.Veiculo;
using WebZi.Plataform.Domain.DTO.WebServices.DetranRio;
using WebZi.Plataform.Domain.DTO.WebServices.Nfe;
using WebZi.Plataform.Domain.DTO.WebServices.Nfse;

#nullable enable
namespace WebZi.Plataform.Domain.DTO.Faturamento
{
    public class FaturamentoConsultaDTO
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public SimulacaoClienteDTO? Cliente { get; set; }

        public SimulacaoDepositoDTO? Deposito { get; set; }

        public SimulacaoProdutoDTO? Produto { get; set; }

        public int? IdentificadorFaturamento { get; set; }

        public int? IdentificadorProcesso { get; set; }

        public string? NumeroProcesso { get; set; }
        public string? CodigoAlvaraDeLiberacao { get; set; }

        public string? CodigoIdentificacaoCliente { get; set; }

        public int? IdentificadorAtendimento { get; set; }

        public DateTime? DataHoraRemocao { get; set; }

        public DateTime? DataHoraGuarda { get; set; }

        public string? StatusOperacaoId { get; set; }
        public string? StatusOperacaoDescricao { get; set; }

        public int? TipoMeioCobrancaId { get; set; }
        public List<NFERetornoFaturamentoDTO?>? NotaFiscal { get; set; }
        public AtendimentoDTO? Atendimento { get; set; }

        public List<SimulacaoFaturamentoDTO>? Faturamentos { get; set; } = new();

        public LiberacaoEspecialDTO? LiberacaoEspecial { get; set; }

        public DetranRioVeiculoDTO? Veiculo { get; set; }
    }

    public class VeiculoConsultaInfo
    {
        public string Placa { get; set; }
        public string Chassi { get; set; }
        public string Renavam { get; set; }
        public string Uf { get; set; }
        public CorDTO Cor { get; set; }
        public MarcaModeloDTO MarcaModelo { get; set; }
        public TipoVeiculoDTO TipoVeiculo { get; set; }
    }

    public class GrvConsultaDados
    {
        public int GrvId { get; set; }
        public string NumeroProcesso { get; set; }
        public string CodigoAlvaraDeLiberacao { get; set; }
        public string? CodigoIdentificacaoCliente { get; set; }
        public DateTime? DataHoraRemocao { get; set; }
        public DateTime? DataHoraGuarda { get; set; }
        public string StatusOperacaoId { get; set; }
        public string StatusOperacaoDescricao { get; set; }
        public ClienteConsultaDados Cliente { get; set; }
        public DepositoConsultaDados Deposito { get; set; }
        public VeiculoConsultaInfo Veiculo { get; set; }
    }

    public class ClienteConsultaDados
    {
        public int ClienteId { get; set; }
        public string Nome { get; set; }
        public string NumeroEndereco { get; set; }
        public string ComplementoEndereco { get; set; }
        public string TipoLogradouro { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Municipio { get; set; }
        public string UF { get; set; }
    }

    public class DepositoConsultaDados
    {
        public int DepositoId { get; set; }
        public string Nome { get; set; }
        public string TelefoneMob { get; set; }
        public string NumeroEndereco { get; set; }
        public string ComplementoEndereco { get; set; }
        public string TipoLogradouro { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Municipio { get; set; }
        public string UF { get; set; }
    }
}