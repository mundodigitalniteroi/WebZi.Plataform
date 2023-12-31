namespace WebZi.Plataform.Domain.DTO.GRV.Pesquisa
{
    public class ReboquistaSimplificadoDTO
    {
        public int IdentificadorReboquista { get; set; }

        public int IdentificadorCliente { get; set; }

        public int IdentificadorDeposito { get; set; }

        public string Nome { get; set; }

        public string FlagAtivo { get; set; }
    }
}