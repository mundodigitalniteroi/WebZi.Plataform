namespace WebZi.Plataform.Domain.ViewModel.Usuario.CadastroUsuario;

public class CadastroUsuarioParameters
{
    public int identificadorUsuario { get; set; }
    public string Login { get; set; }
    public long identificadorPessoa { get; set; }
    public string Matricula { get; set; }

    public List<PermissoesUsuarioParameters> PermissoesUsuario { get; set; }

    public List<int> PerfisDeAcesso { get; set; }
    public List<int> VincularCliente { get; set; }
    public List<int> VincularDeposito { get; set; }
}