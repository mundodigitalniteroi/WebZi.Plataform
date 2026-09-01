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

        builder.Property(x => x.TipoVeiculoId)
            .HasColumnName("id_tipo_veiculo");

        builder.Property(x => x.CorId)
            .HasColumnName("id_cor");

        builder.Property(x => x.MarcaModeloId)
            .HasColumnName("id_marca_modelo");

        builder.Property(x => x.Placa)
            .HasColumnName("placa")
            .HasMaxLength(7)
            .IsUnicode(false);

        builder.Property(x => x.Chassi)
            .HasColumnName("chassi")
            .HasMaxLength(24)
            .IsUnicode(false);

        builder.Property(x => x.Renavam)
            .HasColumnName("renavam")
            .HasMaxLength(15)
            .IsUnicode(false);

        builder.Property(x => x.VeiculoUF)
            .HasColumnName("veiculo_uf")
            .HasMaxLength(2)
            .IsUnicode(false);

        builder.HasOne(x => x.SolicitacaoReboque)
            .WithMany()
            .HasForeignKey(x => x.SolicitacaoReboqueId);

        builder.HasOne(x => x.AutoridadeResponsavel)
            .WithMany()
            .HasForeignKey(x => x.AutoridadeResponsavelId);
    }
}