namespace WebZi.Plataform.Domain.DTO.GRV.Pesquisa
{
    public class ReboqueSimplificadoDTO
    {
        public int IdentificadorReboque { get; set; }

        public int IdentificadorCliente { get; set; }

        public int IdentificadorDeposito { get; set; }

        public string Placa { get; set; }

        public string FlagAtivo { get; set; }
    }
}