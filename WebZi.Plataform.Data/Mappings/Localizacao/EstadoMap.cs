using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Localizacao;

namespace WebZi.Plataform.Data.Mappings.Localizacao
{
    public class EstadoMap : IEntityTypeConfiguration<EstadoModel>
    {
        public void Configure(EntityTypeBuilder<EstadoModel> builder)
        {
            builder
                .ToTable("tb_glo_loc_estados", "dbo")
                .HasKey(e => e.EstadoId);

            builder.Property(e => e.EstadoId)
                .HasColumnName("EstadoId");

            builder.Property(e => e.UF)
                .IsRequired()
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("uf");

            builder.Property(e => e.PaisNumcode)
                .IsRequired()
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("pais_numcode");

            builder.Property(e => e.RegiaoId)
                .IsRequired()
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("regiao");

            builder.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nome");

            builder.Property(e => e.NomePtbr)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nome_ptbr");

            builder.Property(e => e.Capital)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("capital");

            builder.Property(e => e.UtcId)
                .HasColumnName("id_utc");

            builder.Property(e => e.UtcVeraoId)
                .HasColumnName("id_utc_verao");

            builder.HasOne(d => d.Pais).WithMany(p => p.Estados)
                .HasForeignKey(d => d.PaisNumcode)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("fk_tb_glo_loc_estados1");
        }
    }
}