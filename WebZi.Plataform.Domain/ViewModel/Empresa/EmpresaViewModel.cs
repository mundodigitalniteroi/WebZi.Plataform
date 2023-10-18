namespace WebZi.Plataform.Domain.ViewModel.Empresa
{
    public class EmpresaViewModel
    {
        public int EmpresaId { get; set; }

        public int? EmpresaMatrizId { get; set; }

        public byte EmpresaClassificacaoId { get; set; }

        public int? CEPId { get; set; }

        public byte? TipoLogradouroId { get; set; }

        public int? CnaeId { get; set; }

        public int? CnaeListaServicoId { get; set; }

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