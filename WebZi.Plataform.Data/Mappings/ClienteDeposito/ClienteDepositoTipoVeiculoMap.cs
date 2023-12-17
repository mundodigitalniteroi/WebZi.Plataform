using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.ClienteDeposito;

namespace WebZi.Plataform.Data.Mappings.ClienteDeposito
{
    public class ClienteDepositoTipoVeiculoMap : IEntityTypeConfiguration<ClienteDepositoTipoVeiculoModel>
    {
        public void Configure(EntityTypeBuilder<ClienteDepositoTipoVeiculoModel> builder)
        {
            builder
                .ToTable("tb_dep_cliente_deposito_tipos_veiculos", "dbo", tb => tb.HasTrigger("tr_log_cliente_deposito_tipos_veiculos"))
                .HasKey(x => x.ClienteDepositoTipoVeiculoId);

            builder.Property(e => e.ClienteDepositoTipoVeiculoId)
                .HasColumnName("id_cliente_deposito_tipo_veiculo")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.ClienteDepositoId)
                .IsRequired()
                .HasColumnName("id_cliente_deposito");

            builder.Property(e => e.TipoVeiculoId)
                .IsRequired()
                .HasColumnName("id_tipo_veiculo");

            builder.Property(e => e.UsuarioCadastroId)
                .IsRequired()
                .HasColumnName("id_usuario_cadastro");

            builder.Property(e => e.UsuarioAlteracaoId)
                .HasColumnName("id_usuario_alteracao");

            builder.Property(e => e.DataCadastro)
                .IsRequired()
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime")
                .HasColumnName("data_cadastro");

            builder.Property(e => e.DataAlteracao)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_alteracao");

            builder.Property(e => e.FlagAtivo)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('S')")
                .IsFixedLength()
                .HasColumnName("flag_ativo");
        }
    }
}