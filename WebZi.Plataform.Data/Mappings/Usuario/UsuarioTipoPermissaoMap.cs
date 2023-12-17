using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Usuario;

namespace WebZi.Plataform.Data.Mappings.Usuario
{
    public class UsuarioTipoPermissaoMap : IEntityTypeConfiguration<UsuarioTipoPermissaoModel>
    {
        public void Configure(EntityTypeBuilder<UsuarioTipoPermissaoModel> builder)
        {
            builder
                .ToTable("tb_dep_usuarios_tipos_permissoes", "dbo")
                .HasKey(x => x.TipoPermissaoId);

            builder.Property(e => e.TipoPermissaoId)
                .HasColumnName("id_tipo_permissao")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Codigo)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("codigo");

            builder.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("descricao");
        }
    }
}