using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.GRV.Pesquisa
{
    public class ClienteDepositoFlagParcelamentoDTO
    {
        public MensagemDTO Mensagem { get; set; } = new ();
        public int IdentificadorDeposito { get; set; }

        public int IdentificadorCliente { get; set; }

        public string FlagAtivo { get; set; }
        public char FlagPossuiParcelamento { get; set; }
    }
}