namespace WebZi.Plataform.Domain.ViewModel.Empresa
{
    public class EmpresaViewModel
    {
        public int IdentificadorEmpresa { get; set; }

        public int? IdentificadorEmpresaMatriz { get; set; }

        public byte IdentificadorEmpresaClassificacao { get; set; }

        public int? IdentificadorCEP { get; set; }

        public byte? IdentificadorTipoLogradouro { get; set; }

        public int? IdentificadorCNAE { get; set; }

        public int? IdentificadorCNAEListaServico { get; set; }

        public string CNPJ { get; set; }

        public string Nome { get; set; }

        public string NomeFantasia { get; set; }

        public string Logradouro { get; set; }

        public string Numero { get; set; }

        public string Complemento { get; set; }

        public string Bairro { get; set; }

        public string Municipio { get; set; }

        public string UF { get; set; }

        public short? CodigoAlterdata { get; set; }

        public string CodigoSap { get; set; }

        public string InscricaoEstadual { get; set; }

        public string InscricaoMunicipal { get; set; }

        public short? CodigoTributarioMunicipio { get; set; }

        public string Token { get; set; }

        public string FlagOptanteSimplesNacional { get; set; }

        public string FlagIssRetido { get; set; }

        public string FlagAtivo { get; set; }
    }
}