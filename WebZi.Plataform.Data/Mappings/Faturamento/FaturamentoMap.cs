using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Faturamento;

namespace WebZi.Plataform.Data.Mappings.Faturamento
{
    public class FaturamentoMap : IEntityTypeConfiguration<FaturamentoModel>
    {
        public void Configure(EntityTypeBuilder<FaturamentoModel> builder)
        {
            builder
                .ToTable("tb_dep_faturamento", "dbo")
                .HasKey(e => e.FaturamentoId);

            builder.Property(e => e.FaturamentoId)
                .HasColumnName("id_faturamento")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.HoraDiaria)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("hora_diaria");

            builder.Property(e => e.AtendimentoId)
                .HasColumnName("id_atendimento");

            builder.Property(e => e.TipoMeioCobrancaId)
                .HasDefaultValueSql("((1))")
                .HasColumnName("id_tipo_meio_cobranca");

            builder.Property(e => e.UsuarioAlteracaoId)
                .HasColumnName("id_usuario_alteracao");

            builder.Property(e => e.UsuarioCadastroId)
                .HasColumnName("id_usuario_cadastro");

            builder.Property(e => e.MaximoDiariasParaCobranca)
                .HasColumnName("maximo_diarias_para_cobranca");

            builder.Property(e => e.MaximoDiasVencimento)
                .HasColumnName("maximo_dias_vencimento");

            builder.Property(e => e.NumeroIdentificacao)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("numero_identificacao");

            builder.Property(e => e.NumeroNotaFiscal)
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasColumnName("numero_nota_fiscal");

            builder.Property(e => e.Sequencia)
                .HasDefaultValueSql("((1))")
                .HasColumnName("sequencia");

            builder.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("status");

            builder.Property(e => e.ValorFaturado)
                .HasColumnType("money")
                .HasColumnName("valor_faturado");

            builder.Property(e => e.ValorPagamento)
                .HasColumnType("money")
                .HasColumnName("valor_pagamento");

            builder.Property(e => e.DataCalculo)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_calculo");

            builder.Property(e => e.DataEmissaoDocumento)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_emissao_documento");

            builder.Property(e => e.DataEmissaoNotaFiscal)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_emissao_nota_fiscal");

            builder.Property(e => e.DataPagamento)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_pagamento");

            builder.Property(e => e.DataPrazoRetiradaVeiculo)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_prazo_retirada_veiculo");

            builder.Property(e => e.DataRetroativa)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_retroativa");

            builder.Property(e => e.DataVencimento)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_vencimento");

            builder.Property(e => e.DataCadastro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime")
                .HasColumnName("data_cadastro");

            builder.Property(e => e.DataAlteracao)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_alteracao");

            builder.Property(e => e.FlagClienteRealizaFaturamentoArrecadacao)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_cliente_realiza_faturamento_arrecadacao");

            builder.Property(e => e.FlagCobrarDiariasDiasCorridos)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_cobrar_diarias_dias_corridos");

            builder.Property(e => e.FlagLimitacaoJudicial)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_limitacao_judicial");

            builder.Property(e => e.FlagPermissaoDataRetroativaFaturamento)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_permissao_data_retroativa_faturamento");

            builder.Property(e => e.FlagUsarHoraDiaria)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_usar_hora_diaria");
        }
    }
}