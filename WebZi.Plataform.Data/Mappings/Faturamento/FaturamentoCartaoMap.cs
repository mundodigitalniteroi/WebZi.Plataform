using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Faturamento;

namespace WebZi.Plataform.Data.Mappings.Faturamento
{
    public class FaturamentoCartaoMap : IEntityTypeConfiguration<FaturamentoCartaoModel>
    {
        public void Configure(EntityTypeBuilder<FaturamentoCartaoModel> builder)
        {
            builder
                .ToTable("tb_dep_faturamento_cartao", "dbo")
                .HasKey(x => x.FaturamentoCartaoId);

            builder.Property(e => e.FaturamentoCartaoId)
                .HasColumnName("id_faturamento_cartao")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.FaturamentoId)
                .HasColumnName("id_faturamento");

            builder.Property(e => e.UsuarioCadastroId)
                .HasColumnName("id_usuario_cadastro");

            builder.Property(e => e.ReferenceId)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("referenceId");

            builder.Property(e => e.Valor)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("valor");

            builder.Property(e => e.DataIntencao)
                .HasColumnType("datetime")
                .HasColumnName("data_intencao");

            builder.Property(e => e.DataExpiration)
                .HasColumnType("datetime")
                .HasColumnName("data_expiration");
        }
    }
}