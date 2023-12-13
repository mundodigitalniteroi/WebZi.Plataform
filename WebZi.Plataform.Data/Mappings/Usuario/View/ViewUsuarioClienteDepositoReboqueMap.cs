using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Views.Usuario;

namespace WebZi.Plataform.Data.Mappings.Usuario.View
{
    public class ViewUsuarioClienteDepositoReboqueMap : IEntityTypeConfiguration<ViewUsuarioClienteDepositoReboqueModel>
    {
        public void Configure(EntityTypeBuilder<ViewUsuarioClienteDepositoReboqueModel> builder)
        {
            builder.HasNoKey()
                .ToView("vw_dep_usuarios_clientes_depositos_reboques", "dbo");

            builder.Property(e => e.ClienteFlagAtivo)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();

            builder.Property(e => e.ClienteNome)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.DepositoFlagAtivo)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();

            builder.Property(e => e.DepositoNome)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.ReboquePlaca)
                .IsRequired()
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength();

            builder.Property(e => e.UsuarioFlagAtivo)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();

            builder.Property(e => e.ReboqueFlagAtivo)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
        }
    }
}