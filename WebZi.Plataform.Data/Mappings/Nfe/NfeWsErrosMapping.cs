using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Nfe;

namespace WebZi.Plataform.Data.Mappings.Nfe;

public class NfeWsErrosMapping : IEntityTypeConfiguration<NfeWsErrosModel>
{
    public void Configure(EntityTypeBuilder<NfeWsErrosModel> builder)
    {
        builder.ToTable("tb_dep_nfe_ws_erros", "dbo");

        builder.HasKey(e => e.ErroId);

        builder.Property(e => e.ErroId)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.GrvId)
            .IsRequired();
        
        builder.Property(e => e.IdentificadorNota);

        builder.Property(e => e.UsuarioId)
            .IsRequired();

        builder.Property(e => e.Acao)
            .IsRequired()
            .HasMaxLength(1)
            .IsUnicode(false)
            .IsFixedLength();

        builder.Property(e => e.OrigemErro)
            .IsRequired()
            .HasMaxLength(1)
            .IsUnicode(false)
            .IsFixedLength();

        builder.Property(e => e.Status)
            .HasMaxLength(30)
            .IsUnicode(false);

        builder.Property(e => e.CodigoErro)
            .HasMaxLength(30)
            .IsUnicode(false);

        builder.Property(e => e.MensagemErro)
            .HasMaxLength(1000)
            .IsUnicode(false);

        builder.Property(e => e.CorrecaoErro)
            .HasMaxLength(1000)
            .IsUnicode(false);

        builder.Property(e => e.DataHoraCadastro)
            .HasColumnType("smalldatetime")
            .HasDefaultValueSql("(getdate())");
    }
}