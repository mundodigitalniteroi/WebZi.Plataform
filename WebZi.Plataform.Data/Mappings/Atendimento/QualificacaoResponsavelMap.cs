using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Atendimento;

namespace WebZi.Plataform.Data.Mappings.Atendimento
{
    public class QualificacaoResponsavelMap : IEntityTypeConfiguration<QualificacaoResponsavelModel>
    {
        public void Configure(EntityTypeBuilder<QualificacaoResponsavelModel> builder)
        {
            builder
                .ToTable("tb_dep_qualificacao_responsavel", "dbo")
                .HasKey(e => e.QualificacaoResponsavelId);

            builder.Property(e => e.QualificacaoResponsavelId)
                .HasColumnName("id_qualificacao_responsavel")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descricao");
        }
    }
}