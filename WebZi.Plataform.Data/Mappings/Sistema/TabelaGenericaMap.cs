using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Sistema;

namespace WebZi.Plataform.Data.Mappings.Sistema
{
    public class TabelaGenericaMap : IEntityTypeConfiguration<TabelaGenericaModel>
    {
        public void Configure(EntityTypeBuilder<TabelaGenericaModel> builder)
        {
            builder
                .ToTable("tb_dep_tabela_generica", "dbo")
                .HasKey(x => x.TabelaGenericaId);

            builder.Property(e => e.TabelaGenericaId)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Codigo)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(50);

            builder.Property(e => e.Sigla)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(20);

            builder.Property(e => e.ValorCadastro)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.Sequencia)
                .HasDefaultValueSql("(0)")
                .IsRequired();

            builder.Property(e => e.Descricao)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(100);
        }
    }
}