using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.VLock;

namespace WebZi.Plataform.Data.Mappings.VLock;

public class DispotiivoMapping : IEntityTypeConfiguration<DispositivosModel>
{
    public void Configure(EntityTypeBuilder<DispositivosModel> builder)
    {
        builder.ToTable("tb_dispositivo", "dbo")
            .HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(x => x.Imei)
            .HasColumnName("imei")
            .HasMaxLength(50)
            .IsUnicode(false)
            .IsRequired();

        builder.Property(x => x.Ativo)
            .HasColumnName("ativo");

        builder.Property(x => x.StatusId)
            .HasColumnName("id_status");

        builder.Property(x => x.Telefone)
            .HasColumnName("telefone")
            .HasMaxLength(20)
            .IsUnicode(false);

        builder.Property(x => x.Fabricante)
            .HasColumnName("fabricante")
            .HasMaxLength(100)
            .IsUnicode(false);

        builder.Property(x => x.Modelo)
            .HasColumnName("modelo")
            .HasMaxLength(100)
            .IsUnicode(false);

        builder.Property(x => x.NotaFiscal)
            .HasColumnName("nota_fiscal")
            .HasMaxLength(50)
            .IsUnicode(false);

        builder.Property(x => x.ClienteId)
            .HasColumnName("id_cliente");

        builder.Property(x => x.AutoridadeId)
            .HasColumnName("id_autoridade");

        builder.Property(x => x.AgenteId)
            .HasColumnName("id_agente");

        builder.Property(x => x.ParceiroId)
            .HasColumnName("id_parceiro");
        builder.HasOne(x => x.StatusDispositivo)
            .WithMany()
            .HasForeignKey(x => x.StatusId);
    }
}