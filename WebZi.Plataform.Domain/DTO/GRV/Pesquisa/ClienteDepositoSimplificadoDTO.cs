namespace WebZi.Plataform.Domain.DTO.GRV.Pesquisa
{
    public class ClienteDepositoSimplificadoDTO
    {
        public int IdentificadorDeposito { get; set; }

        public int IdentificadorCliente { get; set; }

        public string Nome { get; set; }

        public string FlagAtivo { get; set; }
    }
}