using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Liberacao;

namespace WebZi.Plataform.Data.Mappings.Liberacao
{
    public class CobrancaLegalMap : IEntityTypeConfiguration<CobrancaLegalModel>
    {
        public void Configure(EntityTypeBuilder<CobrancaLegalModel> builder)
        {
            builder
                .ToTable("tb_dep_grv_cobrancas_legais", "dbo")
                .HasKey(e => e.CobrancaLegalId);

            builder.Property(e => e.CobrancaLegalId)
                .HasColumnName("id_cobranca_legal")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.GrvId)
                .HasColumnName("id_grv");

            builder.Property(e => e.TipoCobrancaLegalId)
                .HasColumnName("id_tipo_cobranca_legal");

            builder.Property(e => e.MunicipioId)
                .HasColumnName("id_municipio");

            builder.Property(e => e.UsuarioCadastroId)
                .HasColumnName("id_usuario_cadastro");

            builder.Property(e => e.Exercicio)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("exercicio");

            builder.Property(e => e.NumeroAutoInfracao)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("numero_auto_infracao");

            builder.Property(e => e.Valor)
                .HasColumnType("money")
                .HasColumnName("valor");

            builder.Property(e => e.DataVencimento)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_vencimento");

            builder.Property(e => e.DataCadastro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime")
                .HasColumnName("data_cadastro");
        }
    }
}