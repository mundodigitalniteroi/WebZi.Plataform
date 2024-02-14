using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Banco.PIX.Dinamico.Persistencia;

namespace WebZi.Plataform.Data.Mappings.Banco.PIX.Dinamico
{
    public class PixDinamicoConfiguracaoMap : IEntityTypeConfiguration<PixDinamicoConfiguracaoModel>
    {
        public void Configure(EntityTypeBuilder<PixDinamicoConfiguracaoModel> builder)
        {
            builder
                .ToTable("tb_dep_pix_dinamico_configuracao", "dbo")
                .HasKey(x => x.PixDinamicoConfiguracaoId);

            builder.Property(e => e.PixDinamicoConfiguracaoId)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.BaseUrl)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Certificate)
                .HasColumnType("text");

            builder.Property(e => e.ClientId)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.ClientSecret)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.PixChave)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.SenhaCertificado)
                .HasMaxLength(32)
                .IsUnicode(false);

            builder.Property(e => e.DataCadastro)
                .IsRequired()
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime");

            builder.Property(e => e.DataAlteracao)
                .HasColumnType("smalldatetime");
        }
    }
}