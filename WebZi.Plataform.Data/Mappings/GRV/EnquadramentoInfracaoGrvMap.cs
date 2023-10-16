using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Domain.Models.GRV;

namespace WebZi.Plataform.Data.Mappings.GRV
{
    public class EnquadramentoInfracaoGrvMap : IEntityTypeConfiguration<EnquadramentoInfracaoGrvModel>
    {
        public void Configure(EntityTypeBuilder<EnquadramentoInfracaoGrvModel> builder)
        {
            builder
                .ToTable("tb_dep_grv_enquadramento_infracoes", "dbo")
                .HasKey(e => e.GrvEnquadramentoInfracaoId);

            builder.Property(e => e.GrvEnquadramentoInfracaoId)
                .HasColumnName("id_grv_enquadramento_infracao")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.GrvId)
                .IsRequired()
                .HasColumnName("id_grv");

            builder.Property(e => e.EnquadramentoInfracaoId)
                .IsRequired()
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("id_enquadramento_infracao");
            
            builder.Property(e => e.NumeroInfracao)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("numero_infracao");
        }
    }
}