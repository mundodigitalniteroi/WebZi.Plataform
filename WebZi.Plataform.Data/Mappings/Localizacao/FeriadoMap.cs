using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Localizacao;

namespace WebZi.Plataform.Data.Mappings.Localizacao
{
    public class FeriadoMap : IEntityTypeConfiguration<FeriadoModel>
    {
        public void Configure(EntityTypeBuilder<FeriadoModel> builder)
        {
            builder
                .ToTable("tb_glo_loc_feriados", "dbo")
                .HasKey(e => e.FeriadoId);

            builder.Property(e => e.FeriadoId)
                .HasColumnName("id_feriado")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.UF)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("uf");

            builder.Property(e => e.MunicipioId)
                .HasColumnName("id_municipio");

            builder.Property(e => e.Dia)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("dia");

            builder.Property(e => e.Mes)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("mes");

            builder.Property(e => e.Ano)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("ano");
            
            builder.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descricao");
            
            builder.Property(e => e.FlagFeriadoEstadual)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_feriado_estadual");
            
            builder.Property(e => e.FlagFeriadoNacional)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_feriado_nacional");

            builder.HasOne(d => d.Estado).WithMany(p => p.Feriados)
                .HasPrincipalKey(p => p.UF)
                .HasForeignKey(d => d.UF)
                .HasConstraintName("fk_tb_glo_loc_feriados1");

            builder.HasOne(d => d.Municipio).WithMany(p => p.Feriados)
                .HasForeignKey(d => d.MunicipioId)
                .HasConstraintName("fk_tb_glo_loc_feriados2");
        }
    }
}