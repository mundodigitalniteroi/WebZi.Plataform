using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Vistoria;

namespace WebZi.Plataform.Data.Mappings.Vistoria
{
    public class VistoriaStatusMap : IEntityTypeConfiguration<VistoriaStatusModel>
    {
        public void Configure(EntityTypeBuilder<VistoriaStatusModel> builder)
        {
            builder
                .ToTable("tb_dep_grv_vistoria_status", "dbo")
                .HasKey(e => e.VistoriaStatusId);

            builder.Property(e => e.VistoriaStatusId)
                .HasColumnName("id_grv_vistoria_status")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("descricao");
        }
    }
}