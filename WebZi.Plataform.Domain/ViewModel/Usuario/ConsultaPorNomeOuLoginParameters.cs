namespace WebZi.Plataform.Domain.ViewModel.Usuario;

public class ConsultaPorNomeOuLoginParameters
{
    public string? Login { get; set; }
    public string? Username { get; set; }
    public short? take { get; set; }
    public short? skip { get; set; }
    public bool UsuariosInativos { get; set; }
}