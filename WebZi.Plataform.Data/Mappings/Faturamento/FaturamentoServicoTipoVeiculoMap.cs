using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Faturamento;

namespace WebZi.Plataform.Data.Mappings.Faturamento
{
    public class FaturamentoServicoTipoVeiculoMap : IEntityTypeConfiguration<FaturamentoServicoTipoVeiculoModel>
    {
        public void Configure(EntityTypeBuilder<FaturamentoServicoTipoVeiculoModel> builder)
        {
            builder
                .ToTable("tb_dep_faturamento_servicos_tipo_veiculos", "dbo")
                .HasKey(e => e.IdFaturamentoServicoTipoVeiculo);

            builder.Property(e => e.IdFaturamentoServicoTipoVeiculo)
                .HasColumnName("id_faturamento_servico_tipo_veiculo")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.IdFaturamentoServicoAssociado)
                .HasColumnName("id_faturamento_servico_associado");

            builder.Property(e => e.IdTipoVeiculo)
                .HasColumnName("id_tipo_veiculo");
        }
    }
}