using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Veiculo;

namespace WebZi.Plataform.Data.Mappings.GRV
{
    public class TipoVeiculoMap : IEntityTypeConfiguration<TipoVeiculoModel>
    {
        public void Configure(EntityTypeBuilder<TipoVeiculoModel> builder)
        {
            builder
                .ToTable("tb_dep_tipo_veiculos", "dbo")
                .HasKey(x => x.TipoVeiculoId);

            builder.Property(e => e.TipoVeiculoId)
                .HasColumnName("id_tipo_veiculo")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.DataAlteracao)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_alteracao");

            builder.Property(e => e.DataCadastro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime")
                .HasColumnName("data_cadastro");

            builder.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descricao");

            builder.Property(e => e.FlagAtivo)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('S')")
                .IsFixedLength()
                .HasColumnName("flag_ativo");

            builder.Property(e => e.FlagNaoRequerCnhNaLiberacao)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_nao_requer_cnh_na_liberacao");

            builder.Property(e => e.UsuarioCadastroId)
                .HasColumnName("id_usuario_cadastro");

            builder.Property(e => e.UsuarioAlteracaoId)
                .HasColumnName("id_usuario_alteracao");

            builder.HasOne(d => d.UsuarioCadastro).WithMany(p => p.ListagemUsuarioCadastroTipoVeiculo)
                .HasForeignKey(d => d.UsuarioCadastroId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(d => d.UsuarioAlteracao).WithMany(p => p.ListagemUsuarioAlteracaoTipoVeiculo)
                .HasForeignKey(d => d.UsuarioAlteracaoId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}