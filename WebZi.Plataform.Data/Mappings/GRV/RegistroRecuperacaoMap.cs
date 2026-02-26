using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.GRV.DRFA;

namespace WebZi.Plataform.Data.Mappings.GRV
{
    public class RegistroRecuperacaoMap : IEntityTypeConfiguration<RegistroRecuperacaoModel>
    {
        public void Configure(EntityTypeBuilder<RegistroRecuperacaoModel> builder)
        {
            builder
                .ToTable("tb_dep_grv_drfa_registro_recuperacao", "dbo")
                .HasKey(x => x.GrvDRFARegistroRecuperacaoId);

            builder.Property(e => e.GrvDRFARegistroRecuperacaoId)
                .HasColumnName("id_grv_drfa_registro_recuperacao")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.DRFAId)
                .HasColumnName("id_grv_drfa")
                .IsRequired();

            builder.Property(e => e.AutoridadeDivisaoId)
                .HasColumnName("id_autoridade_divisao")
                .IsRequired();

            builder.Property(e => e.NumeroRegistroRecuperacao)
                .HasColumnName("numero_registro_recuperacao")
                .HasMaxLength(15);

            builder.Property(e => e.MatriculaAgente)
                .HasColumnName("registro_recuperacao_matricula_agente")
                .HasMaxLength(15);

            builder.Property(e => e.NomeAgente)
                .HasColumnName("registro_recuperacao_nome_agente")
                .HasMaxLength(100);

            builder.Property(e => e.DataRegistroRecuperacao)
                .HasColumnName("data_registro_recuperacao")
                .HasColumnType("smalldatetime")
                .IsRequired();
        }
    }
}
