using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.GRV;

namespace WebZi.Plataform.Data.Mappings.GRV
{
    public class GrvFotoMap : IEntityTypeConfiguration<GrvFotoModel>
    {
        public void Configure(EntityTypeBuilder<GrvFotoModel> builder)
        {
            builder
                .ToTable("tb_dep_grv_fotos", "dbo")
                .HasKey(x => x.IdFoto);

            builder.Property(e => e.IdFoto)
                .HasColumnName("id_foto")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.IdGrv)
                .HasColumnName("id_grv");
            builder.Property(e => e.IdUsuarioCadastro)
                .HasColumnName("id_usuario_cadastro");

            builder.Property(e => e.Foto)
                .HasColumnName("foto");

            builder.Property(e => e.TipoFoto)
                .HasColumnName("tipo_foto");

            builder.Property(e => e.DataCadastro)
                .HasColumnName("data_cadastro");
        }
    }
}