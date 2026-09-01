using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.GRV.SolicitacaoReboque;

namespace WebZi.Plataform.Data.Mappings.GRV.SolicitacaoReboque;

public class SolicitacaoReboqueCondutorMap : IEntityTypeConfiguration<SolicitacaoReboqueCondutorModel>
{
    public void Configure(EntityTypeBuilder<SolicitacaoReboqueCondutorModel> builder)
    {
        builder.ToTable("tb_dep_condutor_solicitacao_reboque").HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id_condutor")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.SolicitacaoReboqueGrvId)
            .HasColumnName("id_solicitacao_reboque_grv")
            .IsRequired();

        builder.Property(x => x.PessoaId)
            .HasColumnName("id_pessoa");

        builder.Property(x => x.EnquadramentoInfracaoId)
            .HasColumnName("id_enquadramento_infracao")
            .HasColumnType("numeric(4,0)");

        builder.Property(x => x.Documento)
            .HasColumnName("documento")
            .HasMaxLength(20)
            .IsUnicode(false);

        builder.Property(x => x.Identidade)
            .HasColumnName("identidade")
            .HasMaxLength(20)
            .IsUnicode(false);

        builder.Property(x => x.OrgaoExpedidor)
            .HasColumnName("orgao_expedidor")
            .HasMaxLength(10)
            .IsUnicode(false);

        builder.Property(x => x.Nome)
            .HasColumnName("nome")
            .HasMaxLength(150)
            .IsUnicode(false);

        builder.Property(x => x.Telefone)
            .HasColumnName("telefone")
            .HasMaxLength(9)
            .IsUnicode(false);

        builder.Property(x => x.TelefoneDDD)
            .HasColumnName("telefone_ddd")
            .HasMaxLength(2)
            .IsUnicode(false);

        builder.Property(x => x.Email)
            .HasColumnName("email")
            .HasMaxLength(150)
            .IsUnicode(false);

        builder.Property(x => x.NumeroChaveVeiculo)
            .HasColumnName("numero_chave_veiculo")
            .HasMaxLength(6)
            .IsUnicode(false);

        builder.Property(x => x.NumeroInfracao)
            .HasColumnName("numero_infracao")
            .HasMaxLength(20)
            .IsUnicode(false);

        builder.Property(x => x.InformacoesAdicionais)
            .HasColumnName("informacoes_adicionais")
            .HasMaxLength(1000)
            .IsUnicode(false);

        builder.Property(x => x.OutrosEquipamentos1)
            .HasColumnName("outros_equipamentos1")
            .HasMaxLength(50)
            .IsUnicode(false);

        builder.Property(x => x.OutrosEquipamentos2)
            .HasColumnName("outros_equipamentos2")
            .HasMaxLength(50)
            .IsUnicode(false);

        builder.Property(x => x.OutrosEquipamentos3)
            .HasColumnName("outros_equipamentos3")
            .HasMaxLength(50)
            .IsUnicode(false);

        builder.Property(x => x.OutrosEquipamentos4)
            .HasColumnName("outros_equipamentos4")
            .HasMaxLength(50)
            .IsUnicode(false);

        builder.Property(x => x.OutrosEquipamentos5)
            .HasColumnName("outros_equipamentos5")
            .HasMaxLength(50)
            .IsUnicode(false);

        builder.Property(x => x.StatusAssinaturaCondutor)
            .HasColumnName("status_assinatura_condutor")
            .HasMaxLength(1)
            .IsUnicode(false);

        builder.Property(x => x.FlagChaveVeiculo)
            .HasColumnName("flag_chave_veiculo")
            .HasMaxLength(1)
            .IsUnicode(false);

        builder.Property(x => x.FlagDocumentacaoVeiculo)
            .HasColumnName("flag_documentacao_veiculo")
            .HasMaxLength(1)
            .IsUnicode(false);

        builder.Property(x => x.Celular)
            .HasColumnName("celular")
            .HasMaxLength(9)
            .IsUnicode(false);

        builder.Property(x => x.CelularDDD)
            .HasColumnName("celular_ddd")
            .HasMaxLength(2)
            .IsUnicode(false);

        builder.HasOne(x => x.SolicitacaoReboqueGrv)
            .WithMany()
            .HasForeignKey(x => x.SolicitacaoReboqueGrvId);

        builder.HasOne(x => x.Pessoa)
            .WithMany()
            .HasForeignKey(x => x.PessoaId);

        builder.HasOne(x => x.EnquadramentoInfracao)
            .WithMany()
            .HasForeignKey(x => x.EnquadramentoInfracaoId);
    }
}
