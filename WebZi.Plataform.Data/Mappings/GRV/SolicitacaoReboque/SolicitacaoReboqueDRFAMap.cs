using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.GRV.SolicitacaoReboque;

namespace WebZi.Plataform.Data.Mappings.GRV.SolicitacaoReboque;

public class SolicitacaoReboqueDRFAMap : IEntityTypeConfiguration<SolicitacaoReboqueDRFAModel>
{
    public void Configure(EntityTypeBuilder<SolicitacaoReboqueDRFAModel> builder)
    {
        builder.ToTable("tb_dep_solicitacao_reboque_drfa").HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id_solicitacao_reboque_drfa")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.SolicitacaoReboqueId)
            .HasColumnName("id_solicitacao_reboque")
            .IsRequired();

        builder.Property(x => x.TipoRegistroId)
            .HasColumnName("id_grv_drfa_tipo_registro")
            .HasColumnType("tinyint")
            .IsRequired();

        builder.Property(x => x.OrgaoEmissorId)
            .HasColumnName("id_orgao_emissor")
            .HasColumnType("smallint");

        builder.Property(x => x.AutoridadeDivisaoId)
            .HasColumnName("id_autoridade_divisao")
            .HasColumnType("tinyint");

        builder.Property(x => x.AutoridadeDivisaoComplemento)
            .HasColumnName("autoridade_divisao_complemento")
            .HasMaxLength(15)
            .IsUnicode(false);

        builder.Property(x => x.NumeroRegistroRouboFurto)
            .HasColumnName("numero_registro_roubo_furto")
            .HasMaxLength(35)
            .IsUnicode(false);

        builder.Property(x => x.RegistroRouboFurtoMatriculaAgente)
            .HasColumnName("registro_roubo_furto_matricula_agente")
            .HasMaxLength(35)
            .IsUnicode(false);

        builder.Property(x => x.RegistroRouboFurtoNomeAgente)
            .HasColumnName("registro_roubo_furto_nome_agente")
            .HasMaxLength(100)
            .IsUnicode(false);

        builder.Property(x => x.EstadoGeralVeiculo)
            .HasColumnName("estado_geral_veiculo")
            .HasMaxLength(500)
            .IsUnicode(false);

        builder.HasOne(x => x.SolicitacaoReboque)
            .WithMany()
            .HasForeignKey(x => x.SolicitacaoReboqueId);

        builder.HasOne(x => x.TipoRegistro)
            .WithMany()
            .HasForeignKey(x => x.TipoRegistroId);

        builder.HasOne(x => x.OrgaoEmissor)
            .WithMany()
            .HasForeignKey(x => x.OrgaoEmissorId);

        builder.HasOne(x => x.AutoridadeDivisao)
            .WithMany()
            .HasForeignKey(x => x.AutoridadeDivisaoId);
    }
}