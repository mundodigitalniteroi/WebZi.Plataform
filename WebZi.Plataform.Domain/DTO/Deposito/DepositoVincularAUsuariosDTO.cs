namespace WebZi.Plataform.Domain.DTO.Deposito;

public class DepositoVincularAUsuariosDTO
{
    public int IdentificadorDeposito { get; set; }

    public int? IdentificadorCliente { get; set; }
    public string Nome { get; set; }
    public string FlagAtivo { get; set; }
}