using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebZi.Plataform.Domain.Models.Documento
{
    public class TiposContatosMap : IEntityTypeConfiguration<TiposContatosModel>
    {
        public void Configure(EntityTypeBuilder<TiposContatosModel> builder)
        {
            builder.ToTable("tb_glo_doc_tipos_contatos", "dbo")
                .HasKey(x => x.TipoContatoId);

            builder.Property(x => x.TipoContatoId)
                .HasColumnName("id_tipo_contato")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Descricao)
                .HasColumnName("descricao")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.Formato)
                .HasColumnName("formato")
                .HasMaxLength(30);

            builder.Property(x => x.TamanhoMinimo)
                .HasColumnName("tamanho_minimo")
                .HasColumnType("tinyint")
                .IsRequired();

            builder.Property(x => x.TamanhoMaximo)
                .HasColumnName("tamanho_maximo")
                .HasColumnType("tinyint")
                .IsRequired();

            builder.Property(x => x.OrdemApresentacao)
                .HasColumnName("ordem_apresentacao")
                .HasColumnType("tinyint")
                .IsRequired();

            builder.Property(x => x.FlagAtivo)
                .HasColumnName("flag_ativo")
                .HasMaxLength(1)
                .IsRequired();
        }
    }
}
