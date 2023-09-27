namespace WebZi.Plataform.Domain.Models.Faturamento
{
    public class BoletoWSModel
    {
        public string cedente_banco_nome { get; set; }

        public string cedente_codigo_febraban { get; set; }

        public string cedente_nossoNumeroBoleto { get; set; }

        public string numeroDocumento { get; set; }

        public string cedente_codigo { get; set; }

        public string cedente_cpfCnpj { get; set; }

        public string cedente_nome { get; set; }

        public string cedente_agencia { get; set; }

        public string cedente_conta_corrente { get; set; }

        public string cedente_dv { get; set; }

        public string sacado_cpfCnpj { get; set; }

        public string sacado_nome { get; set; }

        public string sacado_endereco { get; set; }

        public string sacado_bairro { get; set; }

        public string sacado_cidade { get; set; }

        public string sacado_cep { get; set; }

        public string sacado_uf { get; set; }

        public string vencimento { get; set; }

        public string valor_boleto { get; set; }

        public string sacado_carteira { get; set; }

        public string sacado_instrucoes { get; set; }
    }
}