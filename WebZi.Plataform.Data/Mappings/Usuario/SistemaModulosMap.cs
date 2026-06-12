using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Usuario;

namespace WebZi.Plataform.Data.Mappings.Usuario;

public class SistemaModulosMap : IEntityTypeConfiguration<SistemaModulosModel>
{
    public void Configure(EntityTypeBuilder<SistemaModulosModel> builder)
    {
        builder.ToTable("tb_dep_sistema_modulos", "dbo")
            .HasKey(x => x.IdModulo);

        builder.Property(x => x.IdModulo)
            .HasColumnName("id_modulo")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Descricao)
            .HasColumnName("descricao")
            .HasMaxLength(100);
        builder.Property(x => x.Ordenacao)
            .HasColumnName("ordenacao");
        builder.Property(x => x.Menu)
            .HasColumnName("menu")
            .HasMaxLength(100);
    }
}