using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Usuario;

namespace WebZi.Plataform.Data.Mappings.Usuario
{
    public class UsuarioPermissaoMap : IEntityTypeConfiguration<UsuarioPermissaoModel>
    {
        public void Configure(EntityTypeBuilder<UsuarioPermissaoModel> builder)
        {
            builder
                .ToTable("tb_dep_usuarios_permissoes", "dbo")
                .HasKey(e => e.UsuarioPermissaoId);

            builder.Property(e => e.UsuarioPermissaoId)
                .HasColumnName("id_usuario_permissao")
                .ValueGeneratedOnAdd();
            
            builder.Property(e => e.TipoPermissaoId)
                .HasColumnName("id_tipo_permissao");
            
            builder.Property(e => e.UsuarioId)
                .HasColumnName("id_usuario");

            builder.Property(e => e.UsuarioCadastroId)
                .HasColumnName("id_usuario_cadastro");

            builder.Property(e => e.UsuarioAlteracaoId)
                .HasColumnName("id_usuario_alteracao");

            builder.Property(e => e.DataCadastro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime")
                .HasColumnName("data_cadastro");

            builder.Property(e => e.DataAlteracao)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_alteracao");

            builder.Property(e => e.FlagAtivo)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('S')")
                .IsFixedLength()
                .HasColumnName("flag_ativo");

            builder.HasOne(d => d.Usuario).WithMany(p => p.ListagemUsuarioPermissao)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientNoAction);

            builder.HasOne(d => d.UsuarioCadastro).WithMany(p => p.ListagemUsuarioPermissaoCadastro)
                .HasForeignKey(d => d.UsuarioCadastroId)
                .OnDelete(DeleteBehavior.ClientNoAction);

            builder.HasOne(d => d.UsuarioAlteracao).WithMany(p => p.ListagemUsuarioPermissaoAlteracao)
                .HasForeignKey(d => d.UsuarioAlteracaoId)
                .OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}