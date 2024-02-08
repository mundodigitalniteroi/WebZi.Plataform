using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Faturamento;

namespace WebZi.Plataform.Data.Mappings.Faturamento
{
    public class FaturamentoRegraMap : IEntityTypeConfiguration<FaturamentoRegraModel>
    {
        public void Configure(EntityTypeBuilder<FaturamentoRegraModel> builder)
        {
            builder
                .ToTable("tb_dep_faturamento_regras", "dbo", tb => tb.HasTrigger("tr_log_upd_faturamento_regras"))
                .HasKey(x => x.FaturamentoRegraId);

            builder.Property(e => e.FaturamentoRegraId)
                .HasColumnName("id_faturamento_regra")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.FaturamentoRegraTipoId)
                .HasColumnName("id_faturamento_regra_tipo");

            builder.Property(e => e.ClienteId)
                .HasColumnName("id_cliente");

            builder.Property(e => e.DepositoId)
                .HasColumnName("id_deposito");

            builder.Property(e => e.DataVigenciaFinal)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_vigencia_final");

            builder.Property(e => e.DataVigenciaInicial)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_vigencia_inicial");

            builder.Property(e => e.DataCadastro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime")
                .HasColumnName("data_cadastro");

            builder.Property(e => e.DataAlteracao)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_alteracao");

            builder.Property(e => e.UsuarioCadastroId)
                .HasColumnName("id_usuario_cadastro");

            builder.Property(e => e.UsuarioAlteracaoId)
                .HasColumnName("id_usuario_alteracao");

            builder.Property(e => e.Valor)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("valor");
        }
    }
}