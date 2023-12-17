using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Leilao;

namespace WebZi.Plataform.Data.Mappings.Leilao
{
    public class LeilaoLoteStatusMap : IEntityTypeConfiguration<LeilaoLoteStatusModel>
    {
        public void Configure(EntityTypeBuilder<LeilaoLoteStatusModel> builder)
        {
            builder
                .ToTable("tb_lotes_status", "dbo")
                .HasKey(x => x.LeilaoLoteStatusId);

            builder.Property(e => e.LeilaoLoteStatusId)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.FlagAtivo)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('S')")
                .IsFixedLength()
                .HasColumnName("ativo");

            builder.Property(e => e.Codigo)
                .HasColumnName("codigo");

            builder.Property(e => e.CodigoGrupo)
                .HasColumnName("codigo_grupo");

            builder.Property(e => e.CorrelacaoDsin)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("correlacao_dsin");

            builder.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("descricao");

            builder.Property(e => e.LeiloadoId)
                .HasColumnName("id_leiloado");

            builder.Property(e => e.NaoLeiloadoId)
                .HasColumnName("id_nao_leiloado");

            builder.Property(e => e.ReaproveitavelId)
                .HasColumnName("id_reaproveitavel");

            builder.Property(e => e.FlagPermiteAlteracao)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('S')")
                .IsFixedLength()
                .HasColumnName("permite_alteracao");

            builder.Property(e => e.PrefixoLote)
                .HasDefaultValueSql("((0))")
                .HasColumnName("prefixo_lote");

            builder.Property(e => e.FlagReaproveitavel)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("reaproveitavel");

            builder.Property(e => e.ValidaLote)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("valida_lote");
        }
    }
}