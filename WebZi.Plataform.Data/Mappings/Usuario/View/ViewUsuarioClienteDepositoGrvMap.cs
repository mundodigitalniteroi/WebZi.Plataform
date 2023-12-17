using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Views.Usuario;

namespace WebZi.Plataform.Data.Mappings.Usuario.View
{
    public class ViewUsuarioClienteDepositoGrvMap : IEntityTypeConfiguration<ViewUsuarioClienteDepositoGrvModel>
    {
        public void Configure(EntityTypeBuilder<ViewUsuarioClienteDepositoGrvModel> builder)
        {
            builder
                .ToView("vw_dep_usuarios_clientes_depositos_grvs", "dbo")
                .HasKey(x => x.GrvId);

            builder.Property(e => e.Cliente)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.ClienteFlagAtivo)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();

            builder.Property(e => e.Deposito)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.DepositoFlagAtivo)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();

            builder.Property(e => e.FaturamentoProdutoCodigo)
                .IsRequired()
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();

            builder.Property(e => e.Login)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.Matricula)
                .HasMaxLength(15)
                .IsUnicode(false);

            builder.Property(e => e.NumeroFormularioGrv)
                .IsRequired()
                .HasMaxLength(14)
                .IsUnicode(false);

            builder.Property(e => e.UsuarioFlagAtivo)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();

            builder.HasOne(d => d.Grv)
                .WithOne(p => p.UsuarioClienteDepositoGrv)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}