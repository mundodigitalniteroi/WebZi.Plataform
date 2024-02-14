using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Banco.PIX.Dinamico.Persistencia;

namespace WebZi.Plataform.Data.Mappings.Banco.PIX.Dinamico
{
    public class PixDinamicoSenhaConfirmacaoTranferenciaMap : IEntityTypeConfiguration<PixDinamicoSenhaConfirmacaoTranferenciaModel>
    {
        public void Configure(EntityTypeBuilder<PixDinamicoSenhaConfirmacaoTranferenciaModel> builder)
        {
            builder
                .ToTable("tb_dep_pix_dinamico_senha_confirmacao_tranferencia", "dbo")
                .HasKey(x => x.PixDinamicoSenhaConfirmacaoTranferenciaId);

            builder.Property(e => e.PixDinamicoSenhaConfirmacaoTranferenciaId)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.FlagConfirmado)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();

            builder.Property(e => e.Senha)
                .IsRequired()
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();

            builder.Property(e => e.SenhaFinanceiro)
                .IsRequired()
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();

            builder.Property(e => e.DataHoraAutorizacaoFinanceiro)
                .HasColumnType("smalldatetime");

            builder.Property(e => e.DataCadastro)
                .IsRequired()
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime");

            builder.Property(e => e.DataAlteracao)
                .HasColumnType("smalldatetime");
        }
    }
}