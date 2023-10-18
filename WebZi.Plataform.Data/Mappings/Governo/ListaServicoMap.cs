using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Domain.Models.Governo;

namespace WebZi.Plataform.Data.Mappings.Governo
{
    public class ListaServicoMap : IEntityTypeConfiguration<ListaServicoModel>
    {
        public void Configure(EntityTypeBuilder<ListaServicoModel> builder)
        {
            builder
                .ToTable("tb_gov_lista_servico", "dbo")
                .HasKey(e => e.ListaServicoId);

            builder.Property(e => e.ListaServicoId)
                .HasColumnName("ListaServicoID")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.AliquotaIss)
                .IsRequired()
                .HasColumnType("smallmoney");
            
            builder.Property(e => e.ItemLista)
                .IsRequired()
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength();

            builder.Property(e => e.DataCadastro)
                .IsRequired()
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime");

            builder.Property(e => e.DataAlteracao)
                .HasColumnType("smalldatetime");
        }
    }
}