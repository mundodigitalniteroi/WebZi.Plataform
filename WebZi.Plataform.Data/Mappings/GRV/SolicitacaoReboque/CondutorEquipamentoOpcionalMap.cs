using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Data.Mappings.GRV.SolicitacaoReboque;
using WebZi.Plataform.Domain.Models.Condutor;
using WebZi.Plataform.Domain.Models.GRV.SolicitacaoReboque;

namespace WebZi.Plataform.Data.Mappings.Condutor
{
    public class SolicitacaoEquipamentoOpcionalMap : IEntityTypeConfiguration<SolicitacaoEquipamentoOpcionalModel>
    {
        public void Configure(EntityTypeBuilder<SolicitacaoEquipamentoOpcionalModel> builder)
        {
            builder
                .ToTable("tb_solicitacao_equipamento_opcional", "dbo")
                .HasKey(x => x.SolicitacaoEquipamentoOpcionalId);

            builder.Property(e => e.SolicitacaoEquipamentoOpcionalId)
                .HasColumnName("id_solicitacao_equipamento_opcional")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.SolicitacaoReboqueId)
                .HasColumnName("id_solicitacao_reboque");

            builder.Property(e => e.UsuarioCadastroId)
                .HasColumnName("id_usuario_cadastro");

            builder.Property(e => e.UsuarioAlteracaoId)
                .HasColumnName("id_usuario_atualizacao");

            builder.Property(e => e.FlagEquipamentoAvariado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("avariado");

            builder.Property(e => e.CodigoAvaria).HasColumnName("cod_avaria");

            builder.Property(e => e.DataAtualizacao)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_atualizacao");

            builder.Property(e => e.DataCadastro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime")
                .HasColumnName("data_cadastro");

            builder.Property(e => e.FlagPossuiEquipamento)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('S')")
                .IsFixedLength()
                .HasColumnName("flag_possui_equipamento");

            builder.Property(e => e.EquipamentoOpcionalId)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("id_equipamento_opcional");
        }
    }
}