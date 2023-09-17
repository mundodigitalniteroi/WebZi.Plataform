using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Condutor;

namespace WebZi.Plataform.Data.Mappings.Condutor
{
    public class CondutorEquipamentoOpcionalNaoConformidadeMap : IEntityTypeConfiguration<CondutorEquipamentoOpcionalNaoConformidadeModel>
    {
        public void Configure(EntityTypeBuilder<CondutorEquipamentoOpcionalNaoConformidadeModel> builder)
        {
            builder
                .ToTable("tb_dep_condutor_equipamentos_opcionais_nao_conformidade", "dbo")
                .HasKey(e => e.CondutorEquipamentoOpcionalNaoConformidadeId);

            builder.Property(e => e.CondutorEquipamentoOpcionalNaoConformidadeId)
                .HasColumnName("id_condutor_equipamento_opcional_nao_conformidade")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.CondutorEquipamentoOpcionalId)
                .HasColumnName("id_condutor_equipamento_opcional");

            builder.Property(e => e.UsuarioCadastroId)
                .HasColumnName("id_usuario_cadastro");

            builder.Property(e => e.Explicacao)
                .IsRequired()
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("explicacao");

            builder.Property(e => e.DataCadastro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime")
                .HasColumnName("data_cadastro");
        }
    }
}