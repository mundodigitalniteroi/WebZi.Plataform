using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Servico;

namespace WebZi.Plataform.Data.Mappings.Servico
{
    public class ReboquistaMap : IEntityTypeConfiguration<ReboquistaModel>
    {
        public void Configure(EntityTypeBuilder<ReboquistaModel> builder)
        {
            builder
                .ToTable("tb_dep_reboquistas", "dbo", tb => tb.HasTrigger("tr_log_upd_reboquistas"))
                .HasKey(e => e.ReboquistaId);

            builder.Property(e => e.ReboquistaId)
                .HasColumnName("id_reboquista")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.ClienteId)
                .HasColumnName("id_cliente");

            builder.Property(e => e.DepositoId)
                .HasColumnName("id_deposito");

            builder.Property(e => e.UsuarioAlteracaoId)
                .HasColumnName("id_usuario_alteracao");

            builder.Property(e => e.UsuarioCadastroId)
                .HasColumnName("id_usuario_cadastro");

            builder.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nome");

            builder.Property(e => e.DataCadastro)
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