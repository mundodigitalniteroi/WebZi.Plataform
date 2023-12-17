using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Sistema;

namespace WebZi.Plataform.Data.Mappings.Sistema
{
    public class CorMap : IEntityTypeConfiguration<CorModel>
    {
        public void Configure(EntityTypeBuilder<CorModel> builder)
        {
            builder
                .ToTable("tb_glo_sys_cores", "dbo")
                .HasKey(x => x.CorId);

            builder.Property(e => e.CorId)
                .HasColumnName("id_cor")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Cor)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("descricao");

            builder.Property(e => e.CorSecundaria)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("descricao_secundaria");

            builder.Property(e => e.FlagCorPrincipal)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_cor_principal");

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