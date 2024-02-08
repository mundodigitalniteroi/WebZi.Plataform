namespace WebZi.Plataform.Domain.Models.Banco
{
    public class BoletoOriginalModel
    {
        public int BoletoId { get; set; }

        public int? InterfaceUsuarioId { get; set; }

        public int? FaturamentoId { get; set; }

        public DateTime DataCadastroBoleto { get; set; }

        public string CedenteAgencia { get; set; }

        public string CedenteCodigo { get; set; }

        public string CedenteConta { get; set; }

        public string CedenteCpfCnpj { get; set; }

        public string CedenteDigitoConta { get; set; }

        public string CedenteNome { get; set; }

        public string CedenteNossoNumeroBoleto { get; set; }

        public string SacadoBairro { get; set; }

        public string SacadoCep { get; set; }

        public string SacadoCidade { get; set; }

        public string SacadoCpfCnpj { get; set; }

        public string SacadoEndereco { get; set; }

        public string SacadoNome { get; set; }

        public string SacadoUf { get; set; }

        public string BoletoNumeroDocumento { get; set; }

        public string BoletoValor { get; set; }

        public string BoletoVencimento { get; set; }

        public string BoletoBanco { get; set; }

        public string BoletoCarteira { get; set; }

        public string BoletoInstrucoes { get; set; }

        public string NomeArquivoRemessa { get; set; }

        public DateTime? DataGeracaoArquivoRemessa { get; set; }

        public string LinhaDigitavel { get; set; }

        public string EnderecoNumero { get; set; }

        public string EnderecoComplemento { get; set; }

        public string Email { get; set; }

        public string Telefone { get; set; }

        public int? BenificiarioFinalId { get; set; }
    }
}