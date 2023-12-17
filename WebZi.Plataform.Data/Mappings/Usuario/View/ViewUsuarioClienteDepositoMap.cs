using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Views.Usuario;

namespace WebZi.Plataform.Data.Mappings.Usuario.View
{
    internal class ViewUsuarioClienteDepositoMap : IEntityTypeConfiguration<ViewUsuarioClienteDepositoModel>
    {
        public void Configure(EntityTypeBuilder<ViewUsuarioClienteDepositoModel> builder)
        {
            builder
                .ToView("vw_dep_usuarios_clientes_depositos")
                .HasKey(x => x.UsuarioId);

            builder.Property(e => e.UsuarioId)
                .HasColumnName("id_usuario");

            builder.Property(e => e.ClienteId)
                .HasColumnName("id_cliente");

            builder.Property(e => e.DepositoId)
                .HasColumnName("id_deposito");

            builder.Property(e => e.UsuarioClienteId)
                .HasColumnName("id_usuario_cliente");

            builder.Property(e => e.UsuarioDepositoId)
                .HasColumnName("id_usuario_deposito");

            builder.Property(e => e.ClienteNome)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("cliente_nome");

            builder.Property(e => e.ClienteFlagAtivo)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("cliente_flag_ativo");

            builder.Property(e => e.DepositoNome)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("deposito_nome");

            builder.Property(e => e.DepositoFlagAtivo)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("deposito_flag_ativo");

            builder.Property(e => e.Login)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("login");

            builder.Property(e => e.UsuarioFlagAtivo)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("usuario_flag_ativo");

            builder
                .HasOne(d => d.Usuario)
                .WithMany(p => p.ListagemUsuarioClienteDeposito)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}