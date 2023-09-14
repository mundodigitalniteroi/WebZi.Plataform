using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Faturamento;

namespace WebZi.Plataform.Data.Mappings.Faturamento
{
    public class FaturamentoServicoAssociadoMap : IEntityTypeConfiguration<FaturamentoServicoAssociadoModel>
    {
        public void Configure(EntityTypeBuilder<FaturamentoServicoAssociadoModel> builder)
        {
            builder
                .ToTable("tb_dep_faturamento_servicos_associados", "dbo")
                .HasKey(e => e.IdFaturamentoServicoAssociado);

            builder.Property(e => e.IdFaturamentoServicoAssociado)
                .HasColumnName("id_faturamento_servico_associado")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.IdCliente)
                .HasColumnName("id_cliente");

            builder.Property(e => e.IdDeposito)
                .HasColumnName("id_deposito");

            builder.Property(e => e.IdFaturamentoRegra)
                .HasColumnName("id_faturamento_regra");

            builder.Property(e => e.IdFaturamentoServicoTipo)
                .HasColumnName("id_faturamento_servico_tipo");

            builder.Property(e => e.IdSapTipoComposicao)
                .HasColumnName("id_sap_tipo_composicao");

            builder.Property(e => e.IdUsuarioCadastro)
                .HasColumnName("id_usuario_cadastro");

            builder.Property(e => e.IdUsuarioAlteracao)
                .HasColumnName("id_usuario_alteracao");

            builder.Property(e => e.CnaeId)
                .HasColumnName("CnaeID");

            builder.Property(e => e.DataAlteracao)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_alteracao");

            builder.Property(e => e.DataCadastro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime")
                .HasColumnName("data_cadastro");

            builder.Property(e => e.DataVigenciaFinal)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_vigencia_final");

            builder.Property(e => e.DataVigenciaInicial)
                .HasDefaultValueSql("(CONVERT([date],getdate()+(1),(0)))")
                .HasColumnType("smalldatetime")
                .HasColumnName("data_vigencia_inicial");

            builder.Property(e => e.Descricao)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descricao");

            builder.Property(e => e.DescricaoConfiguracaoNfe)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.FlagCobrarSomentePrimeiraFatura)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_cobrar_somente_primeira_fatura");

            builder.Property(e => e.FlagEnviarInscricaoEstadual)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();

            builder.Property(e => e.FlagEnviarValorIss)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();

            builder.Property(e => e.FlagPermiteAlteracaoValor)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_permite_alteracao_valor");

            builder.Property(e => e.FlagPermiteDesconto)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_permite_desconto");

            builder.Property(e => e.FlagServicoObrigatorio)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_servico_obrigatorio");

            builder.Property(e => e.FormaCobranca)
                .IsRequired()
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasDefaultValueSql("('VA')")
                .IsFixedLength()
                .HasComment("AM: Ambos;\r\nVA: Vigência Atual (Valor Padrão);\r\nVI: Vigência Inicial.")
                .HasColumnName("forma_cobranca");

            builder.Property(e => e.ListaServicoId)
                .HasColumnName("ListaServicoID");

            builder.Property(e => e.PrecoPadrao)
                .HasColumnType("smallmoney")
                .HasColumnName("preco_padrao");

            builder.Property(e => e.PrecoValorMinimo)
                .HasColumnType("smallmoney")
                .HasColumnName("preco_valor_minimo");
        }
    }
}