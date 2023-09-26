using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Faturamento;

namespace WebZi.Plataform.Data.Mappings.Faturamento
{
    public class TipoMeioCobrancaMap : IEntityTypeConfiguration<TipoMeioCobrancaModel>
    {
        public void Configure(EntityTypeBuilder<TipoMeioCobrancaModel> builder)
        {
            builder
                .ToTable("tb_dep_tipos_meios_cobrancas", "dbo")
                .HasKey(e => e.TipoMeioCobrancaId);

            builder.Property(e => e.TipoMeioCobrancaId)
                .HasColumnName("id_tipo_meio_cobranca")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Alias)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("alias");

            builder.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descricao");

            builder.Property(e => e.DocumentoImpressao)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("documento_impressao");

            builder.Property(e => e.CodigoERP)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("codigo_sap");

            builder.Property(e => e.FlagBanco)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('S')")
                .IsFixedLength()
                .HasColumnName("flag_banco");

            builder.Property(e => e.FlagPossuiCodigoAutorizacaoCartao)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_possui_codigo_autorizacao_cartao");

            builder.Property(e => e.FlagAtivo)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_ativo");
        }
    }
}