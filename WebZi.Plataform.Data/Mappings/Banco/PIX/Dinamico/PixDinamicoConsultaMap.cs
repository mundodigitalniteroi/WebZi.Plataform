using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Banco.PIX.Dinamico;

namespace WebZi.Plataform.Data.Mappings.Banco.PIX.Dinamico
{
    public class PixDinamicoConsultaMap : IEntityTypeConfiguration<PixDinamicoConsultaModel>
    {
        public void Configure(EntityTypeBuilder<PixDinamicoConsultaModel> builder)
        {
            builder
                .ToTable("tb_dep_pix_dinamico_consulta", "dbo")
                .HasKey(x => x.PixDinamicoConsultaId);

            builder.Property(e => e.PixDinamicoConsultaId)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Json)
                .IsRequired()
                .HasColumnType("text");

            builder.Property(e => e.DataCadastro)
                .IsRequired()
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime");
        }
    }
}