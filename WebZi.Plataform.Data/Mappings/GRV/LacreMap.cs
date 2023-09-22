using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.GRV;

namespace WebZi.Plataform.Data.Mappings.GRV
{
    internal class LacreMap : IEntityTypeConfiguration<LacreModel>
    {
        public void Configure(EntityTypeBuilder<LacreModel> builder)
        {
            builder
                .ToTable("tb_dep_grv_lacres", "dbo", tb => tb.HasTrigger("tr_log_upd_grv_lacres"))
                .HasKey(e => e.LacreId);

            builder.Property(e => e.LacreId)
                .HasColumnName("id_lacre")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.GrvId)
                .HasColumnName("id_grv");

            builder.Property(e => e.LacreMotivoDesassociacaoId)
                .HasColumnName("id_lacre_motivo_desassociacao");

            builder.Property(e => e.UsuarioCadastroId)
                .HasColumnName("id_usuario_cadastro");

            builder.Property(e => e.UsuarioAtualizacaoId)
                .HasColumnName("id_usuario_atualizacao");
            
            builder.Property(e => e.Lacre)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("lacre");

            builder.Property(e => e.LacreAnterior)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("lacre_anterior");

            builder.Property(e => e.DataCadastro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime")
                .HasColumnName("data_cadastro");

            builder.Property(e => e.DataAtualizacao)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_atualizacao");
        }
    }
}