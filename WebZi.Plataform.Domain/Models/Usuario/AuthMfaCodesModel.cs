namespace WebZi.Plataform.Domain.Models.Usuario;

public class AuthMfaCodesModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int UsuarioId { get; set; }
    public string CodeHash { get; set; }
    public DateTime ExpiresAt { get; set; }
    public int Attempts { get; set; }
    public bool Validated { get; set; }
    public DateTime CreatedAt { get; set; }

    public UsuarioModel Usuario { get; set; } = null!;
}