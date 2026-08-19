namespace WebZi.Plataform.Domain.ViewModel.Usuario.AtualizarUsuario;

public class DesvincularClienteDoUsuarioParameters
{
    public string Login { get; set; }
    public List<int> Clientes { get; set; }
}