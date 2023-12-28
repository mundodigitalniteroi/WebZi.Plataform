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
                .HasKey(x => x.DetranVeiculoId);

            builder.Property(x => x.DetranVeiculoId)
                .HasColumnName("id_detran_veiculo");
            
            builder.Property(x => x.AnoFabricacao)
                .HasColumnName("ano_fabricacao");
            
            builder.Property(x => x.AnoModelo)
                .HasColumnName("ano_modelo");
            
            builder.Property(x => x.AnoUltimaLicenca)
                .HasColumnName("ano_ultima_licenca");
            
            builder.Property(x => x.CapacidadeCarga)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("capacidade_carga");
            
            builder.Property(x => x.CapacidadePassageiros)
                .HasColumnName("capacidade_passageiros");
            
            builder.Property(x => x.Chassi)
                .HasMaxLength(24)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("chassi");
            
            builder.Property(x => x.ChassiRemarcado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("chassi_remarcado");
            
            builder.Property(x => x.Classificacao)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("classificacao");
            
            builder.Property(x => x.CodigoCategoria)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("codigo_categoria");

            builder.Property(x => x.DescricaoCategoria)
                .HasMaxLength(12)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("descricao_categoria");
            
            builder.Property(x => x.DescricaoTipo)
                .HasMaxLength(18)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("descricao_tipo");
            
            builder.Property(x => x.CorId)
                .HasColumnName("id_cor");
            
            builder.Property(x => x.MarcaModeloId)
                .HasColumnName("id_detran_marca_modelo");
            
            builder.Property(x => x.InformacaoRoubo)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("informacao_roubo");
            
            builder.Property(x => x.PesoBrutoTotal)
                .HasMaxLength(6)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("peso_bruto_total");
            
            builder.Property(x => x.Placa)
                .HasMaxLength(7)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("placa");
            
            builder.Property(x => x.Renavam)
                .HasMaxLength(11)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("renavam");
            
            builder.Property(x => x.RestricaoEstelionato)
                .HasMaxLength(30)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("restricao_estelionato");
            
            builder.Property(x => x.Uf)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("uf");

            builder.Property(x => x.FlagRegistroNormalizado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("flag_registro_normalizado");

            builder.Property(x => x.DataCadastro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime")
                .HasColumnName("data_cadastro");

            builder.Property(x => x.DataAlteracao)
                .HasColumnType("smalldatetime");
        }
    }
}