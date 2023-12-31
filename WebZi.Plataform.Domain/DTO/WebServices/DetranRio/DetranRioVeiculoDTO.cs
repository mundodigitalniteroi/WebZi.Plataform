using WebZi.Plataform.Domain.DTO.Sistema;
using WebZi.Plataform.Domain.DTO.Veiculo;

namespace WebZi.Plataform.Domain.DTO.WebServices.DetranRio
{
    public class DetranRioVeiculoDTO
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public int IdentificadorVeiculo { get; set; }

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

        public string InformacaoRoubo { get; set; }

        public string PesoBrutoTotal { get; set; }

        public string Placa { get; set; }

        public string Renavam { get; set; }

        public string RestricaoEstelionato { get; set; }

        public string Uf { get; set; }

        public CorDTO Cor { get; set; }

        public MarcaModeloDTO MarcaModelo { get; set; }

        public TipoVeiculoDTO TipoVeiculo { get; set; } = new();

        public List<DetranRioVeiculoRestricaoDTO> ListagemRestricao { get; set; } = new();
    }
}