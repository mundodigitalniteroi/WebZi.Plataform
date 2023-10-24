namespace WebZi.Plataform.Domain.ViewModel.Localizacao
{
    public class EnderecoViewModel
    {
        public MensagemViewModel Mensagem { get; set; } = new();

        public int IdentificadorCEP { get; set; }

        public int IdentificadorMunicipio { get; set; }

        public int? IdentificadorBairro { get; set; }

        public byte? IdentificadorTipoLogradouro { get; set; }

        public string CEP { get; set; }

        public string TipoLogradouro { get; set; }

        public string Logradouro { get; set; }

        public string Bairro { get; set; }

        public string BairroPtbr { get; set; }

        public string Municipio { get; set; }

        public string MunicipioPtbr { get; set; }

        public string CodigoMunicipio { get; set; }

        public string CodigoMunicipioIbge { get; set; }

        public string Estado { get; set; }

        public string EstadoPtbr { get; set; }

        public string UF { get; set; }

        public string SiglaRegiao { get; set; }

        public string Regiao { get; set; }
    }
}