using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Nfe;

namespace WebZi.Plataform.Data.Mappings.Nfe
{
    public class NfeRetornoSolicitacaoMap : IEntityTypeConfiguration<NfeRetornoSolicitacaoModel>
    {
        public void Configure(EntityTypeBuilder<NfeRetornoSolicitacaoModel> builder)
        {
            builder.ToTable("tb_dep_nfe_retorno_solicitacao", "dbo")
                .HasKey(x => x.RetornoSolicitacaoId);

            builder.Property(x => x.RetornoSolicitacaoId)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.NfeId)
                .IsRequired();

            builder.Property(x => x.NfePrestadorId);

            builder.Property(x => x.NaturezaOperacao)
                .HasMaxLength(1);

            builder.Property(x => x.OptanteSimplesNacional)
                .HasMaxLength(1);

            builder.Property(x => x.TomadorCpfCnpj)
                .HasMaxLength(11);

            builder.Property(x => x.TomadorCnpj)
                .HasMaxLength(14);

            builder.Property(x => x.TomadorNomeRazaoSocial)
                .HasMaxLength(100);

            builder.Property(x => x.TomadorTelefone)
                .HasMaxLength(11);

            builder.Property(x => x.TomadorEmail)
                .HasMaxLength(80);

            builder.Property(x => x.TomadorEnderecoLogradouro)
                .HasMaxLength(100);

            builder.Property(x => x.TomadorEnderecoNumero)
                .HasMaxLength(20);

            builder.Property(x => x.TomadorEnderecoComplemento)
                .HasMaxLength(60);

            builder.Property(x => x.TomadorEnderecoBairro)
                .HasMaxLength(60);

            builder.Property(x => x.TomadorEnderecoCodigoMunicipio)
                .HasMaxLength(7);

            builder.Property(x => x.TomadorEnderecoUf)
                .HasMaxLength(2);

            builder.Property(x => x.TomadorEnderecoCep)
                .HasMaxLength(8);

            builder.Property(x => x.ServicoAliquota)
                .HasMaxLength(10);

            builder.Property(x => x.ServicoDiscriminacao)
                .HasMaxLength(400);

            builder.Property(x => x.ServicoIssRetido)
                .HasMaxLength(1);

            builder.Property(x => x.ServicoValorIss)
                .HasMaxLength(10);

            builder.Property(x => x.ServicoCodigoCnae)
                .HasMaxLength(7);

            builder.Property(x => x.ServicoItemListaServico)
                .HasMaxLength(5);

            builder.Property(x => x.ServicoValorServicos)
                .HasMaxLength(10);

            builder.Property(x => x.ServicoCodigoTributarioMunicipio)
                .HasMaxLength(10);

            builder.Property(x => x.RespostaEnvio)
                .HasMaxLength(1000);

            builder.Property(x => x.Json)
                .HasColumnType("varchar(max)");

            builder.Property(x => x.DataEmissao)
                .HasColumnType("smalldatetime");

            builder.Property(x => x.DataCadastro)
                .HasColumnType("smalldatetime")
                .IsRequired();

            builder.Property(x => x.Json)
                .HasMaxLength(4000);

            builder.HasOne(x => x.Nfe)
                .WithMany()
                .HasForeignKey(x => x.NfeId);
        }
    }
}
