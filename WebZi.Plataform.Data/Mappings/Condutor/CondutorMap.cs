using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Condutor;

namespace WebZi.Plataform.Data.Mappings.Condutor
{
    public class CondutorMap : IEntityTypeConfiguration<CondutorModel>
    {
        public void Configure(EntityTypeBuilder<CondutorModel> builder)
        {
            builder
                .ToTable("tb_dep_condutor", "dbo", tb => tb.HasTrigger("tr_log_upd_condutor"))
                .HasKey(x => x.CondutorId);

            builder.Property(e => e.CondutorId)
                .HasColumnName("id_condutor")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.GrvId)
                .HasColumnName("id_grv");

            builder.Property(e => e.Documento)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("documento");

            builder.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("email");

            builder.Property(e => e.PessoaId)
                .HasColumnName("id_pessoa");

            builder.Property(e => e.Identidade)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("identidade");

            builder.Property(e => e.InformacoesAdicionais)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("informacoes_adicionais");

            builder.Property(e => e.Nome)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("nome");

            builder.Property(e => e.NumeroChaveVeiculo)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("numero_chave_veiculo");

            builder.Property(e => e.NumeroInfracao)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("numero_infracao");

            builder.Property(e => e.OrgaoExpedidor)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("orgao_expedidor");

            builder.Property(e => e.StatusAssinaturaCondutor)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('2')")
                .IsFixedLength()
                .HasComment("1 = ASSINOU;\r\n2 = AUSENTE;\r\n3 = EVADIU-SE;\r\n4 = RECUSOU-SE.")
                .HasColumnName("status_assinatura_condutor");

            builder.Property(e => e.Telefone)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("telefone");

            builder.Property(e => e.TelefoneDDD)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("telefone_ddd");

            builder.Property(e => e.FlagChaveVeiculo)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_chave_veiculo");

            builder.Property(e => e.FlagDocumentacaoVeiculo)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_documentacao_veiculo");
        }
    }
}