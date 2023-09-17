using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Localizacao;

namespace WebZi.Plataform.Data.Mappings.Localizacao
{
    public class MunicipioMap : IEntityTypeConfiguration<MunicipioModel>
    {
        public void Configure(EntityTypeBuilder<MunicipioModel> builder)
        {
            builder
                .ToTable("tb_glo_loc_municipios", "dbo")
                .HasKey(e => e.MunicipioId);

            builder.Property(e => e.MunicipioId)
                .HasColumnName("id_municipio")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.CodigoMunicipio)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("codigo_municipio");
            
            builder.Property(e => e.CodigoMunicipioIbge)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("codigo_municipio_ibge");
            
            builder.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(75)
                .IsUnicode(false)
                .HasColumnName("nome");
            
            builder.Property(e => e.NomePtbr)
                .IsRequired()
                .HasMaxLength(75)
                .IsUnicode(false)
                .HasColumnName("nome_ptbr");
            
            builder.Property(e => e.Uf)
                .IsRequired()
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("uf");
        }
    }
}