﻿namespace WebZi.Plataform.Domain.DTO.Faturamento.Servico
{
    public class ServicoAssociadoGrvDTO
    {
        public int GrvId { get; set; }

        public string NumeroFormularioGrv { get; set; }

        public int ClienteId { get; set; }

        public short ClienteIdAgenciaBancaria { get; set; }

        public byte? ClienteTipoMeioCobrancaId { get; set; }

        public int? ClienteEmpresaId { get; set; }

        public string ClienteNome { get; set; }

        public string ClienteCnpj { get; set; }

        public decimal? ClienteMetragemTotal { get; set; }

        public decimal? ClienteMetragemGuarda { get; set; }

        public string ClienteCodigoERP { get; set; }

        public string ClienteHoraDiaria { get; set; }

        public short ClienteMaximoDiariasParaCobranca { get; set; }

        public short ClienteMaximoDiasVencimento { get; set; }

        public string ClienteFlagUsarHoraDiaria { get; set; }

        public string ClienteFlagEmissaoNotaFiscalERP { get; set; }

        public string ClienteFlagCobrarDiariasDiasCorridos { get; set; }

        public string ClienteFlagClienteRealizaFaturamentoArrecadacao { get; set; }

        public string ClienteFlagAtivo { get; set; }

        public int DepositoId { get; set; }

        public string DepositoNome { get; set; }

        public byte GrvMinimoFotosExigidas { get; set; }

        public byte GrvLimiteMinimoDatahoraGuarda { get; set; }

        public string DepositoFlagAtivo { get; set; }

        public byte? TipoMeioCobrancaId { get; set; }

        public string TiposMeiosCobrancasCodigoERP { get; set; }

        public string TiposMeiosCobrancasDescricao { get; set; }

        public string TiposMeiosCobrancasFlagBanco { get; set; }

        public string TiposMeiosCobrancasFlagPossuiCodigoAutorizacaoCartao { get; set; }

        public int FaturamentoServicoTipoId { get; set; }

        public int TipoComposicaoERPId { get; set; }

        public string ServicoDescricao { get; set; }

        public string CodigoMaterial { get; set; }

        public string DescricaoERP { get; set; }

        public string CodigoDescricaoERP { get; set; }

        public byte? CondicaoPagamentoERPId { get; set; }

        public string CondicaoPagamentoCodigoERP { get; set; }

        public string CondicaoPagamentoDescricaoERP { get; set; }

        public string TipoCobranca { get; set; }

        public byte OrdemImpressao { get; set; }

        public string FlagServicoObrigatorioGlobal { get; set; }

        public string FlagCobrarTelaGrv { get; set; }

        public string FlagNaoCobrarSeNaoUsouReboque { get; set; }

        public string FlagRebocada { get; set; }

        public string FlagImpressaoAgrupada { get; set; }

        public string FlagTributacao { get; set; }

        public string FaturamentoProdutoId { get; set; }

        public string FaturamentoProdutoDescricao { get; set; }

        public int FaturamentoServicoAssociadoId { get; set; }

        public string Descricao { get; set; }

        public decimal PrecoPadrao { get; set; }

        public decimal PrecoValorMinimo { get; set; }

        public DateTime DataVigenciaInicial { get; set; }

        public DateTime? DataVigenciaFinal { get; set; }

        public string FormaCobranca { get; set; }

        public string FlagServicoObrigatorio { get; set; }

        public string FlagPermiteAlteracaoValor { get; set; }

        public string FlagPermiteDesconto { get; set; }

        public string FlagCobrarSomentePrimeiraFatura { get; set; }

        public int FaturamentoServicoTipoVeiculoId { get; set; }

        public byte TipoVeiculoId { get; set; }

        public string TipoVeiculosNome { get; set; }

        public string VeiculoDescricao { get; set; }

        public string TipoVeiculosFlagNaoRequerCnhNaLiberacao { get; set; }

        public string TipoVeiculosFlagAtivo { get; set; }

        public short? FaturamentoRegraTipoId { get; set; }

        public string FaturamentoRegraTipoCodigo { get; set; }

        public string FaturamentoRegraTipoDescricao { get; set; }

        public string FaturamentoRegraTipoFlagPossuiValor { get; set; }

        public string FaturamentoRegraTipoFlagAtivo { get; set; }

        public int? FaturamentoServicoGrvId { get; set; }

        public decimal? Valor { get; set; }

        public string TempoTrabalhado { get; set; }

        public string FlagRealizarCobranca { get; set; }

        public int? UsuarioDescontoId { get; set; }

        public string TipoDesconto { get; set; }

        public int? QuantidadeDesconto { get; set; }

        public decimal? ValorDesconto { get; set; }

        public string ObservacaoDesconto { get; set; }

        public string NomeUsuarioDesconto { get; set; }
    }
}