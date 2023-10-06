namespace WebZi.Plataform.Domain.ViewModel.Faturamento
{
    public class GerarPagamentoReboqueEstadiaViewModel
    {
        public MensagemViewModel Mensagem { get; set; } = new();

        public string PlacaChassi { get; set; }

        public string Placa { get; set; }

        public string Chassi { get; set; }

        public string Renavam { get; set; }

        public string NumeroFormularioGrv { get; set; }

        public string DataHoraRemocao { get; set; }

        public string DataHoraGuarda { get; set; }

        public string DataGuarda { get; set; }

        public string HoraGuarda { get; set; }

        public string EstacionamentoSetor { get; set; }

        public string EstacionamentoNumeroVaga { get; set; }

        public string NumeroChave { get; set; }

        public string ReboquistaNome { get; set; }

        public string ReboquePlaca { get; set; }

        public string MarcaModelo { get; set; }

        public string Cor { get; set; }

        public string TipoVeiculo { get; set; }

        public string QualificacaoResponsavel { get; set; }

        public string AtendimentoResponsavelNome { get; set; }

        public string AtendimentoResponsavelDocumento { get; set; }

        public string AtendimentoFormaLiberacao { get; set; }

        public string AtendimentoFormaLiberacaoNome { get; set; }

        public string AtendimentoFormaLiberacaoCNH { get; set; }

        public string AtendimentoFormaLiberacaoCPF { get; set; }

        public string AtendimentoFormaLiberacaoPlaca { get; set; }

        public string ClienteNome { get; set; }

        public string ClienteCNPJ { get; set; }

        public string ClienteEndereco { get; set; }

        public string ClienteDadosBancarios { get; set; }

        public string Identificador { get; set; }

        public string CreditoDe { get; set; }

        public string DepositoNome { get; set; }

        public string DepositoEndereco { get; set; }

        public string DepositoMunicipio { get; set; }


        // FATURAMENTO
        public string FaturamentoNumeroIdentificacao { get; set; }

        public string FaturamentoDataVencimento { get; set; }


        public int QuantidadeEstadias { get; set; }

        public string QuantidadeQuilometragem { get; set; }


        public string PrecoEstadias { get; set; }

        public string PrecoReboque { get; set; }

        public string PrecoQuilometragem { get; set; }


        public string FaturamentoValorFaturado { get; set; }

        public string ValorFaturadoEstadias { get; set; }

        public string ValorFaturadoReboque { get; set; }

        public string ValorFaturadoQuilometragem { get; set; }

        public string ValorDemaisServicos { get; set; }


        public string FaturamentoValorPagar { get; set; }


        public string DataHoraAtual { get; set; }

        public string DataAtual { get; set; }

        public string HoraAtual { get; set; }

        public DateTime DataHoraAtualDateTime { get; set; }

        public string Rodape { get; set; }

        public byte[] Logo { get; set; }

        public byte[] QrCode { get; set; }

        public string PixChave { get; set; }
    }
}