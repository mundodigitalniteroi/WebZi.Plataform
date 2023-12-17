using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Pessoa.Documento;

namespace WebZi.Plataform.Data.Mappings.Pessoa.Documento
{
    public class TipoDocumentoIdentificacaoMap : IEntityTypeConfiguration<TipoDocumentoIdentificacaoModel>
    {
        public void Configure(EntityTypeBuilder<TipoDocumentoIdentificacaoModel> builder)
        {
            builder
                .ToTable("tb_glo_doc_tipos_documentos_identificacao", "dbo")
                .HasKey(x => x.TipoDocumentoIdentificacaoId);

            builder.Property(e => e.TipoDocumentoIdentificacaoId)
                .HasColumnName("id_tipo_documento_identificacao")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Codigo)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("codigo");

            builder.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descricao");

            builder.Property(e => e.FlagAtivo)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('S')")
                .IsFixedLength()
                .HasColumnName("flag_ativo");

            builder.Property(e => e.FlagPossuiComplemento)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_possui_complemento");

            builder.Property(e => e.FlagPossuiDataEmissao)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_possui_data_emissao");

            builder.Property(e => e.FlagPossuiDataValidade)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_possui_data_validade");

            builder.Property(e => e.FlagPrincipal)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_principal");

            builder.Property(e => e.Formato)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("formato");

            builder.Property(e => e.OrdemApresentacao)
                .HasDefaultValueSql("((255))")
                .HasColumnName("ordem_apresentacao");

            builder.Property(e => e.TamanhoMaximo)
                .HasColumnName("tamanho_maximo");

            builder.Property(e => e.TamanhoMinimo)
                .HasDefaultValueSql("((1))")
                .HasColumnName("tamanho_minimo");
        }
    }
}