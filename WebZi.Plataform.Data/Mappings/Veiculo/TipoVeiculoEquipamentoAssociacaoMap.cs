using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Veiculo;

namespace WebZi.Plataform.Data.Mappings.Veiculo
{
    public class TipoVeiculoEquipamentoAssociacaoMap : IEntityTypeConfiguration<TipoVeiculoEquipamentoAssociacaoModel>
    {
        public void Configure(EntityTypeBuilder<TipoVeiculoEquipamentoAssociacaoModel> builder)
        {
            builder
                .ToTable("tb_dep_tipo_veiculos_equipamentos_associacao", "dbo")
                .HasKey(e => e.TipoVeiculoEquipamentoAssociacaoId);

            builder.Property(e => e.TipoVeiculoEquipamentoAssociacaoId)
                .HasColumnName("id_tipo_veiculo_equipamento_associacao")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.EquipamentoOpcionalId)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("id_equipamento_opcional");

            builder.Property(e => e.TipoVeiculoId)
                .HasColumnName("id_tipo_veiculo");

            builder.Property(e => e.UsuarioCadastroId)
                .HasColumnName("id_usuario_cadastro");

            builder.Property(e => e.DataCadastro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime")
                .HasColumnName("data_cadastro");
        }
    }
}