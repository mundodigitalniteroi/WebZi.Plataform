using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Domain.Models.WebServices.Rio;

namespace WebZi.Plataform.Data.Mappings.WebServices.DetranRio
{
    public class DetranRioVeiculoRestricaoMap : IEntityTypeConfiguration<DetranRioVeiculoRestricaoModel>
    {
        public void Configure(EntityTypeBuilder<DetranRioVeiculoRestricaoModel> builder)
        {
            builder
                .ToTable("tb_detran_veiculos_ws_restricoes", "dbo")
                .HasKey(e => e.DetranVeiculoRestricaoId);

            builder.Property(e => e.DetranVeiculoRestricaoId).HasColumnName("id_detran_veiculos_ws_restricoes");
            
            builder.Property(e => e.CodigoRestricao).HasColumnName("codigo_restricao");
            
            builder.Property(e => e.DetranVeiculoId).HasColumnName("id_detran_veiculo");
            
            builder.Property(e => e.DetranVeiculoOrigemRestricaoId).HasColumnName("id_detran_veiculos_ws_restricao_origem");
            
            builder.Property(e => e.Restricao)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("restricao");

            builder.Property(e => e.TipoRestricao)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasComment("A = Administrativa;\r\nE = Estelionato;\r\nJ = Jurídica;\r\nR = Roubo/Furto")
                .HasColumnName("tipo_restricao");

            //builder.HasOne(d => d.IdDetranVeiculoNavigation).WithMany(p => p.TbDetranVeiculosWsRestricos)
            //    .HasForeignKey(d => d.IdDetranVeiculo)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("fk_tb_detran_veiculos_ws_restricoes1");

            //builder.HasOne(d => d.IdDetranVeiculosWsRestricaoOrigemNavigation).WithMany(p => p.TbDetranVeiculosWsRestricos)
            //    .HasForeignKey(d => d.IdDetranVeiculosWsRestricaoOrigem)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("fk_tb_detran_veiculos_ws_restricoes2");
        }
    }
}