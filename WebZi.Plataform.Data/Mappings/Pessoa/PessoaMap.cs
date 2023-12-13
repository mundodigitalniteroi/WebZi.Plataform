using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Pessoa;

namespace WebZi.Plataform.Data.Mappings.Pessoa
{
    public class PessoaMap : IEntityTypeConfiguration<PessoaModel>
    {
        public void Configure(EntityTypeBuilder<PessoaModel> builder)
        {
            builder
                .ToTable("tb_glo_pes_pessoas", "dbo")
                .HasKey(e => e.IdPessoa);

            builder.Property(e => e.IdPessoa)
                .HasColumnName("id_pessoa")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.CpfUsuarioAlteracao)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("cpf_usuario_alteracao");

            builder.Property(e => e.CpfUsuarioCadastro)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("cpf_usuario_cadastro");

            builder.Property(e => e.DataAlteracao)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_alteracao");

            builder.Property(e => e.DataCadastro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime")
                .HasColumnName("data_cadastro");

            builder.Property(e => e.DataNascimento)
                .HasColumnType("date")
                .HasColumnName("data_nascimento");

            builder.Property(e => e.EmpresaUsuarioAlteracao)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("empresa_usuario_alteracao");

            builder.Property(e => e.EmpresaUsuarioCadastro)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("empresa_usuario_cadastro");

            builder.Property(e => e.IdPessoaMae)
                .HasColumnName("id_pessoa_mae");

            builder.Property(e => e.IdPessoaPai)
                .HasColumnName("id_pessoa_pai");

            builder.Property(e => e.IdTipoEstadoCivil)
                .HasColumnName("id_tipo_estado_civil");

            builder.Property(e => e.IdTipoProfissao)
                .HasDefaultValueSql("((1))")
                .HasColumnName("id_tipo_profissao");

            builder.Property(e => e.UsuarioAlteracaoId)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("ip_usuario_alteracao");

            builder.Property(e => e.UsuarioCadastroId)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("ip_usuario_cadastro");

            builder.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nome");

            builder.Property(e => e.NomeMeio)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nome_meio");

            builder.Property(e => e.NomeUsuarioAlteracao)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("nome_usuario_alteracao");

            builder.Property(e => e.NomeUsuarioCadastro)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("nome_usuario_cadastro");

            builder.Property(e => e.Sexo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("sexo");

            builder.Property(e => e.Sobrenome)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sobrenome");
        }
    }
}