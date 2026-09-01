using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.GRV.SolicitacaoReboque;

namespace WebZi.Plataform.Data.Mappings.GRV.SolicitacaoReboque;

public class SolicitacaoReboqueMap : IEntityTypeConfiguration<SolicitacaoReboqueModel>
{
    public void Configure(EntityTypeBuilder<SolicitacaoReboqueModel> builder)
    {
        builder.ToTable("tb_dep_solicitacao_reboque").HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id_solicitacao_reboque")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.ClienteDepositoId)
            .HasColumnName("id_cliente_deposito");

        builder.Property(x => x.ReboqueId)
            .HasColumnName("id_reboque");
        builder.Property(x => x.ReboquistaId)
            .HasColumnName("id_reboquista");

        builder.Property(x => x.SolicitacaoReboqueTipoId)
            .HasColumnName("id_solicitacao_reboque_tipo")
            .HasColumnType("tinyint");
        builder.Property(x => x.SolicitacaoReboqueStatusId)
            .HasColumnName("id_solicitacao_reboque_status")
            .HasColumnType("tinyint");

        builder.Property(x => x.MotivoApreensaoId)
            .HasColumnName("id_motivo_apreensao")
            .HasColumnType("tinyint");

        builder.Property(x => x.GrvId)
            .HasColumnName("id_grv");

        builder.Property(x => x.UsuarioCadastroId)
            .HasColumnName("id_usuario_cadastro");
        builder.Property(x => x.UsuarioAlteracaoId)
            .HasColumnName("id_usuario_alteracao");

        builder.Property(x => x.LocalRemocaoCompleto)
            .HasColumnName("local_remocao_endereco_completo")
            .HasMaxLength(200);
        builder.Property(x => x.LocalRemocaoReferencia)
            .HasColumnName("local_remocao_referencia")
            .HasMaxLength(200);
        builder.Property(x => x.LocalRemocaoLatitude)
            .HasColumnName("local_remocao_latitude")
            .HasMaxLength(15);
        builder.Property(x => x.LocalRemocaoLongitude)
            .HasColumnName("local_remocao_longitude")
            .HasMaxLength(15);
        builder.Property(x => x.DataCadastro)
            .HasColumnName("data_cadastro");
        builder.Property(x => x.DataAlteracao)
            .HasColumnName("data_alteracao");

        builder.HasOne(x => x.ClienteDeposito)
            .WithMany()
            .HasForeignKey(x => x.ClienteDepositoId);

        builder.HasOne(x => x.Reboque)
            .WithMany()
            .HasForeignKey(x => x.ReboqueId);

        builder.HasOne(x => x.Reboquista)
            .WithMany()
            .HasForeignKey(x => x.ReboquistaId);

        builder.HasOne(x => x.SolicitacaoReboqueTipo)
            .WithMany()
            .HasForeignKey(x => x.SolicitacaoReboqueTipoId);

        builder.HasOne(x => x.SolicitacaoReboqueStatus)
            .WithMany()
            .HasForeignKey(x => x.SolicitacaoReboqueStatusId);

        builder.HasOne(x => x.Grv)
            .WithMany()
            .HasForeignKey(x => x.GrvId);

        builder.HasOne(x => x.UsuarioCadastro)
            .WithMany()
            .HasForeignKey(x => x.UsuarioCadastroId);

        builder.HasOne(x => x.UsuarioAlteracao)
            .WithMany()
            .HasForeignKey(x => x.UsuarioAlteracaoId);
        builder.HasOne(x => x.MotivoApreensao)
            .WithMany()
            .HasForeignKey(x => x.MotivoApreensaoId);
    }
}