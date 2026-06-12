using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Usuario;

namespace WebZi.Plataform.Data.Mappings.Usuario;

public class SistemaPerfilAcessoSubModuloMap : IEntityTypeConfiguration<SistemaPerfilAcessoSubModulosModel>
{
    public void Configure(EntityTypeBuilder<SistemaPerfilAcessoSubModulosModel> builder)
    {
        builder.ToTable("tb_dep_sistema_perfil_acesso_sub_modulos", "dbo")
            .HasKey(x => x.IdPerfilAcessoSubModulo);

        builder.Property(x => x.IdPerfilAcessoSubModulo)
            .HasColumnName("id_perfil_acesso_sub_modulo")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.IdPerfilAcesso)
            .HasColumnName("id_perfil_acesso")
            .IsRequired();
        builder.Property(x => x.IdSubModulo)
            .HasColumnName("id_sub_modulo")
            .IsRequired();
        builder.Property(x => x.Crud)
            .IsRequired()
            .HasMaxLength(1)
            .IsUnicode(false)
            .IsFixedLength()
            .HasColumnName("crud");
    }
}