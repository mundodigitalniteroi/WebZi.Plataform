using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Atendimento;

namespace WebZi.Plataform.Data.Mappings.Atendimento
{
    public class AtendimentoSaidaParaReparoMap : IEntityTypeConfiguration<AtendimentoSaidaParaReparoModel>
    {
        public void Configure(EntityTypeBuilder<AtendimentoSaidaParaReparoModel> builder)
        {
            builder.ToTable("tb_dep_atendimento_saida_reparo", "dbo")
                .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();
            
            builder.Property(x => x.AtendimentoId)
                .IsRequired();

            builder.Property(x => x.DataSaida)
                .HasColumnType("smalldatetime")
                .IsRequired();
            
            builder.Property(x => x.DataPrevisaoRetorno)
                .HasColumnType("smalldatetime")
                .IsRequired();

            builder.Property(x => x.MotivoSaida)
                .HasColumnType("varchar(500)")
                .IsRequired();

            builder.Property(x => x.DataRetorno)
                .HasColumnType("smalldatetime");
            
            builder.Property(x => x.IdUsuario);

            builder.HasOne(x => x.Atendimento)
                .WithOne(x => x.SaidaParaReparo)
                .HasForeignKey<AtendimentoSaidaParaReparoModel>(x => x.AtendimentoId);
        }
    }
}
