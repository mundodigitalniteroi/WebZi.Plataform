using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Views.Veiculos;

namespace WebZi.Plataform.Data.Mappings.Veiculo.View;

public class ViewEstoqueVeiculosMap : IEntityTypeConfiguration<ViewEstoqueVeiculosModel>
{
    public void Configure(EntityTypeBuilder<ViewEstoqueVeiculosModel> builder)
    {
        builder.ToView("vw_estoque_veiculos", "dbo");

        builder.HasNoKey();

        builder.Property(x => x.NumeroFormularioGrv).HasColumnName("numero_formulario_grv");
        builder.Property(x => x.Renavam).HasColumnName("renavam");
        builder.Property(x => x.Placa).HasColumnName("placa");
        builder.Property(x => x.PlacaOstentada).HasColumnName("placa_ostentada");
        builder.Property(x => x.Chassi).HasColumnName("chassi");
        builder.Property(x => x.MarcaModelo).HasColumnName("marca_modelo");
        builder.Property(x => x.TipoVeiculo).HasColumnName("tipo_veiculo");
        builder.Property(x => x.Cor).HasColumnName("cor");
        builder.Property(x => x.CorOstentada).HasColumnName("cor_ostentada");

        builder.Property(x => x.AutoridadeOrgao).HasColumnName("autoridade_orgao");
        builder.Property(x => x.AutoridadeSigla).HasColumnName("autoridade_sigla");
        builder.Property(x => x.AutoridadeDivisao).HasColumnName("autoridade_divisao");

        builder.Property(x => x.FlagComboio).HasColumnName("flag_comboio");
        builder.Property(x => x.ReboquePlaca).HasColumnName("reboque_placa");

        builder.Property(x => x.DataHoraRemocao).HasColumnName("data_hora_remocao");
        builder.Property(x => x.DataHoraGuarda).HasColumnName("data_hora_guarda");

        builder.Property(x => x.Status).HasColumnName("status");

        builder.Property(x => x.ClienteNome).HasColumnName("cliente_nome");
        builder.Property(x => x.DepositoNome).HasColumnName("deposito_nome");

        builder.Property(x => x.IdGrv).HasColumnName("id_grv");
        builder.Property(x => x.IdTarifaTipoVeiculo).HasColumnName("id_tarifa_tipo_veiculo");
        builder.Property(x => x.IdCliente).HasColumnName("id_cliente");
        builder.Property(x => x.IdDeposito).HasColumnName("id_deposito");
        builder.Property(x => x.IdReboquista).HasColumnName("id_reboquista");
        builder.Property(x => x.IdReboque).HasColumnName("id_reboque");
        builder.Property(x => x.IdAutoridadeResponsavel).HasColumnName("id_autoridade_responsavel");

        builder.Property(x => x.NomeAutoridadeResponsavel).HasColumnName("nome_autoridade_responsavel");

        builder.Property(x => x.IdCor).HasColumnName("id_cor");
        builder.Property(x => x.IdDetranMarcaModelo).HasColumnName("id_detran_marca_modelo");

        builder.Property(x => x.ValorFaturado).HasColumnName("valor_faturado");
        builder.Property(x => x.Expr1).HasColumnName("expr1");

        builder.Property(x => x.NumeroNotaFiscal).HasColumnName("numero_nota_fiscal");
        builder.Property(x => x.DataPagamento).HasColumnName("data_pagamento");

        builder.Property(x => x.TipoComposicao).HasColumnName("tipo_composicao");
        builder.Property(x => x.Diarias).HasColumnName("diarias");

        builder.Property(x => x.IdOrgaoEmissor).HasColumnName("id_orgao_emissor");

        builder.Property(x => x.DataCadastro).HasColumnName("data_cadastro");

        builder.Property(x => x.Logradouro).HasColumnName("logradouro");
        builder.Property(x => x.Municipio).HasColumnName("municipio");
        builder.Property(x => x.Uf).HasColumnName("uf");

        builder.Property(x => x.IdUsuarioCadastro).HasColumnName("id_usuario_cadastro");

        builder.Property(x => x.MatriculaAutoridadeResponsavel).HasColumnName("matricula_autoridade_responsavel");

        builder.Property(x => x.EstacionamentoSetor).HasColumnName("estacionamento_setor");
        builder.Property(x => x.EstacionamentoNumeroVaga).HasColumnName("estacionamento_numero_vaga");

        builder.Property(x => x.Frota).HasColumnName("frota");
        builder.Property(x => x.NomeReboquista).HasColumnName("nome_reboquista");

        builder.Property(x => x.FlagChaveDeposito).HasColumnName("flag_chave_deposito");
        builder.Property(x => x.NumeroChave).HasColumnName("numero_chave");

        builder.Property(x => x.Classificacao).HasColumnName("classificacao");

        builder.Property(x => x.Quilometragem).HasColumnName("quilometragem");

        builder.Property(x => x.EmpresaReboque).HasColumnName("empresa_reboque");

        builder.Property(x => x.TarifaReboqueTerceirizado).HasColumnName("tarifa_reboque_terceirizado");

        builder.Ignore(x => x.Grv);
        builder.Ignore(x => x.Cliente);
        builder.Ignore(x => x.StatusOperacao);
        builder.Ignore(x => x.Reboque);
        builder.Ignore(x => x.Reboquista);
        builder.Ignore(x => x.AutoridadeResponsavel);
    }
}