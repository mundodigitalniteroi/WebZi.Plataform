using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.GRV.SolicitacaoReboque;

namespace WebZi.Plataform.Data.Mappings.GRV.SolicitacaoReboque;

public class SolicitacaoReboqueGrvMap : IEntityTypeConfiguration<SolicitacaoReboqueGrvModel>
{
    public void Configure(EntityTypeBuilder<SolicitacaoReboqueGrvModel> builder)
    {
        builder.ToTable("tb_dep_solicitacao_reboque_grv").HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id_solicitacao_reboque_grv")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.SolicitacaoReboqueId)
            .HasColumnName("id_solicitacao_reboque");

        builder.Property(x => x.AutoridadeResponsavelId)
            .HasColumnName("id_autoridade_responsavel");

        builder.Property(x => x.MatriculaAutoridadeResponsavel)
            .HasColumnName("matricula_autoridade_responsavel");
        builder.Property(x => x.NomeAutoridadeResponsavel)
            .HasColumnName("nome_autoridade_responsavel");


        builder.HasOne(x => x.SolicitacaoReboque)
            .WithMany()
            .HasForeignKey(x => x.SolicitacaoReboqueId);
        builder.HasOne(x => x.AutoridadeResponsavel)
            .WithMany()
            .HasForeignKey(x => x.AutoridadeResponsavelId);
    }
}