using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.WebServices.DetranRio;

namespace WebZi.Plataform.Data.Mappings.WebServices.DetranRio
{
    public class DetranRioVeiculoOrigemRestricaoMap : IEntityTypeConfiguration<DetranRioVeiculoOrigemRestricaoModel>
    {
        public void Configure(EntityTypeBuilder<DetranRioVeiculoOrigemRestricaoModel> builder)
        {
            builder
                .ToTable("tb_detran_veiculos_ws_restricoes_origem", "dbo")
                .HasKey(x => x.DetranVeiculoOrigemRestricaoId);

            builder.Property(x => x.DetranVeiculoOrigemRestricaoId)
                .ValueGeneratedOnAdd()
                .HasColumnName("id_detran_veiculos_ws_restricao_origem");

            builder.Property(x => x.Descricao)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("descricao");

            builder.Property(x => x.FlagPermiteEdicao)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_permite_edicao");
        }
    }
}