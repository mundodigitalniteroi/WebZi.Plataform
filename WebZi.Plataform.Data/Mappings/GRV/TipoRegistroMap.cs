using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.GRV;

namespace WebZi.Plataform.Data.Mappings.GRV
{
    public class TipoRegistroMap : IEntityTypeConfiguration<TipoRegistroModel>
    {
        public void Configure(EntityTypeBuilder<TipoRegistroModel> builder)
        {
            builder.ToTable("tb_dep_grv_drfa_tipo_registro", "dbo")
                .HasKey(x => x.IdentificadorTipoRegistro);

            builder.Property(x => x.IdentificadorTipoRegistro)
                .HasColumnName("id_grv_drfa_tipo_registro");
            builder.Property(x => x.Codigo)
                .HasColumnName("codigo")
                .HasMaxLength(20)
                .IsRequired();
            builder.Property(x => x.Descricao)
                .HasColumnName("descricao")
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
