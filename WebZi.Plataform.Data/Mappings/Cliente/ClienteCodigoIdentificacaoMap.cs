using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Cliente;

namespace WebZi.Plataform.Data.Mappings.Cliente
{
    public class ClienteCodigoIdentificacaoMap : IEntityTypeConfiguration<ClienteCodigoIdentificacaoModel>
    {
        public void Configure(EntityTypeBuilder<ClienteCodigoIdentificacaoModel> builder)
        {
            builder
                .ToTable("tb_dep_grv_clientes_codigo_identificacao", "dbo")
                .HasKey(e => e.ClienteCodigoIdentificacaoId);

            builder.Property(e => e.ClienteCodigoIdentificacaoId)
                .HasColumnName("id_cliente_codigo_identificacao")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.GrvId)
                .HasColumnName("id_grv");

            builder.Property(e => e.UsuarioCadastroId)
                .HasColumnName("id_usuario_cadastro");

            builder.Property(e => e.UsuarioAlteracaoId)
                .HasColumnName("id_usuario_alteracao");

            builder.Property(e => e.CodigoIdentificacao)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("codigo_identificacao");

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