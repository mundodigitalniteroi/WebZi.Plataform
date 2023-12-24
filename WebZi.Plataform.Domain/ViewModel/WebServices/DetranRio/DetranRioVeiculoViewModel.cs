using WebZi.Plataform.Domain.Models.WebServices.Rio;
using WebZi.Plataform.Domain.ViewModel.Sistema;
using WebZi.Plataform.Domain.ViewModel.Veiculo;

namespace WebZi.Plataform.Domain.ViewModel.WebServices.DetranRio
{
    public class DetranRioVeiculoViewModel
    {
        public MensagemViewModel Mensagem { get; set; } = new();

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

        public string DescricaoTipo { get; set; }

        public string InformacaoRoubo { get; set; }

        public string PesoBrutoTotal { get; set; }

        public string Placa { get; set; }

        public string Renavam { get; set; }

        public string RestricaoEstelionato { get; set; }

        public string Uf { get; set; }

        public CorViewModel Cor { get; set; }

        public MarcaModeloViewModel MarcaModelo { get; set; }

        public virtual ICollection<DetranRioVeiculoRestricaoViewModel> ListagemRestricao { get; set; }
    }
}