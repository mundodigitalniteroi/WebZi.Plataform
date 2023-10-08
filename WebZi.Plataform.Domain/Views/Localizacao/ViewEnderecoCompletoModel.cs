using WebZi.Plataform.Domain.Models.Cliente;
using WebZi.Plataform.Domain.Models.Deposito;

namespace WebZi.Plataform.Domain.Views.Localizacao
{
    public class ViewEnderecoCompletoModel
    {
        public int CEPId { get; set; }

        public int MunicipioId { get; set; }

        public int? BairroId { get; set; }

        public byte? TipoLogradouroId { get; set; }

        public string CEP { get; set; }

        public string TipoLogradouro { get; set; }

        public string CodigoLogradouro { get; set; }

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

        public string FlagNormalizado { get; set; } = "N";

        public virtual ICollection<ClienteModel> Clientes { get; set; }

        public virtual ICollection<DepositoModel> Depositos { get; set; }
    }
}