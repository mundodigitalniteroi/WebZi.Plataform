using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.GRV;

namespace WebZi.Plataform.Data.Mappings.GRV
{
    public class StatusOperacaoMap : IEntityTypeConfiguration<StatusOperacaoModel>
    {
        public void Configure(EntityTypeBuilder<StatusOperacaoModel> builder)
        {
            builder
                .ToTable("tb_dep_status_operacoes", "dbo")
                .HasKey(e => e.StatusOperacaoId);

            builder.Property(e => e.StatusOperacaoId)
                .HasColumnName("id_status_operacao")
                .HasMaxLength(1)
                .IsRequired()
                .IsFixedLength();

            builder.Property(e => e.Descricao)
                .HasColumnName("descricao")
                .IsUnicode(false)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.Sequencia)
                .HasColumnName("sequencia")
                .IsUnicode(false);

            builder.Property(e => e.FlagVeiculoApreendido)
                .HasColumnName("flag_veiculo_apreendido")
                .IsUnicode(false)
                .HasMaxLength(1)
                .HasDefaultValueSql("('S')")
                .IsRequired()
                .IsFixedLength();

            builder.Property(e => e.FlagLeilao)
                .HasColumnName("flag_leilao")
                .IsUnicode(false)
                .HasMaxLength(1)
                .HasDefaultValueSql("('N')")
                .IsRequired()
                .IsFixedLength();
        }
    }
}