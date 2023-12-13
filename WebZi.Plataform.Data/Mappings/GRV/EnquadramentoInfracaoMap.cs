using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.GRV;

namespace WebZi.Plataform.Data.Mappings.GRV
{
    public class EnquadramentoInfracaoMap : IEntityTypeConfiguration<EnquadramentoInfracaoModel>
    {
        public void Configure(EntityTypeBuilder<EnquadramentoInfracaoModel> builder)
        {
            builder
                .ToTable("tb_dep_enquadramento_infracoes", "dbo", tb => tb.HasTrigger("tr_log_upd_enquadramento_infracoes"))
                .HasKey(e => e.EnquadramentoInfracaoId);

            builder.Property(e => e.EnquadramentoInfracaoId)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("id_enquadramento_infracao")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.UsuarioId)
                .HasColumnName("id_usuario");

            builder.Property(e => e.Artigo)
                .HasColumnName("artigo");

            builder.Property(e => e.CodigoInfracao)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("codigo_infracao");

            builder.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descricao");

            builder.Property(e => e.Inciso)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("inciso");

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
        }
    }
}