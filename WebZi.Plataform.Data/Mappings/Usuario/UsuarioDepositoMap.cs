using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Usuario;

namespace WebZi.Plataform.Data.Mappings.Usuario
{
    public class UsuarioDepositoMap : IEntityTypeConfiguration<UsuarioDepositoModel>
    {
        public void Configure(EntityTypeBuilder<UsuarioDepositoModel> builder)
        {
            builder
                .ToTable("tb_dep_usuarios_depositos", "dbo", tb => tb.HasTrigger("tr_log_upd_usuarios_depositos"))
                .HasKey(x => x.UsuarioDepositoId);

            builder.Property(e => e.UsuarioDepositoId)
                .HasColumnName("id_usuario_deposito")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.DepositoId)
                .HasColumnName("id_deposito");

            builder.Property(e => e.UsuarioId)
                .HasColumnName("id_usuario");

            builder.Property(e => e.UsuarioCadastroId)
                .HasColumnName("id_usuario_cadastro");

            builder.Property(e => e.DataCadastro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime")
                .HasColumnName("data_cadastro");

            builder.HasOne(d => d.Usuario)
                .WithMany(p => p.ListagemUsuarioDeposito)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientNoAction);

            builder.HasOne(d => d.UsuarioCadastro)
                .WithMany(p => p.ListagemUsuarioDepositoCadastro)
                .HasForeignKey(d => d.UsuarioCadastroId)
                .OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}