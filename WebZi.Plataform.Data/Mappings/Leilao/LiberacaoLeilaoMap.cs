using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Leilao;

namespace WebZi.Plataform.Data.Mappings.Leilao
{
    internal class LiberacaoLeilaoMap : IEntityTypeConfiguration<LiberacaoLeilaoModel>
    {
        public void Configure(EntityTypeBuilder<LiberacaoLeilaoModel> builder)
        {
            builder
                .ToTable("tb_dep_liberacao_leilao", "dbo")
                .HasKey(x => x.LiberacaoLeilaoId);

            builder.Property(e => e.LiberacaoLeilaoId)
                .HasColumnName("id_liberacao_leilao")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.GrvId)
                .HasColumnName("id_grv");

            builder.Property(e => e.StatusOperacaoLeilaoId)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('1')")
                .IsFixedLength()
                .HasColumnName("id_status_operacao_leilao");

            builder.Property(e => e.UsuarioCadastroId)
                .HasColumnName("id_usuario_cadastro");

            builder.Property(e => e.DataCadastro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime")
                .HasColumnName("data_cadastro");
        }
    }
}