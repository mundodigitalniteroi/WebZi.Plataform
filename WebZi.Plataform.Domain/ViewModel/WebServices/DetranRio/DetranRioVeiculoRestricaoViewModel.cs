namespace WebZi.Plataform.Domain.ViewModel.WebServices.DetranRio
{
    public class DetranRioVeiculoRestricaoViewModel
    {
        public int IdentificadorDetranVeiculoRestricao { get; set; }

        public string TipoRestricao { get; set; }

        public byte CodigoRestricao { get; set; }

        public string Restricao { get; set; }

        public virtual DetranRioVeiculoOrigemRestricaoViewModel DetranRioVeiculoOrigemRestricao { get; set; }
    }
}