using WebZi.Plataform.Domain.Models.Sistema;
using WebZi.Plataform.Domain.Models.Veiculo;
using WebZi.Plataform.Domain.Models.WebServices.Rio;

namespace WebZi.Plataform.Domain.Models.WebServices.DetranRio
{
    public class DetranRioVeiculoModel
    {
        public int DetranVeiculoId { get; set; }

        public int MarcaModeloId { get; set; }

        public int CorId { get; set; }

        public short? AnoFabricacao { get; set; }

        public short? AnoModelo { get; set; }

        public short? AnoUltimaLicenca { get; set; }

        public decimal? CapacidadeCarga { get; set; }

        public byte? CapacidadePassageiros { get; set; }

        public string Chassi { get; set; }

        public string ChassiRemarcado { get; set; }

        public string Classificacao { get; set; }

        public string CodigoCategoria { get; set; }

        public string DescricaoCategoria { get; set; }

        public string DescricaoTipo { get; set; }

        public string InformacaoRoubo { get; set; }

        public string PesoBrutoTotal { get; set; }

        public string Placa { get; set; }

        public string Renavam { get; set; }

        public string RestricaoEstelionato { get; set; }

        public string Uf { get; set; }

        public DateTime DataCadastro { get; set; }

        public string FlagRegistroNormalizado { get; set; } = "N";

        public virtual CorModel Cor { get; set; }

        public virtual MarcaModeloModel MarcaModelo { get; set; }

        public virtual ICollection<DetranRioVeiculoRestricaoModel> ListagemDetranRioVeiculoRestricao { get; set; }
    }
}