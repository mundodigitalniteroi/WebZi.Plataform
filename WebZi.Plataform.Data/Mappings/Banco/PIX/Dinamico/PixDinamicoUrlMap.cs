﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Banco.PIX.Dinamico.Persistencia;

namespace WebZi.Plataform.Data.Mappings.Banco.PIX.Dinamico
{
    public class PixDinamicoUrlMap : IEntityTypeConfiguration<PixDinamicoUrlModel>
    {
        public void Configure(EntityTypeBuilder<PixDinamicoUrlModel> builder)
        {
            builder
                .ToTable("tb_dep_pix_dinamico_urls", "dbo")
                .HasKey(x => x.PixDinamicoUrlsId);

            builder.Property(e => e.PixDinamicoUrlsId)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.NomeMetodo)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false);

            builder.Property(e => e.UrlApi)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
        }
    }
}