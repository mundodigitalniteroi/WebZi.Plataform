using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Localizacao;

namespace WebZi.Plataform.Data.Mappings.Localizacao
{
    public class BairroMap : IEntityTypeConfiguration<BairroModel>
    {
        public void Configure(EntityTypeBuilder<BairroModel> builder)
        {
            builder
                .ToTable("tb_glo_loc_bairros", "dbo")
                .HasKey(e => e.BairroId);

            builder.Property(e => e.BairroId)
                .HasColumnName("id_bairro")
                .ValueGeneratedOnAdd();
           
            builder.Property(e => e.MunicipioId)
                .HasColumnName("id_municipio");
            
            builder.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("nome");
            
            builder.Property(e => e.NomePtbr)
                .IsRequired()
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("nome_ptbr");
        }
    }
}