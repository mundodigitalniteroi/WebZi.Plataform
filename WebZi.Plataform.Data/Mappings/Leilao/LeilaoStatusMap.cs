using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Leilao;

namespace WebZi.Plataform.Data.Mappings.Leilao
{
    public class LeilaoStatusMap : IEntityTypeConfiguration<LeilaoStatusModel>
    {
        public void Configure(EntityTypeBuilder<LeilaoStatusModel> builder)
        {
            builder
                .ToTable("tb_leilao_status", "dbo")
                .HasKey(e => e.LeilaoStatusId);

            builder.Property(e => e.LeilaoStatusId)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Ativo)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('A')")
                .IsFixedLength()
                .HasColumnName("ativo");

            builder.Property(e => e.Descricao)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descricao");

            builder.Property(e => e.ExibeMensagemConferencia)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("exibe_mensagem_conferencia");

            builder.Property(e => e.Sequencia)
                .HasColumnName("sequencia");
        }
    }
}