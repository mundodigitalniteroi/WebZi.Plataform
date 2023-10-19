﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Domain.Models.Vistoria;

namespace WebZi.Plataform.Data.Mappings.Vistoria
{
    public class VistoriaSituacaoChassiMap : IEntityTypeConfiguration<VistoriaSituacaoChassiModel>
    {
        public void Configure(EntityTypeBuilder<VistoriaSituacaoChassiModel> builder)
        {
            builder
                .ToTable("tb_dep_grv_vistoria_situacao_chassi", "dbo")
                .HasKey(e => e.VistoriaSituacaoChassiId);

            builder.Property(e => e.VistoriaSituacaoChassiId)
                .HasColumnName("id_grv_vistoria_situacao_chassi")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("descricao");
        }
    }
}