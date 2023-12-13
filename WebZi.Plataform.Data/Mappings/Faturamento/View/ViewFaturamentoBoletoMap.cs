using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Views.Faturamento;

namespace WebZi.Plataform.Data.Mappings.Faturamento.View
{
    internal class ViewFaturamentoBoletoMap : IEntityTypeConfiguration<ViewFaturamentoBoletoModel>
    {
        public void Configure(EntityTypeBuilder<ViewFaturamentoBoletoModel> builder)
        {
            builder
                .HasNoKey()
                .ToView("vw_dep_boleto", "dbo");

            builder.Property(e => e.FaturamentoId)
                .HasColumnName("id_faturamento");

            builder.Property(e => e.CedenteAgencia)
                .IsRequired()
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("cedente_agencia");

            builder.Property(e => e.CedenteBancoNome)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .UseCollation("Latin1_General_CI_AS")
                .HasColumnName("cedente_banco_nome");

            builder.Property(e => e.CedenteCodigo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("cedente_codigo");

            builder.Property(e => e.CedenteCodigoFebraban)
                .IsRequired()
                .HasMaxLength(5)
                .IsUnicode(false)
                .UseCollation("Latin1_General_CI_AS")
                .HasColumnName("cedente_codigo_febraban");

            builder.Property(e => e.CedenteContaCorrente)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("cedente_conta_corrente");

            builder.Property(e => e.CedenteDocumento)
                .IsRequired()
                .HasMaxLength(14)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("Latin1_General_CI_AS")
                .HasColumnName("cedente_documento");

            builder.Property(e => e.CedenteDv)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("cedente_dv");

            builder.Property(e => e.CedenteNome)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("Latin1_General_CI_AS")
                .HasColumnName("cedente_nome");

            builder.Property(e => e.CedenteNossoNumero)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("cedente_nosso_numero");

            builder.Property(e => e.NumeroDocumento)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("numero_documento");

            builder.Property(e => e.SacadoBairro)
                .HasMaxLength(39)
                .IsUnicode(false)
                .HasColumnName("sacado_bairro");

            builder.Property(e => e.SacadoCarteira)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("sacado_carteira");

            builder.Property(e => e.SacadoCEP)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("sacado_cep");

            builder.Property(e => e.SacadoCidade)
                .IsRequired()
                .HasMaxLength(29)
                .IsUnicode(false)
                .HasColumnName("sacado_cidade");

            builder.Property(e => e.SacadoDocumento)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("sacado_documento");

            builder.Property(e => e.SacadoEndereco)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("sacado_endereco");

            builder.Property(e => e.SacadoInstrucoes)
                .HasMaxLength(288)
                .IsUnicode(false)
                .HasColumnName("sacado_instrucoes");

            builder.Property(e => e.SacadoNome)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("sacado_nome");

            builder.Property(e => e.SacadoUF)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("sacado_uf");

            builder.Property(e => e.ValorBoleto)
                .HasMaxLength(8000)
                .IsUnicode(false)
                .HasColumnName("valor_boleto");

            builder.Property(e => e.Vencimento)
                .HasMaxLength(64)
                .IsUnicode(false)
                .HasColumnName("vencimento");
        }
    }
}