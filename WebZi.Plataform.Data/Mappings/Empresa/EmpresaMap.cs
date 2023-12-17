using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Empresa;

namespace WebZi.Plataform.Data.Mappings.Empresa
{
    public class EmpresaMap : IEntityTypeConfiguration<EmpresaModel>
    {
        public void Configure(EntityTypeBuilder<EmpresaModel> builder)
        {
            builder
                .ToTable("tb_glo_emp_empresas", "dbo")
                .HasKey(x => x.EmpresaId);

            builder.Property(e => e.EmpresaId)
                .HasColumnName("id_empresa")
                .ValueGeneratedOnAdd();


            builder.Property(e => e.Bairro)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("bairro");

            builder.Property(e => e.CnaeId)
                .HasColumnName("CnaeID");

            builder.Property(e => e.CnaeListaServicoId)
                .HasColumnName("CnaeListaServicoID");

            builder.Property(e => e.CNPJ)
                .IsRequired()
                .HasMaxLength(14)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("cnpj");

            builder.Property(e => e.CodigoSap)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("codigo_sap");

            builder.Property(e => e.CodigoTributarioMunicipio)
                .HasColumnName("codigo_tributario_municipio");

            builder.Property(e => e.Complemento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("complemento");

            builder.Property(e => e.CEPId)
                .HasColumnName("id_cep");

            builder.Property(e => e.EmpresaClassificacaoId)
                .HasColumnName("id_empresa_classificacao");

            builder.Property(e => e.EmpresaMatrizId)
                .HasColumnName("id_empresa_matriz");

            builder.Property(e => e.TipoLogradouroId)
                .HasColumnName("id_tipo_logradouro");

            builder.Property(e => e.UsuarioAlteracaoId)
                .HasColumnName("id_usuario_alteracao");

            builder.Property(e => e.UsuarioCadastroId)
                .HasColumnName("id_usuario_cadastro");

            builder.Property(e => e.InscricaoEstadual)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("inscricao_estadual");

            builder.Property(e => e.InscricaoMunicipal)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("inscricao_municipal");

            builder.Property(e => e.Logradouro)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("logradouro");

            builder.Property(e => e.Municipio)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("municipio");

            builder.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nome");

            builder.Property(e => e.NomeFantasia)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nome_fantasia");

            builder.Property(e => e.Numero)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("numero");

            builder.Property(e => e.FlagOptanteSimplesNacional)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("optante_simples_nacional");

            builder.Property(e => e.Token)
                .HasMaxLength(32)
                .IsUnicode(false);

            builder.Property(e => e.UF)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("uf");

            builder.Property(e => e.DataCadastro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime")
                .HasColumnName("data_cadastro");

            builder.Property(e => e.DataAlteracao)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_alteracao");

            builder.Property(e => e.FlagIssRetido)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_iss_retido");

            builder.Property(e => e.FlagAtivo)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('S')")
                .IsFixedLength()
                .HasColumnName("flag_ativo");

            builder.HasOne(d => d.Endereco).WithMany(p => p.Empresas)
                .HasForeignKey(d => d.CEPId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}