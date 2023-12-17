using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Liberacao;

namespace WebZi.Plataform.Data.Mappings.Liberacao
{
    public class LiberacaoMap : IEntityTypeConfiguration<LiberacaoModel>
    {
        public void Configure(EntityTypeBuilder<LiberacaoModel> builder)
        {
            builder
                .ToTable("tb_dep_liberacao", "dbo")
                .HasKey(x => x.LiberacaoId);

            builder.Property(e => e.LiberacaoId)
                .HasColumnName("id_liberacao")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.TipoLiberacaoId)
                .HasColumnName("id_liberacao_tipo")
                .IsRequired();

            builder.Property(e => e.UsuarioCadastroId)
                .HasColumnName("id_usuario_cadastro")
                .IsRequired();

            builder.Property(e => e.DataCadastro)
                .HasColumnName("data_cadastro")
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime")
                .IsRequired();

            builder.HasOne(d => d.TipoLiberacao)
                .WithMany(p => p.ListagemLiberacao)
                .HasForeignKey(d => d.TipoLiberacaoId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(d => d.Usuario)
                .WithMany(p => p.ListagemUsuarioLiberacao)
                .HasForeignKey(d => d.UsuarioCadastroId)
                .OnDelete(DeleteBehavior.ClientNoAction);

            //builder.HasOne(d => d.IdUsuarioCadastroNavigation).WithMany(p => p.TbDepLiberacaos)
            //    .HasForeignKey(d => d.IdUsuarioCadastro)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("fk_tb_dep_liberacao2");



            //builder.HasOne(d => d.UsuarioAlteracao)
            //    .WithMany(p => p.ListagemUsuarioPermissaoAlteracao)
            //    .HasForeignKey(d => d.UsuarioAlteracaoId)
            //    .OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}