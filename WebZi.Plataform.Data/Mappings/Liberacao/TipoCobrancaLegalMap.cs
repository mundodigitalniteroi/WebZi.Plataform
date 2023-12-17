using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Liberacao;

namespace WebZi.Plataform.Data.Mappings.Liberacao
{
    public class TipoCobrancaLegalMap : IEntityTypeConfiguration<TipoCobrancaLegalModel>
    {
        public void Configure(EntityTypeBuilder<TipoCobrancaLegalModel> builder)
        {
            builder
                .ToTable("tb_dep_tipos_cobrancas_legais", "dbo")
                .HasKey(x => x.TipoCobrancaLegalId);

            builder.Property(e => e.TipoCobrancaLegalId)
                .HasColumnName("id_tipo_cobranca_legal")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Descricao)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("descricao");
        }
    }
}