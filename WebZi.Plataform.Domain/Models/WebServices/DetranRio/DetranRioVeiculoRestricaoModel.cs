using WebZi.Plataform.Domain.Models.WebServices.DetranRio;

namespace WebZi.Plataform.Domain.Models.WebServices.Rio
{
    public class DetranRioVeiculoRestricaoModel
    {
        public int DetranVeiculoRestricaoId { get; set; }

        public int IdDetranVeiculo { get; set; }

        public byte IdDetranVeiculosWsRestricaoOrigem { get; set; }

        /// <summary>
        /// A = Administrativa;
        /// E = Estelionato;
        /// J = Jurídica;
        /// R = Roubo/Furto
        /// </summary>
        public string TipoRestricao { get; set; }

        public byte CodigoRestricao { get; set; }

        public string Restricao { get; set; }

        public virtual DetranRioVeiculoRestricaoModel DetranRioVeiculo { get; set; }

        public virtual DetranRioVeiculoOrigemRestricaoModel DetranRioVeiculoOrigemRestricao { get; set; }
    }
}