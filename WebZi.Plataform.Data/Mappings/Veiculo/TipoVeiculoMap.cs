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

            builder.Property(x => x.TipoVeiculoId)
                .HasColumnName("id_tipo_veiculo")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Descricao)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descricao");

            builder.Property(x => x.FlagAtivo)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('S')")
                .IsFixedLength()
                .HasColumnName("flag_ativo");

            builder.Property(x => x.FlagNaoRequerCnhNaLiberacao)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_nao_requer_cnh_na_liberacao");

            builder.Property(x => x.UsuarioCadastroId)
                .HasColumnName("id_usuario_cadastro");

            builder.Property(x => x.UsuarioAlteracaoId)
                .HasColumnName("id_usuario_alteracao");

            builder.Property(x => x.DataCadastro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime")
                .HasColumnName("data_cadastro");

            builder.Property(x => x.DataAlteracao)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_alteracao");

            builder
                .HasOne(x => x.UsuarioCadastro)
                .WithMany(x => x.ListagemUsuarioCadastroTipoVeiculo)
                .HasForeignKey(x => x.UsuarioCadastroId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(x => x.UsuarioAlteracao)
                .WithMany(x => x.ListagemUsuarioAlteracaoTipoVeiculo)
                .HasForeignKey(x => x.UsuarioAlteracaoId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}