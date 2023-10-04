using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Domain.Models.Documento;

namespace WebZi.Plataform.Data.Mappings.Documento
{
    public class OrgaoEmissorMap : IEntityTypeConfiguration<OrgaoEmissorModel>
    {
        public void Configure(EntityTypeBuilder<OrgaoEmissorModel> builder)
        {
            builder
                .ToTable("tb_glo_doc_orgaos_emissores", "dbo")
                .HasKey(e => e.OrgaoEmissorId);

            builder.Property(e => e.OrgaoEmissorId)
                .HasColumnName("id_orgao_emissor")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.CodigoOrgao)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("codigo_orgao");
            
            builder.Property(e => e.Descricao)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descricao");
            
            builder.Property(e => e.Sigla)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("sigla");
            
            builder.Property(e => e.UF)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("uf");

            builder.Property(e => e.FlagAutoridadeResponsavel)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_autoridade_responsavel");

            builder.Property(e => e.FlagDetran)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_detran");

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