using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.GRV;

namespace WebZi.Plataform.Data.Mappings.GRV
{
    public class AutoridadesDivisoesMap : IEntityTypeConfiguration<AutoridadeDivisaoModel>
    {
        public void Configure(EntityTypeBuilder<AutoridadeDivisaoModel> builder)
        {
            builder.ToTable("tb_glo_doc_autoridades_divisoes", "dbo")
                .HasKey(x => x.AutoridadeDivisaoId);

            builder.Property(x => x.AutoridadeDivisaoId)
                .HasColumnName("id_autoridade_divisao")
                .HasColumnType("tinyint")
                .IsRequired();

            builder.Property(x => x.Descricao)
                .HasColumnName("descricao")
                .HasMaxLength(25)
                .IsRequired();
        }
    }
}
