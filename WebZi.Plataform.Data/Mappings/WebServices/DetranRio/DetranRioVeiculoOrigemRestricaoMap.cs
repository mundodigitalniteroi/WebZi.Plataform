using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Domain.Models.WebServices.DetranRio;

namespace WebZi.Plataform.Data.Mappings.WebServices.DetranRio
{
    public class DetranRioVeiculoOrigemRestricaoMap : IEntityTypeConfiguration<DetranRioVeiculoOrigemRestricaoModel>
    {
        public void Configure(EntityTypeBuilder<DetranRioVeiculoOrigemRestricaoModel> builder)
        {
            builder
                .ToTable("tb_detran_veiculos_ws_restricoes_origem", "dbo")
                .HasKey(e => e.DetranVeiculoOrigemRestricaoId);

            builder.Property(e => e.DetranVeiculoOrigemRestricaoId)
                .ValueGeneratedOnAdd()
                .HasColumnName("id_detran_veiculos_ws_restricao_origem");

            builder.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("descricao");

            builder.Property(e => e.FlagPermiteEdicao)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_permite_edicao");
        }
    }
}