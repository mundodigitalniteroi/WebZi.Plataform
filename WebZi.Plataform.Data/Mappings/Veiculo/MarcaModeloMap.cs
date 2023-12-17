using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Veiculo;

namespace WebZi.Plataform.Data.Mappings.Veiculo
{
    public class MarcaModeloMap : IEntityTypeConfiguration<MarcaModeloModel>
    {
        public void Configure(EntityTypeBuilder<MarcaModeloModel> builder)
        {
            builder
                .ToTable("tb_detran_marca_modelo", "dbo")
                .HasKey(x => x.MarcaModeloId);

            builder.Property(e => e.MarcaModeloId)
                .HasColumnName("id_detran_marca_modelo")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.UsuarioCadastroId)
                .HasDefaultValueSql("((1))")
                .HasColumnName("id_usuario_cadastro");

            builder.Property(e => e.UsuarioAlteracaoId)
                .HasColumnName("id_usuario_alteracao");

            builder.Property(e => e.MarcaModelo)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descricao");

            builder.Property(e => e.FlagOrigemDetran)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('S')")
                .IsFixedLength()
                .HasColumnName("flag_origem_detran");

            builder.Property(e => e.DataCadastro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime")
                .HasColumnName("data_cadastro");

            builder.Property(e => e.DataAlteracao)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_alteracao");
        }
    }
}