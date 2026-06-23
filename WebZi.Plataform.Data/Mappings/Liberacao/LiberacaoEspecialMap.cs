using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Liberacao;

namespace WebZi.Plataform.Data.Mappings.Liberacao
{
    public class LiberacaoEspecialMap : IEntityTypeConfiguration<LiberacaoEspecialModel>
    {
        public void Configure(EntityTypeBuilder<LiberacaoEspecialModel> builder)
        {
            builder.ToTable("tb_dep_liberacao_especial", "dbo", t =>
                {
                    t.HasTrigger("tr_log_upd_liberacao_especial");
                })
                .HasKey(x => x.IdLiberacaoEspecial);

            builder.Property(e => e.IdLiberacaoEspecial)
                .HasColumnName("id_liberacao_especial")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.IdGrv)
                .HasColumnName("id_grv")
                .IsRequired();

            builder.Property(e => e.IdFaturamento)
                .HasColumnName("id_faturamento")
                .IsRequired();

            builder.Property(e => e.IdLiberacaoEspecialTipo)
                .HasColumnName("id_liberacao_especial_tipo")
                .IsRequired();

            builder.Property(e => e.IdUsuarioCadastro)
                .HasColumnName("id_usuario_cadastro")
                .IsRequired();

            builder.Property(e => e.NumeroDocumento)
                .HasColumnName("numero_documento")
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.TipoDocumento)
                .HasColumnName("tipo_documento")
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.NumeroProcesso)
                .HasColumnName("numero_processo")
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.OrgaoEmissor)
                .HasColumnName("orgao_emissor")
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.PortadorNome)
                .HasColumnName("portador_nome")
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.PortadorCargo)
                .HasColumnName("portador_cargo")
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.PortadorMatricula)
                .HasColumnName("portador_matricula")
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.SignatarioNomeDocumento)
                .HasColumnName("signatario_nome_documento")
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.SignatarioMatricula)
                .HasColumnName("signatario_matricula")
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.SignatarioTitulo)
                .HasColumnName("signatario_titulo")
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.DataEmissaoDocumento)
                .HasColumnName("data_emissao_documento")
                .HasColumnType("date");

            builder.Property(e => e.DataLiberacao)
                .HasColumnName("data_liberacao")
                .HasColumnType("smalldatetime");
        }
    }
}
