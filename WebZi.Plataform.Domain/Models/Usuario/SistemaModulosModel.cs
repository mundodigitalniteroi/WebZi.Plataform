namespace WebZi.Plataform.Domain.Models.Usuario;

public class SistemaModulosModel
{
    public byte IdModulo { get; set; }
    public string Descricao { get; set; }
    public byte Ordenacao { get; set; }
    public string Menu { get; set; }

    public ICollection<SistemaSubModulosModel> SistemaSubModulos { get; set; } = null!;
}