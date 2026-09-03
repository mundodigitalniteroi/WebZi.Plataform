using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.VLock;

namespace WebZi.Plataform.Data.Mappings.VLock;

public class RecolhimentoMapping : IEntityTypeConfiguration<RecolhimentoModel>
{
    public void Configure(EntityTypeBuilder<RecolhimentoModel> builder)
    {
        builder.ToTable("tb_recolhimento", "dbo")
            .HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(x => x.IdDispositivo)
            .HasColumnName("id_dispositivo")
            .IsRequired();

        builder.Property(x => x.IdGrv)
            .HasColumnName("id_grv")
            .IsRequired();

        builder.Property(x => x.Ativo)
            .HasColumnName("ativo")
            .IsRequired();

        builder.Property(x => x.CertaVirtual)
            .HasColumnName("cerca_virtual")
            .IsRequired();
    }
}