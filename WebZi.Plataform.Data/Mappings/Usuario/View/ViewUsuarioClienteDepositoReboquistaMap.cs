using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Views.Usuario;

namespace WebZi.Plataform.Data.Mappings.Usuario.View
{
    public class ViewUsuarioClienteDepositoReboquistaMap : IEntityTypeConfiguration<ViewUsuarioClienteDepositoReboquistaModel>
    {
        public void Configure(EntityTypeBuilder<ViewUsuarioClienteDepositoReboquistaModel> builder)
        {
            builder
                .HasNoKey()
                .ToView("vw_dep_usuarios_clientes_depositos_reboquistas", "dbo");

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

            builder.Property(e => e.ReboquistaNome)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.UsuarioFlagAtivo)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();

            builder.Property(e => e.ReboquistaFlagAtivo)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
        }
    }
}