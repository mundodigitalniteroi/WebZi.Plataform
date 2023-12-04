namespace WebZi.Plataform.Domain.ViewModel.Report
{
    public class GuiaAutorizacaoRetiradaVeiculoViewModel
    {
        public MensagemViewModel Mensagem = new();

        public string EmpresaNome { get; set; }

        public string empresa_endereco { get; set; }

        public string dados_codigo_autorizacao { get; set; }

        public string dados_aviso { get; set; }

        public string dados_processo_grv { get; set; }

        public string dados_tipo_processo { get; set; }

        public string dados_reboque { get; set; }

        public string dados_data_entrada { get; set; }

        public string dados_hora_entrada { get; set; }

        public string dados_permanencia { get; set; }

        public string dados_autorizada_retirada_veiculo_em { get; set; }

        public string veiculo_marca_modelo { get; set; }

        public string veiculo_placa { get; set; }

        public string veiculo_renavam { get; set; }

        public string veiculo_chassi { get; set; }

        public string veiculo_cor { get; set; }

        public string veiculo_ano { get; set; }

        public string texto_apresentacao { get; set; }

        public string proprietario_procurador { get; set; }

        public string proprietario_rg { get; set; }

        public string proprietario_cpf { get; set; }

        public string usuario_nome { get; set; }

        public string usuario_cargo { get; set; }

        public string usuario_matricula { get; set; }

        public byte[] barcode128 { get; set; }

        public byte[] byteBarcode128 { get; set; }

        public string grv_estacionamento_setor { get; set; }

        public string grv_estacionamento_numero_vaga { get; set; }

        public string grv_numero_chave { get; set; }

        public string atendimento_forma_liberacao { get; set; }

        public string atendimento_forma_liberacao_nome { get; set; }

        public string atendimento_forma_liberacao_cnh { get; set; }

        public string atendimento_forma_liberacao_cpf_placa { get; set; }

        public string label_atendimento_forma_liberacao_cpf_placa { get; set; }
    }
}