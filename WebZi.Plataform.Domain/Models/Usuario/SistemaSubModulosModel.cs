namespace WebZi.Plataform.Domain.Models.Usuario;

public class SistemaSubModulosModel
{
    public byte IdSubModulo { get; set; }
    public byte IdModulo { get; set; }
    public long Menu { get; set; }
    public long Formulario { get; set; }
    public long Descricao { get; set; }
    public Guid Icone { get; set; }
    public char Status { get; set; }
    public byte Ordenacao { get; set; }
    public SistemaModulosModel SistemaModulos { get; set; } = null!;
}