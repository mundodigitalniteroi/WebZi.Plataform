using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.GRV.DRFA;

namespace WebZi.Plataform.Data.Mappings.GRV
{
    public class DRFAMap : IEntityTypeConfiguration<DRFAModel>
    {
        public void Configure(EntityTypeBuilder<DRFAModel> builder)
        {
            builder
            .ToTable("tb_dep_grv_drfa", "dbo", tb => tb.HasTrigger("tb_log_grv_drfa"))
                .HasKey(x => x.GrvDrfaId);

            builder.Property(e => e.GrvDrfaId)
                .HasColumnName("id_grv_drfa")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.GrvId)
                .HasColumnName("id_grv")
                .IsRequired();

            builder.Property(e => e.TipoRegistroId)
                .HasColumnName("id_grv_drfa_tipo_registro")
                .HasColumnType("tinyint")
                .IsRequired();

            builder.Property(e => e.OrgaoEmissorId)
                .HasColumnName("id_orgao_emissor")
                .HasColumnType("smallint");

            builder.Property(e => e.AutoridadeDivisaoId)
                .HasColumnType("tinyint")
                .HasColumnName("id_autoridade_divisao");

            builder.Property(e => e.UsuarioCadastroId)
                .HasColumnName("id_usuario_cadastro");

            builder.Property(e => e.UsuarioAlteracaoId)
                .HasColumnName("id_usuario_alteracao");

            builder.Property(e => e.AutoridadeDivisaoComplemento)
                .HasColumnName("autoridade_divisao_complemento")
                .HasMaxLength(15)
                .IsUnicode(false);

            builder.Property(e => e.NumeroRegistroRouboFurto)
                .HasColumnName("numero_registro_roubo_furto")
                .HasMaxLength(35)
                .IsUnicode(false);

            builder.Property(e => e.RegistroRouboFurtoMatriculaAgente)
                .HasColumnName("registro_roubo_furto_matricula_agente")
                .HasMaxLength(35)
                .IsUnicode(false);

            builder.Property(e => e.RegistroRouboFurtoNomeAgente)
                .HasColumnName("registro_roubo_furto_nome_agente")
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.LocalRemocaoEnderecoCompleto)
                .HasColumnName("local_remocao_endereco_completo")
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.LocalRemocaoReferencia)
                .HasColumnName("local_remocao_referencia")
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.LocalRemocaoLatitude)
                .HasColumnName("local_remocao_latitude")
                .HasMaxLength(15)
                .IsUnicode(false);

            builder.Property(e => e.LocalRemocaoLongitude)
                .HasColumnName("local_remocao_longitude")
                .HasMaxLength(15)
                .IsUnicode(false);

            builder.Property(e => e.EstadoGeralVeiculo)
                .HasColumnName("estado_geral_veiculo")
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.DataCadastro)
                .HasColumnName("data_cadastro")
                .IsRequired();

            builder.Property(e => e.DataAlteracao)
                .HasColumnName("data_alteracao")
                .HasColumnType("smalldatetime");

            builder.Property(e => e.FlagRegistroRecuperacao)
                .HasColumnName("flag_registro_recuperacao")
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();

            builder.Property(e => e.FlagRegistroAgendado)
                .HasColumnName("flag_registro_agendado")
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
        }
    }
}
