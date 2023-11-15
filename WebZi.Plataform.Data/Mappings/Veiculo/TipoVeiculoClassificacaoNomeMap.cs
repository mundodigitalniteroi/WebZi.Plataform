using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Domain.Models.Veiculo;

namespace WebZi.Plataform.Data.Mappings.Veiculo
{
    public class TipoVeiculoClassificacaoNomeMap : IEntityTypeConfiguration<TipoVeiculoClassificacaoNomeModel>
    {
        public void Configure(EntityTypeBuilder<TipoVeiculoClassificacaoNomeModel> builder)
        {
            builder
                .ToTable("tb_dep_tipo_veiculos_classificacao_nome", "dbo")
                .HasKey(e => e.TipoVeiculoClassificacaoNomeId);

            builder.Property(e => e.TipoVeiculoClassificacaoNomeId)
                .HasColumnName("id_tipo_veiculo_classificacao_nome")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descricao");

            builder.Property(e => e.Classificacao)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("classificacao");
        }
    }
}