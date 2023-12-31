namespace WebZi.Plataform.Domain.DTO.Servico
{
    public class ReboqueDTO
    {
        public int IdentificadorReboque { get; set; }

        public int IdentificadorCliente { get; set; }

        public int IdentificadorDeposito { get; set; }

        public string Placa { get; set; }

        public string FlagAtivo { get; set; }
    }
}