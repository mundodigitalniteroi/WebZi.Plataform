namespace WebZi.Plataform.Domain.Models.Banco.ViewModel
{
    public class AgenciaBancariaViewModel
    {
        public short AgenciaBancariaId { get; set; }

        public string CodigoAgencia { get; set; }

        public string ContaCorrente { get; set; }

        public string DigitoVerificador { get; set; }

        public string CodigoCedente { get; set; }

        public string SacadoCarteira { get; set; }

        public string FlagAtivo { get; set; } = "S";
    }
}