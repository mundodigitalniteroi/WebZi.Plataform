using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Leilao;

namespace WebZi.Plataform.Data.Mappings.Leilao
{
    public class LeilaoMap : IEntityTypeConfiguration<LeilaoModel>
    {
        public void Configure(EntityTypeBuilder<LeilaoModel> builder)
        {
            builder
                .ToTable("tb_leilao", "dbo", tb =>
                {
                    tb.HasTrigger("tb_leilao_delete");
                    tb.HasTrigger("tb_leilao_insert");
                    tb.HasTrigger("tb_leilao_insert_identificacao_orgao");
                    tb.HasTrigger("tb_leilao_update");
                })
                .HasKey(e => e.LeilaoId);

            builder.Property(e => e.LeilaoId)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();
            
            builder.Property(e => e.Bairro)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("bairro");
            
            builder.Property(e => e.CEP)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("cep");
            
            builder.Property(e => e.DataAgendamento)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("data_agendamento");
            
            builder.Property(e => e.DataEditalLiberacao)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_edital_liberacao");
            
            builder.Property(e => e.DataEncerramento)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_encerramento");
            
            builder.Property(e => e.DataFimRetirada)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("data_fim_retirada");
            
            builder.Property(e => e.DataHoraCadastro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("data_hora_cadastro");
            
            builder.Property(e => e.DataHoraPublicacaoDiarioOficial)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("data_hora_publicacao_diario_oficial");
            
            builder.Property(e => e.DataInicioRetirada)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("data_inicio_retirada");
            
            builder.Property(e => e.DataLeilao)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("data_leilao");
            
            builder.Property(e => e.DataNotificacao)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_notificacao");
            
            builder.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descricao");
            
            builder.Property(e => e.EmailNotificacao)
                .IsUnicode(false)
                .HasColumnName("email_notificacao");
            
            builder.Property(e => e.ComplementoEndereco)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("end_complemento");
            
            builder.Property(e => e.NumeroEndereco)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("end_numero");
            
            builder.Property(e => e.Endereco)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("endereco");
            
            builder.Property(e => e.HoraLeilao)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("hora_leilao");
            
            builder.Property(e => e.ComitenteId)
                .HasColumnName("id_comitente");
            
            builder.Property(e => e.EmpresaId)
                .HasColumnName("id_empresa");
            
            builder.Property(e => e.ExpositorId)
                .HasColumnName("id_expositor");
            
            builder.Property(e => e.LeiloeiroId)
                .HasColumnName("id_leiloeiro");
            
            builder.Property(e => e.RegraPrestacaoContaId)
                .HasColumnName("id_regra_prestacao_contas");
            
            builder.Property(e => e.LeilaoStatusId)
                .HasColumnName("id_status");
            
            builder.Property(e => e.UsuarioCadastroId)
                .HasColumnName("id_usuario_cadastro");
            
            builder.Property(e => e.IdentificacaoLeilaoOrgao)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("identificacao_leilao_orgao");
            
            builder.Property(e => e.LeilaoDsin)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("leilao_dsin");
            
            builder.Property(e => e.Municipio)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("municipio");
            
            builder.Property(e => e.NomeLocal)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nome_local");
            
            builder.Property(e => e.NumeroDiarioOficial)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("numero_diario_oficial");
            
            builder.Property(e => e.OrdemInternaLeilao)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("ordem_interna_leilao");
            
            builder.Property(e => e.OrdemInternaMatriz)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("ordem_interna_matriz");
            
            builder.Property(e => e.UF)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("uf");
        }
    }
}