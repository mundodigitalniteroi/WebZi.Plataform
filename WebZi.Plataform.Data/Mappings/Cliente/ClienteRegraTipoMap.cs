using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Cliente;

namespace WebZi.Plataform.Data.Mappings.Cliente
{
    public class ClienteRegraTipoMap : IEntityTypeConfiguration<ClienteRegraTipoModel>
    {
        public void Configure(EntityTypeBuilder<ClienteRegraTipoModel> builder)
        {
            builder
                .ToTable("tb_dep_cliente_regras_tipos", "dbo")
                .HasKey(e => e.ClienteRegraTipoId);

            builder.Property(e => e.ClienteRegraTipoId)
                .HasColumnName("ClienteRegraTipoID")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Codigo)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.PossuiValor)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('S')")
                .IsFixedLength();

            builder.Property(e => e.Ativo)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('S')")
                .IsFixedLength();
        }
    }
}