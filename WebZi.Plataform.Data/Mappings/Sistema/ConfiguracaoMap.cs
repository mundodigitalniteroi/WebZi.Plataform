using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Domain.Models.Sistema;

namespace WebZi.Plataform.Data.Mappings.Sistema
{
    public class ConfiguracaoMap : IEntityTypeConfiguration<ConfiguracaoModel>
    {
        public void Configure(EntityTypeBuilder<ConfiguracaoModel> builder)
        {
            builder
                .ToTable("tb_dep_configuracoes", "dbo")
                .HasKey(e => e.ConfiguracaoId);

            builder.Property(e => e.ConfiguracaoId)
                .HasColumnName("id_configuracao")
                .ValueGeneratedOnAdd();
            
            builder.Property(e => e.CheckUrl)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("check_url");
            
            builder.Property(e => e.DataNovaVersao)
                .HasColumnType("datetime")
                .HasColumnName("data_nova_versao");
            
            builder.Property(e => e.FlagAtualizacaoObrigatoria)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('S')")
                .IsFixedLength()
                .HasColumnName("flag_atualizacao_obrigatoria");
            
            builder.Property(e => e.FlagDetranDesenvolvimentoOnline)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('S')")
                .IsFixedLength()
                .HasColumnName("flag_detran_desenvolvimento_online");
            
            builder.Property(e => e.FlagDetranOnline)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('S')")
                .IsFixedLength()
                .HasColumnName("flag_detran_online");
            
            builder.Property(e => e.FlagDetranProducaoOnline)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('S')")
                .IsFixedLength()
                .HasColumnName("flag_detran_producao_online");
            
            builder.Property(e => e.FlagServicosWindowsAtivos)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_servicos_windows_ativos");
            
            builder.Property(e => e.FtpGrvDocumentosIp)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("FtpGrvDocumentosIP");
            
            builder.Property(e => e.FtpGrvDocumentosPass)
                .HasMaxLength(15)
                .IsUnicode(false);
            
            builder.Property(e => e.FtpGrvDocumentosUser)
                .HasMaxLength(15)
                .IsUnicode(false);
            
            builder.Property(e => e.HorarioVerao)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            
            builder.Property(e => e.PixPassword)
                .HasMaxLength(20)
                .IsUnicode(false);
            
            builder.Property(e => e.PixUrl)
                .HasMaxLength(100)
                .IsUnicode(false);
            
            builder.Property(e => e.PixUsername)
                .HasMaxLength(20)
                .IsUnicode(false);
            
            builder.Property(e => e.RepositorioArquivoNomeBucket)
                .HasMaxLength(20)
                .IsUnicode(false);
            
            builder.Property(e => e.RepositorioArquivoPassword)
                .HasMaxLength(20)
                .IsUnicode(false);
            
            builder.Property(e => e.RepositorioArquivoUrl)
                .HasMaxLength(50)
                .IsUnicode(false);
            
            builder.Property(e => e.RepositorioArquivoUsername)
                .HasMaxLength(20)
                .IsUnicode(false);
            
            builder.Property(e => e.SystemUpdateFtpHost)
                .HasMaxLength(15)
                .IsUnicode(false);
            
            builder.Property(e => e.SystemUpdateFtpPassword)
                .HasMaxLength(25)
                .IsUnicode(false);
            
            builder.Property(e => e.SystemUpdateFtpPort)
                .HasMaxLength(5)
                .IsUnicode(false);
            
            builder.Property(e => e.SystemUpdateFtpUserName)
                .HasMaxLength(25)
                .IsUnicode(false);
            
            builder.Property(e => e.SystemUpdateInstallDirectory)
                .HasMaxLength(150)
                .IsUnicode(false);
            
            builder.Property(e => e.TamanhoMaximoArquivoEnvioServidor)
                .HasColumnName("tamanho_maximo_arquivo_envio_servidor");
            
            builder.Property(e => e.VersaoSistema)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("versao_sistema");
        }
    }
}