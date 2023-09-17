﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Faturamento;

namespace WebZi.Plataform.Data.Mappings.Faturamento
{
    public class FaturamentoBoletoImagemMap : IEntityTypeConfiguration<FaturamentoBoletoImagemModel>
    {
        public void Configure(EntityTypeBuilder<FaturamentoBoletoImagemModel> builder)
        {
            builder
                .ToTable("tb_dep_faturamento_boletos_imagens", "dbo")
                .HasKey(e => e.FaturamentoBoletoImagemId);

            builder.Property(e => e.FaturamentoBoletoImagemId)
                .HasColumnName("id_faturamento_boleto_imagem")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.FaturamentoBoletoId)
                .HasColumnName("id_faturamento_boleto");

            builder.Property(e => e.Imagem)
                .IsRequired()
                .HasColumnName("imagem");
        }
    }
}