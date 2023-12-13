using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Banco;

namespace WebZi.Plataform.Data.Mappings.Banco
{
    public class BancoMap : IEntityTypeConfiguration<BancoModel>
    {
        public void Configure(EntityTypeBuilder<BancoModel> builder)
        {
            builder
                .ToTable("tb_dep_bancos", "dbo")
                .HasKey(e => e.BancoId);

            builder.Property(e => e.BancoId)
                .HasColumnName("id_banco")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.UsuarioCadastroId)
                .HasColumnName("id_usuario_cadastro");

            builder.Property(e => e.UsuarioAlteracaoId)
                .HasColumnName("id_usuario_alteracao");

            builder.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nome");

            builder.Property(e => e.CodigoFebraban)
                .IsRequired()
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("codigo_febraban");

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