using WebZi.Plataform.Domain.DTO.Faturamento.Simulacao;
using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Report
{
    public class GuiaAutorizacaoRetiradaVeiculoDTO
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public int IdentificadorProcesso { get; set; }

        public string NumeroProcesso { get; set; }
        public string NumeroAlvaraLiberacao { get; set; }

        public string ClienteNome { get; set; }

        public string ClienteEndereco { get; set; }

        public string DadosCodigoAutorizacao { get; set; }

        public string DadosProcessoGrv { get; set; }

        public string DadosTipoProcesso { get; set; }

        public string DadosReboque { get; set; }

        public string DadosDataEntrada { get; set; }

        public string DadosHoraEntrada { get; set; }

        public string DadosPermanencia { get; set; }

        public string DadosAutorizadaRetiradaVeiculoEm { get; set; }

        public string DadosDaRetiradaDoVeiculo { get; set; }

        public string VeiculoTipo { get; set; }

        public string VeiculoMarcaModelo { get; set; }

        public string VeiculoPlaca { get; set; }

        public string VeiculoRenavam { get; set; }

        public string VeiculoChassi { get; set; }

        public string VeiculoCor { get; set; }

        public string? DescricaoObjeto { get; set; }

        public string? FlagObjeto { get; set; }

        public string MotivoSaida { get; set; }

        public string DataSaida { get; set; }

        public string? DataEntrega { get; set; }

        public string ObservacoesSaidaParaReparo { get; set; }

        public string DataSaidaSaidaParaReparo { get; set; }

        public string? DataVoltaSaidaParaReparo { get; set; }

        public string TextoApresentacao { get; set; }

        public string TextoDeclaracaoRetirada1 { get; set; }

        public string TextoDeclaracaoRetirada2 { get; set; }

        public string ProprietarioProcurador { get; set; }

        public string ProprietarioCpf { get; set; }

        public string UsuarioNome { get; set; }

        public string UsuarioMatricula { get; set; }

        public string GrvEstacionamentoSetor { get; set; }

        public string GrvEstacionamentoNumeroVaga { get; set; }

        public string GrvNumeroChave { get; set; }

        public string AtendimentoFormaLiberacao { get; set; }

        public string AtendimentoFormaLiberacaoNome { get; set; }

        public string AtendimentoFormaLiberacaoCNH { get; set; }

        public string AtendimentoFormaLiberacaoCpfPlaca { get; set; }

        public string LabelAtendimentoFormaLiberacaoCpfPlaca { get; set; }

        public List<string> ListagemLacre { get; set; }

        public string QRCodeString { get; set; }

        public string Logo { get; set; }

        public byte[] QRCode { get; set; }

        public string? FaturamentoNumeroIdentificacao { get; set; }

        public string? FaturamentoDataVencimento { get; set; }

        public string? StatusPagamento { get; set; }

        public string? PrazoRetiradaVeiculo { get; set; }

        public int QuantidadeEstadias { get; set; }

        public string? QuantidadeQuilometragem { get; set; }

        public string? PrecoEstadias { get; set; }

        public string? PrecoReboque { get; set; }

        public string? PrecoQuilometragem { get; set; }

        public string? FaturamentoValorFaturado { get; set; }

        public string? ValorFaturadoEstadias { get; set; }

        public string? ValorFaturadoReboque { get; set; }

        public string? ValorFaturadoQuilometragem { get; set; }

        public string? ValorDemaisServicos { get; set; }

        public string? TipoDescontoEstadias { get; set; }

        public string? ValorDescontoEstadias { get; set; }

        public string? TipoDescontoReboque { get; set; }

        public string? ValorDescontoReboque { get; set; }

        public string? TipoDescontoQuilometragem { get; set; }

        public string? ValorDescontoQuilometragem { get; set; }

        public string? ValorTotalDesconto { get; set; }

        public string? FaturamentoValorPagar { get; set; }

        public List<SimulacaoFaturamentoComposicaoDTO> ListagemServico { get; set; }
    }
}