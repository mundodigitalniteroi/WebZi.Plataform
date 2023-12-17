using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.ClienteDeposito;

namespace WebZi.Plataform.Data.Mappings.ClienteDeposito
{
    internal class ClienteDepositoMap : IEntityTypeConfiguration<ClienteDepositoModel>
    {
        public void Configure(EntityTypeBuilder<ClienteDepositoModel> builder)
        {
            builder
                .ToTable("tb_dep_clientes_depositos", "dbo", tb => tb.HasTrigger("tr_log_upd_clientes_depositos"))
                .HasKey(x => x.ClienteDepositoId);

            builder.Property(e => e.ClienteDepositoId)
                .HasColumnName("id_cliente_deposito")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.ClienteId)
                .HasColumnName("id_cliente");

            builder.Property(e => e.DepositoId)
                .HasColumnName("id_deposito");

            builder.Property(e => e.EmpresaId)
                .HasColumnName("id_empresa");

            builder.Property(e => e.OrgaoEmissorId)
                .HasColumnName("id_orgao_emissor");

            builder.Property(e => e.SistemaExternoId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("id_sistema_externo");

            builder.Property(e => e.UsuarioCadastroId)
                .HasColumnName("id_usuario_cadastro");

            builder.Property(e => e.UsuarioAlteracaoId)
                .HasColumnName("id_usuario_alteracao");

            builder.Property(e => e.AliquotaIss)
                .HasColumnType("smallmoney");

            builder.Property(e => e.CodigoDetran)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("codigo_detran");

            builder.Property(e => e.CodigoSap)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("codigo_sap");

            builder.Property(e => e.CodigoERPOrdemVenda)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("codigo_sap_ordem_vendas");

            builder.Property(e => e.FlagAtivo)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('S')")
                .IsFixedLength()
                .HasColumnName("flag_ativo");

            builder.Property(e => e.FlagCadastrarGrvComStatusOperacaoBloqueado)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('S')")
                .IsFixedLength()
                .HasColumnName("flag_cadastrar_grv_bloqueado");

            builder.Property(e => e.FlagUtilizaSistemaMobileGgv)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_utiliza_sistema_mobile_ggv");

            builder.Property(e => e.FlagValorIssIgualProdutoBaseCalculoAliquota)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();

            builder.Property(e => e.DataCadastro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime")
                .HasColumnName("data_cadastro");

            builder.Property(e => e.DataAlteracao)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_alteracao");
        }
    }
}