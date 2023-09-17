using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Faturamento;

namespace WebZi.Plataform.Data.Mappings.Faturamento
{
    public class FaturamentoCondicaoPagamentoMap : IEntityTypeConfiguration<FaturamentoCondicaoPagamentoModel>
    {
        public void Configure(EntityTypeBuilder<FaturamentoCondicaoPagamentoModel> builder)
        {
            builder
                .ToTable("tb_dep_faturamento_condicao_pagamento", "dbo")
                .HasKey(e => e.FaturamentoCondicaoPagamentoId);

            builder.Property(e => e.FaturamentoCondicaoPagamentoId)
                .HasColumnName("id_faturamento_condicao_pagamento")
                .ValueGeneratedOnAdd();
            
            builder.Property(e => e.Descricao)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("descricao");
        }
    }
}