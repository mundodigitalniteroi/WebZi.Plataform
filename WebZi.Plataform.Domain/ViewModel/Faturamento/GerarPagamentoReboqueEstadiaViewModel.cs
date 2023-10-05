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

        public int id_tarifa_tipo_veiculo { get; set; }

        public int id_tarifa { get; set; }

        public string tarifas_descricao { get; set; }

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
        public string faturamento_numero_identificacao { get; set; }

        public string faturamento_data_vencimento { get; set; }

        public string faturamento_prazo_retirar_veiculo { get; set; }


        public int faturamento_quantidade_estadias { get; set; }

        public string faturamento_quantidade_quilometragem { get; set; }


        public string faturamento_preco_estadias { get; set; }

        public string faturamento_preco_reboque { get; set; }

        public string faturamento_preco_quilometragem { get; set; }


        public string faturamento_valor_faturado { get; set; }

        public string faturamento_valor_pagamento { get; set; }

        public string faturamento_valor_faturado_estadias { get; set; }

        public string faturamento_valor_faturado_reboque { get; set; }

        public string faturamento_valor_faturado_quilometragem { get; set; }

        public string faturamento_valor_outros_servicos { get; set; }


        public string faturamento_valor_a_pagar { get; set; }


        public string DataHoraAtual { get; set; }

        public string DataAtual { get; set; }

        public string HoraAtual { get; set; }

        public DateTime DataHoraAtualDateTime { get; set; }

        public string rodape { get; set; }

        public byte[] Logo { get; set; }

        public byte[] QrCode { get; set; }

        public string PixChave { get; set; }
    }
}