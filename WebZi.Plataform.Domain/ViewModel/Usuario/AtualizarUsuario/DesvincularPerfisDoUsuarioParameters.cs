namespace WebZi.Plataform.Domain.ViewModel.Usuario.AtualizarUsuario;

public class DesvincularPerfisDoUsuarioParameters
{
    public string Login { get; set; }
    public List<int> Perfis { get; set; }
}