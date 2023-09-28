using WebZi.Plataform.Domain.Models.Usuario;

namespace WebZi.Plataform.Domain.Models.Faturamento.Boleto
{
    public class FaturamentoBoletoModel
    {
        public int FaturamentoBoletoId { get; set; }

        public int FaturamentoId { get; set; }

        public int BoletoId { get; set; }

        public int UsuarioCadastroId { get; set; }

        public byte SequenciaEmissao { get; set; } = 1;

        public byte? Via { get; set; }

        public byte DiasConfiguracaoDataVencimento { get; set; }

        public decimal Valor { get; set; }

        public string Linha { get; set; }

        public DateTime DataEmissao { get; set; }

        /// <summary>
        /// Status:
        /// N = Não Pago;
        /// P = Pago;
        /// C = Cancelado.
        /// </summary>
        public string Status { get; set; } = "N";

        public virtual FaturamentoModel Faturamento { get; set; }

        public virtual UsuarioModel UsuarioCadastro { get; set; }

        public virtual FaturamentoBoletoImagemModel FaturamentoBoletoImagem { get; set; }
    }
}