using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Cliente;

namespace WebZi.Plataform.Data.Mappings.Cliente
{
    public class ClienteMap : IEntityTypeConfiguration<ClienteModel>
    {
        public void Configure(EntityTypeBuilder<ClienteModel> builder)
        {
            builder
                .ToTable("tb_dep_clientes", "dbo")
                .HasKey(e => e.ClienteId);

            builder.Property(e => e.ClienteId)
                .HasColumnName("id_cliente")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Cnpj)
                .IsRequired()
                .HasMaxLength(14)
                .IsUnicode(false)
                .HasColumnName("cnpj");

            builder.Property(e => e.CodigoOrgao)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("codigo_orgao");

            builder.Property(e => e.CodigoSap)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("codigo_sap");

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

            builder.Property(e => e.FlagAtivo)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('S')")
                .IsFixedLength()
                .HasColumnName("flag_ativo");

            builder.Property(e => e.FlagCadastrarQuilometragem)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('S')")
                .IsFixedLength()
                .HasColumnName("flag_cadastrar_quilometragem");

            builder.Property(e => e.FlagClienteRealizaFaturamentoArrecadacao)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_cliente_realiza_faturamento_arrecadacao");

            builder.Property(e => e.FlagCobrarDiariasDiasCorridos)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_cobrar_diarias_dias_corridos");

            builder.Property(e => e.FlagEmissaoNotaFiscal)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('S')")
                .IsFixedLength()
                .HasColumnName("flag_emissao_nota_fiscal_sap");

            builder.Property(e => e.FlagEnderecoCadastroManual)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_endereco_cadastro_manual");

            builder.Property(e => e.FlagLancarIpvaMultas)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_lancar_ipva_multas");

            builder.Property(e => e.FlagPermiteAlteracaoTipoVeiculo)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_permite_alteracao_tipo_veiculo");

            builder.Property(e => e.FlagPossuiClienteCodigoIdentificacao)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasColumnName("flag_possui_cliente_codigo_identificacao");

            builder.Property(e => e.FlagPossuiPix)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();

            builder.Property(e => e.FlagPossuiPixDinamico)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();

            builder.Property(e => e.FlagPossuiPixEstatico)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();

            builder.Property(e => e.FlagUsarHoraDiaria)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('S')")
                .IsFixedLength()
                .HasColumnName("flag_usar_hora_diaria");

            builder.Property(e => e.GpsLatitude)
                .HasColumnType("numeric(10, 8)")
                .HasColumnName("gps_latitude");

            builder.Property(e => e.GpsLongitude)
                .HasColumnType("numeric(10, 8)")
                .HasColumnName("gps_longitude");

            builder.Property(e => e.HoraDiaria)
                .IsRequired()
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasDefaultValueSql("('00:00')")
                .HasColumnName("hora_diaria");

            builder.Property(e => e.AgenciaBancariaId).HasColumnName("id_agencia_bancaria");

            builder.Property(e => e.BairroId).HasColumnName("id_bairro");

            builder.Property(e => e.CepId).HasColumnName("id_cep");

            builder.Property(e => e.EmpresaId).HasColumnName("id_empresa");

            builder.Property(e => e.OrgaoExecutivoTransitoId).HasColumnName("id_orgao_executivo_transito");

            builder.Property(e => e.TipoLogradouroId).HasColumnName("id_tipo_logradouro");

            builder.Property(e => e.TipoMeioCobrancaId).HasColumnName("id_tipo_meio_cobranca");

            builder.Property(e => e.UsuarioAlteracaoId).HasColumnName("id_usuario_alteracao");

            builder.Property(e => e.UsuarioCadastroId).HasColumnName("id_usuario_cadastro");

            builder.Property(e => e.LabelClienteCodigoIdentificacao)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("label_cliente_codigo_identificacao");

            builder.Property(e => e.Logradouro)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("logradouro");

            builder.Property(e => e.MaximoDiariasParaCobranca).HasColumnName("maximo_diarias_para_cobranca");

            builder.Property(e => e.MaximoDiasVencimento).HasColumnName("maximo_dias_vencimento");

            builder.Property(e => e.MetragemGuarda)
                .HasColumnType("numeric(5, 2)")
                .HasColumnName("metragem_guarda");

            builder.Property(e => e.MetragemTotal)
                .HasColumnType("numeric(5, 2)")
                .HasColumnName("metragem_total");

            builder.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nome");

            builder.Property(e => e.Numero)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("numero");

            builder.Property(e => e.PixChave)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.TipoPix)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
        }
    }
}