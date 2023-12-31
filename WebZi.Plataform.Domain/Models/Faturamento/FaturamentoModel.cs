using WebZi.Plataform.Domain.Models.Atendimento;
using WebZi.Plataform.Domain.Models.Banco.PIX;
using WebZi.Plataform.Domain.Models.Banco.PIX.Dinamico;
using WebZi.Plataform.Domain.Models.Usuario;
using WebZi.Plataform.Domain.Models.WebServices.Boleto;

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

        public virtual UsuarioModel UsuarioCadastro { get; set; }

        public virtual UsuarioModel UsuarioAlteracao { get; set; }

        public virtual ICollection<BoletoModel> ListagemBoleto { get; set; } = new List<BoletoModel>();

        public virtual ICollection<FaturamentoCodigoAutorizacaoCartaoModel> ListagemFaturamentoCodigoAutorizacaoCartao { get; set; } = new List<FaturamentoCodigoAutorizacaoCartaoModel>();

        public virtual ICollection<FaturamentoComposicaoModel> ListagemFaturamentoComposicao { get; set; } = new List<FaturamentoComposicaoModel>();

        public virtual ICollection<FaturamentoCartaoModel> ListagemFaturamentoCartao { get; set; } = new List<FaturamentoCartaoModel>();

        //public virtual ICollection<LiberacaoEspecial> LiberacaoEspecials { get; set; }

        //public virtual ICollection<PixDinamicoSenhaConfirmacaoTranferencium> PixDinamicoSenhaConfirmacaoTranferencia { get; set; }

        public virtual ICollection<PixEstaticoModel> ListagemPixEstatico { get; set; }

        public virtual ICollection<PixDinamicoModel> ListagemPixDinamico { get; set; }
    }
}