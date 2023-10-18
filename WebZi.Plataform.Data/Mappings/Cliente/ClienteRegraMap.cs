using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Domain.Models.Cliente;

namespace WebZi.Plataform.Data.Mappings.Cliente
{
    public class ClienteRegraMap : IEntityTypeConfiguration<ClienteRegraModel>
    {
        public void Configure(EntityTypeBuilder<ClienteRegraModel> builder)
        {
            builder
                .ToTable("tb_dep_cliente_regras", "dbo")
                .HasKey(e => e.ClienteRegraId);

            builder.Property(e => e.ClienteRegraId)
                .HasColumnName("ClienteRegraID")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.ClienteRegraTipoId)
                .HasColumnName("ClienteRegraTipoID");
            
            builder.Property(e => e.ClienteId)
                .IsRequired()
                .HasColumnName("ClienteID");
            
            builder.Property(e => e.ClienteRegraTipoId)
                .IsRequired()
                .HasColumnName("ClienteRegraTipoID");

            builder.Property(e => e.Valor)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.DataVigenciaInicial)
                .IsRequired()
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime");

            builder.Property(e => e.DataVigenciaFinal)
                .HasColumnType("smalldatetime");
            
            builder.Property(e => e.UsuarioCadastroId)
                .IsRequired()
                .HasColumnName("UsuarioCadastroID");

            builder.Property(e => e.UsuarioAlteracaoId)
                .HasColumnName("UsuarioAlteracaoID");
        }
    }
}