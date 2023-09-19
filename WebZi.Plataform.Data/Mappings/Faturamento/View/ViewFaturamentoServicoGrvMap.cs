using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebZi.Plataform.Domain.Models.Faturamento.View;

namespace WebZi.Plataform.Data.Mappings.Faturamento.View
{
    public class ViewFaturamentoServicoGrvMap : IEntityTypeConfiguration<ViewFaturamentoServicoGrvModel>
    {
        public void Configure(EntityTypeBuilder<ViewFaturamentoServicoGrvModel> builder)
        {
            builder.ToView("vw_dep_faturamento_servicos_grv")
                .HasNoKey();

            builder.Property(e => e.ClienteCnpj)
                .IsRequired()
                .HasMaxLength(14)
                .IsUnicode(false)
                .HasColumnName("cliente_cnpj");
            
            builder.Property(e => e.ClienteCodigoSap)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("cliente_codigo_sap");
            
            builder.Property(e => e.ClienteFlagAtivo)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("cliente_flag_ativo");
            
            builder.Property(e => e.ClienteFlagClienteRealizaFaturamentoArrecadacao)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("cliente_flag_cliente_realiza_faturamento_arrecadacao");
            
            builder.Property(e => e.ClienteFlagCobrarDiariasDiasCorridos)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("cliente_flag_cobrar_diarias_dias_corridos");
            
            builder.Property(e => e.ClienteFlagEmissaoNotaFiscalSap)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("cliente_flag_emissao_nota_fiscal_sap");
            
            builder.Property(e => e.ClienteFlagUsarHoraDiaria)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("cliente_flag_usar_hora_diaria");
            
            builder.Property(e => e.ClienteHoraDiaria)
                .IsRequired()
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("cliente_hora_diaria");
            
            builder.Property(e => e.ClienteIdAgenciaBancaria)
                .HasColumnName("cliente_id_agencia_bancaria");
            
            builder.Property(e => e.ClienteEmpresaId)
                .HasColumnName("cliente_id_empresa");
            
            builder.Property(e => e.ClienteTipoMeioCobrancaId)
                .HasColumnName("cliente_id_tipo_meio_cobranca");
            
            builder.Property(e => e.ClienteMaximoDiariasParaCobranca)
                .HasColumnName("cliente_maximo_diarias_para_cobranca");
            
            builder.Property(e => e.ClienteMaximoDiasVencimento)
                .HasColumnName("cliente_maximo_dias_vencimento");
            
            builder.Property(e => e.ClienteMetragemGuarda)
                .HasColumnType("numeric(5, 2)")
                .HasColumnName("cliente_metragem_guarda");
            
            builder.Property(e => e.ClienteMetragemTotal)
                .HasColumnType("numeric(5, 2)")
                .HasColumnName("cliente_metragem_total");
            
            builder.Property(e => e.ClienteNome)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("cliente_nome");
            
            builder.Property(e => e.CodigoMaterial)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("codigo_material");
            
            builder.Property(e => e.DataVigenciaFinal)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_vigencia_final");
            
            builder.Property(e => e.DataVigenciaInicial)
                .HasColumnType("smalldatetime")
                .HasColumnName("data_vigencia_inicial");
            
            builder.Property(e => e.DepositoFlagAtivo)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("deposito_flag_ativo");
            
            builder.Property(e => e.DepositoNome)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("deposito_nome");
            
            builder.Property(e => e.Descricao)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descricao");
            
            builder.Property(e => e.FaturamentoProdutoId)
                .IsRequired()
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("faturamento_produto_codigo");
            
            builder.Property(e => e.FaturamentoProdutoDescricao)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("faturamento_produto_descricao");
            
            builder.Property(e => e.FaturamentoRegraTipoCodigo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("faturamento_regra_tipo_codigo");
            
            builder.Property(e => e.FaturamentoRegraTipoDescricao)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("faturamento_regra_tipo_descricao");
            
            builder.Property(e => e.FaturamentoRegraTipoFlagAtivo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("faturamento_regra_tipo_flag_ativo");
            
            builder.Property(e => e.FaturamentoRegraTipoFlagPossuiValor)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("faturamento_regra_tipo_flag_possui_valor");
            
            builder.Property(e => e.FlagCobrarSomentePrimeiraFatura)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("flag_cobrar_somente_primeira_fatura");
            
            builder.Property(e => e.FlagCobrarTelaGrv)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("flag_cobrar_tela_grv");
            
            builder.Property(e => e.FlagImpressaoAgrupada)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("flag_impressao_agrupada");
            
            builder.Property(e => e.FlagNaoCobrarSeNaoUsouReboque)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("flag_nao_cobrar_se_nao_usou_reboque");
            
            builder.Property(e => e.FlagPermiteAlteracaoValor)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("flag_permite_alteracao_valor");
            
            builder.Property(e => e.FlagPermiteDesconto)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("flag_permite_desconto");
            
            builder.Property(e => e.FlagRealizarCobranca)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("flag_realizar_cobranca");
            
            builder.Property(e => e.FlagRebocada)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("flag_rebocada");
            
            builder.Property(e => e.FlagServicoObrigatorio)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("flag_servico_obrigatorio");
            
            builder.Property(e => e.FlagServicoObrigatorioGlobal)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("flag_servico_obrigatorio_global");
            
            builder.Property(e => e.FlagTributacao)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("flag_tributacao");
            
            builder.Property(e => e.FormaCobranca)
                .IsRequired()
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("forma_cobranca");
            
            builder.Property(e => e.GrvLimiteMinimoDatahoraGuarda)
                .HasColumnName("grv_limite_minimo_datahora_guarda");
            
            builder.Property(e => e.GrvMinimoFotosExigidas)
                .HasColumnName("grv_minimo_fotos_exigidas");
            
            builder.Property(e => e.ClienteId)
                .HasColumnName("id_cliente");
            
            builder.Property(e => e.DepositoId)
                .HasColumnName("id_deposito");
            
            builder.Property(e => e.FaturamentoRegraTipoId)
                .HasColumnName("id_faturamento_regra_tipo");
            
            builder.Property(e => e.FaturamentoServicoAssociadoId)
                .HasColumnName("id_faturamento_servico_associado");
            
            builder.Property(e => e.FaturamentoServicoGrvId)
                .HasColumnName("id_faturamento_servico_grv");
            
            builder.Property(e => e.FaturamentoServicoTipoId)
                .HasColumnName("id_faturamento_servico_tipo");
            
            builder.Property(e => e.FaturamentoServicoTipoVeiculoId)
                .HasColumnName("id_faturamento_servico_tipo_veiculo");
            
            builder.Property(e => e.GrvId)
                .HasColumnName("id_grv");
            
            builder.Property(e => e.SapCondicaoPagamentoId)
                .HasColumnName("id_sap_condicao_pagamento");
            
            builder.Property(e => e.SapTipoComposicaoId)
                .HasColumnName("id_sap_tipo_composicao");
            
            builder.Property(e => e.TipoMeioCobrancaId)
                .HasColumnName("id_tipo_meio_cobranca");
            
            builder.Property(e => e.TipoVeiculoId)
                .HasColumnName("id_tipo_veiculo");
            
            builder.Property(e => e.UsuarioDescontoId)
                .HasColumnName("id_usuario_desconto");
            
            builder.Property(e => e.NomeUsuarioDesconto)
                .HasMaxLength(152)
                .IsUnicode(false)
                .UseCollation("Latin1_General_CI_AS")
                .HasColumnName("nome_usuario_desconto");
            
            builder.Property(e => e.NumeroFormularioGrv)
                .IsRequired()
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasColumnName("numero_formulario_grv");
            
            builder.Property(e => e.ObservacaoDesconto)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("observacao_desconto");
            
            builder.Property(e => e.OrdemImpressao)
                .HasColumnName("ordem_impressao");
            
            builder.Property(e => e.PrecoPadrao)
                .HasColumnType("smallmoney")
                .HasColumnName("preco_padrao");
            
            builder.Property(e => e.PrecoValorMinimo)
                .HasColumnType("smallmoney")
                .HasColumnName("preco_valor_minimo");
            
            builder.Property(e => e.QuantidadeDesconto)
                .HasColumnName("quantidade_desconto");
            
            builder.Property(e => e.SapCodigoDescricao)
                .IsRequired()
                .HasMaxLength(63)
                .IsUnicode(false)
                .HasColumnName("sap_codigo_descricao");
            
            builder.Property(e => e.SapCondicaoPagamentoCodigo)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("sap_condicao_pagamento_codigo");
            
            builder.Property(e => e.SapCondicaoPagamentoDescricao)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sap_condicao_pagamento_descricao");
            
            builder.Property(e => e.SapDescricao)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sap_descricao");
            
            builder.Property(e => e.ServicoDescricao)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("servico_descricao");
            
            builder.Property(e => e.TempoTrabalhado)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("tempo_trabalhado");
            
            builder.Property(e => e.TipoCobranca)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("tipo_cobranca");
            
            builder.Property(e => e.TipoDesconto)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("tipo_desconto");
            
            builder.Property(e => e.TipoVeiculosFlagAtivo)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("tipo_veiculos_flag_ativo");
            
            builder.Property(e => e.TipoVeiculosFlagNaoRequerCnhNaLiberacao)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("tipo_veiculos_flag_nao_requer_cnh_na_liberacao");
            
            builder.Property(e => e.TipoVeiculosNome)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipo_veiculos_nome");
            
            builder.Property(e => e.TiposMeiosCobrancasCodigoSap)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("tipos_meios_cobrancas_codigo_sap");
            
            builder.Property(e => e.TiposMeiosCobrancasDescricao)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipos_meios_cobrancas_descricao");
            
            builder.Property(e => e.TiposMeiosCobrancasFlagBanco)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("tipos_meios_cobrancas_flag_banco");
            
            builder.Property(e => e.TiposMeiosCobrancasFlagPossuiCodigoAutorizacaoCartao)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("tipos_meios_cobrancas_flag_possui_codigo_autorizacao_cartao");
            
            builder.Property(e => e.Valor)
                .HasColumnType("smallmoney")
                .HasColumnName("valor");
            
            builder.Property(e => e.ValorDesconto)
                .HasColumnType("money")
                .HasColumnName("valor_desconto");
            
            builder.Property(e => e.VeiculoDescricao)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("veiculo_descricao");
        }
    }
}