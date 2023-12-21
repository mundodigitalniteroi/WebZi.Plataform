using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Domain.Models.WebServices.DetranRio;

namespace WebZi.Plataform.Data.Mappings.WebServices.DetranRio
{
    public class DetranRioVeiculoMap : IEntityTypeConfiguration<DetranRioVeiculoModel>
    {
        public void Configure(EntityTypeBuilder<DetranRioVeiculoModel> builder)
        {
            builder
                .ToTable("tb_detran_veiculos_ws", "dbo")
                .HasKey(e => e.DetranVeiculoId);

            builder.Property(e => e.DetranVeiculoId).HasColumnName("id_detran_veiculo");
            
            builder.Property(e => e.AnoFabricacao).HasColumnName("ano_fabricacao");
            
            builder.Property(e => e.AnoModelo).HasColumnName("ano_modelo");
            
            builder.Property(e => e.AnoUltimaLicenca).HasColumnName("ano_ultima_licenca");
            
            builder.Property(e => e.CapacidadeCarga)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("capacidade_carga");
            
            builder.Property(e => e.CapacidadePassageiros).HasColumnName("capacidade_passageiros");
            
            builder.Property(e => e.Chassi)
                .HasMaxLength(24)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("chassi");
            
            builder.Property(e => e.ChassiRemarcado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("chassi_remarcado");
            
            builder.Property(e => e.Classificacao)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("classificacao");
            
            builder.Property(e => e.CodigoCategoria)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("codigo_categoria");
            
            builder.Property(e => e.DataCadastro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime")
                .HasColumnName("data_cadastro");
            
            builder.Property(e => e.DescricaoCategoria)
                .HasMaxLength(12)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("descricao_categoria");
            
            builder.Property(e => e.DescricaoTipo)
                .HasMaxLength(18)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("descricao_tipo");
            
            builder.Property(e => e.FlagRegistroNormalizado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("flag_registro_normalizado");
            
            builder.Property(e => e.CorId)
                .HasColumnName("id_cor");
            
            builder.Property(e => e.MarcaModeloId)
                .HasColumnName("id_detran_marca_modelo");
            
            builder.Property(e => e.InformacaoRoubo)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("informacao_roubo");
            
            builder.Property(e => e.PesoBrutoTotal)
                .HasMaxLength(6)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("peso_bruto_total");
            
            builder.Property(e => e.Placa)
                .HasMaxLength(7)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("placa");
            
            builder.Property(e => e.Renavam)
                .HasMaxLength(11)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("renavam");
            
            builder.Property(e => e.RestricaoEstelionato)
                .HasMaxLength(30)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("restricao_estelionato");
            
            builder.Property(e => e.Uf)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("uf");
        }
    }
}