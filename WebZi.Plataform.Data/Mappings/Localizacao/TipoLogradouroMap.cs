using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Localizacao;

namespace WebZi.Plataform.Data.Mappings.Localizacao
{
    public class TipoLogradouroMap : IEntityTypeConfiguration<TipoLogradouroModel>
    {
        public void Configure(EntityTypeBuilder<TipoLogradouroModel> builder)
        {
            builder
                .ToTable("tb_glo_loc_tipos_logradouros", "dbo")
                .HasKey(e => e.TipoLogradouroId);

            builder.Property(e => e.TipoLogradouroId)
                .ValueGeneratedOnAdd()
                .HasColumnName("id_tipo_logradouro");
            
            builder.Property(e => e.Codigo)
                .IsRequired()
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("codigo");
            
            builder.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("descricao");
        }
    }
}