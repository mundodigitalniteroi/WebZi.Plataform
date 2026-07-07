using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Liberacao;

namespace WebZi.Plataform.Data.Mappings.Liberacao;

public class TipoLiberacaoEspecialMap : IEntityTypeConfiguration<TipoLiberacaoEspecialModel>
{
    public void Configure(EntityTypeBuilder<TipoLiberacaoEspecialModel> builder)
    {
        builder
            .ToTable("tb_dep_liberacao_especial_tipo", "dbo")
            .HasKey(x => x.IdLiberacaoEspecialTipo);
        builder
            .Property(x => x.IdLiberacaoEspecialTipo)
            .HasColumnName("id_liberacao_especial_tipo")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(x => x.Descricao)
            .HasMaxLength(25)
            .HasColumnName("descricao")
            .IsRequired();
    }
}