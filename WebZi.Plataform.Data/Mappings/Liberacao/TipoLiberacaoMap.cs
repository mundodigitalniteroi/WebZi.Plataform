using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Liberacao;

namespace WebZi.Plataform.Data.Mappings.Liberacao
{
    public class TipoLiberacaoMap : IEntityTypeConfiguration<TipoLiberacaoModel>
    {
        public void Configure(EntityTypeBuilder<TipoLiberacaoModel> builder)
        {
            builder
                .ToTable("tb_dep_liberacao_tipo", "dbo")
                .HasKey(e => e.TipoLiberacaoId);

            builder.Property(e => e.TipoLiberacaoId)
                .HasColumnName("id_liberacao_tipo")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Descricao)
                .HasColumnName("descricao")
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsRequired();
        }
    }
}