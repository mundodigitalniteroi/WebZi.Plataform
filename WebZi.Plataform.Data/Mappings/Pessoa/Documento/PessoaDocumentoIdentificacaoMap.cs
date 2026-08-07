using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Pessoa.Documento;

namespace WebZi.Plataform.Data.Mappings.Pessoa.Documento
{
    public class PessoaDocumentoIdentificacaoMap : IEntityTypeConfiguration<PessoaDocumentoIdentificacaoModel>
    {
        public void Configure(EntityTypeBuilder<PessoaDocumentoIdentificacaoModel> builder)
        {
            builder
                .ToTable("tb_glo_pes_pessoas_documentos_identificacao", "dbo")
                .HasKey(x => x.PessoaDocumentoIdentificacaoId);

            builder.Property(e => e.PessoaDocumentoIdentificacaoId)
                .HasColumnName("id_pessoa_documento_identificacao")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.IdPessoa)
                .HasColumnName("id_pessoa");

            builder.Property(e => e.IdOrgaoEmissor)
                .HasColumnName("id_orgao_emissor");

            builder.Property(e => e.IdTipoDocumentoIdentificacao)
                .HasColumnName("id_tipo_documento_identificacao");

            builder.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descricao");

            builder.Property(e => e.DataEmissao)
                .HasColumnType("date")
                .HasColumnName("data_emissao");

            builder.Property(e => e.DataValidade)
                .HasColumnType("date")
                .HasColumnName("data_validade");

            builder.Property(e => e.Complemento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("complemento");

            builder.HasOne(d => d.Pessoa)
                .WithMany(p => p.DocumentosIdentificacao)
                .HasForeignKey(d => d.IdPessoa)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.TipoDocumentoIdentificacao)
                .WithMany()
                .HasForeignKey(d => d.IdTipoDocumentoIdentificacao)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.OrgaoEmissor)
                .WithMany()
                .HasForeignKey(d => d.IdOrgaoEmissor);
        }
    }
}
