namespace WebZi.Plataform.Domain.DTO.Banco
{
    public class AgenciaBancariaDTO
    {
        public short IdentificadorAgenciaBancaria { get; set; }

        public string CodigoAgencia { get; set; }

        public string ContaCorrente { get; set; }

        public string DigitoVerificador { get; set; }

        public string CodigoCedente { get; set; }

        public string SacadoCarteira { get; set; }

        public string FlagAtivo { get; set; }
    }
}