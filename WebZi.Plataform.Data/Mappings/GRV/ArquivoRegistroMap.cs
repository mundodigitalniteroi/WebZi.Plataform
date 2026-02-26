using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.GRV.DRFA;

namespace WebZi.Plataform.Data.Mappings.GRV
{
    public class ArquivoRegistroMap : IEntityTypeConfiguration<ArquivoRegistroModel>
    {
        public void Configure(EntityTypeBuilder<ArquivoRegistroModel> builder)
        {
            builder.ToTable("tb_dep_grv_drfa_arquivo_registro", "dbo")
                .HasKey(x => x.GrvDRFAAqruivoRegistroId);

            builder.Property(x => x.GrvDRFAAqruivoRegistroId)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.GrvDRFAId)
                .IsRequired();

            builder.Property(x => x.NomeArquivo)
                .HasMaxLength(255);

            builder.Property(x => x.ArquivoRegistro)
                .HasColumnType("varbinary");

            builder.Property(x => x.TipoArquivo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsRequired();
        }
    }
}
