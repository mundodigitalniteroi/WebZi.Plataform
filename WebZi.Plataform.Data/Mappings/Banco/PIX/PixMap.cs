using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Domain.Models.Banco.PIX;

namespace WebZi.Plataform.Data.Mappings.Banco.PIX
{
    public class PixMap : IEntityTypeConfiguration<PixEstaticoModel>
    {
        public void Configure(EntityTypeBuilder<PixEstaticoModel> builder)
        {
            builder
                .ToTable("tb_dep_pix", "dbo")
                .HasKey(e => e.PixId);

            builder.Property(e => e.PixId)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.FaturamentoId)
                .IsRequired();

            builder.Property(e => e.Chave)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.SolicitacaoPagador)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Valor)
                .HasColumnType("smallmoney");

            builder.Property(e => e.MerchantName)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.MerchantCity)
                .IsRequired()
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();

            builder.Property(e => e.QRString)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("QRString");

            builder.Property(e => e.QRCode)
                .HasColumnType("text")
                .HasColumnName("QRCode");

            builder.Property(e => e.DataCadastro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime");
        }
    }
}