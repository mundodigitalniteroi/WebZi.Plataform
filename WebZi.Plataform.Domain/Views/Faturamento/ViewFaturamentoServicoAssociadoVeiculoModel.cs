namespace WebZi.Plataform.Domain.Views.Faturamento
{
    public class ViewFaturamentoServicoAssociadoVeiculoModel
    {
        public int ClienteId { get; set; }

        public short ClienteAgenciaBancariaId { get; set; }

        public byte? ClienteTipoMeioCobrancaId { get; set; }

        public int? ClienteEmpresaId { get; set; }

        public string ClienteNome { get; set; }

        public string ClienteCnpj { get; set; }

        public decimal? ClienteMetragemTotal { get; set; }

        public decimal? ClienteMetragemGuarda { get; set; }

        public string ClienteCodigoSap { get; set; }

        public string ClienteHoraDiaria { get; set; }

        public short ClienteMaximoDiariasParaCobranca { get; set; }

        public short ClienteMaximoDiasVencimento { get; set; }

        public string ClienteFlagUsarHoraDiaria { get; set; }

        public string ClienteFlagEmissaoNotaFiscalSap { get; set; }

        public string ClienteFlagCobrarDiariasDiasCorridos { get; set; }

        public string ClienteFlagClienteRealizaFaturamentoArrecadacao { get; set; }

        public string ClienteFlagAtivo { get; set; }

        public int DepositoId { get; set; }

        public string DepositoNome { get; set; }

        public byte GrvMinimoFotosExigidas { get; set; }

        public byte GrvLimiteMinimoDatahoraGuarda { get; set; }

        public string DepositoFlagAtivo { get; set; }

        public byte? TipoMeioCobrancaId { get; set; }

        public string TiposMeiosCobrancasCodigoSap { get; set; }

        public string TiposMeiosCobrancasDescricao { get; set; }

        public string TiposMeiosCobrancasFlagBanco { get; set; }

        public string TiposMeiosCobrancasFlagPossuiCodigoAutorizacaoCartao { get; set; }

        public int FaturamentoServicoTipoId { get; set; }

        public int IdSapTipoComposicao { get; set; }

        public string ServicoDescricao { get; set; }

        public string CodigoMaterial { get; set; }

        public string SapDescricao { get; set; }

        public string SapCodigoDescricao { get; set; }

        public byte? SapCondicaoPagamentoId { get; set; }

        public string SapCondicaoPagamentoCodigo { get; set; }

        public string SapCondicaoPagamentoDescricao { get; set; }

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

        public short? FaturamentoRegraId { get; set; }

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

        public short? FaturamentoRegraTipoId { get; set; }

        public string FaturamentoRegraTipoCodigo { get; set; }

        public string FaturamentoRegraTipoDescricao { get; set; }

        public string FaturamentoRegraTipoFlagPossuiValor { get; set; }

        public string FaturamentoRegraTipoFlagAtivo { get; set; }

        public string NomeUsuarioCadastro { get; set; }

        public DateTime DataCadastro { get; set; }

        public string NomeUsuarioAlteracao { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public int FaturamentoServicoTipoVeiculoId { get; set; }

        public byte TipoVeiculoId { get; set; }

        public string TipoVeiculosNome { get; set; }

        public string VeiculoDescricao { get; set; }

        public string TipoVeiculosFlagNaoRequerCnhNaLiberacao { get; set; }

        public string TipoVeiculosFlagAtivo { get; set; }
    }
}