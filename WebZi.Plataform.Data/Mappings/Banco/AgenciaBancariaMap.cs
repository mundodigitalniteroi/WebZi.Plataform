using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Banco;

namespace WebZi.Plataform.Data.Mappings.Banco
{
    public class AgenciaBancariaMap : IEntityTypeConfiguration<AgenciaBancariaModel>
    {
        public void Configure(EntityTypeBuilder<AgenciaBancariaModel> builder)
        {
            builder
                .ToTable("tb_dep_agencias_bancarias", "dbo", tb => tb.HasTrigger("tr_log_upd_agencias_bancarias"))
                .HasKey(e => e.AgenciaBancariaId);

            builder.Property(e => e.AgenciaBancariaId)
                .HasColumnName("id_agencia_bancaria")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.BancoId)
                .HasColumnName("id_banco");

            builder.Property(e => e.UsuarioCadastroId)
                .HasColumnName("id_usuario_cadastro");

            builder.Property(e => e.UsuarioAlteracaoId)
                .HasColumnName("id_usuario_alteracao");

            builder.Property(e => e.CodigoAgencia)
                .IsRequired()
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("codigo_agencia");

            builder.Property(e => e.CodigoCedente)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("codigo_cedente");

            builder.Property(e => e.ContaCorrente)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("conta_corrente");

            builder.Property(e => e.DigitoVerificador)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("digito_verificador");

            builder.Property(e => e.SacadoCarteira)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("sacado_carteira");

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