using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Governo;

namespace WebZi.Plataform.Data.Mappings.Governo
{
    public class AssociacaoCnaeListaServicoMap : IEntityTypeConfiguration<AssociacaoCnaeListaServicoModel>
    {
        public void Configure(EntityTypeBuilder<AssociacaoCnaeListaServicoModel> builder)
        {
            builder
                .ToTable("tb_gov_cnae_lista_servico", "dbo")
                .HasKey(x => x.CnaeListaServicoId);

            builder.Property(e => e.CnaeListaServicoId)
                .HasColumnName("CnaeListaServicoID")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.CnaeId)
                .IsRequired()
                .HasColumnName("CnaeID");

            builder.Property(e => e.ListaServicoId)
                .IsRequired()
                .HasColumnName("ListaServicoID");

            builder.Property(e => e.DataCadastro)
                .IsRequired()
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime");
        }
    }
}