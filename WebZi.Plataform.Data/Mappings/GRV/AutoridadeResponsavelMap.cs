using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.GRV;

namespace WebZi.Plataform.Data.Mappings.GRV
{
    internal class AutoridadeResponsavelMap : IEntityTypeConfiguration<AutoridadeResponsavelModel>
    {
        public void Configure(EntityTypeBuilder<AutoridadeResponsavelModel> builder)
        {
            builder
                .ToTable("tb_dep_autoridades_responsaveis", "dbo")
                .HasKey(x => x.AutoridadeResponsavelId);

            builder.Property(e => e.AutoridadeResponsavelId)
                .HasColumnName("id_autoridade_responsavel")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.OrgaoEmissorId)
                .HasColumnName("id_orgao_emissor");

            builder.Property(e => e.SistemaExternoId)
                .HasColumnName("id_externo");

            builder.Property(e => e.UsuarioCadastroId)
                .HasColumnName("id_usuario_cadastro");

            builder.Property(e => e.UsuarioAlteracaoId)
                .HasColumnName("id_usuario_alteracao");

            builder.Property(e => e.Divisao)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("divisao");

            builder.Property(e => e.DataCadastro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime")
                .HasColumnName("data_cadastro");

            builder.Property(e => e.DataAlteracao)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_alteracao");

            builder.Property(e => e.FlagAtivo)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('S')")
                .IsFixedLength()
                .HasColumnName("flag_ativo");
        }
    }
}