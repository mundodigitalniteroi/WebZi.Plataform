using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Localizacao;

namespace WebZi.Plataform.Data.Mappings.Localizacao
{
    public class RegiaoMap : IEntityTypeConfiguration<RegiaoModel>
    {
        public void Configure(EntityTypeBuilder<RegiaoModel> builder)
        {
            builder
                .ToTable("tb_glo_loc_regioes", "dbo")
                .HasKey(e => e.RegiaoId);

            builder.Property(e => e.RegiaoId)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("regiao");

            builder.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("nome");
        }
    }
}