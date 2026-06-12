using WebZi.Plataform.Domain.Models.Usuario;

namespace WebZi.Plataform.Data.Mappings.Usuario;

public class SistemaPerfilAcessoSubModulosModel
{
    public byte IdPerfilAcessoSubModulo { get; set; }
    public int IdPerfilAcesso { get; set; }
    public byte IdSubModulo { get; set; }
    public char Crud { get; set; }

    public SistemaPerfilAcessoModel PerfilAcesso { get; set; } = null!;
    public SistemaSubModulosModel SubModulos { get; set; } = null!;
}