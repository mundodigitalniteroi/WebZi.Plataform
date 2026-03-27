using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Usuario;

namespace WebZi.Plataform.Data.Mappings.Usuario
{
    public class SistemaPerfilAcessoMap : IEntityTypeConfiguration<SistemaPerfilAcessoModel>
    {
        public void Configure(EntityTypeBuilder<SistemaPerfilAcessoModel> builder)
        {
            builder.ToTable("tb_dep_sistema_perfil_acesso", "dbo", b => b.HasTrigger("tb_log_sistema_perfil_acesso"))
                .HasKey(x => x.PerfilAcessoId);

            builder.Property(x => x.PerfilAcessoId)
                .HasColumnName("id_perfil_acesso")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.UsuarioCadastroId)
                .HasColumnName("id_usuario_cadastro")
                .IsRequired();

            builder.Property(x => x.UsuarioAlteracaoId)
                .HasColumnName("id_usuario_alteracao");

            builder.Property(x => x.Descricao)
                .HasColumnName("descricao")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.DataCadastro)
                .HasColumnName("data_cadastro")
                .HasColumnType("smalldatetime")
                .IsRequired();

            builder.Property(x => x.DataAlteracao)
                .HasColumnName("data_alteracao")
                .HasColumnType("smalldatetime");

            builder.Property(x => x.FlagAtivo)
                .HasMaxLength(1)
                .HasColumnName("flag_ativo");

        }
    }
}
