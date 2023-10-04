namespace WebZi.Plataform.Domain.ViewModel.Faturamento
{
    public class GuiaPagamentoReboqueEstadiaViewModel
    {
        public int id_grv { get; set; }

        public int id_atendimento { get; set; }

        public string grv_placa_chassi { get; set; }

        public string grv_placa { get; set; }

        public string grv_chassi { get; set; }

        public string grv_renavam { get; set; }

        public string grv_numero_formulario { get; set; }

        public string grv_data_hora_remocao { get; set; }

        public string grv_data_hora_guarda { get; set; }

        public string grv_data_guarda { get; set; }

        public string grv_hora_guarda { get; set; }


        public string grv_estacionamento_setor { get; set; }

        public string grv_estacionamento_numero_vaga { get; set; }

        public string grv_numero_chave { get; set; }


        public string reboquista_nome { get; set; }

        public string reboque_placa { get; set; }

        public string marca_modelo { get; set; }

        public string cor { get; set; }

        public string tipo_veiculo { get; set; }

        public string qualificacao_responsavel_descricao { get; set; }

        public int id_tarifa_tipo_veiculo { get; set; }

        public int id_tarifa { get; set; }

        public string tarifas_descricao { get; set; }

        public string atendimento_responsavel_nome { get; set; }

        public string atendimento_responsavel_documento { get; set; }

        public string atendimento_forma_liberacao { get; set; }

        public string atendimento_forma_liberacao_nome { get; set; }

        public string atendimento_forma_liberacao_cnh { get; set; }

        public string atendimento_forma_liberacao_cpf { get; set; }

        public string atendimento_forma_liberacao_placa { get; set; }

        public string cliente_nome { get; set; }

        public string cliente_cnpj { get; set; }

        public string cliente_endereco { get; set; }

        public string cliente_dados_bancarios { get; set; }

        public string identificador { get; set; }

        public string credito_de { get; set; }

        public string deposito_descricao { get; set; }

        public string deposito_endereco { get; set; }

        public string deposito_municipio { get; set; }


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


        public string data_hora_atual { get; set; }

        public string data_atual { get; set; }

        public string hora_atual { get; set; }

        public DateTime data_hora_atual_dt { get; set; }

        public string rodape { get; set; }

        public byte[] Logo { get; set; }

        public byte[] QrCode { get; set; }

        public string PixChave { get; set; }
    }
}