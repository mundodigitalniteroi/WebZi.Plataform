using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Faturamento;

namespace WebZi.Plataform.Data.Mappings.GRV
{
    public class FaturamentoRegraTipoMap : IEntityTypeConfiguration<FaturamentoRegraTipoModel>
    {
        public void Configure(EntityTypeBuilder<FaturamentoRegraTipoModel> builder)
        {
            builder
                .ToTable("tb_dep_faturamento_regras_tipos", "dbo")
                .HasKey(e => e.FaturamentoRegraTipoId);

            builder.Property(e => e.FaturamentoRegraTipoId)
                .HasColumnName("id_faturamento_regra_tipo")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Codigo)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("codigo");

            builder.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descricao");

            builder.Property(e => e.FlagPossuiValor)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('S')")
                .IsFixedLength()
                .HasColumnName("flag_possui_valor");

            builder.Property(e => e.FlagAtivo)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('S')")
                .IsFixedLength()
                .HasColumnName("flag_ativo");
        }
    }
}