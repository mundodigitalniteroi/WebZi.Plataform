using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Faturamento;

namespace WebZi.Plataform.Data.Mappings.Faturamento
{
    public class FaturamentoProdutoMap : IEntityTypeConfiguration<FaturamentoProdutoModel>
    {
        public void Configure(EntityTypeBuilder<FaturamentoProdutoModel> builder)
        {
            builder
                .ToTable("tb_dep_faturamento_produtos", "dbo")
                .HasKey(x => x.FaturamentoProdutoId);

            builder.Property(e => e.FaturamentoProdutoId)
                .HasColumnName("faturamento_produto_codigo");

            builder.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descricao");

            builder.Property(e => e.FlagSolicitacaoReboque)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_solicitacao_reboque");
        }
    }
}