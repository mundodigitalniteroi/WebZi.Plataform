using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Usuario;

namespace WebZi.Plataform.Data.Mappings.Usuario
{
    public class SistemaPerfilAcessoUsuariosMap : IEntityTypeConfiguration<SistemaPerfilAcessoUsuariosModel>
    {
        public void Configure(EntityTypeBuilder<SistemaPerfilAcessoUsuariosModel> builder)
        {
            builder.ToTable("tb_dep_sistema_perfil_acesso_usuarios", "dbo")
                .HasKey(x => x.PerfilUsuarioAcessoId);

            builder.Property(x => x.PerfilUsuarioAcessoId)
                .HasColumnName("id_perfil_acesso_usuario")
                .IsRequired()
                .ValueGeneratedOnAdd();


            builder.Property(x => x.PerfilAcessoId)
                .HasColumnName("id_perfil_acesso")
                .IsRequired();
                

            builder.Property(x => x.UsuarioId)
                .HasColumnName("id_usuario")
                .IsRequired();


        }
    }
}
