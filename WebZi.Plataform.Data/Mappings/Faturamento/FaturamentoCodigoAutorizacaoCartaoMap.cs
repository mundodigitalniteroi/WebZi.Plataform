using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Faturamento;

namespace WebZi.Plataform.Data.Mappings.Faturamento
{
    public class FaturamentoCodigoAutorizacaoCartaoMap : IEntityTypeConfiguration<FaturamentoCodigoAutorizacaoCartaoModel>
    {
        public void Configure(EntityTypeBuilder<FaturamentoCodigoAutorizacaoCartaoModel> builder)
        {
            builder
                .ToTable("tb_dep_faturamento_codigo_autorizacao_cartao", "dbo", tb => tb.HasTrigger("tr_del_faturamento_codigo_autorizacao_cartao"))
                .HasKey(e => e.FaturamentoCodigoAutorizacaoCartaoId);

            builder.Property(e => e.FaturamentoCodigoAutorizacaoCartaoId)
                .HasColumnName("id_faturamento_codigo_autorizacao_cartao")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.CodigoAutorizacaoCartao)
                .IsRequired()
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("codigo_autorizacao_cartao");

            builder.Property(e => e.CartaoId).HasColumnName("id_cartao");

            builder.Property(e => e.FaturamentoId).HasColumnName("id_faturamento");

            builder.Property(e => e.NumeroCartao)
                .HasMaxLength(16)
                .IsUnicode(false);

            builder.Property(e => e.Valor)
                .HasColumnType("money")
                .HasColumnName("valor");
        }
    }
}