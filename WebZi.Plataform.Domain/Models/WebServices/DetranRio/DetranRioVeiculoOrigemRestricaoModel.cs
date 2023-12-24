using WebZi.Plataform.Domain.Models.WebServices.Rio;

namespace WebZi.Plataform.Domain.Models.WebServices.DetranRio
{
    public class DetranRioVeiculoOrigemRestricaoModel
    {
        public byte DetranVeiculoOrigemRestricaoId { get; set; }

        public string Descricao { get; set; }

        public string FlagPermiteEdicao { get; set; } = "N";

        public virtual ICollection<DetranRioVeiculoRestricaoModel> ListagemDetranRioVeiculoRestricao { get; set; }
    }
}