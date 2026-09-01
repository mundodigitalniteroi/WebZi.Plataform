using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.GRV.SolicitacaoReboque;

namespace WebZi.Plataform.Data.Mappings.GRV.SolicitacaoReboque;

public class SolicitacaoReboqueEnquadramentoInfracaoMap : IEntityTypeConfiguration<SolicitacaoReboqueEnquadramentoInfracaoModel>
{
    public void Configure(EntityTypeBuilder<SolicitacaoReboqueEnquadramentoInfracaoModel> builder)
    {
        builder.ToTable("tb_dep_enquadramento_infracoes_soliciatacao_reboque").HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id_enquadramento_infracao_solicitacao_reboque")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.SolicitacaoReboqueId)
            .HasColumnName("id_solicitacao_reboque")
            .IsRequired();

        builder.Property(x => x.EnquadramentoInfracaoId)
            .HasColumnName("id_enquadramento_infracao")
            .HasColumnType("numeric(4,0)")
            .IsRequired();

        builder.Property(x => x.NumeroInfracao)
            .HasColumnName("numero_infracao")
            .HasMaxLength(20)
            .IsUnicode(false)
            .IsRequired();

        builder.HasOne(x => x.SolicitacaoReboque)
            .WithMany()
            .HasForeignKey(x => x.SolicitacaoReboqueId);

        builder.HasOne(x => x.EnquadramentoInfracao)
            .WithMany()
            .HasForeignKey(x => x.EnquadramentoInfracaoId);
    }
}
