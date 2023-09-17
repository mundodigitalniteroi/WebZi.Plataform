using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Faturamento;

namespace WebZi.Plataform.Data.Mappings.Faturamento
{
    public class FaturamentoComposicaoNotaFiscalMap : IEntityTypeConfiguration<FaturamentoComposicaoNotaFiscalModel>
    {
        public void Configure(EntityTypeBuilder<FaturamentoComposicaoNotaFiscalModel> builder)
        {
            builder
                .ToTable("tb_dep_faturamento_composicao_nf", "dbo", tb => tb.HasTrigger("tr_del_faturamento_composicao_nf"))
                .HasKey(e => e.FaturamentoComposicaoNotaFiscalId);

            builder.Property(e => e.FaturamentoComposicaoNotaFiscalId)
                .HasColumnName("id_faturamento_composicao_nf")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.DataEmissaoNota)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_emissao_nota");
            
            builder.Property(e => e.FaturamentoComposicaoId)
                .HasColumnName("id_faturamento_composicao");
            
            builder.Property(e => e.Nota)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("nota");
        }
    }
}