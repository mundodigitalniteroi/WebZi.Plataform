using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebZi.Plataform.Domain.Models.Pessoa.Contato
{
    public class TiposContatoPessoaMap : IEntityTypeConfiguration<TiposContatoPessoaModel>
    {
        public void Configure(EntityTypeBuilder<TiposContatoPessoaModel> builder)
        {
            builder.ToTable("tb_glo_pes_pessoas_tipos_contatos", "dbo")
                .HasKey(x => x.PessoaTipoContatoId);

            builder.Property(x => x.PessoaTipoContatoId)
                .HasColumnType("bigint")
                .HasColumnName("id_pessoa_tipo_contato")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.PessoaId)
                .HasColumnName("id_pessoa")
                .HasColumnType("bigint")
                .IsRequired();

            builder.Property(x => x.TipoContatoId)
                .HasColumnName("id_tipo_contato")
                .IsRequired();

            builder.Property(x => x.Descricao)
                .HasColumnName("descricao")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(x => x.FlagContatoPrincipal)
                .HasColumnName("flag_contato_principal")
                .HasMaxLength(1)
                .IsRequired();

            builder.HasOne(x => x.TiposContatos)
                .WithMany()
                .HasForeignKey(x => x.TipoContatoId);
        }
    }
}
