using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.GRV.DRFA;

namespace WebZi.Plataform.Data.Mappings.GRV
{
    public class AgendamentoRetiradaMap : IEntityTypeConfiguration<AgendamentoRetiradaModel>
    {
        public void Configure(EntityTypeBuilder<AgendamentoRetiradaModel> builder)
        {
            builder
                 .ToTable("tb_dep_grv_drfa_agendamento_retirada", "dbo")
                 .HasKey(x => x.GrvDRFAAgendamentoRetiradaId);

            builder.Property(e => e.GrvDRFAAgendamentoRetiradaId)
                .HasColumnName("id_grv_drfa_agendamento_retirada")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.GrvDRFAId)
                .HasColumnName("id_grv_drfa");

            builder.Property(e => e.UsuarioRegistroAgendamentoId)
                .HasColumnName("id_usuario_registro_agendamento");

            builder.Property(e => e.NomeResponsavelAgendamento)
                .HasColumnName("nome_responsavel_agendamento")
                .HasMaxLength(100);

            builder.Property(e => e.CpfResponsavelAgendamento)
                .HasColumnName("cpf_responsavel_agendamento")
                .HasMaxLength(11);

            builder.Property(e => e.DataRegistroAgendamento)
                .HasColumnName("data_registro_agendamento")
                .HasColumnType("smalldatetime")
                .IsRequired();

            builder.Property(e => e.DataAgendamento)
                .HasColumnName("data_agendamento")
                .HasColumnType("smalldatetime")
                .IsRequired();
        }
    }
}
