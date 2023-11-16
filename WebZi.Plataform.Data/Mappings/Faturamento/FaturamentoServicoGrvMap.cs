using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Faturamento;

namespace WebZi.Plataform.Data.Mappings.Faturamento
{
    public class FaturamentoServicoGrvMap : IEntityTypeConfiguration<FaturamentoServicoGrvModel>
    {
        public void Configure(EntityTypeBuilder<FaturamentoServicoGrvModel> builder)
        {
            builder
                .ToTable("tb_dep_faturamento_servicos_grv", "dbo")
                .HasKey(e => e.FaturamentoServicoGrvId);

            builder.Property(e => e.FaturamentoServicoGrvId)
                .HasColumnName("id_faturamento_servico_grv")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.FaturamentoServicoTipoVeiculoId)
                .HasColumnName("id_faturamento_servico_tipo_veiculo");

            builder.Property(e => e.GrvId)
                .HasColumnName("id_grv");

            builder.Property(e => e.UsuarioDescontoId)
                .HasColumnName("id_usuario_desconto");

            builder.Property(e => e.ObservacaoDesconto)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("observacao_desconto");

            builder.Property(e => e.OrigemCadastro)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('G')")
                .IsFixedLength()
                .HasColumnName("origem_cadastro");

            builder.Property(e => e.QuantidadeDesconto)
                .HasColumnName("quantidade_desconto");

            builder.Property(e => e.TempoTrabalhado)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("tempo_trabalhado");

            builder.Property(e => e.TipoDesconto)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("tipo_desconto");

            builder.Property(e => e.Valor)
                .HasColumnType("smallmoney")
                .HasColumnName("valor");

            builder.Property(e => e.ValorDesconto)
                .HasColumnType("money")
                .HasColumnName("valor_desconto");

            builder.Property(e => e.FlagRealizarCobranca)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('S')")
                .IsFixedLength()
                .HasColumnName("flag_realizar_cobranca");

            builder.HasOne(d => d.Grv)
                .WithMany(p => p.ListagemFaturamentoServicoGrv)
                .HasForeignKey(d => d.GrvId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}