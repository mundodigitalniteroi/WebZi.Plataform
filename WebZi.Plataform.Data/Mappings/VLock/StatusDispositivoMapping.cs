using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.VLock;

namespace WebZi.Plataform.Data.Mappings.VLock;

public class StatusDispositivoMapping : IEntityTypeConfiguration<StatusDispositivoModel>
{
    public void Configure(EntityTypeBuilder<StatusDispositivoModel> builder)
    {
        builder.ToTable("tb_status_dispositivo", "dbo")
            .HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Descricao)
            .HasColumnName("descricao");
    }
}