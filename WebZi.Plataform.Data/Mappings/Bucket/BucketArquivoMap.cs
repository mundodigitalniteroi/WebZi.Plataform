using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Bucket;

namespace WebZi.Plataform.Data.Mappings.Bucket
{
    internal class BucketArquivoMap : IEntityTypeConfiguration<BucketArquivoModel>
    {
        public void Configure(EntityTypeBuilder<BucketArquivoModel> builder)
        {
            builder
                .ToTable("tb_dep_repositorio_arquivos", "dbo")
                .HasKey(e => e.RepositorioArquivoId);

            builder.Property(e => e.RepositorioArquivoId)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.NomeTabelaOrigemId)
                .IsRequired();

            builder.Property(e => e.NomeArquivo)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            
            builder.Property(e => e.PermissaoAcesso)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);
            
            builder.Property(e => e.TipoArquivo)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);
            
            builder.Property(e => e.TipoCadastro)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            
            builder.Property(e => e.Url)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.DataHoraCadastro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime");
        }
    }
}