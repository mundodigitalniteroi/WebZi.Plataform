namespace WebZi.Plataform.Domain.ViewModel.WebServices.DetranRio
{
    public class DetranRioVeiculoRestricaoViewModel
    {
        public int IdentificadorRestricao { get; set; }

        public string TipoRestricao { get; set; }

        public string TipoRestricaoDescricao { get; set; }

        public byte CodigoRestricao { get; set; }

        public string Restricao { get; set; }

        public virtual DetranRioVeiculoOrigemRestricaoViewModel DetranRioVeiculoOrigemRestricao { get; set; } = new();
    }
}