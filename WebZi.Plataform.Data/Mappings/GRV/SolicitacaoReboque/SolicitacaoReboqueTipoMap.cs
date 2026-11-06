using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.GRV.SolicitacaoReboque;

namespace WebZi.Plataform.Data.Mappings.GRV.SolicitacaoReboque;

public class SolicitacaoReboqueTipoMap : IEntityTypeConfiguration<SolicitacaoReboqueTipoModel>
{
    public void Configure(EntityTypeBuilder<SolicitacaoReboqueTipoModel> builder)
    {
        builder.ToTable("tb_dep_solicitacao_reboque_tipo").HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id_solicitacao_reboque_tipo")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.MotivoApreensaoId)
            .HasColumnName("id_motivo_apreensao");

        builder.Property(x => x.FaturamentoProdutoCodigo)
            .HasColumnName("faturamento_produto_codigo")
            .HasColumnType("char(3)");

        builder.Property(x => x.Descricao)
            .HasColumnName("descricao")
            .HasColumnType("varchar(15)");

        builder.HasOne(x => x.MotivoApreensao)
            .WithMany()
            .HasForeignKey(x => x.MotivoApreensaoId);
    }
}