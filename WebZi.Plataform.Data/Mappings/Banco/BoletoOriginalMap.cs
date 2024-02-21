using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Banco;

namespace WebZi.Plataform.Data.Mappings.Banco
{
    public class BoletoOriginalMap : IEntityTypeConfiguration<BoletoOriginalModel>
    {
        public void Configure(EntityTypeBuilder<BoletoOriginalModel> builder)
        {
            builder
                .ToTable("tb_bol_boletos", "dbo")
                .HasKey(e => e.BoletoId);

            builder.Property(e => e.BoletoId)
                .HasColumnName("id_boleto")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.BoletoBanco)
                .IsRequired()
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("boleto_banco");

            builder.Property(e => e.BoletoCarteira)
                .IsRequired()
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("boleto_carteira");

            builder.Property(e => e.BoletoInstrucoes)
                .IsRequired()
                .IsUnicode(false)
                .HasColumnName("boleto_instrucoes");

            builder.Property(e => e.BoletoNumeroDocumento)
                .IsRequired()
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("boleto_numeroDocumento");

            builder.Property(e => e.BoletoValor)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("boleto_valor");

            builder.Property(e => e.BoletoVencimento)
                .IsRequired()
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("boleto_vencimento");

            builder.Property(e => e.CedenteAgencia)
                .IsRequired()
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("cedente_agencia");

            builder.Property(e => e.CedenteCodigo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("cedente_codigo");

            builder.Property(e => e.CedenteConta)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("cedente_conta");

            builder.Property(e => e.CedenteCpfCnpj)
                .IsRequired()
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("cedente_cpfCnpj");

            builder.Property(e => e.CedenteDigitoConta)
                .IsRequired()
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("cedente_digitoConta");

            builder.Property(e => e.CedenteNome)
                .IsUnicode(false)
                .HasColumnName("cedente_nome");

            builder.Property(e => e.CedenteNossoNumeroBoleto)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("cedente_nossoNumeroBoleto");

            builder.Property(e => e.DataCadastroBoleto)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime")
                .HasColumnName("data_cadastro_boleto");

            builder.Property(e => e.DataGeracaoArquivoRemessa)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_geracao_arquivo_remessa");

            builder.Property(e => e.Email)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("email");

            builder.Property(e => e.EnderecoComplemento)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("endereco_complemento");

            builder.Property(e => e.EnderecoNumero)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("endereco_numero");

            builder.Property(e => e.BenificiarioFinalId).HasColumnName("id_benificiario_final");

            builder.Property(e => e.FaturamentoId).HasColumnName("id_faturamento");

            builder.Property(e => e.InterfaceUsuarioId).HasColumnName("id_interface_usuario");

            builder.Property(e => e.LinhaDigitavel)
                .IsUnicode(false)
                .HasColumnName("linha_digitavel");

            builder.Property(e => e.NomeArquivoRemessa)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nome_arquivo_remessa");

            builder.Property(e => e.SacadoBairro)
                .IsUnicode(false)
                .HasColumnName("sacado_bairro");

            builder.Property(e => e.SacadoCep)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("sacado_cep");

            builder.Property(e => e.SacadoCidade)
                .IsUnicode(false)
                .HasColumnName("sacado_cidade");

            builder.Property(e => e.SacadoCpfCnpj)
                .IsRequired()
                .HasMaxLength(18)
                .IsUnicode(false)
                .HasColumnName("sacado_cpfCnpj");

            builder.Property(e => e.SacadoEndereco)
                .IsUnicode(false)
                .HasColumnName("sacado_endereco");

            builder.Property(e => e.SacadoNome)
                .IsUnicode(false)
                .HasColumnName("sacado_nome");

            builder.Property(e => e.SacadoUf)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("sacado_uf");

            builder.Property(e => e.Telefone)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("telefone");
        }
    }
}