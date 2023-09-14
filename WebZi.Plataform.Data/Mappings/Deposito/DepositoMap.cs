using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Deposito;

namespace WebZi.Plataform.Data.Mappings.Deposito
{
    public class DepositoMap : IEntityTypeConfiguration<DepositoModel>
    {
        public void Configure(EntityTypeBuilder<DepositoModel> builder)
        {
            builder
                .ToTable("tb_dep_depositos", "dbo")
                .HasKey(e => e.DepositoId);

            builder.Property(e => e.DepositoId)
                .HasColumnName("id_deposito")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Complemento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("complemento");

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

            builder.Property(e => e.EmailNfe)
                .IsRequired()
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasDefaultValueSql("('contato@mob-link.net.br')")
                .HasColumnName("email_nfe");

            builder.Property(e => e.EnderecoMob)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("endereco_mob");

            builder.Property(e => e.FlagAtivo)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('S')")
                .IsFixedLength()
                .HasColumnName("flag_ativo");

            builder.Property(e => e.FlagEnderecoCadastroManual)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_endereco_cadastro_manual");

            builder.Property(e => e.FlagVirtual)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("flag_virtual");

            builder.Property(e => e.GrvLimiteMinimoDatahoraGuarda)
                .HasColumnName("grv_limite_minimo_datahora_guarda");

            builder.Property(e => e.GrvMinimoFotosExigidas)
                .HasColumnName("grv_minimo_fotos_exigidas");

            builder.Property(e => e.BairroId)
                .HasColumnName("id_bairro");

            builder.Property(e => e.CepId)
                .HasColumnName("id_cep");

            builder.Property(e => e.EmpresaId)
                .HasColumnName("id_empresa");

            builder.Property(e => e.SistemaExternoId)
                .HasColumnName("id_sistema_externo");

            builder.Property(e => e.TipoLogradouroId)
                .HasColumnName("id_tipo_logradouro");

            builder.Property(e => e.UsuarioAlteracaoId)
                .HasColumnName("id_usuario_alteracao");

            builder.Property(e => e.UsuarioCadastroId)
                .HasColumnName("id_usuario_cadastro");

            builder.Property(e => e.Latitude)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("latitude");

            builder.Property(e => e.Logradouro)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("logradouro");

            builder.Property(e => e.Longitude)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("longitude");

            builder.Property(e => e.Numero)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("numero");

            builder.Property(e => e.TelefoneMob)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("telefone_mob");
        }
    }
}