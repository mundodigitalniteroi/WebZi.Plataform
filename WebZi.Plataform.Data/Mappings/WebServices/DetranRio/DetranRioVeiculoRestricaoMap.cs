using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.WebServices.Rio;

namespace WebZi.Plataform.Data.Mappings.WebServices.DetranRio
{
    public class DetranRioVeiculoRestricaoMap : IEntityTypeConfiguration<DetranRioVeiculoRestricaoModel>
    {
        public void Configure(EntityTypeBuilder<DetranRioVeiculoRestricaoModel> builder)
        {
            builder
                .ToTable("tb_detran_veiculos_ws_restricoes", "dbo", x => x.HasTrigger("tr_log_upd_detran_veiculos_ws_restricoes"))
                .HasKey(e => e.DetranVeiculoRestricaoId);

            builder.Property(x => x.DetranVeiculoRestricaoId).HasColumnName("id_detran_veiculos_ws_restricoes");

            builder.Property(x => x.CodigoRestricao).HasColumnName("codigo_restricao");

            builder.Property(x => x.DetranVeiculoId).HasColumnName("id_detran_veiculo");

            builder.Property(x => x.DetranVeiculoOrigemRestricaoId).HasColumnName("id_detran_veiculos_ws_restricao_origem");

            builder.Property(x => x.Restricao)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("restricao");

            builder.Property(x => x.TipoRestricao)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasComment("A = Administrativa;\r\nE = Estelionato;\r\nJ = Jurídica;\r\nR = Roubo/Furto")
                .HasColumnName("tipo_restricao");

            builder
                .HasOne(x => x.DetranRioVeiculo)
                .WithMany(x => x.ListagemDetranRioVeiculoRestricao)
                .HasForeignKey(x => x.DetranVeiculoId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(x => x.DetranRioVeiculoOrigemRestricao)
                .WithMany(x => x.ListagemDetranRioVeiculoRestricao)
                .HasForeignKey(x => x.DetranVeiculoOrigemRestricaoId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}