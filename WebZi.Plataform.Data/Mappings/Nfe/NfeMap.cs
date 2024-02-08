using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Nfe;

namespace WebZi.Plataform.Data.Mappings.Nfe
{
    public class NfeMap : IEntityTypeConfiguration<NfeModel>
    {
        public void Configure(EntityTypeBuilder<NfeModel> builder)
        {
            builder.HasKey(e => e.NfeId);

            builder.ToTable("tb_dep_nfe", tb =>
            {
                tb.HasTrigger("tr_log_del_nfe");
                tb.HasTrigger("tr_log_upd_nfe");
            });

            builder.Property(e => e.CaminhoXmlNotaFiscal)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.Cnpj)
                .IsRequired()
                .HasMaxLength(14)
                .IsUnicode(false);

            builder.Property(e => e.CodigoVerificacao)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.DataAlteracao)
                .HasColumnType("smalldatetime");

            builder.Property(e => e.DataCadastro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime");

            builder.Property(e => e.DataEmissao)
                .HasColumnType("datetime");

            builder.Property(e => e.Numero)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.NumeroNotaFiscal)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.NumeroRps)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.Referencia)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.SerieRps)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('C')")
                .IsFixedLength()
                .HasComment("C: Cadastro;\r\nA: Aguardando Processamento (envio da solicitação com sucesso, para a Prefeitura);\r\nP: Processado (download da Nfe e atualização da Nfe no Sistema concluídos com sucesso);\r\nE: Erro (quando a Prefeitura indicou algum problema);\r\nR: Reprocessar (marcação manual para o envio de uma nova solicitação de Nfe para o mesmo GRV, esta opção gera um novo registro de Nfe);\r\nS: ReproceSsado (conclusão do reprocessamento);\r\nI: Inválido;\r\nN: CaNcelado;\r\nM: Cadastro Manual.");

            builder.Property(e => e.StatusNfe)
                .HasMaxLength(15)
                .IsUnicode(false);

            builder.Property(e => e.Url)
                .HasMaxLength(500)
                .IsUnicode(false);

            //builder.HasOne(d => d.Grv).WithMany(p => p.TbDepNves)
            //    .HasForeignKey(d => d.GrvId)
            //    .OnDelete(DeleteBehavior.NoAction)
            //    .HasConstraintName("FK_tb_dep_nfe1");

            //builder.HasOne(d => d.NfeComplementar).WithMany(p => p.InverseNfeComplementar)
            //    .HasForeignKey(d => d.NfeComplementarId)
            //    .HasConstraintName("FK_tb_dep_nfe2");

            //builder.HasOne(d => d.StatusNavigation).WithMany(p => p.TbDepNves)
            //    .HasForeignKey(d => d.Status)
            //    .OnDelete(DeleteBehavior.NoAction)
            //    .HasConstraintName("FK_tb_dep_nfe4");

            //builder.HasOne(d => d.UsuarioCadastro).WithMany(p => p.TbDepNves)
            //    .HasForeignKey(d => d.UsuarioCadastroId)
            //    .OnDelete(DeleteBehavior.NoAction)
            //    .HasConstraintName("FK_tb_dep_nfe3");
        }
    }
}
