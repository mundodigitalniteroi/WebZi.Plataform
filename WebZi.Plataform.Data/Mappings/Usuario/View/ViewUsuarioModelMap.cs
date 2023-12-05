using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Views.Usuario;

namespace WebZi.Plataform.Data.Mappings.Usuario.View
{
    public class ViewUsuarioModelMap : IEntityTypeConfiguration<ViewUsuarioModel>
    {
        public void Configure(EntityTypeBuilder<ViewUsuarioModel> builder)
        {
            builder
                .ToView("vw_dep_usuarios_nome", "dbo")
                .HasNoKey();

            builder.Property(e => e.Cpf)
                .HasMaxLength(20)
                .IsUnicode(false)
                .UseCollation("Latin1_General_CI_AS")
                .HasColumnName("cpf");
            
            builder.Property(e => e.CpfFormatado)
                .HasMaxLength(14)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("Latin1_General_CI_AS")
                .HasColumnName("cpf_formatado");
            
            builder.Property(e => e.DataAlteracao)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_alteracao");
            
            builder.Property(e => e.DataCadastro)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_cadastro");
            
            builder.Property(e => e.DataCadastroSenha)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_cadastro_senha");
            
            builder.Property(e => e.DataUltimoAcesso)
                .HasColumnType("datetime")
                .HasColumnName("data_ultimo_acesso");
            
            builder.Property(e => e.FlagAtivo)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("flag_ativo");
            
            builder.Property(e => e.FlagPermissaoDataRetroativaFaturamento)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("flag_permissao_data_retroativa_faturamento");
            
            builder.Property(e => e.FlagPermissaoDesconto)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("flag_permissao_desconto");
            
            builder.Property(e => e.FlagReceberEmailErro)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("flag_receber_email_erro");
            
            builder.Property(e => e.UsuarioId).HasColumnName("id_usuario");
            
            builder.Property(e => e.UsuarioAlteracaoId).HasColumnName("id_usuario_alteracao");
            
            builder.Property(e => e.UsuarioCadastroId).HasColumnName("id_usuario_cadastro");
            
            builder.Property(e => e.Login)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("login");
            
            builder.Property(e => e.Matricula)
                .HasMaxLength(15)
                .IsUnicode(false);
            
            builder.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("Latin1_General_CI_AS")
                .HasColumnName("nome");
            
            builder.Property(e => e.NomeCompleto)
                .IsRequired()
                .HasMaxLength(152)
                .IsUnicode(false)
                .UseCollation("Latin1_General_CI_AS")
                .HasColumnName("nome_completo");
            
            builder.Property(e => e.NomeMeio)
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("Latin1_General_CI_AS")
                .HasColumnName("nome_meio");
            
            builder.Property(e => e.PessoaId).HasColumnName("PessoaID");
            
            builder.Property(e => e.Sobrenome)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("Latin1_General_CI_AS")
                .HasColumnName("sobrenome");
        }
    }
}