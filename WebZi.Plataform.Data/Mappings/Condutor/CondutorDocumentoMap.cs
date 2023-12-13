using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Condutor;

namespace WebZi.Plataform.Data.Mappings.Condutor
{
    public class CondutorDocumentoMap : IEntityTypeConfiguration<CondutorDocumentoModel>
    {
        public void Configure(EntityTypeBuilder<CondutorDocumentoModel> builder)
        {
            builder
                .ToTable("tb_dep_condutor_documentos", "dbo")
                .HasKey(e => e.CondutorDocumentoId);

            builder.Property(e => e.CondutorDocumentoId)
                .HasColumnName("CondutorDocumentoId")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.TipoDocumentoIdentificacaoId)
                .HasColumnName("TipoDocumentoIdentificacaoId")
                .IsRequired();

            builder.Property(e => e.DataCadastro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime");
        }
    }
}