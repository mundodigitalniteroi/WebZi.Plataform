using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Domain.Models.Banco.PIX.Dinamico;

namespace WebZi.Plataform.Data.Mappings.Banco.PIX.Dinamico
{
    public class PixDinamicoTipoStatusGeracaoMap : IEntityTypeConfiguration<PixDinamicoTipoStatusGeracaoModel>
    {
        public void Configure(EntityTypeBuilder<PixDinamicoTipoStatusGeracaoModel> builder)
        {
            builder
                .ToTable("tb_dep_pix_dinamico_tipo_status_geracao", "dbo")
                .HasKey(e => e.PixDinamicoTipoStatusGeracaoId);

            builder.Property(e => e.PixDinamicoTipoStatusGeracaoId)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            
            builder.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasComment("A: O PIX foi enviado com sucesso ao Banco e está sendo processado;\r\nC: O PIX foi transferido;\r\nR: O PIX não foi transferido.");
        }
    }
}