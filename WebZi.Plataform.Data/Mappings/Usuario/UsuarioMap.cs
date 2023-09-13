using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Usuario;

namespace WebZi.Plataform.Data.Mappings.GRV
{
    public class UsuarioMap : IEntityTypeConfiguration<UsuarioModel>
    {
        public void Configure(EntityTypeBuilder<UsuarioModel> builder)
        {
            builder
                .ToTable("tb_dep_usuarios", "dbo")
                .HasKey(e => e.UsuarioId);

            builder.Property(e => e.UsuarioId)
                .HasColumnName("id_usuario")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.DataAlteracao)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_alteracao");

            builder.Property(e => e.DataCadastro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime")
                .HasColumnName("data_cadastro");

            builder.Property(e => e.DataCadastroSenha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime")
                .HasColumnName("data_cadastro_senha");

            builder.Property(e => e.DataUltimoAcesso)
                .HasColumnType("datetime")
                .HasColumnName("data_ultimo_acesso");

            builder.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("email");

            builder.Property(e => e.FlagAtivo)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('S')")
                .IsFixedLength()
                .HasColumnName("flag_ativo");

            builder.Property(e => e.FlagPermissaoDataRetroativaFaturamento)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_permissao_data_retroativa_faturamento");

            builder.Property(e => e.FlagPermissaoDesconto)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_permissao_desconto");

            builder.Property(e => e.FlagReceberEmailErro)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_receber_email_erro");

            builder.Property(e => e.FuncionarioId)
                .HasColumnName("id_funcionario");

            builder.Property(e => e.TipoOperadorId)
                .HasColumnName("id_tipo_operador");

            builder.Property(e => e.UsuarioAlteracaoId)
                .HasColumnName("id_usuario_alteracao");

            builder.Property(e => e.UsuarioCadastroId)
                .HasColumnName("id_usuario_cadastro");

            builder.Property(e => e.Login)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("login");

            builder.Property(e => e.Matricula)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("matricula");

            builder.Property(e => e.PessoaId)
                .HasColumnName("PessoaID");

            builder.Property(e => e.Senha1)
                .IsRequired()
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasDefaultValueSql("(hashbytes('MD5','NONE'))")
                .HasColumnName("senha1");

            builder.Property(e => e.Senha2)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("senha2");

            builder.Property(e => e.Senha3)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("senha3");

            builder.Property(e => e.Senha4)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("senha4");

            builder.Property(e => e.Senha5)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("senha5");

            builder.Property(e => e.SenhaAndroid)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("senha_android");
        }
    }
}