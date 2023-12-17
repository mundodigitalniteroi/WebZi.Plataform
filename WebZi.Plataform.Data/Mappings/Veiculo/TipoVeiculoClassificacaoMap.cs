using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Veiculo;

namespace WebZi.Plataform.Data.Mappings.Veiculo
{
    public class TipoVeiculoClassificacaoMap : IEntityTypeConfiguration<TipoVeiculoClassificacaoModel>
    {
        public void Configure(EntityTypeBuilder<TipoVeiculoClassificacaoModel> builder)
        {
            builder
                .ToTable("tb_dep_tipo_veiculos_classificacao", "dbo")
                .HasKey(x => x.TipoVeiculoClassificacaoId);

            builder.Property(e => e.TipoVeiculoClassificacaoId)
                .HasColumnName("id_tipo_veiculo_classificacao")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.TipoVeiculoId)
                .HasColumnName("id_tipo_veiculo");

            builder.Property(e => e.TipoVeiculoClassificacaoNomeId)
                .HasColumnName("id_tipo_veiculo_classificacao_nome");
        }
    }
}