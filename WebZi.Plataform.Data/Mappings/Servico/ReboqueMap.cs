using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Servico;

namespace WebZi.Plataform.Data.Mappings.Servico
{
    public class ReboqueMap : IEntityTypeConfiguration<ReboqueModel>
    {
        public void Configure(EntityTypeBuilder<ReboqueModel> builder)
        {
            builder
                .ToTable("tb_dep_reboques", "dbo", tb => tb.HasTrigger("tr_log_upd_reboques"))
                .HasKey(x => x.ReboqueId);

            builder.Property(e => e.ReboqueId)
                .HasColumnName("id_reboque")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Ano)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("ano");

            builder.Property(e => e.Chassi)
                .HasMaxLength(24)
                .IsUnicode(false)
                .HasColumnName("chassi");

            builder.Property(e => e.Codigo)
                .IsRequired()
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("codigo");

            builder.Property(e => e.ClienteId)
                .HasColumnName("id_cliente");

            builder.Property(e => e.DepositoId)
                .HasColumnName("id_deposito");

            builder.Property(e => e.UsuarioCadastroId)
                .HasColumnName("id_usuario_cadastro");

            builder.Property(e => e.UsuarioAlteracaoId)
                .HasColumnName("id_usuario_alteracao");

            builder.Property(e => e.Marca)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("marca");

            builder.Property(e => e.Modelo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("modelo");

            builder.Property(e => e.Placa)
                .IsRequired()
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("placa");

            builder.Property(e => e.Renavam)
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasColumnName("renavam");

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