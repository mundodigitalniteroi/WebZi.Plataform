using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Usuario;

namespace WebZi.Plataform.Data.Mappings.Usuario;

public class SistemaSubModulosMap : IEntityTypeConfiguration<SistemaSubModulosModel>
{
    public void Configure(EntityTypeBuilder<SistemaSubModulosModel> builder)
    {
        builder.ToTable("tb_dep_sistema_sub_modulos", "dbo")
            .HasKey(x => x.IdSubModulo);


        builder.Property(x => x.IdSubModulo)
            .HasColumnName("id_sub_modulo")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.IdModulo)
            .HasColumnName("id_modulo");

        builder.Property(x => x.Menu)
            .HasColumnName("menu")
            .HasMaxLength(100);

        builder.Property(x => x.Formulario)
            .HasColumnName("formulario")
            .HasMaxLength(100);

        builder.Property(x => x.Descricao)
            .HasColumnName("descricao")
            .HasMaxLength(100);

        builder.Property(x => x.Icone)
            .HasColumnName("icone")
            .HasColumnType("varbinary(MAX)");

        builder.Property(e => e.Status)
            .IsRequired()
            .HasMaxLength(1)
            .IsUnicode(false)
            .HasDefaultValueSql("('S')")
            .IsFixedLength()
            .HasColumnName("status");

        builder.Property(x => x.Ordenacao)
            .HasColumnName("ordenacao");

        builder.HasOne(x => x.SistemaModulos)
            .WithMany(x => x.SistemaSubModulos)
            .HasForeignKey(x => x.IdModulo)
            .OnDelete(DeleteBehavior.Restrict);
    }
}