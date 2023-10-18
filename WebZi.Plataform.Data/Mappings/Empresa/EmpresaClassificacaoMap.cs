using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Domain.Models.Empresa;

namespace WebZi.Plataform.Data.Mappings.Empresa
{
    public class EmpresaClassificacaoMap : IEntityTypeConfiguration<EmpresaClassificacaoModel>
    {
        public void Configure(EntityTypeBuilder<EmpresaClassificacaoModel> builder)
        {
            builder
                .ToTable("tb_glo_emp_empresas_classificacao", "dbo")
                .HasKey(e => e.IdEmpresaClassificacao);

            builder.Property(e => e.IdEmpresaClassificacao)
                .HasColumnName("id_empresa_classificacao")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descricao");

            builder.Property(e => e.FlagMatriz)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_matriz");
        }
    }
}