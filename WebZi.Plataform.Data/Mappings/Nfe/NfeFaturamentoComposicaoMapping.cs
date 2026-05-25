using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Nfe;

namespace WebZi.Plataform.Data.Mappings.Nfe;

public class NfeFaturamentoComposicaoMapping : IEntityTypeConfiguration<NfeFaturamentoComposicaoModel>
{
    public void Configure(EntityTypeBuilder<NfeFaturamentoComposicaoModel> builder)
    {
        builder.ToTable("tb_dep_nfe_faturamento_composicao", "dbo");

        builder.HasKey(e => e.NfeFaturamentoComposicaoId);

        builder.Property(e => e.NfeFaturamentoComposicaoId)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.NfeId);

        builder.Property(e => e.FaturamentoComposicaoId);

        builder.Property(e => e.StatusCadastroErp)
            .IsRequired()
            .HasMaxLength(1)
            .IsUnicode(false)
            .HasDefaultValueSql("('P')")
            .IsFixedLength()
            .HasComment("P = PENDENTE DE CADASTRO; F = CADASTRO FINALIZADO; E = ERRO NO CADASTRO");

        builder.HasOne(d => d.Nfe)
            .WithMany(x => x.NfeFaturamentoComposicao) // <-- aponta para a coleção
            .HasForeignKey(d => d.NfeId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(d => d.FaturamentoComposicao)
            .WithMany()
            .HasForeignKey(d => d.FaturamentoComposicaoId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
