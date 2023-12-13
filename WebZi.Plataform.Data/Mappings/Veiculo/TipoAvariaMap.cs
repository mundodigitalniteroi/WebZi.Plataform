using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Veiculo;

namespace WebZi.Plataform.Data.Mappings.Veiculo
{
    public class TipoAvariaMap : IEntityTypeConfiguration<TipoAvariaModel>
    {
        public void Configure(EntityTypeBuilder<TipoAvariaModel> builder)
        {
            builder
                .ToTable("tb_dep_pre_grv_tipo_avarias", "dbo")
                .HasKey(e => e.TipoAvariaId);

            builder.Property(e => e.TipoAvariaId)
                .HasColumnName("id_pre_grv_avarias")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Descricao)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("descricao");
        }
    }
}