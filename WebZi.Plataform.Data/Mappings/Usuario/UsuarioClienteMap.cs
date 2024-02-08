using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Usuario;

namespace WebZi.Plataform.Data.Mappings.Usuario
{
    public class UsuarioClienteMap : IEntityTypeConfiguration<UsuarioClienteModel>
    {
        public void Configure(EntityTypeBuilder<UsuarioClienteModel> builder)
        {
            builder
                .ToTable("tb_dep_usuarios_clientes", "dbo", tb => tb.HasTrigger("tr_log_upd_usuarios_clientes"))
                .HasKey(x => x.UsuarioClienteId);

            builder.Property(e => e.UsuarioClienteId)
                .HasColumnName("id_usuario_cliente")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.ClienteId)
                .HasColumnName("id_cliente");

            builder.Property(e => e.UsuarioId)
                .HasColumnName("id_usuario");

            builder.Property(e => e.UsuarioCadastroId)
                .HasColumnName("id_usuario_cadastro");

            builder.Property(e => e.DataCadastro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime")
                .HasColumnName("data_cadastro");

            builder.HasOne(d => d.Usuario).WithMany(p => p.ListagemUsuarioCliente)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientNoAction);

            builder.HasOne(d => d.UsuarioCadastro).WithMany(p => p.ListagemUsuarioClienteCadastro)
                .HasForeignKey(d => d.UsuarioCadastroId)
                .OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}