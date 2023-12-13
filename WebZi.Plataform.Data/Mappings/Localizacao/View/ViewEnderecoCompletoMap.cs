using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Views.Localizacao;

namespace WebZi.Plataform.Data.Mappings.Localizacao.View
{
    public class ViewEnderecoCompletoMap : IEntityTypeConfiguration<ViewEnderecoCompletoModel>
    {
        public void Configure(EntityTypeBuilder<ViewEnderecoCompletoModel> builder)
        {
            builder
                .ToView("vw_glo_consultar_endereco_completo", "dbo")
                .HasKey(t => t.CEPId);

            builder.Property(e => e.CEPId)
                .HasColumnName("id_cep");

            builder.Property(e => e.BairroId)
                .HasColumnName("id_bairro");

            builder.Property(e => e.MunicipioId)
                .HasColumnName("id_municipio");

            builder.Property(e => e.TipoLogradouroId)
                .HasColumnName("id_tipo_logradouro");

            builder.Property(e => e.Bairro)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("bairro");

            builder.Property(e => e.BairroPtbr)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("bairro_ptbr");

            builder.Property(e => e.CEP)
                .IsRequired()
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("CEP");

            builder.Property(e => e.CodigoLogradouro)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("codigo_logradouro");

            builder.Property(e => e.CodigoMunicipio)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("codigo_municipio");

            builder.Property(e => e.CodigoMunicipioIbge)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("codigo_municipio_ibge");

            builder.Property(e => e.Estado)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("estado");

            builder.Property(e => e.EstadoPtbr)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("estado_ptbr");

            builder.Property(e => e.FlagNormalizado)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("flag_sanitizado");

            builder.Property(e => e.Logradouro)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("logradouro");

            builder.Property(e => e.Municipio)
                .IsRequired()
                .HasMaxLength(75)
                .IsUnicode(false)
                .HasColumnName("municipio");

            builder.Property(e => e.MunicipioPtbr)
                .IsRequired()
                .HasMaxLength(75)
                .IsUnicode(false)
                .HasColumnName("municipio_ptbr");

            builder.Property(e => e.SiglaRegiao)
                .IsRequired()
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("regiao");

            builder.Property(e => e.Regiao)
                .IsRequired()
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("regiao_nome");

            builder.Property(e => e.TipoLogradouro)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("tipo_logradouro");

            builder.Property(e => e.UF)
                .IsRequired()
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("uf");
        }
    }
}