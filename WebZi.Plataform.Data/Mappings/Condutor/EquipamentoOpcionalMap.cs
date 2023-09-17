using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Condutor;

namespace WebZi.Plataform.Data.Mappings.Condutor
{
    public class EquipamentoOpcionalMap : IEntityTypeConfiguration<EquipamentoOpcionalModel>
    {
        public void Configure(EntityTypeBuilder<EquipamentoOpcionalModel> builder)
        {
            builder
                .ToTable("tb_dep_equipamentos_opcionais", "dbo", tb => tb.HasTrigger("tr_log_upd_equipamentos_opcionais"))
                .HasKey(e => e.EquipamentoOpcionalId);

            builder.Property(e => e.EquipamentoOpcionalId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("id_equipamento_opcional");
            
            builder.Property(e => e.EquipamentoOpcionalLocalizacaoId).HasColumnName("id_equipamento_opcional_localizacao");
            
            builder.Property(e => e.UsuarioCadastroId).HasColumnName("id_usuario");
            
            builder.Property(e => e.UsuarioAlteracaoId).HasColumnName("id_usuario_alteracao");

            builder.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descricao");

            builder.Property(e => e.ItemObrigatorio)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .HasColumnName("item_obrigatorio");
            
            builder.Property(e => e.ItemOcorrenciaDetranBa)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("item_ocorrencia_detran_ba");
            
            builder.Property(e => e.OrdemVistoria)
                .HasDefaultValueSql("((0))")
                .HasColumnName("ordem_vistoria");
            
            builder.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('S')")
                .IsFixedLength()
                .HasColumnName("status");

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