using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Localizacao;

namespace WebZi.Plataform.Data.Mappings.Localizacao
{
    public class PaisMap : IEntityTypeConfiguration<PaisModel>
    {
        public void Configure(EntityTypeBuilder<PaisModel> builder)
        {
            builder
                .ToTable("tb_glo_loc_paises", "dbo")
                .HasKey(e => e.PaisNumcode);

            builder.Property(e => e.PaisNumcode)
                .HasColumnName("pais_numcode")
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();

            builder.Property(e => e.ContinenteId)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("continente");
            
            builder.Property(e => e.Iso)
                .IsRequired()
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("iso");
            
            builder.Property(e => e.Iso3)
                .IsRequired()
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("iso3");
            
            builder.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nome");
            
            builder.Property(e => e.NomePtbr)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nome_ptbr");
        }
    }
}