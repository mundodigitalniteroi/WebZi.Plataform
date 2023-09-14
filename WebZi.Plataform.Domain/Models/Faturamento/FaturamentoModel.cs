using WebZi.Plataform.Domain.Models.Atendimento;

namespace WebZi.Plataform.Domain.Models.Faturamento
{
    public class FaturamentoModel
    {
        public int FaturamentoId { get; set; }

        public int AtendimentoId { get; set; }

        public byte TipoMeioCobrancaId { get; set; } = 1; // BOLETO

        public int UsuarioCadastroId { get; set; }

        public int? UsuarioAlteracaoId { get; set; }

        public string NumeroIdentificacao { get; set; }

        public decimal ValorFaturado { get; set; }

        public decimal? ValorPagamento { get; set; }

        public string HoraDiaria { get; set; }

        public short MaximoDiariasParaCobranca { get; set; }

        public short MaximoDiasVencimento { get; set; }

        public byte Sequencia { get; set; } = 1;

        public string NumeroNotaFiscal { get; set; }

        public DateTime? DataCalculo { get; set; }

        public DateTime? DataRetroativa { get; set; }

        public DateTime DataVencimento { get; set; }

        public DateTime? DataPrazoRetiradaVeiculo { get; set; }

        public DateTime? DataPagamento { get; set; }

        public DateTime? DataEmissaoDocumento { get; set; }

        public DateTime? DataEmissaoNotaFiscal { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public string Status { get; set; } = "N";

        public string FlagUsarHoraDiaria { get; set; } = "N";

        public string FlagLimitacaoJudicial { get; set; } = "N";

        public string FlagClienteRealizaFaturamentoArrecadacao { get; set; } = "N";

        public string FlagCobrarDiariasDiasCorridos { get; set; } = "N";

        public string FlagPermissaoDataRetroativaFaturamento { get; set; } = "N";

        public virtual AtendimentoModel Atendimento { get; set; }

        public virtual TipoMeioCobrancaModel TipoMeioCobranca { get; set; }

        //public virtual TbDepUsuario IdUsuarioAlteracaoNavigation { get; set; }

        //public virtual TbDepUsuario IdUsuarioCadastroNavigation { get; set; }

        //public virtual ICollection<TbDepFaturamentoBoleto> TbDepFaturamentoBoletos { get; set; }

        //public virtual ICollection<TbDepFaturamentoCodigoAutorizacaoCartao> TbDepFaturamentoCodigoAutorizacaoCartaos { get; set; }

        public virtual ICollection<FaturamentoComposicaoModel> FaturamentoComposicoes { get; set; }

        public virtual ICollection<FaturamentoCartaoModel> FaturamentoCartoes { get; set; }

        //public virtual ICollection<TbDepLiberacaoEspecial> TbDepLiberacaoEspecials { get; set; }

        //public virtual ICollection<TbDepPixDinamicoSenhaConfirmacaoTranferencium> TbDepPixDinamicoSenhaConfirmacaoTranferencia { get; set; }

        //public virtual ICollection<TbDepPixDinamico> TbDepPixDinamicos { get; set; }

        //public virtual ICollection<TbDepPix> TbDepPixes { get; set; }
    }
}