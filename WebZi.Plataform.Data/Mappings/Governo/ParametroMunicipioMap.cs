using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Governo;

namespace WebZi.Plataform.Data.Mappings.Governo
{
    public class ParametroMunicipioMap : IEntityTypeConfiguration<ParametroMunicipioModel>
    {
        public void Configure(EntityTypeBuilder<ParametroMunicipioModel> builder)
        {
            builder
                .ToTable("tb_gov_parametro_municipio", "dbo")
                .HasKey(e => e.ParametroMunicipioId);

            builder.Property(e => e.ParametroMunicipioId)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.CnaeListaServicoId)
                .IsRequired();

            builder.Property(e => e.MunicipioId)
                .IsRequired();

            builder.Property(e => e.CodigoCnae)
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength();

            builder.Property(e => e.ItemListaServico)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength();

            builder.Property(e => e.CodigoMunicipioIbge)
                .HasMaxLength(10)
                .IsUnicode(false);

            builder.Property(e => e.CodigoTributarioMunicipio)
                .HasMaxLength(10)
                .IsUnicode(false);

            builder.Property(e => e.DataCadastro)
                .IsRequired()
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime");

            builder.Property(e => e.DataAlteracao)
                .HasColumnType("smalldatetime");
        }
    }
}