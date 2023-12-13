using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Localizacao;

namespace WebZi.Plataform.Data.Mappings.Localizacao
{
    public class UTCMap : IEntityTypeConfiguration<UTCModel>
    {
        public void Configure(EntityTypeBuilder<UTCModel> builder)
        {
            builder
                .ToTable("tb_glo_loc_utc", "dbo")
                .HasKey(e => e.UtcId);

            builder.Property(e => e.UtcId)
                .ValueGeneratedOnAdd()
                .HasColumnName("id_utc");

            builder.Property(e => e.Utc)
                .IsRequired()
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("utc");
        }
    }
}