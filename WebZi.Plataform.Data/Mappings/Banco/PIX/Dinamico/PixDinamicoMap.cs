using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Banco.PIX.Dinamico.Persistencia;

namespace WebZi.Plataform.Data.Mappings.Banco.PIX.Dinamico
{
    public class PixDinamicoMap : IEntityTypeConfiguration<PixDinamicoModel>
    {
        public void Configure(EntityTypeBuilder<PixDinamicoModel> builder)
        {
            builder
                .ToTable("tb_dep_pix_dinamico", "dbo")
                .HasKey(x => x.PixDinamicoId);

            builder.Property(e => e.PixDinamicoId)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.CalendarioCriacao)
                .HasColumnType("datetime");

            builder.Property(e => e.Chave)
                .HasMaxLength(36)
                .IsUnicode(false);

            builder.Property(e => e.Devedor)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.InfoAdicionais)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Json)
                .IsRequired()
                .HasColumnType("text");

            builder.Property(e => e.Location)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.PagadorCnpj)
                .HasMaxLength(14)
                .IsUnicode(false);

            builder.Property(e => e.PagadorCpf)
                .HasMaxLength(11)
                .IsUnicode(false);

            builder.Property(e => e.PagadorNome)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Pix)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.PixHorario)
                .HasColumnType("datetime");

            builder.Property(e => e.QrCode)
                .HasColumnType("text");

            builder.Property(e => e.QrString)
                .HasColumnType("text");

            builder.Property(e => e.SolicitacaoPagador)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.TipoCobranca)
                .HasMaxLength(10)
                .IsUnicode(false);

            builder.Property(e => e.TransactionId)
                .IsRequired()
                .HasMaxLength(32)   
                .IsUnicode(false)
                .HasColumnName("TxId");

            builder.Property(e => e.ValorOriginal)
                .HasColumnType("smallmoney");

            builder.Property(e => e.DataCadastro)
                .IsRequired()
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime");

            builder.Property(e => e.DataAlteracao)
                .HasColumnType("smalldatetime");
        }
    }
}