using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.GRV.SolicitacaoReboque;

namespace WebZi.Plataform.Data.Mappings.GRV.SolicitacaoReboque;

public class SolicitacaoReboqueLacreMap : IEntityTypeConfiguration<SolicitacaoReboqueLacreModel>
{
    public void Configure(EntityTypeBuilder<SolicitacaoReboqueLacreModel> builder)
    {
        builder.ToTable("tb_dep_solicitacao_reboque_lacres").HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id_lacre")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.SolicitacaoReboqueId)
            .HasColumnName("id_solicitacao_reboque")
            .IsRequired();

        builder.Property(x => x.LacreMotivoDesassociacaoId)
            .HasColumnName("id_lacre_motivo_desassociacao");

        builder.Property(x => x.UsuarioCadastroId)
            .HasColumnName("id_usuario_cadastro")
            .IsRequired();

        builder.Property(x => x.UsuarioAtualizacaoId)
            .HasColumnName("id_usuario_atualizacao");

        builder.Property(x => x.Lacre)
            .HasColumnName("lacre")
            .HasMaxLength(20)
            .IsUnicode(false)
            .IsRequired();

        builder.Property(x => x.LacreAnterior)
            .HasColumnName("lacre_anterior")
            .HasMaxLength(20)
            .IsUnicode(false);

        builder.Property(x => x.DataCadastro)
            .HasColumnName("data_cadastro")
            .IsRequired();

        builder.Property(x => x.DataAtualizacao)
            .HasColumnName("data_atualizacao");

        builder.HasOne(x => x.SolicitacaoReboque)
            .WithMany()
            .HasForeignKey(x => x.SolicitacaoReboqueId);

        builder.HasOne(x => x.UsuarioCadastro)
            .WithMany()
            .HasForeignKey(x => x.UsuarioCadastroId);

        builder.HasOne(x => x.UsuarioAtualizacao)
            .WithMany()
            .HasForeignKey(x => x.UsuarioAtualizacaoId);
    }
}
