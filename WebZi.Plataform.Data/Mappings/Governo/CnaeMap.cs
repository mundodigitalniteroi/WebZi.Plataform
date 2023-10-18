using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Domain.Models.Governo;

namespace WebZi.Plataform.Data.Mappings.Governo
{
    public class CnaeMap : IEntityTypeConfiguration<CnaeModel>
    {
        public void Configure(EntityTypeBuilder<CnaeModel> builder)
        {
            builder
                .ToTable("tb_gov_cnae", "dbo")
                .HasKey(e => e.CnaeId);

            builder.Property(e => e.CnaeId)
                .HasColumnName("CnaeID")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Codigo)
                .IsRequired()
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength();
            
            builder.Property(e => e.CodigoFormatado)
                .IsRequired()
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength();

            builder.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);
            
            builder.Property(e => e.FlagPrincipal)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();

            builder.Property(e => e.DataCadastro)
                .IsRequired()
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime");

            builder.Property(e => e.DataAlteracao)
                .HasColumnType("smalldatetime");
        }
    }
}