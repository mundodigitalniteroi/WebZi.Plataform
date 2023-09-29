using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Bucket;

namespace WebZi.Plataform.Data.Mappings.Bucket
{
    internal class BucketNomeTabelaOrigemMap : IEntityTypeConfiguration<BucketNomeTabelaOrigemModel>
    {
        public void Configure(EntityTypeBuilder<BucketNomeTabelaOrigemModel> builder)
        {
            builder
                .ToTable("tb_dep_configuracoes_nome_tabela_origem", "dbo")
                .HasKey(e => e.NomeTabelaOrigemId);

            builder.Property(e => e.NomeTabelaOrigemId)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Codigo)
                .IsRequired()
                .HasMaxLength(15)
                .IsUnicode(false)
                .IsFixedLength();
            
            builder.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            
            builder.Property(e => e.DiretorioRemoto)
                .IsRequired()
                .HasMaxLength(25)
                .IsUnicode(false);
            
            builder.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
        }
    }
}