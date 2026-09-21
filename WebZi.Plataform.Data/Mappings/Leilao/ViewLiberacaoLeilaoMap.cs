using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Leilao;

namespace WebZi.Plataform.Data.Mappings.Leilao;

public class ViewLiberacaoLeilaoMap : IEntityTypeConfiguration<ViewLiberacaoLeilaoModel>
{
    public void Configure(EntityTypeBuilder<ViewLiberacaoLeilaoModel> builder)
    {
        builder
            .ToView("vw_dep_liberacao_leilao", "dbo")
            .HasKey(x => x.IdGrv);

        builder.Property(x => x.IdGrv)
            .HasColumnName("id_grv");

        builder.Property(x => x.Nome)
            .HasColumnName("nome");

        builder.Property(x => x.EnderecoCompleto)
            .HasColumnName("endereco_completo");

        builder.Property(x => x.DataAtual)
            .HasColumnName("data_atual");

        builder.Property(x => x.Registro)
            .HasColumnName("registro");

        builder.Property(x => x.Mensagem1)
            .HasColumnName("mensagem1");

        builder.Property(x => x.Processo)
            .HasColumnName("processo");

        builder.Property(x => x.MarcaModelo)
            .HasColumnName("marca_modelo");

        builder.Property(x => x.Placa)
            .HasColumnName("placa");

        builder.Property(x => x.CodigoLote)
            .HasColumnName("codigo_lote");

        builder.Property(x => x.Renavam)
            .HasColumnName("renavam");

        builder.Property(x => x.Chassi)
            .HasColumnName("chassi");

        builder.Property(x => x.Cor)
            .HasColumnName("cor");

        builder.Property(x => x.Ano)
            .HasColumnName("ano");

        builder.Property(x => x.Mensagem2)
            .HasColumnName("mensagem2");

        builder.Property(x => x.Mensagem3)
            .HasColumnName("mensagem3");

        builder.Property(x => x.Mensagem4)
            .HasColumnName("mensagem4");

        builder.Property(x => x.Mensagem5)
            .HasColumnName("mensagem5");

        builder.Property(x => x.Mensagem6)
            .HasColumnName("mensagem6");

        builder.Property(x => x.ArrematanteNomeArrematante)
            .HasColumnName("arrematante_nome_arrematante");

        builder.Property(x => x.ArrematanteCpfCnpj)
            .HasColumnName("arrematante_cpf_cnpj");

        builder.Property(x => x.GrvEstacionamentoSetor)
            .HasColumnName("grv_estacionamento_setor");

        builder.Property(x => x.GrvEstacionamentoNumeroVaga)
            .HasColumnName("grv_estacionamento_numero_vaga");

        builder.Property(x => x.GrvNumeroChave)
            .HasColumnName("grv_numero_chave");
    }
}
