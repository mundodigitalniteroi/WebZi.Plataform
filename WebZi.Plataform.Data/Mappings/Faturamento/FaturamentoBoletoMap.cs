using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Faturamento.Boleto;

namespace WebZi.Plataform.Data.Mappings.Faturamento
{
    public class FaturamentoBoletoMap : IEntityTypeConfiguration<FaturamentoBoletoModel>
    {
        public void Configure(EntityTypeBuilder<FaturamentoBoletoModel> builder)
        {
            builder
                .ToTable("tb_dep_faturamento_boletos", "dbo", tb => tb.HasTrigger("tr_del_faturamento_boletos"))
                .HasKey(e => e.FaturamentoBoletoId);

            builder.Property(e => e.FaturamentoBoletoId)
                .HasColumnName("id_faturamento_boleto")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.DataEmissao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime")
                .HasColumnName("data_emissao");

            builder.Property(e => e.DiasConfiguracaoDataVencimento)
                .HasColumnName("dias_configuracao_data_vencimento");

            builder.Property(e => e.BoletoId)
                .HasColumnName("id_boleto");

            builder.Property(e => e.FaturamentoId)
                .HasColumnName("id_faturamento");

            builder.Property(e => e.UsuarioCadastroId)
                .HasColumnName("id_usuario_cadastro");

            builder.Property(e => e.Linha)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("linha");

            builder.Property(e => e.SequenciaEmissao)
                .HasDefaultValueSql("((1))")
                .HasColumnName("sequencia_emissao");

            builder.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasComment("Status:\r\nN = Não Pago;\r\nP = Pago;\r\nC = Cancelado.")
                .HasColumnName("status");

            builder.Property(e => e.Valor)
                .HasColumnType("money")
                .HasColumnName("valor");

            builder.Property(e => e.Via)
                .HasColumnName("via");
        }
    }
}