using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.GRV;

namespace WebZi.Plataform.Data.Mappings.GRV
{
    internal class MotivoApreensaoMap : IEntityTypeConfiguration<MotivoApreensaoModel>
    {
        public void Configure(EntityTypeBuilder<MotivoApreensaoModel> builder)
        {
            builder
                .ToTable("tb_dep_grv_motivo_apreensao", "dbo")
                .HasKey(e => e.MotivoApreensaoId);

            builder.Property(e => e.MotivoApreensaoId)
                .HasColumnName("id_motivo_apreensao")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Codigo)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("codigo");
            
            builder.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("descricao");
            
            builder.Property(e => e.FlagDefault)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_default");
        }
    }
}