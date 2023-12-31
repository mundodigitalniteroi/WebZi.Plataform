﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Sistema;

namespace WebZi.Plataform.Data.Mappings.Sistema
{
    public class ConfiguracaoLogoMap : IEntityTypeConfiguration<ConfiguracaoLogoModel>
    {
        public void Configure(EntityTypeBuilder<ConfiguracaoLogoModel> builder)
        {
            builder
                .ToTable("tb_dep_configuracoes_logo", "dbo")
                .HasKey(x => x.ConfiguracaoLogoId);

            builder.Property(e => e.ConfiguracaoLogoId)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.LogoPadraoSistema);
        }
    }
}