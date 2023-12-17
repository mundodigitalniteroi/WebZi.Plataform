using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Faturamento;

namespace WebZi.Plataform.Data.Mappings.Faturamento
{
    public class FaturamentoComposicaoMap : IEntityTypeConfiguration<FaturamentoComposicaoModel>
    {
        public void Configure(EntityTypeBuilder<FaturamentoComposicaoModel> builder)
        {
            builder
                .ToTable("tb_dep_faturamento_composicao", "dbo")
                .HasKey(x => x.FaturamentoComposicaoId);

            builder.Property(e => e.FaturamentoComposicaoId)
                .HasColumnName("id_faturamento_composicao")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.FaturamentoId)
                .HasColumnName("id_faturamento");

            builder.Property(e => e.FaturamentoServicoTipoVeiculoId)
                .HasColumnName("id_faturamento_servico_tipo_veiculo");

            builder.Property(e => e.UsuarioAlteracaoQuantidadeId)
                .HasColumnName("id_usuario_alteracao_quantidade");

            builder.Property(e => e.UsuarioDescontoId)
                .HasColumnName("id_usuario_desconto");

            builder.Property(e => e.ObservacaoDesconto)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("observacao_desconto");

            builder.Property(e => e.ObservacaoQuantidadeAlterada)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("observacao_quantidade_alterada");

            builder.Property(e => e.QuantidadeAlterada)
                .HasColumnType("smallmoney")
                .HasColumnName("quantidade_alterada");

            builder.Property(e => e.QuantidadeComposicao)
                .HasDefaultValueSql("((0))")
                .HasColumnType("smallmoney")
                .HasColumnName("quantidade_composicao");

            builder.Property(e => e.QuantidadeDesconto)
                .HasColumnName("quantidade_desconto");

            builder.Property(e => e.TipoComposicao)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasComment("TIPOS DE COBRANÇA:\r\nD = Diárias;\r\nH = Quantidade de HH:MM vezes o Preço;\r\nP = Porcentagem;\r\nQ = Quantidade;\r\nT = Tempo entre duas Datas;\r\nV = Valor.")
                .HasColumnName("tipo_composicao");

            builder.Property(e => e.TipoDesconto)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("tipo_desconto");

            builder.Property(e => e.TipoLancamento)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('C')")
                .IsFixedLength()
                .HasColumnName("tipo_lancamento");

            builder.Property(e => e.ValorComposicao)
                .HasColumnType("money")
                .HasColumnName("valor_composicao");

            builder.Property(e => e.ValorDesconto)
                .HasColumnType("money")
                .HasColumnName("valor_desconto");

            builder.Property(e => e.ValorFaturado)
                .HasColumnType("money")
                .HasColumnName("valor_faturado");

            builder.Property(e => e.ValorTipoComposicao)
                .HasColumnType("money")
                .HasColumnName("valor_tipo_composicao");
        }
    }
}