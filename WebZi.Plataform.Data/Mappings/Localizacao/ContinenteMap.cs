using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Localizacao;

namespace WebZi.Plataform.Data.Mappings.Localizacao
{
    public class ContinenteMap : IEntityTypeConfiguration<ContinenteModel>
    {
        public void Configure(EntityTypeBuilder<ContinenteModel> builder)
        {
            builder
                .ToTable("tb_glo_loc_continentes", "dbo")
                .HasKey(e => e.ContinenteId);

            builder.Property(e => e.ContinenteId)
                .HasColumnName("continente");

            builder.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nome");

            builder.Property(e => e.NomePtbr)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nome_ptbr");
        }
    }
}