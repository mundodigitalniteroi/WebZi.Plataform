using WebZi.Plataform.Domain.Models.Localizacao;

namespace WebZi.Plataform.Domain.Models.Governo
{
    public class ParametroMunicipioModel
    {
        public int ParametroMunicipioId { get; set; }

        public int CnaeListaServicoId { get; set; }

        public int MunicipioId { get; set; }

        public string CodigoCnae { get; set; }

        public string ItemListaServico { get; set; }

        public string CodigoMunicipioIbge { get; set; }

        public string CodigoTributarioMunicipio { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public virtual AssociacaoCnaeListaServicoModel CnaeListaServico { get; set; }

        public virtual MunicipioModel Municipio { get; set; }
    }
}