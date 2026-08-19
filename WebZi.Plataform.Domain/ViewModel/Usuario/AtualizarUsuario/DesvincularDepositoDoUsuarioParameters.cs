namespace WebZi.Plataform.Domain.ViewModel.Usuario.AtualizarUsuario;

public class DesvincularDepositoDoUsuarioParameters
{
    public string Login { get; set; }
    public List<int> Deposito { get; set; }
}