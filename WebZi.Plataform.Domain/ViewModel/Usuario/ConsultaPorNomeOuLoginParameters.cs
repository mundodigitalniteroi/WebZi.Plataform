namespace WebZi.Plataform.Domain.ViewModel.Usuario;

public class ConsultaPorNomeOuLoginParameters
{
    public string? Login { get; set; }
    public string? Username { get; set; }
    public byte? Take { get; set; }
    public byte? Skip { get; set; }
    public bool UsuariosInativos { get; set; }
}