namespace WebZi.Plataform.Domain.Models.WebServices.DetranRio
{
    public class DetranRetornoConsultaModel
    {
        public string Retorno { get; set; }

        public short? AnoFabricacao { get; set; }

        public short? AnoModelo { get; set; }

        public short? AnoUltimaLicenca { get; set; }

        public decimal? CapacidadeCarga { get; set; }

        public byte CapacidadePassageiros { get; set; }

        public string ChassiRemarcado { get; set; }

        public string NumeroRenavam { get; set; }

        public string Placa { get; set; }

        public string Chassi { get; set; }

        public string Classificacao { get; set; }

        public string CodigoCategoria { get; set; }

        public string DescricaoCategoria { get; set; }

        public string DescricaoCor { get; set; }

        public string DescricaoMarca { get; set; }

        public string DescricaoTipo { get; set; }

        public string InformacaoRoubo { get; set; }

        public string PesoBrutoTotal { get; set; }

        public string RestricaoEstelionato { get; set; }

        public string Uf { get; set; }

        public string DiaJuliano { get; set; }

        public string Sequencial { get; set; }

        public string Transacao { get; set; }

        public List<DetranRetornoConsultaRestricaoModel> RestricoesAdministrativas { get; set; }

        public List<DetranRetornoConsultaRestricaoModel> RestricoesJuridicas { get; set; }
    }
}