using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.GRV.SolicitacaoReboque;

namespace WebZi.Plataform.Data.Mappings.GRV.SolicitacaoReboque;

public class SolicitacaoReboqueStatusMap : IEntityTypeConfiguration<SolicitacaoReboqueStatusModel>
{
    public void Configure(EntityTypeBuilder<SolicitacaoReboqueStatusModel> builder)
    {
        builder.ToTable("tb_dep_solicitacao_reboque_status").HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id_solicitacao_reboque_status")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Descricao)
            .HasColumnName("descricao")
            .HasColumnType("varchar(15)");
    }
}