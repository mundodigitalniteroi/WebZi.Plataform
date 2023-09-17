using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Condutor;

namespace WebZi.Plataform.Data.Mappings.Condutor
{
    public class EquipamentoOpcionalLocalizacaoMap : IEntityTypeConfiguration<EquipamentoOpcionalLocalizacaoModel>
    {
        public void Configure(EntityTypeBuilder<EquipamentoOpcionalLocalizacaoModel> builder)
        {
            builder
                .ToTable("tb_dep_equipamentos_opcionais_localizacao", "dbo", tb => tb.HasTrigger("tr_log_upd_equipamentos_opcionais_localizacao"))
                .HasKey(e => e.EquipamentoOpcionalLocalizacaoId);

            builder.Property(e => e.EquipamentoOpcionalLocalizacaoId)
                .ValueGeneratedOnAdd()
                .HasColumnName("id_equipamento_opcional_localizacao");

            builder.Property(e => e.UsuarioCadastroId)
                .HasColumnName("id_usuario_cadastro");

            builder.Property(e => e.UsuarioAlteracaoId)
                .HasColumnName("id_usuario_alteracao");

            builder.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descricao");

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