using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Localizacao;

namespace WebZi.Plataform.Data.Mappings.Localizacao
{
    public class CEPMap : IEntityTypeConfiguration<CEPModel>
    {
        public void Configure(EntityTypeBuilder<CEPModel> builder)
        {
            builder
                .ToTable("tb_glo_loc_cep", "dbo")
                .HasKey(e => e.CEPId);

            builder.Property(e => e.CEPId)
                .HasColumnName("id_cep")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.CEP)
                .IsRequired()
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("cep");

            builder.Property(e => e.FlagSanitizado)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_sanitizado");

            builder.Property(e => e.BairroId)
                .HasColumnName("id_bairro");

            builder.Property(e => e.MunicipioId)
                .HasColumnName("id_municipio");

            builder.Property(e => e.TipoLogradouroId)
                .HasColumnName("id_tipo_logradouro");

            builder.Property(e => e.Logradouro)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("logradouro");
        }
    }
}