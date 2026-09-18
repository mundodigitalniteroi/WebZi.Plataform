using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebZi.Plataform.Domain.Models.Arrematantes;

namespace WebZi.Plataform.Data.Mappings.Arrematantes;

public class ArrematantesMapping : IEntityTypeConfiguration<ArrematantesModel>
{
    public void Configure(EntityTypeBuilder<ArrematantesModel> builder)
    {
        builder.ToTable("tb_dep_arrematante", "dbo")
            .HasKey(x => x.ArrematanteId);
        builder.Property(x => x.ArrematanteId)
            .HasColumnName("id_arrematante")
            .ValueGeneratedOnAdd()
            .IsRequired();
        builder.Property(x => x.GrvId)
            .HasColumnName("id_grv");
        builder.Property(x => x.NumeroProcesso)
            .HasColumnType("varchar(14)")
            .HasColumnName("numero_processo");
        builder.Property(x => x.Nome)
            .HasColumnType("varchar(255)")
            .HasColumnName("nome");
        builder.Property(x => x.CpfCnpj)
            .HasColumnType("varchar(14)")
            .HasColumnName("cpf_cnpj");
        builder.Property(x => x.TelefoneCelular)
            .HasColumnType("varchar(20)")
            .HasColumnName("telefone_celular");
        builder.Property(x => x.Email)
            .HasColumnType("varchar(255)")
            .HasColumnName("email");
        builder.Property(x => x.Logradouro)
            .HasColumnType("varchar(500)")
            .HasColumnName("logradouro");
        builder.Property(x => x.Numero)
            .HasColumnType("varchar(10)")
            .HasColumnName("numero");
        builder.Property(x => x.Complemento)
            .HasColumnType("varchar(500)")
            .HasColumnName("complemento");
        builder.Property(x => x.Bairro)
            .HasColumnType("varchar(500)")
            .HasColumnName("bairro");
        builder.Property(x => x.Cidade)
            .HasColumnType("varchar(500)")
            .HasColumnName("cidade");
        builder.Property(x => x.Estado)
            .HasColumnType("varchar(2)")
            .HasColumnName("estado");
        builder.Property(x => x.Cep)
            .HasColumnType("varchar(10)")
            .HasColumnName("cep");
        builder.Property(x => x.NomeLeilao)
            .HasColumnType("varchar(15)")
            .HasColumnName("nome_leilao");
        builder.Property(x => x.NumeroLote)
            .HasColumnType("varchar(10)")
            .HasColumnName("numero_lote");
        builder.Property(x => x.ValorArrematacao)
            .HasColumnType("varchar(15)")
            .HasColumnName("valor_arrematacao");
        builder.Property(x => x.ValorTaxaAdministrativa)
            .HasColumnType("varchar(15)")
            .HasColumnName("valor_taxa_administrativa");
        builder.Property(x => x.ValorOutrasTaxas)
            .HasColumnType("varchar(15)")
            .HasColumnName("valor_outras_taxas");
        builder.Property(x => x.ValorComissao)
            .HasColumnType("varchar(15)")
            .HasColumnName("valor_comissao");
        builder.Property(x => x.ValorTotal)
            .HasColumnType("varchar(15)")
            .HasColumnName("valor_total");
        builder.Property(x => x.DataLeilao)
            .HasColumnType("datetime")
            .HasColumnName("data_leilao");
        builder.Property(x => x.DataCadastro)
            .HasColumnType("datetime")
            .HasColumnName("data_cadastro");

    }
}
