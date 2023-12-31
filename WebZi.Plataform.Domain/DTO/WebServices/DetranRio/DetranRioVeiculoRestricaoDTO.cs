namespace WebZi.Plataform.Domain.DTO.WebServices.DetranRio
{
    public class DetranRioVeiculoRestricaoDTO
    {
        public int IdentificadorRestricao { get; set; }

        public string TipoRestricao { get; set; }

        public string TipoRestricaoDescricao { get; set; }

        public byte CodigoRestricao { get; set; }

        public string Restricao { get; set; }

        public virtual DetranRioVeiculoOrigemRestricaoDTO DetranRioVeiculoOrigemRestricao { get; set; } = new();
    }
}