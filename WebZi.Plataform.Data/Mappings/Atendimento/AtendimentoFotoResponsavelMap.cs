using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Atendimento;

namespace WebZi.Plataform.Data.Mappings.Atendimento
{
    public class AtendimentoFotoResponsavelMap : IEntityTypeConfiguration<AtendimentoFotoResponsavelModel>
    {
        public void Configure(EntityTypeBuilder<AtendimentoFotoResponsavelModel> builder)
        {
            builder
                .ToTable("tb_dep_atendimento_fotos_responsaveis", "dbo", tb => tb.HasTrigger("tr_del_atendimento_fotos_responsaveis"))
                .HasKey(e => e.AtendimentoFotoResponsavelId);

            builder.Property(e => e.AtendimentoFotoResponsavelId)
                .HasColumnName("id_atendimento_foto_responsavel")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.AtendimentoId)
                .HasColumnName("id_atendimento");

            builder.Property(e => e.Foto)
                .HasColumnName("foto");
        }
    }
}