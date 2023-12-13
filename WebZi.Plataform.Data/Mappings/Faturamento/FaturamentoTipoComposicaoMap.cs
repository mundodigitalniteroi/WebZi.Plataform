using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Faturamento;

namespace WebZi.Plataform.Data.Mappings.Faturamento
{
    public class FaturamentoTipoComposicaoMap : IEntityTypeConfiguration<FaturamentoTipoComposicaoModel>
    {
        public void Configure(EntityTypeBuilder<FaturamentoTipoComposicaoModel> builder)
        {
            builder
                .ToTable("tb_dep_faturamento_tipo_composicao", "dbo")
                .HasKey(e => e.FaturamentoTipoComposicaoId);

            builder.Property(e => e.FaturamentoTipoComposicaoId)
                .HasColumnName("id_faturamento_tipo_composicao")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.FaturamentoCondicaoPagamentoId)
                .HasColumnName("id_faturamento_condicao_pagamento");

            builder.Property(e => e.CodigoSap)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("codigo_sap");

            builder.Property(e => e.Descricao)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("descricao");

            builder.Property(e => e.DescricaoSap)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descricao_sap");

            builder.Property(e => e.Origem)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("origem");

            builder.Property(e => e.Tipo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("tipo");
        }
    }
}