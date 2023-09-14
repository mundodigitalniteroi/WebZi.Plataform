using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Faturamento;

namespace WebZi.Plataform.Data.Mappings.Faturamento
{
    public class FaturamentoServicoTipoMap : IEntityTypeConfiguration<FaturamentoServicoTipoModel>
    {
        public void Configure(EntityTypeBuilder<FaturamentoServicoTipoModel> builder)
        {
            builder
                .ToTable("tb_dep_faturamento_servicos_tipos", "dbo")
                .HasKey(e => e.IdFaturamentoServicoTipo);

            builder.Property(e => e.IdFaturamentoServicoTipo)
                .HasColumnName("id_faturamento_servico_tipo")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.IdUsuarioCadastro)
                .HasColumnName("id_usuario_cadastro");

            builder.Property(e => e.IdUsuarioAlteracao)
                .HasColumnName("id_usuario_alteracao");

            builder.Property(e => e.DataAlteracao)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_alteracao");

            builder.Property(e => e.DataCadastro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime")
                .HasColumnName("data_cadastro");

            builder.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descricao");

            builder.Property(e => e.FaturamentoProdutoCodigo)
                .IsRequired()
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasDefaultValueSql("('DEP')")
                .IsFixedLength()
                .HasColumnName("faturamento_produto_codigo");

            builder.Property(e => e.FlagAtivo)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('S')")
                .IsFixedLength()
                .HasColumnName("flag_ativo");

            builder.Property(e => e.FlagCobrancaPorHora)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_cobranca_por_hora");

            builder.Property(e => e.FlagCobrarTelaGrv)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_cobrar_tela_grv");

            builder.Property(e => e.FlagImpressaoAgrupada)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_impressao_agrupada");

            builder.Property(e => e.FlagNaoCobrarSeNaoUsouReboque)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_nao_cobrar_se_nao_usou_reboque");

            builder.Property(e => e.FlagRebocada)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_rebocada");

            builder.Property(e => e.FlagServicoObrigatorio)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_servico_obrigatorio");

            builder.Property(e => e.FlagTributacao)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_tributacao");

            builder.Property(e => e.OrdemImpressao)
                .HasDefaultValueSql("((1))")
                .HasColumnName("ordem_impressao");

            builder.Property(e => e.TipoCobranca)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasComment("TIPOS DE COBRANÇA:\r\nD = Diárias;\r\nH = Quantidade de HH:MM vezes o Preço;\r\nP = Porcentagem;\r\nQ = Quantidade;\r\nT = Tempo entre duas Datas;\r\nV = Valor.")
                .HasColumnName("tipo_cobranca");
        }
    }
}