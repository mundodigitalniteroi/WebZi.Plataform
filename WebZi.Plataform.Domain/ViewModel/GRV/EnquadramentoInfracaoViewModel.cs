namespace WebZi.Plataform.Domain.ViewModel.GRV
{
    public class EnquadramentoInfracaoViewModel
    {
        public decimal IdentificadorEnquadramentoInfracao { get; set; }

        public string CodigoInfracao { get; set; }

        public short? Artigo { get; set; }

        public string Inciso { get; set; }

        public string Descricao { get; set; }

        public string FlagAtivo { get; set; }
    }
}