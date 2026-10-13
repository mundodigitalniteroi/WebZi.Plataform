using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Leilao;

namespace WebZi.Plataform.Data.Mappings.Leilao;

public class ViewLeilaoArrematanteMap : IEntityTypeConfiguration<ViewLeilaoArremanteteModel>
{
    public void Configure(EntityTypeBuilder<ViewLeilaoArremanteteModel> builder)
    {
        builder
            .ToView("vw_dep_leilao_arrematante", "dbo")
            .HasKey(x => x.IdGrv);

        builder.Property(x => x.IdGrv)
            .HasColumnName("id_grv");

        builder.Property(x => x.ArrematanteNomeArrematante)
            .HasColumnName("arrematante_nome_arrematante");

        builder.Property(x => x.ArrematanteCpfCnpj)
            .HasColumnName("arrematante_cpf_cnpj");

        builder.Property(x => x.ArrematanteTelefoneFixo)
            .HasColumnName("arrematante_telefone_fixo");

        builder.Property(x => x.ArrematanteTelefoneCelular)
            .HasColumnName("arrematante_telefone_celular");

        builder.Property(x => x.ArrematanteEmail)
            .HasColumnName("arrematante_email");

        builder.Property(x => x.ArrematanteLogradouro)
            .HasColumnName("arrematante_logradouro");

        builder.Property(x => x.ArrematanteNumero)
            .HasColumnName("arrematante_numero");

        builder.Property(x => x.ArrematanteComplemento)
            .HasColumnName("arrematante_complemento");

        builder.Property(x => x.ArrematanteBairro)
            .HasColumnName("arrematante_bairro");

        builder.Property(x => x.ArrematanteCidade)
            .HasColumnName("arrematante_cidade");

        builder.Property(x => x.ArrematanteEstado)
            .HasColumnName("arrematante_estado");

        builder.Property(x => x.ArrematanteCep)
            .HasColumnName("arrematante_cep");
    }
}

